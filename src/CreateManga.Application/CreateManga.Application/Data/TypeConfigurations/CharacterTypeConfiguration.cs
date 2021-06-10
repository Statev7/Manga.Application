namespace CreateManga.Application.Data.TypeConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using CreateManga.Application.Data.Models;

    public class CharacterTypeConfiguration : IEntityTypeConfiguration<Character>
    {
        public void Configure(EntityTypeBuilder<Character> builder)
        {
            builder
                .ToTable("Characters")
                .HasOne(character => character.Manga)
                .WithMany(manga => manga.Characters)
                .HasForeignKey(character => character.MangaId);
        }
    }
}
