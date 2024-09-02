using TFA.Domain.Models;

namespace TFA.Domain.UseCases.GetForums;

public interface IGetForumsUseCase
{
    Task<IEnumerable<ForumDomain>> Execute(CancellationToken cancellationToken);
}