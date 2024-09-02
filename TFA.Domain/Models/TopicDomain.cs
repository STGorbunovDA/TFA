namespace TFA.Domain.Models;

public class TopicDomain
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public string Author { get; set; }
}