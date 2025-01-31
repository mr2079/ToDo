using Application.Models;
using FluentValidation;

namespace Application.Commands.CreateTask;

public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
{
    public CreateTaskCommandValidator()
    {
        RuleFor(t => t.Title)
            .NotNull().WithMessage(ValidatorMessages.NotNull("عنوان"))
            .NotEmpty().WithMessage(ValidatorMessages.NotEmpty("عنوان"))
            .MaximumLength(150).WithMessage(ValidatorMessages.MaxLength("عنوان", 150));

        RuleFor(t => t.Description)
            .NotNull().WithMessage(ValidatorMessages.NotNull("توضیحات"))
            .NotEmpty().WithMessage(ValidatorMessages.NotEmpty("توضیحات"))
            .MaximumLength(500).WithMessage(ValidatorMessages.MaxLength("توضیحات", 500));

        RuleFor(t => t.DueDate)
            .NotNull().WithMessage(ValidatorMessages.NotNull("تاریخ مقرر"))
            .NotEmpty().WithMessage(ValidatorMessages.NotEmpty("تاریخ مقرر"))
            .Must(d => d > DateTime.Now).WithMessage(ValidatorMessages.Invalid("تاریخ مقرر"));
    }
}
