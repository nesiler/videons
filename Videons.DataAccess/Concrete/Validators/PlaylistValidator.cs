using FluentValidation;
using Videons.Entities.DTOs;

namespace Videons.Core.Utilities.Validators;

public class PlaylistValidator : AbstractValidator<PlaylistDto>
{
    public PlaylistValidator()
    {
        RuleFor(p => p.Name).NotEmpty().WithMessage("Name cannot be empty");
        RuleFor(p => p.Name).MinimumLength(3);
        RuleFor(p => p.Description).NotEmpty().WithMessage("Description cannot be empty");
        RuleFor(p => p.Description).MinimumLength(3);
        RuleFor(p => p.ChannelId).NotEmpty().WithMessage("ChannelId cannot be empty");
    }
}