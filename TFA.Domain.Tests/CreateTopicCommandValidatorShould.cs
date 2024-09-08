using FluentAssertions;
using TFA.Domain.UseCases.CreateTopic;

namespace TFA.Domain.Tests;

public class CreateTopicCommandValidatorShould
{
    private readonly CreateTopicCommandValidator sut = new();

    [Fact]
    public void ReturnSuccess_WhenCommandIsValid()
    {
        var actual = sut.Validate(new CreateTopicCommand(Guid.Parse("6D7EB993-AC66-4B6E-8178-FDF11FCC7BCB"), "Hello"));
        actual.IsValid.Should().BeTrue();
    }

    public static IEnumerable<object[]> GetInvalidCommands()
    {
        var validCommand = new CreateTopicCommand(Guid.Parse("4D9DDA12-1EA9-433D-B409-F55B10CD86A7"), "Hello");
        // yield return new object[] { validCommand with { ForumId = Guid.Empty }, nameof(CreateTopicCommand.ForumId), "Empty" };
        // yield return new object[] { validCommand with { Title = string.Empty }, nameof(CreateTopicCommand.Title), "Empty" };
        // yield return new object[] { validCommand with { Title = "    " }, nameof(CreateTopicCommand.Title), "Empty" };
        // yield return new object[] { validCommand with { Title = string.Join("a", Enumerable.Range(0, 100)) }, nameof(CreateTopicCommand.Title), "TooLong" };
        // yield return new object[] { validCommand with { Title = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc eget leo ac lectus ultricies ullamcorper. Etiam lobortis, augue faucibus tristique." }, nameof(CreateTopicCommand.Title), "TooLong" };
        yield return new object[] { validCommand with { ForumId = Guid.Empty } };
        yield return new object[] { validCommand with { Title = string.Empty } };
        yield return new object[] { validCommand with { Title = "    " } };
        yield return new object[] { validCommand with { Title = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nunc eget leo ac lectus ultricies ullamcorper. Etiam lobortis, augue faucibus tristique." } };
    }

    [Theory]
    [MemberData(nameof(GetInvalidCommands))]
    public void ReturnFailure_WhenCommandIsInvalid(CreateTopicCommand command) //string expectedInvalidPropertyName, string expectedErrorCode
    {
        var actual = sut.Validate(command);
        actual.IsValid.Should().BeFalse();
        //actual.Errors.Should().Contain(f => f.PropertyName == expectedInvalidPropertyName && f.ErrorCode == expectedErrorCode);
    }
}