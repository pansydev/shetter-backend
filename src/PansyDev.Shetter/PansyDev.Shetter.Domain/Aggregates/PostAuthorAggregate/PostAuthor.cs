using System;
using System.Collections.Generic;
using PansyDev.Shetter.Domain.Aggregates.PostAggregate;
using Volo.Abp.Domain.Entities;

namespace PansyDev.Shetter.Domain.Aggregates.PostAuthorAggregate
{
    public class PostAuthor : BasicAggregateRoot<Guid>
    {
        public Guid AccountId { get; private set; }
        public string Username { get; private set; } = null!;

        protected PostAuthor() { }

        internal IReadOnlyList<Post> Posts { get; private set; } = null!;

        public PostAuthor(Guid id, Guid accountId, string username) : base(id)
        {
            AccountId = accountId;
            Username = username;
        }
    }
}
