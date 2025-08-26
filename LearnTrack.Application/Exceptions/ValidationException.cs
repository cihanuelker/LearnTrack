namespace LearnTrack.Application.Exceptions;

public class ValidationException(IEnumerable<string> errors) : Exception("Validation failed")
{
    public IEnumerable<string> Errors { get; } = errors;
}
