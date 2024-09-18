using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using TFA.Domain.Models;
using TFA.Domain.UseCases.GetForums;

namespace TFA.Storage.Storages;

internal class GetForumsStorage : IGetForumsStorage
{
    private readonly IMemoryCache memoryCache;
    private readonly ForumDbContext dbContext;

    public GetForumsStorage(
        IMemoryCache memoryCache,
        ForumDbContext dbContext)
    {
        this.memoryCache = memoryCache;
        this.dbContext = dbContext;
    }

    public async Task<IEnumerable<ForumDomain>> GetForums(CancellationToken cancellationToken) =>
        await memoryCache.GetOrCreateAsync<ForumDomain[]>(
            nameof(GetForums),
            entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10);
                return dbContext.Forums
                    .Select(f => new ForumDomain()
                    {
                        Id = f.ForumId,
                        Title = f.Title
                    })
                    .ToArrayAsync(cancellationToken);
            });
}