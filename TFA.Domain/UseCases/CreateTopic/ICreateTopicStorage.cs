using TFA.Domain.Models;

namespace TFA.Domain.UseCases.CreateTopic;

public interface ICreateTopicStorage
{
    Task<bool> ForumExists(Guid forumId, CancellationToken cancellationToken);
    Task<TopicDomain> CreateTopic(Guid forumId, Guid userId, string title, CancellationToken cancellationToken);
}