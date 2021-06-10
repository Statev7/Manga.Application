namespace CreateManga.Application.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.IO;

    using Microsoft.AspNetCore.Hosting;

    using CreateManga.Application.Data;
    using CreateManga.Application.Data.Models;
    using CreateManga.Application.Areas.Designing.Chapters.BindingModels;
    using CreateManga.Application.Areas.Designing.Chapters.ViewModels;
    using CreateManga.Application.Areas.Designing.Mangas.ViewModels;
    using CreateManga.Application.Services.Interfaces;

    public class ChaptersService : IChaptersService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IWebHostEnvironment hostEnvironment;

        public ChaptersService(ApplicationDbContext dbContext, IWebHostEnvironment hostEnvironment)
        {
            this.dbContext = dbContext;
            this.hostEnvironment = hostEnvironment;
        }

        public IEnumerable<GetAllChaptersViewModel> GetAll()
        {
            IEnumerable<GetAllChaptersViewModel> chapters = this.dbContext.Chapter
                .Select(chapters => new GetAllChaptersViewModel
                {
                    Id = chapters.Id,
                    Title = chapters.Title,
                    ImageName = chapters.ImageName,
                })
                .ToList();

            return chapters;
        }

        public DetailsChaptersViewModel GetDetailsById(int id)
        {
            DetailsChaptersViewModel chapter = this.dbContext.Chapter
                .Select(chapter => new DetailsChaptersViewModel
                {
                    Id = chapter.Id,
                    Title = chapter.Title,
                    Story = chapter.Story,
                })
                .SingleOrDefault(chapter => chapter.Id == id);

            return chapter;
        }

        public IEnumerable<MangasIdNameViewModel> GetByName()
        {
            IEnumerable<MangasIdNameViewModel> mangas = this.dbContext.Manga
                .Select(mangas => new MangasIdNameViewModel
                {
                    Id = mangas.Id,
                    Name = mangas.Name,
                })
                .OrderBy(mangas => mangas.Name)
                .ToList();

            return mangas;
        }

        public Chapter GetByModelName(string modelName)
        {
            Chapter mangaFromDb = this.dbContext.Chapter
                .SingleOrDefault(chapter => chapter.Title == modelName);

            return mangaFromDb;
        }

        public async Task CreateAsync(CreateChaptersBindingModel model)
        {
            Chapter chapter = new Chapter();
            chapter.Title = model.Title;
            chapter.Story = model.Story;
            chapter.ImageFile = model.ImageFile;
            chapter.ImageName = model.ImageName;
            chapter.MangaId = model.MangaId;

            if (model.ImageFile != null)
            {
                string wwwRootPath = hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(chapter.ImageFile.FileName);
                string exension = Path.GetExtension(chapter.ImageFile.FileName);
                chapter.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + exension;
                string path = Path.Combine(wwwRootPath + "/Img/ChaptersImage/", fileName);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await chapter.ImageFile.CopyToAsync(fileStream);
                }
            }

            await this.dbContext.Chapter.AddAsync(chapter);
            await this.dbContext.SaveChangesAsync();
        }

        public UpdateChaptersBindingModel UpdateById(int id)
        {
            UpdateChaptersBindingModel chapter = this.dbContext.Chapter
                .Select(chapter => new UpdateChaptersBindingModel
                {
                    Id = chapter.Id,
                    Title = chapter.Title,
                    Story = chapter.Story,
                    MangaId = chapter.MangaId,
                })
                .SingleOrDefault(chapter => chapter.Id == id);

            return chapter;
        }

        public async Task UpdateAsync(UpdateChaptersBindingModel model)
        {
            Chapter chapter = this.dbContext.Chapter
                .Find(model.Id);

            bool isCharacterNull = chapter == null;
            if (isCharacterNull)
            {
                return;
            }

            chapter.Title = model.Title;
            chapter.Story = model.Story;
            chapter.ImageFile = model.ImageFile;
            chapter.MangaId = model.MangaId;

            if (chapter.ImageName != null && chapter.ImageName == null)
            {
                model.ImageName = chapter.ImageName;
            }

            if (model.ImageFile != null)
            {
                string wwwRootPath = hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(chapter.ImageFile.FileName);
                string exension = Path.GetExtension(chapter.ImageFile.FileName);
                chapter.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + exension;
                string path = Path.Combine(wwwRootPath + "/ImageFromUsers/", fileName);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await chapter.ImageFile.CopyToAsync(fileStream);
                }
            }

            this.dbContext.Chapter.Update(chapter);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Chapter chapter = new Chapter();
            chapter = this.dbContext.Chapter
                   .Find(id);

            bool isCharacterNull = chapter == null;
            if (isCharacterNull)
            {
                return;
            }

            this.dbContext.Chapter.Remove(chapter);
            await this.dbContext.SaveChangesAsync();
        }

    }
}
