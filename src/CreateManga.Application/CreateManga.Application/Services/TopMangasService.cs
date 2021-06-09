namespace CreateManga.Application.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CreateManga.Application.Areas.Ranking.Models.TopMangas.ViewModels;
    using CreateManga.Application.Data;
    using CreateManga.Application.Services.Interfaces;

    public class TopMangasService : ITopMangasService
    {
        private readonly ApplicationDbContext dbContext;

        public TopMangasService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<TopMangaViewModel> GetAll()
        {

            IEnumerable<TopMangaViewModel> mangas = this.dbContext.Manga
               .Select(manga => new TopMangaViewModel
               {
                   Id = manga.Id,
                   MangaName = manga.Name,
                   StartDate = manga.StartDate,
                   EndDate = manga.EndDate,
                   Description = manga.Description,
                   ImageName = manga.ImageName,
                   Votes = manga.Votes,
               })
               .OrderByDescending(m => m.Votes)
               .Take(5)
               .ToList();

            return mangas;
        }
    }
}
