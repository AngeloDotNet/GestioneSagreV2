using GestioneSagre.Utility.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestioneSagre.Utility.Infrastructure.DataAccess.Mapper;

public class EmailMessageMapper : IEntityTypeConfiguration<EmailMessage>
{
    public void Configure(EntityTypeBuilder<EmailMessage> entity)
    {
        entity.ToTable("EmailMessages");
        entity.HasKey(e => e.Id);
        entity.Property(x => x.Status).HasConversion<string>();
    }
}