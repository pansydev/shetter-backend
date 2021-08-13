using PansyDev.Shetter.Domain.Aggregates.PostAggregate.TextTokens;

namespace PansyDev.Shetter.Domain.Services.TokenParsing
{
    internal record ModifierPattern(string Pattern, TextTokenModifier Modifier);
}