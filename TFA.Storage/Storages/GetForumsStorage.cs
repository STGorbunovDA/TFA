using Microsoft.EntityFrameworkCore;
using TFA.Domain.Models;
using TFA.Domain.UseCases.GetForums;

namespace TFA.Storage.Storages;

internal class GetForumsStorage : IGetForumsStorage
{
    private readonly ForumDbContext dbContext;

    public GetForumsStorage(
        ForumDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IEnumerable<ForumDomain>> GetForums(CancellationToken cancellationToken) =>
        await dbContext.Forums
            .Select(f => new ForumDomain()
            {
                Id = f.ForumId,
                Title = f.Title
            })
            .ToArrayAsync(cancellationToken);
}