namespace LearnTrack.Application.Topics;

public class TopicRequest
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public List<string> Tags { get; set; } = [];

    public bool IsDone { get; set; } = false;
}
