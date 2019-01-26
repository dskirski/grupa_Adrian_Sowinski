using System;
using System.Collections.Generic;
using System.Text;

namespace EbookShop.Services.Dtos
{
   public  class EbookDto
    {
        public int EbookId { get; set; }

        public string Title { get; set; }

        public string ISBN { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
    }
}
