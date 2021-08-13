using System.IO;

namespace PansyDev.Shetter.Application.Services.Abstractions
{
    public interface IImageBlurHashResolver
    {
        string ResolveBlurHash(Stream stream);
    }
}