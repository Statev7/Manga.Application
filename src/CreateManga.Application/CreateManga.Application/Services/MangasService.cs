namespace CreateManga.Application.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CreateManga.Application.Data;
    using CreateManga.Application.Data.Models;
    using CreateManga.Application.Models.Mangas.BindingModels;
    using CreateManga.Application.Models.Mangas.ViewModels;
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
            List<MangaViewModel> mangas = this.dbContext.Mangas
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
            MangaViewModel manga = this.dbContext.Mangas
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
            Manga mangaFromDb = this.dbContext.Mangas
                .SingleOrDefault(manga => manga.Name == modelName);

            return mangaFromDb;
        }

        public async Task CreateAsync(CreateMangaBindingModel model)
        {
            Manga manga = new Manga();

            manga.Name = model.Name;
            manga.StartDate = model.StartDate;
            manga.EndDate = model.EndDate;
            manga.Description = model.Description;

            await this.dbContext.Mangas.AddAsync(manga);
            await this.dbContext.SaveChangesAsync();
        }

        public UpdateMangaBiningModel UpdateById(int id)
        {
            UpdateMangaBiningModel manga = this.dbContext.Mangas
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
            Manga manga = this.dbContext.Mangas
                .Find(model.Id);

            bool isMangaNull = manga == null;
            if (isMangaNull)
            {
                return ;
            }

            manga.Name = model.Name;
            manga.StartDate = model.StartDate;
            manga.EndDate = model.EndDate;
            manga.Description = model.Description;

            this.dbContext.Mangas.Update(manga);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Manga manga = new Manga();
            manga = this.dbContext.Mangas
                   .Find(id);

            bool isMangaNull = manga == null;
            if (isMangaNull)
            {
                return;
            }

            this.dbContext.Mangas.Remove(manga);
            await this.dbContext.SaveChangesAsync();
        }
    }
}
