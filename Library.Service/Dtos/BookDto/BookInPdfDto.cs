namespace Library.Application.Dtos.BookDto
{
    public class BookInPdfDto
    {
        public string? MimeType { get; set; }

        public string? OriginalFileName { get; set; }

        public string? UniqueFileName { get; set; }

        public string? Path { get; set; }
    }
}