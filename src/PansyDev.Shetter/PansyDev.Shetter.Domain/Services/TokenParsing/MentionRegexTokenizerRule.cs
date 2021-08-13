using System.Text.RegularExpressions;
using System.Threading.Tasks;
using PansyDev.Shetter.Domain.Aggregates.PostAggregate.TextTokens;
using PansyDev.Shetter.Domain.Aggregates.PostAuthorAggregate;
using PansyDev.Shetter.Domain.Specifications;
using Volo.Abp.DependencyInjection;

namespace PansyDev.Shetter.Domain.Services.TokenParsing
{
    internal class MentionTokenParser : ITokenParser, ISingletonDependency
    {
        private readonly IPostAuthorRepository _postAuthorRepository;

        public MentionTokenParser(IPostAuthorRepository postAuthorRepository)
        {
            _postAuthorRepository = postAuthorRepository;
        }

        public Regex Pattern => new(@"@(\w+)", RegexOptions.Multiline);

        public async Task<TextToken> Handle(Match match)
        {
            var username = match.Groups[1].Value;

            var author = await _postAuthorRepository.FindAsync(new PostAuthorUsernameSpecification(username));

            return author is null
                ? new PlainTextToken(match.Value)
                : new MentionTextToken(match.Value, author.Id);
        }
    }
}