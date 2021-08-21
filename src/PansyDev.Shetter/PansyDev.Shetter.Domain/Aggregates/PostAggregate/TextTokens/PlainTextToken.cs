using System;
using System.Collections.Generic;

namespace PansyDev.Shetter.Domain.Aggregates.PostAggregate.TextTokens
{
    public class PlainTextToken : TextToken
    {
        protected PlainTextToken() { }

        internal PlainTextToken(string text, IReadOnlyList<TextTokenModifier>? modifiers = null) : base(text)
        {
            Modifiers = modifiers ?? ArraySegment<TextTokenModifier>.Empty;
        }

        public IReadOnlyList<TextTokenModifier> Modifiers { get; private set; } = ArraySegment<TextTokenModifier>.Empty;
    }
}
