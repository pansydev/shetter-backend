using System;
using System.Linq;
using AutoMapper;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using PansyDev.Shetter.Application.Models.ReadModels;
using PansyDev.Shetter.Infrastructure.Data;

namespace PansyDev.Shetter.Web.Queries
{
    [ExtendObjectType("Query")]
    public class PostQuery
    {
        private readonly IMapper _mapper;

        public PostQuery(IMapper mapper)
        {
            _mapper = mapper;
        }

        [UseDbContext(typeof(ShetterDbContext))]
        [UseProjection]
        [UsePaging(IncludeTotalCount = true)]
        public IQueryable<PostReadModel> GetPosts([ScopedService] ShetterDbContext context)
        {
            return _mapper.ProjectTo<PostReadModel>(context.Posts.OrderByDescending(x => x.CreationTime));
        }

        [UseDbContext(typeof(ShetterDbContext))]
        [UseProjection]
        [UseFirstOrDefault]
        public IQueryable<PostReadModel> GetPost([ScopedService] ShetterDbContext context, Guid id)
        {
            return _mapper.ProjectTo<PostReadModel>(context.Posts.Where(x => x.Id == id));
        }
    }
}
