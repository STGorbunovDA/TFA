using TFA.Domain.Models;

namespace TFA.Domain.UseCases.CreateForum;

public interface ICreateForumStorage
{
    public Task<ForumDomain> Create(string title, CancellationToken cancellationToken);
}