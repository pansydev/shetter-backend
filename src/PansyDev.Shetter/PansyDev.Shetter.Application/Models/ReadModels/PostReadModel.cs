using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace PansyDev.Shetter.Application.Models.ReadModels
{
    public class PostReadModel : EntityDto<Guid>
    {
        public Guid AuthorId { get; set; }

        public DateTime CreationTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public PostAuthorReadModel Author { get; set; } = null!;
        public PostVersionReadModel CurrentVersion { get; set; } = null!;
        public List<PostVersionReadModel> PreviousVersions { get; set; } = null!;
    }
}