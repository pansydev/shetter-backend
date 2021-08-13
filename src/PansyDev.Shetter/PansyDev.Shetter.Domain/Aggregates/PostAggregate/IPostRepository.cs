using System;
using Volo.Abp.Domain.Repositories;

namespace PansyDev.Shetter.Domain.Aggregates.PostAggregate
{
    public interface IPostRepository : IRepository<Post, Guid> { }
}