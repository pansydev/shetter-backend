using System.Collections.Generic;
using System.IO;

namespace PansyDev.Shetter.Application.Models.WriteModels
{
    public class CreatePostWriteModel
    {
        public CreatePostWriteModel(string? text, IReadOnlyList<Stream>? images)
        {
            Text = text;
            Images = images;
        }

        public string? Text { get; }
        public IReadOnlyList<Stream>? Images { get; }
    }
}