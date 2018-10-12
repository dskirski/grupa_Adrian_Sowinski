using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.DataModels
{
    public class User
    {


        public int UserId { get; set; }

        [Key]
        public int Id { get; set; }
        [StringLength(100), Required]
        public string FirstName { get; set; }
        [StringLength(100), Required]
        public string LastName { get; set; }

        [NotMapped]
        public virtual string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        public string FB_Token { get; set; }

        

        
    }
}
