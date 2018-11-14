namespace EbookShop.Models
{
    public class EbookCategories
    {
        public int EbookId { get; set; }
        public Ebook Ebook { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}