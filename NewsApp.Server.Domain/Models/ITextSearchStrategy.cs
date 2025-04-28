namespace NewsApp.Server.Domain.Models
{
    internal interface ITextSearchStrategy
    {
        bool Fits(string text, string pattern);
    }
}
