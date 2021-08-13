namespace PansyDev.Shetter.Infrastructure.Options
{
    internal class ImageStorageOptions
    {
        public string ImagePublicPrefix { get; set; } = null!;
        public string ImagePathPrefix { get; set; } = null!;
        public string ImageExtension { get; set; } = null!;
        public string ContentType { get; set; } = null!;
        public string BucketName { get; set; } = null!;
        public string TokenPath { get; set; } = null!;
    }
}