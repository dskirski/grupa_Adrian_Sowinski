using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace EbookShop.Models
{
   public class AppUser : IdentityUser
    {

        [StringLength(100), Required]
        public string FirstName { get; set; }
        [StringLength(100), Required]
        public string LastName { get; set; }
        public long? FacebookId { get; set; }

    }

}
