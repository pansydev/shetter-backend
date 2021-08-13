namespace PansyDev.Shetter.Domain.Aggregates.PostAggregate.TextTokens
{
    public abstract class TextToken
    {
        protected TextToken() { }

        protected TextToken(string text)
        {
            Text = text;
        }

        public string Text { get; private set; } = null!;
    }
}