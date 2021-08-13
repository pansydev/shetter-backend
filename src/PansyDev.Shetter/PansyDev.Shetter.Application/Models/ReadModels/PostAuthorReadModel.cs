using System;
using Volo.Abp.Application.Dtos;

namespace PansyDev.Shetter.Application.Models.ReadModels
{
    public class PostAuthorReadModel : EntityDto<Guid>
    {
        public string Username { get; set; } = null!;
    }
}
