using Moq;
using TFA.Domain.Authentication;

namespace TFA.Domain.Tests.Authentication;

public class IdentityProviderShould
{
    [Fact]
    public void SetCurrent_ShouldAssignIdentity()
    {
        var identityProviderMock = new Mock<IIdentityProvider>();
        var mockIdentity = new Mock<IIdentity>();
        identityProviderMock.SetupProperty(ip => ip.Current);

        identityProviderMock.Object.Current = mockIdentity.Object;
        var result = identityProviderMock.Object.Current;

        Assert.NotNull(result);
        Assert.Equal(mockIdentity.Object, result);
    }

    [Fact]
    public void GetCurrent_ShouldReturnNull_WhenNotSet()
    {
        var identityProviderMock = new Mock<IIdentityProvider>();
        identityProviderMock.SetupProperty(ip => ip.Current);

        var result = identityProviderMock.Object.Current;

        Assert.Null(result);
    }
}