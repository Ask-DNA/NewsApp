using HtmlAgilityPack;
using NewsApp.Server.Infrastructure.Data.DTO;
using NewsApp.Server.Infrastructure.Data.Interfaces;
using System.Net;

namespace NewsApp.Server.Infrastructure.WebParser
{
    public class HtmlParser : IWebParser
    {
        private const string Url = "https://datamanagement365.com/blog/news/";
        private const string NewsXPath = "//body/section/div[2]/*[@class=\"news-page__item\"]";
        private const string NewsHeaderRelativeXPath = "div[2]/h2/a";
        private const string NewsDateRelativeXPath = "div[2]/div[1]/span";
        private const string NewsExcerptRelativeXPath = "div[2]/div[2]/p";

        public async Task<NewsItemDTO[]> GetAllNewsAsync()
        {
            HtmlDocument doc = await Load();
            return Parse(doc);
        }

        private static async Task<HtmlDocument> Load()
        {
            var web = new HtmlWeb();
            HtmlDocument doc;
            try
            {
                doc = await web.LoadFromWebAsync(Url);
            }
            catch (WebException ex) { throw new InvalidOperationException($"Error while loading from {Url}", ex); }
            return doc;
        }

        private static NewsItemDTO[] Parse(HtmlDocument doc)
        {
            InvalidOperationException unexpectedDocumentStructureException
                = new($"Error while parsing {Url}: Unexpected document structure");

            HtmlNodeCollection? nodes = doc.DocumentNode.SelectNodes(NewsXPath)
                ?? throw unexpectedDocumentStructureException;

            string title, dateStr, excerpt;
            NewsItemDTO[] result = new NewsItemDTO[nodes.Count];
            for (int i = 0; i < nodes.Count; i++)
            {
                title = nodes[i].SelectSingleNode(NewsHeaderRelativeXPath)?.InnerText ?? throw unexpectedDocumentStructureException;
                dateStr = nodes[i].SelectSingleNode(NewsDateRelativeXPath)?.InnerText.Trim() ?? throw unexpectedDocumentStructureException;
                excerpt = nodes[i].SelectSingleNode(NewsExcerptRelativeXPath)?.InnerText ?? throw unexpectedDocumentStructureException;
                if (!DateOnly.TryParse(dateStr, out DateOnly date))
                    throw unexpectedDocumentStructureException;
                result[i] = new(title, date, excerpt);
            }

            return result;
        }
    }
}
