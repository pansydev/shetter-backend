using HotChocolate.Types;
using HotChocolate.Types.Pagination;
using PansyDev.Shetter.Application.Models.ReadModels;
using PansyDev.Shetter.Domain.Aggregates.PostAggregate;

namespace PansyDev.Shetter.Web.Types
{
    internal class PostType : ObjectType<PostReadModel>
    {
        protected override void Configure(IObjectTypeDescriptor<PostReadModel> descriptor)
        {
            descriptor.Name(nameof(Post));

            descriptor.Field(x => x.PreviousVersions)
                .UseProjection()
                .UsePaging(options: new PagingOptions { IncludeTotalCount = true });
        }
    }
}
