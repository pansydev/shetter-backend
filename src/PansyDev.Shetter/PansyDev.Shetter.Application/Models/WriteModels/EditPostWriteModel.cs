using System.Collections.Generic;

namespace PansyDev.Shetter.Application.Models.WriteModels
{
    public class EditPostWriteModel
    {
        public EditPostWriteModel(string? text, IReadOnlyList<PostImageWriteModel>? images)
        {
            Text = text;
            Images = images;
        }

        public string? Text { get; }
        public IReadOnlyList<PostImageWriteModel>? Images { get; }
    }
}