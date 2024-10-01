using MediatR;
using TFA.Domain.Models;
using TFA.Domain.UseCases.GetForums;

namespace TFA.Domain.UseCases.GetTopics;

internal class GetTopicsUseCase(
    IGetForumsStorage getForumsStorage,
    IGetTopicsStorage storage)
    : IRequestHandler<GetTopicsQuery, (IEnumerable<TopicDomain> resources, int totalCount)>
{
    public async Task<(IEnumerable<TopicDomain> resources, int totalCount)> Handle(
        GetTopicsQuery query, CancellationToken cancellationToken)
    {
        await getForumsStorage.ThrowIfForumNotFound(query.ForumId, cancellationToken);
        return await storage.GetTopics(query.ForumId, query.Skip, query.Take, cancellationToken);
    }
}