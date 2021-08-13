using System;
using PansyDev.Shetter.Domain.Aggregates.PostAggregate;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace PansyDev.Shetter.Infrastructure.Data.Repositories
{
    internal class PostRepository : EfCoreRepository<ShetterDbContext, Post, Guid>, IPostRepository
    {
        public PostRepository(IDbContextProvider<ShetterDbContext> dbContextProvider) : base(dbContextProvider) { }
    }
}