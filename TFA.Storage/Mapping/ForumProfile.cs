using AutoMapper;
using TFA.Domain.Models;
using TFA.Storage.Entities;

namespace TFA.Storage.Mapping;

internal class ForumProfile : Profile
{
    public ForumProfile()
    {
        CreateMap<Forum, ForumDomain>()
            .ForMember(d => d.Id, s => s.MapFrom(f => f.ForumId));
    }
}