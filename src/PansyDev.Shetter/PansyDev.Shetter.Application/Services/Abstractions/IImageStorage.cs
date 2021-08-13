using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace PansyDev.Shetter.Application.Services.Abstractions
{
    public interface IImageStorage
    {
        Task UploadImage(Guid imageId, Stream stream, CancellationToken cancellationToken = default);
        Task DeleteImage(Guid imageId, CancellationToken cancellationToken = default);
    }
}