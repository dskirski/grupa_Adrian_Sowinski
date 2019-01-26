using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace EbookShop.Models
{
    public class Author
    {

        [Key]
        public int Id { get; set; }
        [StringLength(100), Required]
        public string FirstName { get; set; }
        [StringLength(100), Required]
        public string LastName { get; set; }


        public virtual ICollection<EbookAuthor> Ebooks { get; set; }

    }
}
