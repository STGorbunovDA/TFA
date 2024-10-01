using MediatR;
using TFA.Domain.Models;

namespace TFA.Domain.UseCases.GetForums;

internal class GetForumsUseCase(
    IGetForumsStorage storage) : IRequestHandler<GetForumsQuery, IEnumerable<ForumDomain>>
{
    public Task<IEnumerable<ForumDomain>> Handle(GetForumsQuery query, CancellationToken cancellationToken) => 
        storage.GetForums(cancellationToken);
}