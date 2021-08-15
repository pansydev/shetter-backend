using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace PansyDev.Shetter.Application.Models.ReadModels
{
    public class PostAuthorReadModel : EntityDto<Guid>
    {
        public string Username { get; set; } = null!;
        public List<PostReadModel> Posts { get; set; } = null!;
    }
}
