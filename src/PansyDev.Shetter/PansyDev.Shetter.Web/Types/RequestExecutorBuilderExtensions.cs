using System;
using HotChocolate.Execution.Configuration;
using HotChocolate.Types;
using Microsoft.Extensions.DependencyInjection;
using PansyDev.Common.Web.GraphQL.Extensions;
using PansyDev.Shetter.Application.Models.ReadModels;
using PansyDev.Shetter.Domain.Aggregates.PostAggregate;
using PansyDev.Shetter.Domain.Aggregates.PostAggregate.TextTokens;
using PansyDev.Shetter.Domain.Aggregates.PostAuthorAggregate;
using PansyDev.Shetter.Web.Mutations;
using PansyDev.Shetter.Web.Queries;
using PansyDev.Shetter.Web.Subscriptions;

namespace PansyDev.Shetter.Web.Types
{
    public static class RequestExecutorBuilderExtensions
    {
        public static IRequestExecutorBuilder AddShetterTypes(this IRequestExecutorBuilder builder)
        {
            builder.AddCommonTypes();

            builder.AddEntities();
            builder.AddWriteModels();
            builder.AddResults();
            builder.AddTextTokens();

            builder.AddQueryType();
            builder.AddTypeExtension<PostQuery>();

            builder.AddMutationType();
            builder.AddTypeExtension<PostMutation>();

            builder.AddSubscriptionType();
            builder.AddTypeExtension<PostSubscription>();

            return builder;
        }

        private static void AddResults(this IRequestExecutorBuilder builder)
        {
            builder.AddOperationResult<PostReadModel>(nameof(Post));
            builder.AddOperationResult<PostAuthorReadModel>(nameof(PostAuthor));
        }

        private static void AddEntities(this IRequestExecutorBuilder builder)
        {
            builder.AddType<PostType>();
            builder.AddType<PostAuthorType>();

            builder.AddObjectType<PostVersionReadModel>(x => x.Name(nameof(PostVersion)));
        }

        private static void AddWriteModels(this IRequestExecutorBuilder builder)
        {
            builder.AddType<UploadType>();

            builder.AddInputObjectType<EditPostInput>();
            builder.AddInputObjectType<CreatePostInput>();
            builder.AddInputObjectType<PostImageInput>();
        }

        private static void AddTextTokens(this IRequestExecutorBuilder builder)
        {
            builder.AddInterfaceType<TextToken>();

            builder.AddObjectType<PlainTextToken>(x => x
                .Field(f => f.Modifiers)
                .Type<NonNullType<ListType<NonNullType<EnumType<TextTokenModifier>>>>>()
                .Resolve(ctx => ctx.Parent<PlainTextToken>().Modifiers ?? ArraySegment<TextTokenModifier>.Empty));

            builder.AddObjectType<LinkTextToken>();
            builder.AddObjectType<MentionTextToken>();
        }
    }
}
