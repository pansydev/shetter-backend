using System;

namespace PansyDev.Shetter.Application.Models.ReadModels
{
    public class PostLikeReadModel
    {
        public Guid AuthorId { get; set; }
        public PostAuthorReadModel Author { get; set; } = null!;
    }
}
