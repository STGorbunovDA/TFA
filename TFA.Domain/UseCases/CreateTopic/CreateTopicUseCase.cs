﻿using MediatR;
using TFA.Domain.Authentication;
using TFA.Domain.Authorization;
using TFA.Domain.Models;
using TFA.Domain.UseCases.GetForums;

namespace TFA.Domain.UseCases.CreateTopic;

internal class CreateTopicUseCase(
    IIntentionManager intentionManager,
    IIdentityProvider identityProvider,
    IGetForumsStorage getForumsStorage,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreateTopicCommand, TopicDomain>
{
    public async Task<TopicDomain> Handle(CreateTopicCommand command, CancellationToken cancellationToken)
    {
        var (forumId, title) = command;
        intentionManager.ThrowIfForbidden(TopicIntention.Create);

        await getForumsStorage.ThrowIfForumNotFound(forumId, cancellationToken);

        await using var scope = await unitOfWork.StartScope(cancellationToken);
        var topic = await scope.GetStorage<ICreateTopicStorage>()
            .CreateTopic(forumId, identityProvider.Current.UserId, title, cancellationToken);
        await scope.GetStorage<IDomainEventStorage>().AddEvent(topic, cancellationToken);
        await scope.Commit(cancellationToken);

        return topic;
    }
}