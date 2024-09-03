using TFA.Domain.Models;

namespace TFA.Domain.UseCases.CreateTopic;

public interface ICreateTopicUseCase
{
    Task<TopicDomain> Execute(Guid forumId, string title, CancellationToken cancellationToken);
}