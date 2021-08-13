using System.Threading.Tasks;
using HotChocolate.Subscriptions;
using PansyDev.Shetter.Application.Events;
using PansyDev.Shetter.Web.Subscriptions;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;

namespace PansyDev.Shetter.Web.EventHandlers.PostCreated
{
    internal class SendToSubscribersHandler : ILocalEventHandler<PostCreatedAppEventData>, ISingletonDependency
    {
        private readonly ITopicEventSender _topicEventSender;

        public SendToSubscribersHandler(ITopicEventSender topicEventSender)
        {
            _topicEventSender = topicEventSender;
        }

        public async Task HandleEventAsync(PostCreatedAppEventData eventData)
        {
            await _topicEventSender.SendAsync(nameof(PostSubscription.PostCreated), eventData.Post);
        }
    }
}