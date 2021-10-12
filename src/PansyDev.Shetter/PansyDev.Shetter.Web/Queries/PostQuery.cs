using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using PansyDev.Shetter.Application.Models.ReadModels;
using PansyDev.Shetter.Application.Services;
using PansyDev.Shetter.Infrastructure.Data;
using Volo.Abp.DependencyInjection;

namespace PansyDev.Shetter.Web.Queries
{
    [ExtendObjectType("Query")]
    public class PostQuery : IScopedDependency
    {
        private readonly IMapper _mapper;
        private readonly AuthorAppService _authorAppService;

        public PostQuery(IMapper mapper, AuthorAppService authorAppService)
        {
            _mapper = mapper;
            _authorAppService = authorAppService;
        }

        [UseDbContext(typeof(ShetterDbContext))]
        [UseProjection]
        [UsePaging(IncludeTotalCount = true)]
        public async Task<IQueryable<PostReadModel>> GetPosts([ScopedService] ShetterDbContext context)
        {
            var authorId = await _authorAppService.GetCurrentAuthorId();

            return _mapper.ProjectTo<PostReadModel>(
                context.Posts.OrderByDescending(x => x.CreationTime),
                new { currentAuthorId = authorId });
        }

        [UseDbContext(typeof(ShetterDbContext))]
        [UseProjection]
        [UseFirstOrDefault]
        public IQueryable<PostReadModel> GetPost([ScopedService] ShetterDbContext context, Guid id)
        {
            return _mapper.ProjectTo<PostReadModel>(context.Posts.Where(x => x.Id == id));
        }

        [UseDbContext(typeof(ShetterDbContext))]
        [UseProjection]
        [UseFirstOrDefault]
        public IQueryable<PostAuthorReadModel> GetAuthor([ScopedService] ShetterDbContext context, Guid id)
        {
            return _mapper.ProjectTo<PostAuthorReadModel>(context.PostAuthors.Where(x => x.Id == id));
        }
    }
}
