using Volo.Abp;

namespace PansyDev.Shetter.Domain.Exceptions
{
    public class PostAlreadyLikedException : BusinessException
    {
        public PostAlreadyLikedException() : base(ShetterDomainErrorCodes.PostAlreadyLiked) { }
    }
}
