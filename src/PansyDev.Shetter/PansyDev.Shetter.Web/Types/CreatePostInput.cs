using System.Collections.Generic;
using HotChocolate.Types;

namespace PansyDev.Shetter.Web.Types
{
    public class CreatePostInput
    {
        public string? Text { get; set; }
        public IReadOnlyList<IFile>? Images { get; set; }
    }
}