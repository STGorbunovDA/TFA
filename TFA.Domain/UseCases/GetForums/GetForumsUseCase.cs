using TFA.Domain.Models;

namespace TFA.Domain.UseCases.GetForums;

public class GetForumsUseCase : IGetForumsUseCase
{
    private readonly IGetForumsStorage storage;

    public GetForumsUseCase(
        IGetForumsStorage storage)
    {
        this.storage = storage;
    }

    public Task<IEnumerable<ForumDomain>> Execute(CancellationToken cancellationToken) =>
        storage.GetForums(cancellationToken);
}