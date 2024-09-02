﻿using Microsoft.EntityFrameworkCore;
using TFA.Domain.Models;
using TFA.Storage;

namespace TFA.Domain.UseCases.GetForums;

public class GetForumsUseCase : IGetForumsUseCase
{
    private readonly ForumDbContext forumDbContext;

    public GetForumsUseCase(ForumDbContext forumDbContext)
    {
        this.forumDbContext = forumDbContext;
    }

    public async Task<IEnumerable<ForumDomain>> Execute(CancellationToken cancellationToken) =>
        await forumDbContext.Forums
            .Select(f => new ForumDomain
            {
                Id = f.ForumId,
                Title = f.Title
            })
            .ToArrayAsync(cancellationToken);
}