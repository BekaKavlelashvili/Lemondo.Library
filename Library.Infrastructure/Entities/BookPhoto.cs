namespace Library.Infrastructure.Entities
{
    public class BookPhoto
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public string? ImageName { get; set; }

        public string? ImagePath { get; set; }
    }
}