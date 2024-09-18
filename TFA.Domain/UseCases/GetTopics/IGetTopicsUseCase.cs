using TFA.Domain.Models;

namespace TFA.Domain.UseCases.GetTopics;

public interface IGetTopicsUseCase
{
    Task<(IEnumerable<TopicDomain> resources, int totalCount)> Execute(
        GetTopicsQuery query, CancellationToken cancellationToken);
}