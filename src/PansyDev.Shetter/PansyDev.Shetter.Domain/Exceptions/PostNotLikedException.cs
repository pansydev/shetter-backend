using Volo.Abp;

namespace PansyDev.Shetter.Domain.Exceptions
{
    public class PostNotLikedException : BusinessException
    {
        public PostNotLikedException() : base(ShetterDomainErrorCodes.PostNotLiked) { }
    }
}
