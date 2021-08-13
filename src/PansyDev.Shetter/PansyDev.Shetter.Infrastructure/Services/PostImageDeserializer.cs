using System.Collections.Generic;
using Newtonsoft.Json;
using PansyDev.Shetter.Application.Services.Abstractions;
using PansyDev.Shetter.Domain.Aggregates.PostAggregate;
using Volo.Abp.DependencyInjection;

namespace PansyDev.Shetter.Infrastructure.Services
{
    internal class PostImageDeserializer : ISingletonDependency
    {
        private readonly IImageUrlResolver _imageUrlResolver;

        public PostImageDeserializer(IImageUrlResolver imageUrlResolver)
        {
            _imageUrlResolver = imageUrlResolver;
        }

        public IReadOnlyList<PostImage> Deserialize(string json)
        {
            var images = JsonConvert.DeserializeObject<IReadOnlyList<PostImage>>(json)!;

            foreach (var postImage in images)
            {
                postImage.Url = _imageUrlResolver.ResolveUrl(postImage.Id);
            }

            return images;
        }
    }
}