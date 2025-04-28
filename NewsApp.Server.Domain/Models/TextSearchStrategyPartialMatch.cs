namespace NewsApp.Server.Domain.Models
{
    internal class TextSearchStrategyPartialMatch(bool caseSensitive) : TextSearchStrategyBase(caseSensitive)
    {
        public override bool Fits(string text, string pattern)
        {
            if (CaseSensitive)
                return text.Contains(pattern);
            else
                return text.Contains(pattern, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}
