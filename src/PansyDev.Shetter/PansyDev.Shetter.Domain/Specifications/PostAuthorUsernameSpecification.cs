using System;
using System.Linq.Expressions;
using PansyDev.Shetter.Domain.Aggregates.PostAuthorAggregate;
using Volo.Abp.Specifications;

namespace PansyDev.Shetter.Domain.Specifications
{
    public class PostAuthorUsernameSpecification : Specification<PostAuthor>
    {
        private readonly string _username;

        public PostAuthorUsernameSpecification(string username)
        {
            _username = username.ToLower();
        }

        public override Expression<Func<PostAuthor, bool>> ToExpression()
        {
            return author => author.Username.ToLower() == _username;
        }
    }
}