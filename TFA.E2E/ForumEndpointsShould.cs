using System.Net.Http.Json;
using FluentAssertions;
using TFA.API.Models;

namespace TFA.E2E;

public class ForumEndpointsShould : IClassFixture<ForumApiApplicationFactory>
{
    private readonly ForumApiApplicationFactory factory;

    public ForumEndpointsShould(ForumApiApplicationFactory factory)
    {
        this.factory = factory;
    }

    [Fact]
    public async Task CreateNewForum()
    {
        const string forumTitle = "0069517D-CA29-453B-BB4C-AC22F51E690E";
        
        using var httpClient = factory.CreateClient();

        // Проверяем что список форумов пуст
        using var getInitialForumsResponse = await httpClient.GetAsync("forums");
        getInitialForumsResponse.IsSuccessStatusCode.Should().BeTrue();
        var initialForums = await getInitialForumsResponse.Content.ReadFromJsonAsync<ForumDto[]>();
        initialForums
            .Should().NotBeNull().And
            .Subject.As<ForumDto[]>().Should().NotContain(f => f.Title.Equals(forumTitle));

        // создаём форум
        using var response = await httpClient.PostAsync("forums",
            JsonContent.Create(new { title = forumTitle }));
        
        response.IsSuccessStatusCode.Should().BeTrue();
        var forum = await response.Content.ReadFromJsonAsync<ForumDto>();
        forum
            .Should().NotBeNull().And
            .Subject.As<ForumDto>().Title.Should().Be(forumTitle);

        // Проверяем что форум присутствует
        using var getForumsResponse = await httpClient.GetAsync("forums");
        var forums = await getForumsResponse.Content.ReadFromJsonAsync<ForumDto[]>();
        forums
            .Should().NotBeNull().And
            .Subject.As<ForumDto[]>().Should().Contain(f => f.Title == forumTitle);
    }
}