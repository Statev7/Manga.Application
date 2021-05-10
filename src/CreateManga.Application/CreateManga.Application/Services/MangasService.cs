namespace CreateManga.Application.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CreateManga.Application.Data;
    using CreateManga.Application.Data.Models;
    using CreateManga.Application.Areas.Designing.Mangas.BindingModels;
    using CreateManga.Application.Areas.Designing.Mangas.ViewModels;
    using CreateManga.Application.Services.Interfaces;

    public class MangasService : IMangasService
    {
        private readonly ApplicationDbContext dbContext;

        public MangasService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<MangaViewModel> GetAll()
        {
            List<MangaViewModel> mangas = this.dbContext.Manga
               .Select(manga => new MangaViewModel
               {
                   Id = manga.Id,
                   MangaName = manga.Name,
                   StartDate = manga.StartDate,
                   EndDate = manga.EndDate,
                   Description = manga.Description,
               })
               .ToList();

            return mangas;
        }

        public MangaViewModel GetDetailsById(int id)
        {
            MangaViewModel manga = this.dbContext.Manga
                .Select(m => new MangaViewModel
                {
                    Id = m.Id,
                    MangaName = m.Name,
                    StartDate = m.StartDate,
                    EndDate = m.EndDate,
                    Description = m.Description,
                })
                .SingleOrDefault(m => m.Id == id);

            return manga;
        }

        public Manga GetByModelName(string modelName)
        {
            Manga mangaFromDb = this.dbContext.Manga
                .SingleOrDefault(manga => manga.Name == modelName);

            return mangaFromDb;
        }

        public async Task CreateAsync(CreateMangaBindingModel model)
        {
            //TODO Notification 

            if (model.StartDate > model.EndDate)
            {
                return;
            }

            Manga manga = new Manga();

            manga.Name = model.Name;
            manga.StartDate = model.StartDate;
            manga.EndDate = model.EndDate;
            manga.Description = model.Description;

            await this.dbContext.Manga.AddAsync(manga);
            await this.dbContext.SaveChangesAsync();
        }

        public UpdateMangaBiningModel UpdateById(int id)
        {
            UpdateMangaBiningModel manga = this.dbContext.Manga
                .Select(m => new UpdateMangaBiningModel
                {
                    Id = m.Id,
                    Name = m.Name,
                    StartDate = m.StartDate,
                    EndDate = m.EndDate,
                    Description = m.Description,
                })
                .SingleOrDefault(m => m.Id == id);

            return manga;
        }

        public async Task UpdateAsync(UpdateMangaBiningModel model)
        {
            Manga manga = this.dbContext.Manga
                .Find(model.Id);

            bool isMangaNull = manga == null;
            if (isMangaNull)
            {
                return;
            }

            manga.Name = model.Name;
            manga.StartDate = model.StartDate;
            manga.EndDate = model.EndDate;
            manga.Description = model.Description;

            this.dbContext.Manga.Update(manga);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Manga manga = new Manga();
            manga = this.dbContext.Manga
                   .Find(id);

            bool isMangaNull = manga == null;
            if (isMangaNull)
            {
                return;
            }

            this.dbContext.Manga.Remove(manga);
            await this.dbContext.SaveChangesAsync();
        }
    }
}
