using System;
using Volo.Abp;

namespace PansyDev.Shetter.Domain.Exceptions
{
    public class PostNotFoundException : BusinessException
    {
        public PostNotFoundException(Guid id) : base(ShetterDomainErrorCodes.PostNotFound)
        {
            WithData("id", id);
        }
    }
}