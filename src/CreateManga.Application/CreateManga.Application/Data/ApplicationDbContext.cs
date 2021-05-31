namespace CreateManga.Application.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    using CreateManga.Application.Data.Models;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Manga> Manga { get; set; }

        public DbSet<Character> Character { get; set; }

        public DbSet<Chapter> Chapter { get; set; }

        public DbSet<MangaUser> MangasUsers { get; set; }

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

            builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }

    }
}
