using System;
using System.Collections.Generic;
using PansyDev.Shetter.Domain.Aggregates.PostAggregate;
using PansyDev.Shetter.Domain.Aggregates.PostAggregate.TextTokens;

namespace PansyDev.Shetter.Application.Models.ReadModels
{
    public class PostVersionReadModel
    {
        public string OriginalText { get; set; } = null!;
        public DateTime CreationTime { get; set; }
        public IReadOnlyList<TextToken> TextTokens { get; set; } = null!;
        public IReadOnlyList<PostImage> Images { get; set; } = null!;
    }
}
