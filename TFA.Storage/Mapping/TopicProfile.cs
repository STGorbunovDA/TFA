using AutoMapper;
using TFA.Domain.Models;

namespace TFA.Storage.Mapping;

internal class TopicProfile : Profile
{
    public TopicProfile()
    {
        CreateMap<Topic, TopicDomain>()
            .ForMember(d => d.Id, s => s.MapFrom(t => t.TopicId));
    }
}