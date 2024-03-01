using Domain.Entities;
using FluentValidation;

namespace Application.Validation;

public class CommentValidation : AbstractValidator<Comment>
{
    public CommentValidation()
    {
        RuleFor(x => x.Text)
            .NotEmpty()
            .MinimumLength(1)
            .MaximumLength(1000)
            .WithMessage("Text is not valid");
    }
}
