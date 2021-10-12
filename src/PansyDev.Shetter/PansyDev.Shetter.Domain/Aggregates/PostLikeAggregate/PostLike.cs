using System;
using PansyDev.Shetter.Domain.Aggregates.PostAuthorAggregate;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;

namespace PansyDev.Shetter.Domain.Aggregates.PostLikeAggregate
{
    public class PostLike : BasicAggregateRoot, IHasCreationTime
    {
        public PostLike(Guid postId, Guid authorId)
        {
            PostId = postId;
            AuthorId = authorId;
        }

        public Guid PostId { get; private set; }
        public Guid AuthorId { get; private set; }
        public DateTime CreationTime { get; private set; }

        internal PostAuthor Author { get; set; } = null!;

        public override object[] GetKeys()
        {
            return new object[] { PostId, AuthorId };
        }
    }
}
