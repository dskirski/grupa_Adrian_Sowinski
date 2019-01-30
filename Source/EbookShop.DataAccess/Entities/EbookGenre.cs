using System;
using System.Collections.Generic;
using System.Text;

namespace EbookShop.Models
{
    public class EbookGenre
    {
        public int EbookId { get; set; }
        public Ebook Ebook { get; set; }

        public int GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}
