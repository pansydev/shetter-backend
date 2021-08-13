using Volo.Abp;

namespace PansyDev.Shetter.Domain.Exceptions
{
    public class EmptyContentException : BusinessException
    {
        public EmptyContentException() : base(ShetterDomainErrorCodes.EmptyContent) { }
    }
}