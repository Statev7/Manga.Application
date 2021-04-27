﻿using Microsoft.EntityFrameworkCore;
using CreateManga.Application.Models.Characters.ViewModels;
namespace CreateManga.Application.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    using CreateManga.Application.Data.Models;

    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Manga> Mangas { get; set; }

        public DbSet<Character> Characters { get; set; }

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
        }

        public DbSet<CreateManga.Application.Models.Characters.ViewModels.DetailsCharactersViewModel> CharactersViewModel { get; set; }
    }
}
