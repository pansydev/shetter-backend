using System;
using System.Threading.Tasks;
using PansyDev.Common.Application.Models;
using PansyDev.Shetter.Domain.Services;
using Volo.Abp;
using Volo.Abp.Application.Services;

namespace PansyDev.Shetter.Application.Services
{
    public class PostLikeAppService : ApplicationService
    {
        private readonly PostLikeService _postLikeService;

        public PostLikeAppService(PostLikeService postLikeService)
        {
            _postLikeService = postLikeService;
        }

        public virtual async Task<OperationResult> LikePost(Guid postId)
        {
            try
            {
                await _postLikeService.LikePost(postId);
                return OperationResult.NoContent;
            }
            catch (BusinessException exception)
            {
                return OperationResult.FromBusinessException(exception);
            }
        }

        public virtual async Task<OperationResult> DislikePost(Guid postId)
        {
            try
            {
                await _postLikeService.DislikePost(postId);
                return OperationResult.NoContent;
            }
            catch (BusinessException exception)
            {
                return OperationResult.FromBusinessException(exception);
            }
        }
    }
}
