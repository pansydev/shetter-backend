using System;
using System.Collections.Generic;
using PansyDev.Shetter.Domain.Aggregates.PostAggregate.TextTokens;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;

namespace PansyDev.Shetter.Domain.Aggregates.PostAggregate
{
    public class PostVersion : Entity<Guid>, IHasCreationTime
    {
        public Guid? PostId { get; private set; }

        public string? Text { get; private set; } = null!;

        public IReadOnlyList<TextToken> TextTokens { get; private set; } = null!;
        public IReadOnlyList<PostImage> Images { get; private set; } = null!;

        public DateTime CreationTime { get; private set; }

        internal Post Post { get; set; } = null!;

        protected PostVersion() { }

        public PostVersion(string? text, IReadOnlyList<TextToken> textTokens, IReadOnlyList<PostImage> images,
            DateTime creationTime)
        {
            Text = text;
            TextTokens = textTokens;
            Images = images;
            CreationTime = creationTime;
        }
    }
}