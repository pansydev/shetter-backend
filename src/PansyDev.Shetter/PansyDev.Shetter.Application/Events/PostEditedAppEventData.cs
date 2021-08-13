using PansyDev.Shetter.Application.Models.ReadModels;

namespace PansyDev.Shetter.Application.Events
{
    public class PostEditedAppEventData
    {
        public PostEditedAppEventData(PostReadModel post)
        {
            Post = post;
        }

        public PostReadModel Post { get; }
    }
}