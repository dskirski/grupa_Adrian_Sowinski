using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace EbookShop.Models
{
    
    public class Order
    {
        public int OrderId { get; set; }

        public DateTime OrderDate { get; set; }


        public int EbookId { get; set; }
        public Ebook Ebook { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

    }
}
