using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PansyDev.Shetter.Domain.Aggregates.PostAggregate.TextTokens;
using PansyDev.Shetter.Domain.Extensions;
using PansyDev.Shetter.Domain.Services.TokenParsing;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Services;

namespace PansyDev.Shetter.Domain.Services
{
    public class PostParser : DomainService, ISingletonDependency
    {
        private readonly ITokenParser[] _tokenParsers;
        private readonly ModifierPattern[] _modifierPatterns;

        public PostParser(IServiceProvider serviceProvider)
        {
            _tokenParsers = new ITokenParser[]
            {
                serviceProvider.GetRequiredService<MentionTokenParser>(),
                serviceProvider.GetRequiredService<UrlTokenParser>()
            };

            _modifierPatterns = new ModifierPattern[]
            {
                new("**", TextTokenModifier.Bold),
                new("__", TextTokenModifier.Underline),
                new("_", TextTokenModifier.Italic),
                new("`", TextTokenModifier.Code),
                new("~", TextTokenModifier.Strikethrough),
            };
        }

        private IReadOnlyList<PlainTextToken> MatchTextModifiersRules(string text)
        {
            var tokens = new List<PlainTextToken>();
            var modifiers = new List<TextTokenModifier>();
            var chars = text.ToArray();

            var buf = "";
            for (var i = 0; i < chars.Length; i++)
            {
                var c = chars[i];
                buf += c;

                if ((i == 0 || c != chars[i - 1]) && (i < chars.Length - 1 && c == chars[i + 1]))
                    continue;

                var rule = _modifierPatterns.FirstOrDefault(x => buf.EndsWith(x.Pattern));
                if (rule is null) continue;

                var plainText = buf.Replace(rule.Pattern, "");

                if (plainText != string.Empty)
                {
                    var textToken = new PlainTextToken(plainText, modifiers.ToArray());
                    tokens.Add(textToken);
                }

                if (modifiers.Contains(rule.Modifier))
                    modifiers.Remove(rule.Modifier);
                else
                    modifiers.Add(rule.Modifier);

                buf = "";
            }

            if (buf != string.Empty)
            {
                var textToken = new PlainTextToken(buf);
                tokens.Add(textToken);
            }

            return tokens;
        }

        public virtual async Task<IReadOnlyList<TextToken>> ParseTokens(string text)
        {
            var result = new List<TextToken>();

            var matches = _tokenParsers.SelectMany(parser =>
            {
                var regexMatches = parser.Pattern.Matches(text);
                return regexMatches.Select(x => new { RegexMatch = x, Parser = parser });
            }).ToList();

            matches.Sort((a, b) => a.RegexMatch.Index - b.RegexMatch.Index);

            if (!matches.Any()) return MatchTextModifiersRules(text);

            var lastIndex = 0;
            foreach (var match in matches)
            {
                var plainText = text.Slice(lastIndex, match.RegexMatch.Index);

                if (plainText != string.Empty)
                {
                    result.AddRange(MatchTextModifiersRules(plainText));
                }

                var token = await match.Parser.Handle(match.RegexMatch);
                result.Add(token);

                lastIndex = match.RegexMatch.Index + match.RegexMatch.Length;
            }

            if (text.Length > lastIndex)
            {
                var plainText = text.Slice(lastIndex, text.Length);

                if (plainText != string.Empty)
                {
                    result.AddRange(MatchTextModifiersRules(plainText));
                }
            }

            return result;
        }
    }
}