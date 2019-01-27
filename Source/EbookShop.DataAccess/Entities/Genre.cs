using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace EbookShop.Models
{
    public class Genre
    {
        public Genre()
        {
          
        }
        [Key]
        public int GenreId { get; set; }
        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        public virtual ICollection<EbookGenre> EbookGenres { get; set; }
    }


}
