using System;
using Volo.Abp.Domain.Entities;

namespace PansyDev.Shetter.Domain.Aggregates.PostAuthorAggregate
{
    public class PostAuthor : BasicAggregateRoot<Guid>
    {
        public Guid AccountId { get; private set; }
        public string Username { get; private set; } = null!;

        protected PostAuthor() { }

        public PostAuthor(Guid id, Guid accountId, string username) : base(id)
        {
            AccountId = accountId;
            Username = username;
        }
    }
}