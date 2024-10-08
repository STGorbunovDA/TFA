﻿using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TFA.API.Models;
using TFA.Domain.UseCases.CreateForum;
using TFA.Domain.UseCases.CreateTopic;
using TFA.Domain.UseCases.GetForums;
using TFA.Domain.UseCases.GetTopics;

namespace TFA.API.Controllers;

[ApiController]
[Route("forums")]
public class ForumController(
    ISender mediator,
    IMapper mapper) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(400)]
    [ProducesResponseType(403)]
    [ProducesResponseType(201, Type = typeof(ForumDto))]
    public async Task<IActionResult> CreateForum(
        [FromBody] CreateForumDto request,
        CancellationToken cancellationToken)
    {
        var command = new CreateForumCommand(request.Title);
        var forum = await mediator.Send(command, cancellationToken);
        return CreatedAtRoute(nameof(GetForums), mapper.Map<ForumDto>(forum));
    }

    /// <summary>
    /// Get list of every forum
    /// </summary>
    /// <param name="useCase"></param>
    /// <param name="mapper"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet(Name = nameof(GetForums))]
    [ProducesResponseType(200, Type = typeof(ForumDto[]))]
    public async Task<IActionResult> GetForums(CancellationToken cancellationToken)
    {
        var forums = await mediator.Send(new GetForumsQuery(), cancellationToken);
        return Ok(forums.Select(mapper.Map<ForumDto>));
    }

    [HttpPost("{forumId:guid}/topics")]
    [ProducesResponseType(400)]
    [ProducesResponseType(403)]
    [ProducesResponseType(410)]
    [ProducesResponseType(201, Type = typeof(TopicDto))]
    public async Task<IActionResult> CreateTopic(
        Guid forumId,
        [FromBody] CreateTopicDto request,
        CancellationToken cancellationToken)
    {
        var command = new CreateTopicCommand(forumId, request.Title);
        var topic = await mediator.Send(command, cancellationToken);
        return CreatedAtRoute(nameof(GetForums), mapper.Map<TopicDto>(topic));
    }

    [HttpGet("{forumId:guid}/topics")]
    [ProducesResponseType(400)]
    [ProducesResponseType(410)]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetTopics(
        [FromRoute] Guid forumId,
        [FromQuery] int skip,
        [FromQuery] int take,
        CancellationToken cancellationToken)
    {
        var query = new GetTopicsQuery(forumId, skip, take);
        var (resources, totalCount) = await mediator.Send(query, cancellationToken);
        return Ok(new { resources = resources.Select(mapper.Map<TopicDto>), totalCount });
    }
}