using System;

namespace PansyDev.Shetter.Domain.Aggregates.PostAggregate.TextTokens
{
    public class LinkTextToken : TextToken
    {
        protected LinkTextToken() { }

        public Uri Url { get; private set; } = null!;

        internal LinkTextToken(string text, Uri url) : base(text)
        {
            Url = url;
        }
    }
}