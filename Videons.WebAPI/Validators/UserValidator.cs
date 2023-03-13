using FluentValidation;
using Videons.Entities.DTOs;

namespace Videons.Core.Utilities.Validators;

public class UserValidator : AbstractValidator<UserForRegisterDto>
{
    public UserValidator()
    {
        RuleFor(u => u.FirstName).MaximumLength(50).WithMessage("Name length must be lower than 50");
        RuleFor(u => u.FirstName).NotEmpty().WithMessage("Name cannot be empty");
        RuleFor(u => u.LastName).MaximumLength(50);
        RuleFor(u => u.Email).NotEmpty().WithMessage("Email cannot be empty!");
        RuleFor(u => u.Email).EmailAddress().WithMessage("Email address is not valid!");
        RuleFor(u => u.Password).NotEmpty().WithMessage("Password cannot be empty!");
        RuleFor(u => u.Password).MinimumLength(6).WithMessage("Password length must be higher than 6 characters!");
    }
}