using TFA.Domain.Models;

namespace TFA.Domain.UseCases.CreateTopic;

public interface ICreateTopicUseCase
{
    Task<TopicDomain> Execute(CreateTopicCommand createTopicCommand, CancellationToken cancellationToken);
}