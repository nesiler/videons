using FluentValidation;
using Videons.Entities.DTOs;

namespace Videons.Core.Utilities.Validators;

public class VideoValidator : AbstractValidator<VideoDto>
{
    public VideoValidator()
    {
        RuleFor(u => u.Title).MaximumLength(50).WithMessage("Title length must be lower than 50");
        RuleFor(u => u.Title).NotEmpty().WithMessage("Title cannot be empty");
        RuleFor(u => u.ChannelId).NotEmpty().WithMessage("ChannelId cannot be empty!");
        RuleFor(u => u.StreamId).MinimumLength(6).WithMessage("StreamId length must be higher than 6 characters!");
    }
}