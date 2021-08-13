using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using PansyDev.Common.Application.Exceptions;
using PansyDev.Common.Application.Extensions;
using PansyDev.Shetter.Application.Models.WriteModels;
using PansyDev.Shetter.Application.Services.Abstractions;
using PansyDev.Shetter.Domain.Aggregates.PostAggregate;
using Volo.Abp.Application.Services;

namespace PansyDev.Shetter.Application.Services
{
    public class ImageUploaderAppService : ApplicationService
    {
        private readonly IImageConverter _imageConverter;
        private readonly IImageStorage _imageStorage;
        private readonly IImageUrlResolver _imageUrlResolver;
        private readonly IImageBlurHashResolver _blurHashResolver;

        public ImageUploaderAppService(IImageConverter imageConverter, IImageStorage imageStorage,
            IImageUrlResolver imageUrlResolver, IImageBlurHashResolver blurHashResolver)
        {
            _imageConverter = imageConverter;
            _imageStorage = imageStorage;
            _imageUrlResolver = imageUrlResolver;
            _blurHashResolver = blurHashResolver;
        }

        public async Task<IReadOnlyList<PostImage>> UploadImages(IReadOnlyList<Stream> writeModels)
        {
            return await writeModels.Select(UploadImage).ToArrayAsync();
        }

        public async Task<IReadOnlyList<PostImage>> UpdateImages(IReadOnlyList<PostImageWriteModel> writeModels,
            Post existingPost)
        {
            var imageGroups = writeModels.Where(x => x.Id.HasValue).GroupBy(x => x.Id);

            if (imageGroups.Any(x => x.Count() > 1))
            {
                throw new InvalidRequestException("Specified image list contains duplicates");
            }

            return await writeModels.Select(x => UpdateImage(x, existingPost)).ToArrayAsync();
        }

        private async Task<PostImage> UpdateImage(PostImageWriteModel writeModel, Post existingPost)
        {
            Debug.Assert(writeModel.Id.HasValue || writeModel.Stream is not null);

            if (!writeModel.Id.HasValue)
            {
                return await UploadImage(writeModel.Stream!);
            }

            var existingImage = existingPost.CurrentVersion.Images.FirstOrDefault(x => x.Id == writeModel.Id);

            if (existingImage is null)
            {
                throw new InvalidRequestException("The post being edited does not contain the specified image ids");
            }

            return existingImage;
        }

        private async Task<PostImage> UploadImage(Stream stream)
        {
            await using var image = _imageConverter.ConvertImage(stream);
            await stream.DisposeAsync();

            var imageId = GuidGenerator.Create();
            await _imageStorage.UploadImage(imageId, image);

            var url = _imageUrlResolver.ResolveUrl(imageId);
            var blurHash = _blurHashResolver.ResolveBlurHash(image);

            return new PostImage(imageId, url, blurHash);
        }
    }
}
