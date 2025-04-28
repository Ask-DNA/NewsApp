namespace NewsApp.Server.Domain.Models
{
    internal abstract class TextSearchStrategyBase(bool caseSensitive) : ITextSearchStrategy
    {
        protected bool CaseSensitive { get; private set; } = caseSensitive;

        public abstract bool Fits(string text, string pattern);
    }
}
