using HotChocolate.Types;
using HotChocolate.Types.Pagination;
using PansyDev.Shetter.Application.Models.ReadModels;
using PansyDev.Shetter.Domain.Aggregates.PostAuthorAggregate;

namespace PansyDev.Shetter.Web.Types
{
    internal class PostAuthorType : ObjectType<PostAuthorReadModel>
    {
        protected override void Configure(IObjectTypeDescriptor<PostAuthorReadModel> descriptor)
        {
            descriptor.Name(nameof(PostAuthor));

            descriptor.Field(x => x.Posts)
                .UseProjection()
                .UsePaging(options: new PagingOptions { IncludeTotalCount = true });
        }
    }
}
