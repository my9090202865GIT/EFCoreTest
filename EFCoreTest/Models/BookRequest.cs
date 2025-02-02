namespace EFCoreTest.Models
{
    public class BookRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int NoOfPages { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public int LanguageId { get; set; }
        public int AuthorId { get; set; }
        public string Currency { get; set; }
        public int Amount { get; set; }
    }
}
