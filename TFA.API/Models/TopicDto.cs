namespace TFA.API.Models;

public class TopicDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}