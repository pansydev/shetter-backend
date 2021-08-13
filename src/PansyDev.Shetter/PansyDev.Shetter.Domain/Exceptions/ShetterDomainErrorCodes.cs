namespace PansyDev.Shetter.Domain.Exceptions
{
    public static class ShetterDomainErrorCodes
    {
        private const string Prefix = "Shetter:";

        public const string PostNotFound = Prefix + nameof(PostNotFound);
        public const string ContentNotChanged = Prefix + nameof(ContentNotChanged);
        public const string EmptyContent = Prefix + nameof(EmptyContent);
    }
}