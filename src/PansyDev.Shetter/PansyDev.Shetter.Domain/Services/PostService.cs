using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PansyDev.Common.Domain.Exceptions;
using PansyDev.Shetter.Domain.Aggregates.PostAggregate;
using PansyDev.Shetter.Domain.Exceptions;
using Volo.Abp.Domain.Services;

namespace PansyDev.Shetter.Domain.Services
{
    public class PostService : DomainService
    {
        private readonly IPostRepository _postRepository;
        private readonly AuthorManager _authorManager;
        private readonly PostVersionManager _postVersionManager;

        public PostService(IPostRepository postRepository, AuthorManager authorManager,
            PostVersionManager postVersionManager)
        {
            _postRepository = postRepository;
            _authorManager = authorManager;
            _postVersionManager = postVersionManager;
        }

        public virtual async Task<Post> CreatePost(string? text, IReadOnlyList<PostImage>? images)
        {
            var version = await _postVersionManager.CreateVersion(text, images);

            var author = await _authorManager.EnsureCurrentAuthorCreated();
            var post = new Post(GuidGenerator.Create(), author.Id, version);

            await _postRepository.InsertAsync(post, true);

            return post;
        }

        public virtual async Task<Post> FindPost(Guid postId)
        {
            var postQueryable = await _postRepository.WithDetailsAsync(
                x => x.CurrentVersion,
                x => x.PreviousVersions,
                x => x.Author);

            postQueryable = postQueryable.Where(x => x.Id == postId);

            var post = await AsyncExecuter.FirstOrDefaultAsync(postQueryable);
            if (post is null) throw new PostNotFoundException(postId);

            return post;
        }

        public virtual async Task EditPost(Post post, string? text, IReadOnlyList<PostImage>? images)
        {
            var author = await _authorManager.FindCurrentAuthor();

            if (author is null || post.AuthorId != author.Id)
                throw new EntityAccessViolationException();

            if (post.CurrentVersion.Text == text &&
                post.CurrentVersion.Images.SequenceEqual(images ?? ArraySegment<PostImage>.Empty))
                throw new ContentNotChangedException();

            var version = await _postVersionManager.CreateVersion(text, images);
            post.Edit(version);

            await _postRepository.UpdateAsync(post, true);
        }
    }
}