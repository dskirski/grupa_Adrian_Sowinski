using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.DataModels
{
    public class Customer
    {


    

        [Key]
        public int CustomerId { get; set; }
        [StringLength(100), Required]
        public string FirstName { get; set; }
        [StringLength(100), Required]
        public string LastName { get; set; }

        [NotMapped]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        public string FB_Token { get; set; }


        public virtual ICollection<Order> Orders { get; set; }


    }
}
