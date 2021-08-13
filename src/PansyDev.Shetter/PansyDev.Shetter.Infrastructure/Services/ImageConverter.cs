using System.IO;
using ImageMagick;
using Microsoft.Extensions.Options;
using PansyDev.Shetter.Application.Services.Abstractions;
using PansyDev.Shetter.Infrastructure.Options;
using Volo.Abp.DependencyInjection;

namespace PansyDev.Shetter.Infrastructure.Services
{
    internal class ImageConverter : IImageConverter, ISingletonDependency
    {
        private readonly ImageConverterOptions _options;

        public ImageConverter(IOptions<ImageConverterOptions> options)
        {
            _options = options.Value;
        }

        public Stream ConvertImage(Stream stream)
        {
            using var image = new MagickImage(stream);

            var resultStream = new MemoryStream();

            image.AdaptiveResize(1920, 1080);
            image.Write(resultStream, _options.ImageFormat);

            return resultStream;
        }
    }
}