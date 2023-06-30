using Library.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Configurations
{
    public class TakenBookConfiguration : IEntityTypeConfiguration<TakenBooks>
    {
        public void Configure(EntityTypeBuilder<TakenBooks> builder)
        {
            builder.ToTable("TakenBooks");
        }
    }
}
