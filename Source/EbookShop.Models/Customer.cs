using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EbookShop.Models
{
    public class Customer
    {

        [Key]
        public int CustomerId { get; set; }


        public string IdentityId { get; set; }
        public AppUser Identity { get; set; }  

        public virtual ICollection<Order> Orders { get; set; }


    }
}
