namespace CreateManga.Application.Services
{
    using System;
    using System.IO;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Hosting;

    using CreateManga.Application.Data;
    using CreateManga.Application.Data.Models;
    using CreateManga.Application.Areas.Designing.Mangas.BindingModels;
    using CreateManga.Application.Areas.Designing.Mangas.ViewModels;
    using CreateManga.Application.Services.Interfaces;


    public class MangasService : IMangasService
    {
        private const string IMAGE_FOLDER_NAME = "/ImageFromUsers/";

        private readonly ApplicationDbContext dbContext;
        private readonly IWebHostEnvironment hostEnvironment;
        private readonly IMangasUsersService mangasUsersService;

        public MangasService(
            ApplicationDbContext dbContext, 
            IWebHostEnvironment hostEnvironment, 
            IMangasUsersService mangasUsersService 
            )
        {
            this.dbContext = dbContext;
            this.hostEnvironment = hostEnvironment;
            this.mangasUsersService = mangasUsersService;
        }

        public IEnumerable<MangaViewModel> GetAll(string id)
        {
            IEnumerable<MangaViewModel> mangas = this.dbContext.Manga
               .Select(manga => new MangaViewModel
               {
                   Id = manga.Id,
                   MangaName = manga.Name,
                   StartDate = manga.StartDate,
                   EndDate = manga.EndDate,
                   Description = manga.Description,
                   ImageName = manga.ImageName,
                   CurrentUserIsVoted = this.mangasUsersService.IsAlreadyVoted(id, manga.Id)
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
                    ImageName = m.ImageName,
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
            Manga manga = new Manga();

            manga.Name = model.Name;
            manga.StartDate = model.StartDate;
            manga.EndDate = model.EndDate;
            manga.Description = model.Description;
            manga.ImageName = model.ImageName;
            manga.ImageFile = model.ImageFile;

            if (model.ImageFile != null)
            {
                await SetImage(manga);
            }

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
                    ImageName = m.ImageName,
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
            manga.ImageFile = model.ImageFile;


            if (manga.ImageName != null && model.ImageName == null)
            {
                model.ImageName = manga.ImageName;
            }

            if (model.ImageFile != null)
            {
                await SetImage(manga);
            }

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

        public bool CheckIfMangaExist(int id)
        {
            Manga manga = this.dbContext.Manga
                .Where(m => m.Id == id)
                .SingleOrDefault();

            bool isExist =  manga != null;

            return isExist;
        }

        private async Task SetImage(Manga manga)
        {
            string wwwRootPath = hostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(manga.ImageFile.FileName);
            string exension = Path.GetExtension(manga.ImageFile.FileName);
            manga.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + exension;
            string path = Path.Combine(wwwRootPath + IMAGE_FOLDER_NAME, fileName);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await manga.ImageFile.CopyToAsync(fileStream);
            }
        }
    }
}
