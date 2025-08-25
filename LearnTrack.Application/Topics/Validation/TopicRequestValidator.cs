using FluentValidation;

namespace LearnTrack.Application.Topics.Validation;

public class TopicRequestValidator : AbstractValidator<TopicRequest>
{
    public TopicRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

        RuleFor(x => x.Tags)
            .NotNull().WithMessage("Tags list must not be null.");
    }
}
