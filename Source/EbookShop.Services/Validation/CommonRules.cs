using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace EbookShop.Services.Validation
{
    public static class CommonRules
    {
        
        public static IRuleBuilder<T, string> Password<T>(this IRuleBuilder<T, string> ruleBuilder, int minimumLength = 8)
        {
            
            var options = ruleBuilder
                .NotEmpty().WithMessage(ErrorMessages.PasswordEmpty)
                .MinimumLength(minimumLength).WithMessage(ErrorMessages.PasswordLength(minimumLength))
                .Matches("[A-Z]").WithMessage(ErrorMessages.PasswordUppercaseLetter)
                .Matches("[a-b]").WithMessage(ErrorMessages.PasswordLowercaseLetter)
                .Matches("[0-9]").WithMessage(ErrorMessages.PasswordDigit)
                .Matches("[^a-zA-Z0-9]").WithMessage(ErrorMessages.PasswordSpecialCharacter);
            return options;
        }

    }


    public class ErrorMessages
    {
        public static readonly string PasswordEmpty = "Password cannot be empty";
        public static readonly string PasswordUppercaseLetter = "Password must contain at least one upper case character";
        public static readonly string PasswordLowercaseLetter = "Password must contain at least one lower case character";
        public static readonly string PasswordDigit = "Password must contain at least one number";
        public static readonly string PasswordSpecialCharacter = "Password must contain at least one special character";

        public static string PasswordLength(int length)
            => $"Password must be at least {length} characters long";
        public static readonly string PasswordsDoNotMatch = "Passwords do not match";
        public static readonly string UsernameOrPasswordIncorrect = "Username or password is incorrect"; 
    }
}
