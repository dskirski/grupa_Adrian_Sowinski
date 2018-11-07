using System;
using System.Collections.Generic;
using System.Text;
using EbookShop.Services.Dtos;
using FluentValidation;

namespace EbookShop.Services.Validation
{
    public class RegistrationDTOValidator : AbstractValidator<RegistrationDTO>
    {
        public RegistrationDTOValidator()
        {
            RuleFor(rdto => rdto.Email).Empty(); 
            RuleFor(rdto => rdto.Password).Empty();
            RuleFor(rdto => rdto.FirstName).Empty();
            RuleFor(rdto => rdto.LastName).Empty();
        }
    }
}
