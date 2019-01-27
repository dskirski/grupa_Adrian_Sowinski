using System;
using System.Collections.Generic;
using System.Text;

namespace EbookShop.Services.Dtos
{
   public  class EbookDto
    {
        public EbookDto()
        {
            Authors = new List<AuthorDto>();
            Genres = new List<GenreDto>(); 
        }
        public int EbookId { get; set; }

        public string Title { get; set; }

        public string ISBN { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public ICollection<AuthorDto> Authors { get; set; }
        public ICollection<GenreDto> Genres { get; set; }
    }
}
