using TFA.Domain.Models;

namespace TFA.Domain.UseCases.CreateForum;

public interface ICreateForumUseCase
{
    Task<ForumDomain> Execute(CreateForumCommand command, CancellationToken cancellationToken);
}