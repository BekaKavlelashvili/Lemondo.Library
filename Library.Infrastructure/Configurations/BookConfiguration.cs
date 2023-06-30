using Library.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Infrastructure.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Books");
            builder.OwnsMany(x => x.Authors, c =>
            {
                c.ToTable("Authors");
                c.HasKey(x => x.Id);
                c.HasIndex(x => x.BookId);
            });
            builder.OwnsOne(x => x.Image, c =>
            {
                c.ToTable("Images");
                c.HasKey(x => x.Id);
                c.HasIndex(x => x.BookId);
            });
        }
    }
}
