using System;

namespace PansyDev.Shetter.Domain.Aggregates.PostAggregate.TextTokens
{
    public class MentionTextToken : TextToken
    {
        protected MentionTextToken() { }

        public Guid AuthorId { get; private set; }

        internal MentionTextToken(string text, Guid authorId) : base(text)
        {
            AuthorId = authorId;
        }
    }
}