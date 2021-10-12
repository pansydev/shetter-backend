using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace PansyDev.Shetter.Domain.Aggregates.PostLikeAggregate
{
    public interface IPostLikeRepository : IRepository<PostLike>
    {
        public Task<PostLike?> FindByPostAndAuthorIds(Guid postId, Guid authorId);
    }
}
