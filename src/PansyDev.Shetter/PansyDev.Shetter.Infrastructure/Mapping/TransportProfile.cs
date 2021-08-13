using System.Linq;
using AutoMapper;
using PansyDev.Shetter.Application.Models.ReadModels;
using PansyDev.Shetter.Domain.Aggregates.PostAggregate;
using PansyDev.Shetter.Domain.Aggregates.PostAuthorAggregate;

namespace PansyDev.Shetter.Infrastructure.Mapping
{
    internal class TransportProfile : Profile
    {
        public TransportProfile()
        {
            ShouldMapProperty = p => p.GetMethod!.IsPublic || p.GetMethod.IsAssembly;

            CreateMap<Post, PostReadModel>()
                .ForMember(x => x.PreviousVersions,
                    x => x.MapFrom(p => p.PreviousVersions.OrderByDescending(v => v.CreationTime)));

            CreateMap<PostVersion, PostVersionReadModel>();
            CreateMap<PostAuthor, PostAuthorReadModel>();
        }
    }
}