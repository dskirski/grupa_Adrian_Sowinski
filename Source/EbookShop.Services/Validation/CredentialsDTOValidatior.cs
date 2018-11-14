using System;
using System.Collections.Generic;
using System.Text;
using EbookShop.Services.Dtos;
using FluentValidation;

namespace EbookShop.Services.Validation
{
    public class CredentialsDTOValidatior : AbstractValidator<CredentialsDTO>
    {
        public CredentialsDTOValidatior()
        {
            RuleFor(p => p.Password).Password();
            RuleFor(p => p.UserName).NotEmpty(); 
        }
    }
}
