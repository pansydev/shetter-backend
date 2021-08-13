using System;

namespace PansyDev.Shetter.Application.Services.Abstractions
{
    public interface IImageUrlResolver
    {
        Uri ResolveUrl(Guid imageId);
    }
}