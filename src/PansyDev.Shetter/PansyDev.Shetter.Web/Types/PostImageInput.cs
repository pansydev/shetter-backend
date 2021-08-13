using System;
using HotChocolate.Types;

namespace PansyDev.Shetter.Web.Types
{
    public class PostImageInput
    {
        public Guid? Id { get; set; }
        public IFile? File { get; set; }
    }
}