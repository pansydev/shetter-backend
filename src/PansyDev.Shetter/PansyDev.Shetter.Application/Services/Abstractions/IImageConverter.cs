using System.IO;

namespace PansyDev.Shetter.Application.Services.Abstractions
{
    public interface IImageConverter
    {
        Stream ConvertImage(Stream image);
    }
}