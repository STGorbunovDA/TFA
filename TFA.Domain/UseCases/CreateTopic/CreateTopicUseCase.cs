using Microsoft.EntityFrameworkCore;
using TFA.Domain.Exceptions;
using TFA.Domain.Models;
using TFA.Storage;

namespace TFA.Domain.UseCases.CreateTopic;

public class CreateTopicUseCase : ICreateTopicUseCase
{
    private readonly IGuidFactory guidFactory;
    private readonly IMomentProvider momentProvider;
    private readonly ForumDbContext dbContext;

    public CreateTopicUseCase(
        IGuidFactory guidFactory,
        IMomentProvider momentProvider,
        ForumDbContext dbContext)
    {
        this.guidFactory = guidFactory;
        this.momentProvider = momentProvider;
        this.dbContext = dbContext;
    }
    
    public async Task<TopicDomain> Execute(Guid forumId, string title, Guid authorId, CancellationToken cancellationToken)
    {
        var forumExists = await dbContext.Forums.AnyAsync(f => f.ForumId == forumId, cancellationToken);
        if (!forumExists)
        {
            throw new ForumNotFoundException(forumId);
        }
        
        var topicId = guidFactory.Create();
        await dbContext.Topics.AddAsync(new Topic
        {
            TopicId = topicId,
            ForumId = forumId,
            UserId = authorId,
            CreatedAt = momentProvider.Now,
            Title = title
        }, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return await dbContext.Topics
            .Where(t => t.TopicId == topicId)
            .Select(t => new TopicDomain()
            {
                Id = t.TopicId,
                Title = t.Title,
                CreatedAt = t.CreatedAt,
                Author = t.Author.Login
            })
            .FirstAsync(cancellationToken);
    }
}