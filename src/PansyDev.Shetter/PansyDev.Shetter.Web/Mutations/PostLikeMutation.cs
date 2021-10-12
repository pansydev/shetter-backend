using System;
using System.Threading.Tasks;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Types;
using PansyDev.Common.Application.Models;
using PansyDev.Shetter.Application.Services;
using Volo.Abp.DependencyInjection;

namespace PansyDev.Shetter.Web.Mutations
{
    [ExtendObjectType(OperationTypeNames.Mutation)]
    public class PostLikeMutation : IScopedDependency
    {
        private readonly PostLikeAppService _postLikeAppService;

        public PostLikeMutation(PostLikeAppService postLikeAppService)
        {
            _postLikeAppService = postLikeAppService;
        }

        [Authorize]
        public Task<OperationResult> LikePost(Guid postId)
        {
            return _postLikeAppService.LikePost(postId);
        }

        [Authorize]
        public Task<OperationResult> DislikePost(Guid postId)
        {
            return _postLikeAppService.DislikePost(postId);
        }
    }
}
