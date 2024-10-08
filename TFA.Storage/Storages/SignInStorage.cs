﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using TFA.Domain.UseCases.SignIn;
using TFA.Storage.Entities;

namespace TFA.Storage.Storages;

internal class SignInStorage(
    IGuidFactory guidFactory,
    ForumDbContext dbContext,
    IMapper mapper)
    : ISignInStorage
{
    public Task<RecognisedUser?> FindUser(string login, CancellationToken cancellationToken) => dbContext.Users
        .Where(u => u.Login.Equals(login))
        .ProjectTo<RecognisedUser>(mapper.ConfigurationProvider)
        .FirstOrDefaultAsync(cancellationToken);

    public async Task<Guid> CreateSession(
        Guid userId, DateTimeOffset expirationMoment, CancellationToken cancellationToken)
    {
        var sessionId = guidFactory.Create();
        await dbContext.Sessions.AddAsync(new Session
        {
            SessionId = sessionId,
            UserId = userId,
            ExpiresAt = expirationMoment,
        }, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return sessionId;
    }
}