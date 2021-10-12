using System;
using System.Threading.Tasks;
using PansyDev.Shetter.Domain.Aggregates.PostLikeAggregate;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace PansyDev.Shetter.Infrastructure.Data.Repositories
{
    internal class PostLikeRepository : EfCoreRepository<ShetterDbContext, PostLike>, IPostLikeRepository
    {
        public PostLikeRepository(IDbContextProvider<ShetterDbContext> dbContextProvider) : base(dbContextProvider) { }

        public async Task<PostLike?> FindByPostAndAuthorIds(Guid postId, Guid authorId)
        {
            return await FindAsync(x => x.PostId == postId && x.AuthorId == authorId);
        }
    }
}
