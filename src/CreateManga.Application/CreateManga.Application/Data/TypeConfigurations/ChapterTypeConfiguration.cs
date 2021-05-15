namespace CreateManga.Application.Data.TypeConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using CreateManga.Application.Data.Models;

    public class ChapterTypeConfiguration : IEntityTypeConfiguration<Chapter>
    {
        public void Configure(EntityTypeBuilder<Chapter> builder)
        {
            builder
                .ToTable("Chapters")
                .HasOne(chapter => chapter.Manga)
                .WithMany(manga => manga.Chapters)
                .HasForeignKey(chapter => chapter.MangaId);
        }
    }
}
