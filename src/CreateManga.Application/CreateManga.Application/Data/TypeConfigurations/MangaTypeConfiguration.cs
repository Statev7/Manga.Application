namespace CreateManga.Application.Data.TypeConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using CreateManga.Application.Data.Models;

    public class MangaTypeConfiguration : IEntityTypeConfiguration<Manga>
    {
        public void Configure(EntityTypeBuilder<Manga> builder)
        {
            builder
                .ToTable("Mangas")
                .HasIndex(m => m.Name)
                .IsUnique();
        }
    }
}
