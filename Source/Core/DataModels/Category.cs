using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace Core.DataModels
{
    public class Category
    {
       
        [Key]
        public int CategoryId { get; set; }
        [Required]
        [StringLength(30)]
        public string CategoryName { get; set; }
     
        public string Description { get; set; }


        public virtual ICollection<EbookCategories> EbookCategories  { get; set; }

    }


}
