using System;
using EbookShop.Services.Dtos;
using FluentValidation;

namespace EbookShop.Services.Validation
{
    public class RegistrationDtoValidator : AbstractValidator<RegistrationDto>
    {
        public RegistrationDtoValidator()
        {
            RuleFor(rdto => rdto.Email).NotEmpty().EmailAddress();
            RuleFor(rdto => rdto.Password).NotEmpty().Password();
            RuleFor(rdto => rdto.ConfirmPassword)
                .NotEmpty()
                .Equal(rdto => rdto.Password)
                .WithMessage(ErrorMessages.PasswordsDoNotMatch);
            RuleFor(rdto => rdto.FirstName).NotEmpty().Length(1, 100);
            RuleFor(rdto => rdto.LastName).NotEmpty().Length(1,100);
        }
    }
}
