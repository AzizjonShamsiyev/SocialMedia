using Domain.Entities;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Application.Validation;

public class UserValidation : AbstractValidator<User>
{
    public UserValidation() 
    {
        RuleFor(user => user.FirstName)
            .NotEmpty()
            .MaximumLength(50)
            .WithMessage("The FirstName must be not empty and be less than 50 characters");

        RuleFor(user => user.LastName)
            .NotEmpty()
            .MaximumLength(50)
            .WithMessage("The LastName must be not empty and be less than 50 characters");

        RuleFor(user => user.Address)
            .NotEmpty()
            .WithMessage("The Address must be not empty");

        RuleFor(user => user.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("The Email is not valid");

        RuleFor(user => user.Password)
            .NotEmpty()
            .MinimumLength(8)
            .MaximumLength(50)
            .WithMessage("The Password must be 8-50 characters");

        RuleFor(user => user.BirthDate) 
            .NotEmpty()
            .Must(x => x <= DateTime.UtcNow)
            .WithMessage("Date is not valid");

        RuleFor(user => user.Phone)
            .NotEmpty()
            .Must(x => Regex.IsMatch(x,"^([+]?[\\s0-9]+)?(\\d{3}|[(]?[0-9]+[)])?([-]?[\\s]?[0-9])+$"))
            .WithMessage("The Phone  number is not valid");

        RuleFor(user => user.Gender)
            .NotNull()
            .WithMessage("Gender must be not null");
    }
}
