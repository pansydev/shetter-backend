using System.Collections.Generic;

namespace PansyDev.Shetter.Web.Types
{
    public class EditPostInput
    {
        public string? Text { get; set; }
        public IReadOnlyList<PostImageInput>? Images { get; set; }
    }
}