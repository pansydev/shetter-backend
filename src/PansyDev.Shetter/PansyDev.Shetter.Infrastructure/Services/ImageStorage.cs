using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.Extensions.Options;
using PansyDev.Shetter.Application.Services.Abstractions;
using PansyDev.Shetter.Infrastructure.Options;
using Volo.Abp.DependencyInjection;

namespace PansyDev.Shetter.Infrastructure.Services
{
    internal class ImageStorage : IImageStorage, ISingletonDependency
    {
        private readonly StorageClient _client;
        private readonly ImageStorageOptions _options;

        public ImageStorage(IOptions<ImageStorageOptions> options)
        {
            _options = options.Value;

            var tokenPath = Path.Combine(Directory.GetCurrentDirectory(), _options.TokenPath);
            var credentials = GoogleCredential.FromFile(tokenPath);

            _client = StorageClient.Create(credentials);
        }

        public async Task UploadImage(Guid imageId, Stream stream, CancellationToken cancellationToken)
        {
            await _client.UploadObjectAsync(_options.BucketName, GetImagePath(imageId), _options.ContentType, stream,
                cancellationToken: cancellationToken);
        }

        public async Task DeleteImage(Guid imageId, CancellationToken cancellationToken = default)
        {
            await _client.DeleteObjectAsync(_options.BucketName, GetImagePath(imageId),
                cancellationToken: cancellationToken);
        }

        private string GetImagePath(Guid imageId)
        {
            return _options.ImagePathPrefix + imageId.ToString("N") + _options.ImageExtension;
        }
    }
}