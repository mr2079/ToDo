using Application.Models;
using FluentValidation;

namespace Application.Commands.DeleteTask;

public class DeleteTaskCommandValidator : AbstractValidator<DeleteTaskCommand>
{
    public DeleteTaskCommandValidator()
    {
        RuleFor(t => t.Id)
            .NotNull().WithMessage(ValidatorMessages.NotNull("شناسه وظیفه"))
            .NotEmpty().WithMessage(ValidatorMessages.NotEmpty("شناسه وظیفه"));
    }
}
