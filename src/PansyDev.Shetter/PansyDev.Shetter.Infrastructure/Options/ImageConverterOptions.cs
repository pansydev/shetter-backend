using ImageMagick;

namespace PansyDev.Shetter.Infrastructure.Options
{
    internal class ImageConverterOptions
    {
        public MagickFormat ImageFormat { get; set; } = MagickFormat.Jpeg;
    }
}