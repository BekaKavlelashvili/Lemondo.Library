namespace Library.Infrastructure.Entities
{
    public class Author
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public Guid EntityId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Surname { get; set; } = string.Empty;

        public string BirthYear { get; set; } = string.Empty;
    }
}