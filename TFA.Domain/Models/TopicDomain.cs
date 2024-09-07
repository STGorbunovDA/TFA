namespace TFA.Domain.Models;

public class TopicDomain
{
    public Guid Id { get; set; }
    public Guid ForumId { get; set; }
    public Guid UserId { get; set; }
    public string Title { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}