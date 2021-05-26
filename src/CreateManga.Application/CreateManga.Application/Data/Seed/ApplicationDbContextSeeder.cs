namespace CreateManga.Application.Data.Seeders
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.Extensions.DependencyInjection;

    using CreateManga.Application.Data.Seed.Seeders;
    using CreateManga.Application.Data.Seed.Seeders.Interfaces;

    public class ApplicationDbContextSeeder
    {
        private readonly IServiceProvider serviceProvider;

        public ApplicationDbContextSeeder(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public async Task SeedDatabaseAsync()
        {
            var dbContext = this.serviceProvider.GetRequiredService<ApplicationDbContext>();

            List<ISeeder> seeders = new List<ISeeder>()
            {
                new RolesSeeder(),
                new UsersSeeder(),
            };

            foreach (var seeder in seeders)
            {
                await seeder.SeedAsync(dbContext, serviceProvider);
            }
        }
    }
}
