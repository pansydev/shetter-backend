using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using PansyDev.Shetter.Domain.Aggregates.PostAggregate.TextTokens;
using PansyDev.Shetter.Domain.Services.Abstractions;
using Volo.Abp.DependencyInjection;

namespace PansyDev.Shetter.Domain.Services.TokenParsing
{
    public class UrlTokenParser : ITokenParser, ISingletonDependency
    {
        private const string UrlPattern =
            @"((?:[A-Za-z]{3,9}):\/\/)?(?:[-;:&=\+\$,\w]+@{1})?((?:[-A-Za-z0-9]+\.)+[A-Za-z]{2,3})(:\d+)?(?:(?:\/[-\+~%/\.\w]+)?\/?(?:[&?][-\+=&;%@\.\w]+)?(?:#[\w]+)?)?";

        private readonly IRootZoneRepository _rootZoneRepository;

        public UrlTokenParser(IRootZoneRepository rootZoneRepository)
        {
            _rootZoneRepository = rootZoneRepository;
        }

        public Regex Pattern => new(UrlPattern);

        public Task<TextToken> Handle(Match match)
        {
            var domain = match.Groups[2].Value;
            var rootZone = domain.Split(".").Last();

            if (!_rootZoneRepository.IsValidRootZone(rootZone))
                return Task.FromResult<TextToken>(new PlainTextToken(match.Value));

            var result = match.Groups[1].Success ? match.Value : "http://" + match.Value;
            return Task.FromResult<TextToken>(new LinkTextToken(match.Value, new Uri(result)));
        }
    }
}