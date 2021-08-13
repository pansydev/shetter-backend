using System.Drawing;
using System.Drawing.Common.Blurhash;
using System.IO;
using PansyDev.Shetter.Application.Services.Abstractions;
using Volo.Abp.DependencyInjection;

namespace PansyDev.Shetter.Infrastructure.Services
{
    internal class ImageBlurHashResolver : IImageBlurHashResolver, ISingletonDependency
    {
        private const int XComponents = 4;
        private const int YComponents = 3;

        private readonly Encoder _encoder = new();

        public string ResolveBlurHash(Stream stream)
        {
            using var image = Image.FromStream(stream);
            return _encoder.Encode(image, XComponents, YComponents);
        }
    }
}