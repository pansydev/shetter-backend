using PansyDev.Shetter.Application.Models.ReadModels;

namespace PansyDev.Shetter.Application.Events
{
    public class PostCreatedAppEventData
    {
        public PostCreatedAppEventData(PostReadModel post)
        {
            Post = post;
        }

        public PostReadModel Post { get; }
    }
}