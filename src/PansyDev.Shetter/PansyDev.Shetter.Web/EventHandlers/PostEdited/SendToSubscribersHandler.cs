using System.Threading.Tasks;
using HotChocolate.Subscriptions;
using PansyDev.Shetter.Application.Events;
using PansyDev.Shetter.Web.Subscriptions;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;

namespace PansyDev.Shetter.Web.EventHandlers.PostEdited
{
    internal class SendToSubscribersHandler : ILocalEventHandler<PostEditedAppEventData>, ISingletonDependency
    {
        private readonly ITopicEventSender _topicEventSender;

        public SendToSubscribersHandler(ITopicEventSender topicEventSender)
        {
            _topicEventSender = topicEventSender;
        }

        public async Task HandleEventAsync(PostEditedAppEventData eventData)
        {
            await _topicEventSender.SendAsync(nameof(PostSubscription.PostEdited), eventData.Post);
        }
    }
}