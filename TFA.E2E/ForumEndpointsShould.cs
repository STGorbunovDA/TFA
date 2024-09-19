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
        using var httpClient = factory.CreateClient();

        // Проверяем что список форумов пуст
        using var getInitialForumsResponse = await httpClient.GetAsync("forums");
        var initialForums = await getInitialForumsResponse.Content.ReadFromJsonAsync<ForumDto[]>();
        initialForums
            .Should().NotBeNull().And
            .Subject.As<ForumDto[]>().Should().BeEmpty();

        // создаём форум
        using var response = await httpClient.PostAsync("forums",
            JsonContent.Create(new { title = "Test" }));
        
        response.Invoking(r => r.EnsureSuccessStatusCode()).Should().NotThrow();
        var forum = await response.Content.ReadFromJsonAsync<ForumDto>();
        forum
            .Should().NotBeNull().And
            .Subject.As<ForumDto>().Title.Should().Be("Test");

        // Проверяем что форум присутствует
        using var getForumsResponse = await httpClient.GetAsync("forums");
        var forums = await getForumsResponse.Content.ReadFromJsonAsync<ForumDto[]>();
        forums
            .Should().NotBeNull().And
            .Subject.As<ForumDto[]>().Should().Contain(f => f.Title == "Test");
    }
}