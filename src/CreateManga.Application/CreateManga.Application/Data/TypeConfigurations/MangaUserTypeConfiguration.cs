namespace CreateManga.Application.Data.TypeConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using CreateManga.Application.Data.Models;

    public class MangaUserTypeConfiguration : IEntityTypeConfiguration<MangaUser>
    {
        public void Configure(EntityTypeBuilder<MangaUser> builder)
        {
            builder
                .HasOne(mu => mu.Manga)
                .WithMany(m => m.MangaUsers)
                .HasForeignKey(mu => mu.MangaId);

            builder
                .HasOne(mu => mu.User)
                .WithMany(u => u.UsersMangas)
                .HasForeignKey(mu => mu.UserId);
        }
    }
}
