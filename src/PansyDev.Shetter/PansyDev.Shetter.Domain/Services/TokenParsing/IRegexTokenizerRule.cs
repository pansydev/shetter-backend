using System.Text.RegularExpressions;
using System.Threading.Tasks;
using PansyDev.Shetter.Domain.Aggregates.PostAggregate.TextTokens;

namespace PansyDev.Shetter.Domain.Services.TokenParsing
{
    internal interface ITokenParser
    {
        Regex Pattern { get; }
        Task<TextToken> Handle(Match match);
    }
}