namespace Library.Infrastructure.Entities
{
    public class BookInPdf
    {
        public int BookId { get; set; }

        public int Id { get; set; }

        public string? MimeType { get; set; }

        public string? OriginalFileName { get; set; }

        public string? UniqueFileName { get; set; }

        public string? Path { get; set; }
    }
}