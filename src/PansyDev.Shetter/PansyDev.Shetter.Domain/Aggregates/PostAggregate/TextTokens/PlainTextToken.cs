using System;
using System.Collections.Generic;

namespace PansyDev.Shetter.Domain.Aggregates.PostAggregate.TextTokens
{
    public class PlainTextToken : TextToken
    {
        protected PlainTextToken() { }

        internal PlainTextToken(string text, IReadOnlyList<TextTokenModifier>? modifiers = null) : base(text)
        {
            Modifiers = modifiers ?? Array.Empty<TextTokenModifier>();
        }

        public IReadOnlyList<TextTokenModifier> Modifiers { get; private set; } = Array.Empty<TextTokenModifier>();
    }
}
