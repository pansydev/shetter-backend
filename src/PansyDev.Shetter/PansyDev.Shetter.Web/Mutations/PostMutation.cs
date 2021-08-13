using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Types;
using PansyDev.Common.Application.Exceptions;
using PansyDev.Common.Application.Models;
using PansyDev.Shetter.Application.Models.ReadModels;
using PansyDev.Shetter.Application.Models.WriteModels;
using PansyDev.Shetter.Application.Services;
using PansyDev.Shetter.Web.Types;
using Volo.Abp.DependencyInjection;

namespace PansyDev.Shetter.Web.Mutations
{
    [ExtendObjectType(OperationTypeNames.Mutation)]
    public class PostMutation : IScopedDependency
    {
        private readonly PostAppService _postAppService;

        public PostMutation(PostAppService postAppService)
        {
            _postAppService = postAppService;
        }

        [Authorize]
        public Task<OperationResult<PostReadModel>> CreatePost(CreatePostInput input)
        {
            var images = input.Images?.Select(x => x.OpenReadStream()).ToArray() ?? ArraySegment<Stream>.Empty;
            var writeModel = new CreatePostWriteModel(input.Text, images);

            return _postAppService.CreateAsync(writeModel);
        }

        [Authorize]
        public Task<OperationResult<PostReadModel>> EditPost(Guid postId, EditPostInput input)
        {
            if (input.Images is null)
            {
                var editPostWriteModel = new EditPostWriteModel(input.Text, null);
                return _postAppService.EditAsync(postId, editPostWriteModel);
            }

            var images = new List<PostImageWriteModel>();

            foreach (var inputImage in input.Images)
            {
                if (inputImage.File is not null && inputImage.Id is not null)
                {
                    throw new InvalidRequestException("You must specify either File or Id in PostImageInput");
                }

                if (inputImage.File is not null)
                {
                    images.Add(new PostImageWriteModel(stream: inputImage.File.OpenReadStream()));
                    continue;
                }

                if (inputImage.Id is not null)
                {
                    images.Add(new PostImageWriteModel(inputImage.Id));
                    continue;
                }

                throw new InvalidRequestException("You must specify File or Id in PostImageInput");
            }

            var writeModel = new EditPostWriteModel(input.Text, images);
            return _postAppService.EditAsync(postId, writeModel);
        }
    }
}