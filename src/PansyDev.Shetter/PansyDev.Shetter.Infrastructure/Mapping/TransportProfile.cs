using System;
using System.Linq;
using AutoMapper;
using PansyDev.Shetter.Application.Models.ReadModels;
using PansyDev.Shetter.Domain.Aggregates.PostAggregate;
using PansyDev.Shetter.Domain.Aggregates.PostAuthorAggregate;
using PansyDev.Shetter.Domain.Aggregates.PostLikeAggregate;

namespace PansyDev.Shetter.Infrastructure.Mapping
{
    internal class TransportProfile : Profile
    {
        public TransportProfile()
        {
            ShouldMapProperty = p => p.GetMethod!.IsPublic || p.GetMethod.IsAssembly;

            Guid? currentAuthorId = null;

            CreateMap<Post, PostReadModel>()
                .ForMember(x => x.PreviousVersions,
                    x => x.MapFrom(p => p.PreviousVersions.OrderByDescending(v => v.CreationTime)))
                .ForMember(x => x.IsLiked,
                    x => x.MapFrom(p => p.Likes.Any(l => l.AuthorId == currentAuthorId)));

            CreateMap<PostVersion, PostVersionReadModel>();
            CreateMap<PostAuthor, PostAuthorReadModel>();
            CreateMap<PostLike, PostLikeReadModel>();
        }
    }
}
