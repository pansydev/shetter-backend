using System;
using System.Threading.Tasks;
using PansyDev.Shetter.Domain.Aggregates.PostAggregate;
using PansyDev.Shetter.Domain.Aggregates.PostLikeAggregate;
using PansyDev.Shetter.Domain.Exceptions;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace PansyDev.Shetter.Domain.Services
{
    public class PostLikeService : DomainService
    {
        private readonly AuthorManager _authorManager;
        private readonly IPostRepository _postRepository;
        private readonly IPostLikeRepository _postLikeRepository;

        public PostLikeService(AuthorManager authorManager, IPostRepository postRepository,
            IPostLikeRepository postLikeRepository)
        {
            _authorManager = authorManager;
            _postRepository = postRepository;
            _postLikeRepository = postLikeRepository;
        }

        public virtual async Task LikePost(Guid postId)
        {
            var author = await _authorManager.EnsureCurrentAuthorCreated();

            var postExists = await _postRepository.AnyAsync(x => x.Id == postId);
            if (!postExists)
                throw new PostNotFoundException(postId);

            var existingLike = await _postLikeRepository.FindByPostAndAuthorIds(postId, author.Id);
            if (existingLike is not null)
                throw new PostAlreadyLikedException();

            var postLike = new PostLike(postId, author.Id);
            await _postLikeRepository.InsertAsync(postLike);
        }

        public virtual async Task DislikePost(Guid postId)
        {
            var author = await _authorManager.EnsureCurrentAuthorCreated();

            var postExists = await _postRepository.AnyAsync(x => x.Id == postId);
            if (!postExists)
                throw new PostNotFoundException(postId);

            var existingLike = await _postLikeRepository.FindByPostAndAuthorIds(postId, author.Id);
            if (existingLike is null)
                throw new PostNotLikedException();

            await _postLikeRepository.DeleteAsync(existingLike);
        }
    }
}
