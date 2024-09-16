using FluentValidation;
using TFA.Domain.Authentication;
using TFA.Domain.Authorization;
using TFA.Domain.Exceptions;
using TFA.Domain.Models;

namespace TFA.Domain.UseCases.CreateTopic;

internal class CreateTopicUseCase : ICreateTopicUseCase
{
    private readonly IValidator<CreateTopicCommand> validator;
    private readonly IIntentionManager intentionManager;
    private readonly IIdentityProvider identityProvider;
    private readonly ICreateTopicStorage storage;

    public CreateTopicUseCase(
        IValidator<CreateTopicCommand> validator,
        IIntentionManager intentionManager,
        IIdentityProvider identityProvider,
        ICreateTopicStorage storage)
    {
        this.validator = validator;
        this.intentionManager = intentionManager;
        this.identityProvider = identityProvider;
        this.storage = storage;
    }
    
    public async Task<TopicDomain> Execute(CreateTopicCommand createTopicCommand, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(createTopicCommand, cancellationToken);
        var (forumId, title) = createTopicCommand;
        intentionManager.ThrowIfForbidden(TopicIntention.Create);

        var forumExists = await storage.ForumExists(forumId, cancellationToken);
        if (!forumExists)
        {
            throw new ForumNotFoundException(forumId);
        }

        return await storage.CreateTopic(forumId, identityProvider.Current.UserId, title, cancellationToken);
    }
}