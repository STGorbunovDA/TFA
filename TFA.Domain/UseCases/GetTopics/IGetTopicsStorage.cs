using TFA.Domain.Models;

namespace TFA.Domain.UseCases.GetTopics;

public interface IGetTopicsStorage
{
    Task<(IEnumerable<TopicDomain> resources, int totalCount)> GetTopics(
        Guid forumId, int skip, int take, CancellationToken cancellationToken);
}