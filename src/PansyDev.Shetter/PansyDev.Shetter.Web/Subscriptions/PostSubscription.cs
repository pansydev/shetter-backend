using HotChocolate;
using HotChocolate.Types;
using PansyDev.Shetter.Application.Models.ReadModels;

namespace PansyDev.Shetter.Web.Subscriptions
{
    [ExtendObjectType(OperationTypeNames.Subscription)]
    internal class PostSubscription
    {
        [Subscribe]
        public PostReadModel PostCreated([EventMessage] PostReadModel post) => post;

        [Subscribe]
        public PostReadModel PostEdited([EventMessage] PostReadModel post) => post;
    }
}