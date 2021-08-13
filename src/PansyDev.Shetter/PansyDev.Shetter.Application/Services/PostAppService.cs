using System;
using System.Threading.Tasks;
using PansyDev.Common.Application.Models;
using PansyDev.Shetter.Application.Events;
using PansyDev.Shetter.Application.Models.ReadModels;
using PansyDev.Shetter.Application.Models.WriteModels;
using PansyDev.Shetter.Domain.Aggregates.PostAggregate;
using PansyDev.Shetter.Domain.Services;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.EventBus.Local;

namespace PansyDev.Shetter.Application.Services
{
    public class PostAppService : ApplicationService
    {
        private readonly PostService _postService;
        private readonly ILocalEventBus _localEventBus;
        private readonly ImageUploaderAppService _imageUploaderAppService;

        public PostAppService(PostService postService, ILocalEventBus localEventBus,
            ImageUploaderAppService imageUploaderAppService)
        {
            _postService = postService;
            _localEventBus = localEventBus;
            _imageUploaderAppService = imageUploaderAppService;
        }

        public virtual async Task<OperationResult<PostReadModel>> CreateAsync(CreatePostWriteModel writeModel)
        {
            try
            {
                var images = writeModel.Images is not null
                    ? await _imageUploaderAppService.UploadImages(writeModel.Images)
                    : null;

                var post = await _postService.CreatePost(writeModel.Text, images);

                var postReadModel = ObjectMapper.Map<Post, PostReadModel>(post);

                await _localEventBus.PublishAsync(new PostCreatedAppEventData(postReadModel));

                return postReadModel;
            }
            catch (BusinessException exception)
            {
                return OperationResult.FromBusinessException(exception);
            }
        }

        public virtual async Task<OperationResult<PostReadModel>> EditAsync(Guid postId, EditPostWriteModel writeModel)
        {
            try
            {
                var post = await _postService.FindPost(postId);

                var images = writeModel.Images is not null
                    ? await _imageUploaderAppService.UpdateImages(writeModel.Images, post)
                    : null;

                await _postService.EditPost(post, writeModel.Text, images);

                var postReadModel = ObjectMapper.Map<Post, PostReadModel>(post);

                await _localEventBus.PublishAsync(new PostEditedAppEventData(postReadModel));

                return postReadModel;
            }
            catch (BusinessException exception)
            {
                return OperationResult.FromBusinessException(exception);
            }
        }
    }
}