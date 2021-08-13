using System;
using System.IO;

namespace PansyDev.Shetter.Application.Models.WriteModels
{
    public class PostImageWriteModel
    {
        public PostImageWriteModel(Guid? id = null, Stream? stream = null)
        {
            Id = id;
            Stream = stream;
        }

        public Guid? Id { get; }
        public Stream? Stream { get; }
    }
}