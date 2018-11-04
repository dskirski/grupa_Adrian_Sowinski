using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace EbookShop.Services.Helpers
{
   public static class Errors
    {
       public static string ErrorMessage(this IdentityResult errors)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var error in errors.Errors)
            {
                sb.AppendLine(error.Description);
            }
            return sb.ToString();
        }
    }
}
