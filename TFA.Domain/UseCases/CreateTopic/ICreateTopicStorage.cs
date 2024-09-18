using TFA.Domain.Models;

namespace TFA.Domain.UseCases.CreateTopic;

public interface ICreateTopicStorage
{
    Task<TopicDomain> CreateTopic(Guid forumId, Guid userId, string title, CancellationToken cancellationToken);
}