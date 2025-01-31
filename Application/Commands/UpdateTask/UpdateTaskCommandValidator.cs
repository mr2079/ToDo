using Application.Models;
using FluentValidation;

namespace Application.Commands.UpdateTask;

public class UpdateTaskCommandValidator : AbstractValidator<UpdateTaskCommand>
{
    public UpdateTaskCommandValidator()
    {
        RuleFor(t => t.Id)
            .NotNull().WithMessage(ValidatorMessages.NotNull("شناسه وظیفه"))
            .NotEmpty().WithMessage(ValidatorMessages.NotEmpty("شناسه وظیفه"));

        RuleFor(t => t.Title)
            .NotNull().WithMessage(ValidatorMessages.NotNull("عنوان"))
            .NotEmpty().WithMessage(ValidatorMessages.NotEmpty("عنوان"))
            .MaximumLength(150).WithMessage(ValidatorMessages.MaxLength("عنوان", 150));

        RuleFor(t => t.Description)
            .NotNull().WithMessage(ValidatorMessages.NotNull("توضیحات"))
            .NotEmpty().WithMessage(ValidatorMessages.NotEmpty("توضیحات"))
            .MaximumLength(500).WithMessage(ValidatorMessages.MaxLength("توضیحات", 500));

        RuleFor(t => t.IsCompleted)
            .NotNull().WithMessage(ValidatorMessages.NotNull("تکمیل شده"));

        RuleFor(t => t.DueDate)
            .NotNull().WithMessage(ValidatorMessages.NotNull("تاریخ مقرر"))
            .NotEmpty().WithMessage(ValidatorMessages.NotEmpty("تاریخ مقرر"));
    }
}
