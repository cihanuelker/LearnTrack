namespace LearnTrack.Application.Topics;

public class TopicDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public List<string> Tags { get; set; } = [];

    public DateTime CreatedAt { get; set; }

    public bool IsDone { get; set; }
}
