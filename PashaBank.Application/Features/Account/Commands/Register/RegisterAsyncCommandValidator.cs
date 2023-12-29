using FluentValidation;

namespace PashaBank.Application.Features.Account.Commands.Register;
public class RegisterAsyncCommandValidator : AbstractValidator<RegisterAsyncCommand>
{
    public RegisterAsyncCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotNull()
            .MaximumLength(50)
            .WithMessage("სახელი უნდა შედგემოდეს მაქსიმუმ 50 სიმბოლოსგან");

        RuleFor(x => x.Surname)
            .NotNull()
            .MaximumLength(50)
            .WithMessage("გვარი უნდა შედგებოდეს მაქსიმუმ 50 სიმბოლოსგან");

        RuleFor(x => x.DocumentNumber)
            .NotNull()
            .MaximumLength(10)
            .WithMessage("დოკუმენტის ნომერი უნდა შედგებოდეს მაქსიმუმ 10 სიმბოლოსგან");

        RuleFor(x => x.DocumentSeries)
            .NotNull()
            .MaximumLength(10)
            .WithMessage("დოკუმენტის სერია უნდა შედგებოდეს მაქსიმუმ 10 სიმბოლოსგან");

        RuleFor(x => x.PersonalNumber)
            .NotNull()
            .Must(value => value != null && value.ToString().Length <= 50)
            .WithMessage("პირადი ნომერი უნდა შედგებოდეს მაქსიმუმ 50 სიმბოლოსგან");

        RuleFor(x => x.IssuingAgency)
            .NotNull()
            .MaximumLength(100)
            .WithMessage("გამცემი ორგანოს ველი უნდა შედგებოდეს მაქსიმუმ 100 სიმბოლოსგან");

        RuleFor(x => x.ContactInformation)
            .NotEmpty()
            .MaximumLength(100)
            .WithMessage("საკონტაქტო ინფორმაცია უნდა შედგებოდეს მაქსიმუმ 100 სიმბოლოსგან");

        RuleFor(x => x.Address)
            .NotNull()
            .MaximumLength(100)
            .WithMessage("მისამართი უნდა შედგებოდეს მაქსიმუმ 100 სიმბოლოსგან");
    }
}