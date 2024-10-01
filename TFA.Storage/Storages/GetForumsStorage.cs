using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using TFA.Domain.Models;
using TFA.Domain.UseCases.GetForums;

namespace TFA.Storage.Storages;

internal class GetForumsStorage(
    IMemoryCache memoryCache,
    ForumDbContext dbContext,
    IMapper mapper)
    : IGetForumsStorage
{
    public async Task<IEnumerable<ForumDomain>> GetForums(CancellationToken cancellationToken) =>
        (await memoryCache.GetOrCreateAsync<ForumDomain[]>(
            nameof(GetForums),
            entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10);
                return dbContext.Forums
                    .ProjectTo<ForumDomain>(mapper.ConfigurationProvider)
                    .ToArrayAsync(cancellationToken);
            }))!;
}