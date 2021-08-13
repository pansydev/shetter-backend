using System.Diagnostics;
using System.Threading.Tasks;
using PansyDev.Shetter.Domain.Aggregates.PostAuthorAggregate;
using Volo.Abp.Domain.Services;
using Volo.Abp.Users;

namespace PansyDev.Shetter.Domain.Services
{
    public class AuthorManager : DomainService
    {
        private readonly IPostAuthorRepository _postAuthorRepository;
        private readonly ICurrentUser _currentUser;

        public AuthorManager(ICurrentUser currentUser, IPostAuthorRepository postAuthorRepository)
        {
            _currentUser = currentUser;
            _postAuthorRepository = postAuthorRepository;
        }

        public virtual async Task<PostAuthor?> FindCurrentAuthor()
        {
            Debug.Assert(_currentUser.Id.HasValue);

            return await _postAuthorRepository.FindByAccountId(_currentUser.Id.Value);
        }

        public virtual async Task<PostAuthor> EnsureCurrentAuthorCreated()
        {
            Debug.Assert(_currentUser.Id.HasValue);

            var author = await FindCurrentAuthor();

            if (author is null)
            {
                author = new PostAuthor(GuidGenerator.Create(), _currentUser.Id.Value, _currentUser.UserName);
                await _postAuthorRepository.InsertAsync(author);
            }

            return author;
        }
    }
}