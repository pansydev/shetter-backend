using System;
using System.Linq;
using System.Threading.Tasks;
using PansyDev.Shetter.Domain.Aggregates.PostAuthorAggregate;
using Volo.Abp.Application.Services;

namespace PansyDev.Shetter.Application.Services
{
    public class AuthorAppService : ApplicationService
    {
        private readonly IPostAuthorRepository _postAuthorRepository;

        public AuthorAppService(IPostAuthorRepository postAuthorRepository)
        {
            _postAuthorRepository = postAuthorRepository;
        }

        public virtual async Task<Guid?> GetCurrentAuthorId()
        {
            if (CurrentUser.Id is null) return null;

            var query = _postAuthorRepository.Where(x => x.AccountId == CurrentUser.Id).Select(x => x.Id);
            return await AsyncExecuter.FirstOrDefaultAsync(query);
        }
    }
}
