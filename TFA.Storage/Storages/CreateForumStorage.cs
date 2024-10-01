using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using TFA.Domain.Models;
using TFA.Domain.UseCases.CreateForum;
using TFA.Storage.Entities;

namespace TFA.Storage.Storages;

internal class CreateForumStorage(
    IMemoryCache memoryCache,
    IGuidFactory guidFactory,
    ForumDbContext dbContext,
    IMapper mapper)
    : ICreateForumStorage
{
    public async Task<ForumDomain> Create(string title, CancellationToken cancellationToken)
    {
        var forumId = guidFactory.Create();
        var forum = new Forum
        {
            ForumId = forumId,
            Title = title,
        };
        await dbContext.Forums.AddAsync(forum, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        memoryCache.Remove(nameof(GetForumsStorage.GetForums));

        return await dbContext.Forums
            .Where(f => f.ForumId == forumId)
            .ProjectTo<ForumDomain>(mapper.ConfigurationProvider)
            .FirstAsync(cancellationToken);
    }
}