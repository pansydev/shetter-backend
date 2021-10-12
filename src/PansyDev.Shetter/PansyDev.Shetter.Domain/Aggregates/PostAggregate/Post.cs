using System;
using System.Collections.Generic;
using System.Linq;
using PansyDev.Shetter.Domain.Aggregates.PostAuthorAggregate;
using PansyDev.Shetter.Domain.Aggregates.PostLikeAggregate;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;

namespace PansyDev.Shetter.Domain.Aggregates.PostAggregate
{
    public class Post : BasicAggregateRoot<Guid>, IHasCreationTime, IHasModificationTime
    {
        public Guid AuthorId { get; private set; }

        public PostVersion CurrentVersion { get; private set; } = null!;
        public Guid CurrentVersionId { get; private set; }

        public DateTime CreationTime { get; private set; }
        public DateTime? LastModificationTime { get; set; }

        public IReadOnlyList<PostVersion> PreviousVersions { get; private set; } = null!;

        protected Post() { }

        internal PostAuthor Author { get; set; } = null!;
        internal IReadOnlyList<PostLike> Likes { get; set; } = null!;

        internal Post(Guid id, Guid authorId, PostVersion version) : base(id)
        {
            AuthorId = authorId;
            CurrentVersion = version;
            PreviousVersions = Array.Empty<PostVersion>();
        }

        internal void Edit(PostVersion version)
        {
            PreviousVersions = PreviousVersions.Prepend(CurrentVersion).ToArray();
            CurrentVersion = version;
        }
    }
}
