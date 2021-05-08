using Microsoft.EntityFrameworkCore;
using CreateManga.Application.Models.Chapters.ViewModels;
using CreateManga.Application.Models.Chapters.BindingModels;
namespace CreateManga.Application.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    using CreateManga.Application.Data.Models;

    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Manga> Manga { get; set; }

        public DbSet<Character> Character { get; set; }

        public DbSet<Chapter> Chapter { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            bool isConfigured = optionsBuilder.IsConfigured;

            if (!isConfigured)
            {
                optionsBuilder
                .UseSqlServer("Server=.;Database=CreateManga;Trusted_Connection=True;MultipleActiveResultSets=true");
            } 
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder
                .Entity<Manga>()
                .HasIndex(m => m.Name)
                .IsUnique();

            builder
                .Entity<Character>()
                .HasOne(character => character.Manga)
                .WithMany(manga => manga.Characters)
                .HasForeignKey(character => character.MangaId);

            builder
                .Entity<Chapter>()
                .HasOne(chapter => chapter.Manga)
                .WithMany(manga => manga.Chapters)
                .HasForeignKey(chapter => chapter.MangaId);

        }

        public DbSet<CreateManga.Application.Models.Chapters.ViewModels.DetailsChaptersViewModel> DetailsChaptersViewModel { get; set; }

        public DbSet<CreateManga.Application.Models.Chapters.BindingModels.UpdateChaptersBindingModel> UpdateChaptersBindingModel { get; set; }
    }
}
