using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace PansyDev.Shetter.Domain.Aggregates.PostAuthorAggregate
{
    public interface IPostAuthorRepository : IRepository<PostAuthor, Guid>
    {
        Task<PostAuthor?> FindByAccountId(Guid accountId);
    }
}