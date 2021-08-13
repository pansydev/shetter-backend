using Volo.Abp;

namespace PansyDev.Shetter.Domain.Exceptions
{
    public class ContentNotChangedException : BusinessException
    {
        public ContentNotChangedException() : base(ShetterDomainErrorCodes.ContentNotChanged) { }
    }
}