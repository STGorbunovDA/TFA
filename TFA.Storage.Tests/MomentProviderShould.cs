using TFA.Domain;

namespace TFA.Storage.Tests;

public class MomentProviderShould
{
    [Fact]
    public void Now_ReturnsCurrentUtcTime()
    {
        var momentProvider = new MomentProvider();

        var currentMoment = momentProvider.Now;

        var expectedMoment = DateTimeOffset.UtcNow;
        var timeDifference = (currentMoment - expectedMoment).TotalSeconds;

        // Проверим, что разница между моментами не больше 1 секунды
        Assert.True(Math.Abs(timeDifference) < 1, 
            $"Expected moment to be close to current UTC time. Difference: {timeDifference} seconds.");
    }
}