using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace Core.DataModels
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
   
        public virtual ICollection<AuthorEbooks> AuthorEbooks { get; set; }
      
        #endregion
    }
}
