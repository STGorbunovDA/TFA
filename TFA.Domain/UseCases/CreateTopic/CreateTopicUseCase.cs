using TFA.Domain.Authentication;
using TFA.Domain.Exceptions;
using TFA.Domain.Models;

namespace TFA.Domain.UseCases.CreateTopic;

public class CreateTopicUseCase : ICreateTopicUseCase
{
    private readonly ICreateTopicStorage storage;
    private readonly IIdentityProvider identityProvider;

    public CreateTopicUseCase(IIdentityProvider identityProvider, ICreateTopicStorage storage)
    {
        this.storage = storage;
        this.identityProvider = identityProvider;
    }
    
    public async Task<TopicDomain> Execute(Guid forumId, string title, CancellationToken cancellationToken)
    {
        var forumExists = await storage.ForumExists(forumId, cancellationToken);
        if (!forumExists)
        {
            throw new ForumNotFoundException(forumId);
        }
        
        return await storage.CreateTopic(forumId, identityProvider.Current.UserId, title, cancellationToken);
    }
}