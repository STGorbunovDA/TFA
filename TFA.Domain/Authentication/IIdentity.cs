namespace TFA.Domain.Authentication;

public interface IIdentity
{
    Guid UserId { get; }
}

public class User : IIdentity
{
    public User(Guid userId)
    {
        UserId = userId;
    }

    public Guid UserId { get; }
}