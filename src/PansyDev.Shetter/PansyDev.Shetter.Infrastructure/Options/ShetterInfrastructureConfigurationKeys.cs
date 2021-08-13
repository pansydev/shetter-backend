namespace PansyDev.Shetter.Infrastructure.Options
{
    internal class ShetterInfrastructureConfigurationKeys
    {
        private const string Prefix = "Shetter:";

        public const string ImageStorage = Prefix + nameof(ImageStorage);
        public const string ImageConverter = Prefix + nameof(ImageConverter);
    }
}