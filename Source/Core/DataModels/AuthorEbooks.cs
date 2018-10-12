using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DataModels
{
   
   public class AuthorEbooks
    {
        public int AuthorId { get; set; }
        public Author Author { get; set; }

        public int EbookId { get; set; }
        public Ebook Ebook { get; set; }
    }
}
