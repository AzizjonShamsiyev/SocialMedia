using Domain.Entities;
using FluentValidation;

namespace Application.Validation;

public class PostValidation : AbstractValidator<Post>
{
    public PostValidation()
    {
        RuleFor(x => x.CreatedDateTime).NotEmpty()
            .NotNull() 
            .NotEmpty()
            .Must(x => x <= DateTime.UtcNow)
            .WithMessage("Creation date is not valid!");

        RuleFor(x => x.Text).NotEmpty()
            .NotNull()
            .MaximumLength(1000)
            .MinimumLength(1)
            .WithMessage("Text must be between 1 and 1000 characters");

    }
}
