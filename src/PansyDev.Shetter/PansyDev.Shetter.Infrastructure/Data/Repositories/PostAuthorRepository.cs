using System;
using System.Threading.Tasks;
using PansyDev.Shetter.Domain.Aggregates.PostAuthorAggregate;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace PansyDev.Shetter.Infrastructure.Data.Repositories
{
    internal class PostAuthorRepository : EfCoreRepository<ShetterDbContext, PostAuthor, Guid>, IPostAuthorRepository
    {
        public PostAuthorRepository(IDbContextProvider<ShetterDbContext> dbContextProvider) :
            base(dbContextProvider) { }

        public async Task<PostAuthor?> FindByAccountId(Guid accountId)
        {
            return await FindAsync(x => x.AccountId == accountId);
        }
    }
}