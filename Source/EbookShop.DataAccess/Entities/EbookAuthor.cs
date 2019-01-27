using System;
using System.Collections.Generic;
using System.Text;

namespace EbookShop.Models
{
    
   public class EbookAuthor
    {
        public int AuthorId { get; set; }
        public Author Author { get; set; }

        public int EbookId { get; set; }
        public Ebook Ebook { get; set; }
    }
}
