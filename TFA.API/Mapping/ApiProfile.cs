using AutoMapper;
using TFA.API.Models;
using TFA.Domain.Models;

namespace TFA.API.Mapping;

internal class ApiProfile : Profile
{
    public ApiProfile()
    {
        CreateMap<ForumDomain, ForumDto>();
        CreateMap<TopicDomain, TopicDto>();
    }
}