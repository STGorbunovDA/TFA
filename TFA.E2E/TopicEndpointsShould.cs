﻿using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using TFA.Domain.Models;

namespace TFA.E2E;

public class TopicEndpointsShould(ForumApiApplicationFactory factory)
    : IClassFixture<ForumApiApplicationFactory>, IAsyncLifetime
{
    public Task InitializeAsync() => Task.CompletedTask;

    public Task DisposeAsync() => Task.CompletedTask;

    // [Fact]
    public async Task ReturnForbidden_WhenNotAuthenticated()
    {
        using var httpClient = factory.CreateClient();

        using var forumCreatedResponse = await httpClient.PostAsync("forums",
            JsonContent.Create(new { title = "Test forum" }));
        forumCreatedResponse.EnsureSuccessStatusCode();

        var createdForum = await forumCreatedResponse.Content.ReadFromJsonAsync<ForumDomain>();
        createdForum.Should().NotBeNull();

        var responseMessage = await httpClient.PostAsync($"forums/{createdForum!.Id}/topics",
            JsonContent.Create(new { title = "Hello world" }));
        responseMessage.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }
}