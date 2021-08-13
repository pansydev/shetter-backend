using System;
using Microsoft.Extensions.Options;
using PansyDev.Shetter.Application.Services.Abstractions;
using PansyDev.Shetter.Infrastructure.Options;
using Volo.Abp.DependencyInjection;

namespace PansyDev.Shetter.Infrastructure.Services
{
    internal class ImageUrlResolver : IImageUrlResolver, ISingletonDependency
    {
        private readonly ImageStorageOptions _options;

        public ImageUrlResolver(IOptions<ImageStorageOptions> options)
        {
            _options = options.Value;
        }

        public Uri ResolveUrl(Guid id)
        {
            return new(_options.ImagePublicPrefix + id.ToString("N") + _options.ImageExtension);
        }
    }
}