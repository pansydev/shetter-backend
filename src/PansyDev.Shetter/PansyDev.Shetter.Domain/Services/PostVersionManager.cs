using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PansyDev.Shetter.Domain.Aggregates.PostAggregate;
using PansyDev.Shetter.Domain.Aggregates.PostAggregate.TextTokens;
using PansyDev.Shetter.Domain.Exceptions;
using Volo.Abp.Domain.Services;

namespace PansyDev.Shetter.Domain.Services
{
    public class PostVersionManager : DomainService
    {
        private readonly PostParser _postParser;

        public PostVersionManager(PostParser postParser)
        {
            _postParser = postParser;
        }

        public virtual async Task<PostVersion> CreateVersion(string? text, IReadOnlyList<PostImage>? images)
        {
            text = text?.Trim();

            if (string.IsNullOrEmpty(text) && images is not { Count: > 0 })
            {
                throw new EmptyContentException();
            }

            var tokens = text is not null ? await _postParser.ParseTokens(text) : ArraySegment<TextToken>.Empty;
            return new PostVersion(text, tokens, images ?? ArraySegment<PostImage>.Empty, Clock.Now);
        }
    }
}