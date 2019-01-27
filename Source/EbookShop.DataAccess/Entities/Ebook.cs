using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace EbookShop.Models
{
    public class Ebook
    {

        [Key]
        public int EbookId { get; set; }

        [StringLength(255), Required]
        public string Title { get; set; }

        [StringLength(13), Required]
        public string ISBN { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }



        #region navigation properties

        public virtual ICollection<FilePath> Files { get; set; }
   
        public virtual ICollection<EbookAuthor> EbookAuthors { get; set; }
        public virtual ICollection<EbookGenre> EbookGenres { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        #endregion
    }
}
