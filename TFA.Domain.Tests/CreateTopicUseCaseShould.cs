using FluentAssertions;
using Moq;
using Moq.Language.Flow;
using TFA.Domain.Authentication;
using TFA.Domain.Exceptions;
using TFA.Domain.Models;
using TFA.Domain.UseCases.CreateTopic;

namespace TFA.Domain.Tests;

public class CreateTopicUseCaseShould
{
    private readonly CreateTopicUseCase sut;
    private readonly Mock<ICreateTopicStorage> storage;
    private readonly ISetup<ICreateTopicStorage, Task<bool>> forumExistsSetup;
    private readonly ISetup<ICreateTopicStorage, Task<TopicDomain>> createTopicSetup;
    private readonly ISetup<IIdentity,Guid> getCurrentUserIdSetup;
    
    public CreateTopicUseCaseShould()
    {
        storage = new Mock<ICreateTopicStorage>();
        forumExistsSetup = storage.Setup(s => s.ForumExists(It.IsAny<Guid>(), It.IsAny<CancellationToken>()));
        createTopicSetup = storage.Setup(s =>
            s.CreateTopic(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<CancellationToken>()));
        
        var identity = new Mock<IIdentity>();
        var identityProvider = new Mock<IIdentityProvider>();
        identityProvider.Setup(p => p.Current).Returns(identity.Object);
        getCurrentUserIdSetup = identity.Setup(s => s.UserId);
        
       
        
        sut = new CreateTopicUseCase(identityProvider.Object, storage.Object);
    }
    
    [Fact]
    public async Task ThrowForumNotFoundException_WhenNoMatchingForum()
    {
        var forumId = Guid.Parse("5E1DCF96-E8F3-41C9-BD59-6479140933B3");
        
        forumExistsSetup.ReturnsAsync(false);
        
        await sut.Invoking(s => s.Execute(forumId, "Some title", CancellationToken.None))
            .Should().ThrowAsync<ForumNotFoundException>();
        
        storage.Verify(s => s.ForumExists(forumId, It.IsAny<CancellationToken>()));
    }
    
    [Fact]
    public async Task ThrowIntentionManagerException_WhenTopicCreationIsNotAllowed()
    {
        var forumId = Guid.Parse("E20A64A3-47E3-4076-96D0-7EF226EAF5F2");
        var userId = Guid.Parse("91B714CC-BDFF-47A1-A6DC-E71DDE8C25F7");
        
        forumExistsSetup.ReturnsAsync(true);
        var expected = new TopicDomain();
        createTopicSetup.ReturnsAsync(expected);
        getCurrentUserIdSetup.Returns(userId);
        
        var actual = await sut.Execute(forumId, "Hello world", CancellationToken.None);
        actual.Should().Be(expected);

        storage.Verify(s => s.CreateTopic(forumId, userId, "Hello world", It.IsAny<CancellationToken>()), Times.Once);
    }

}