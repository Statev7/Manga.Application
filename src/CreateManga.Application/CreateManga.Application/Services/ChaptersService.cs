﻿namespace CreateManga.Application.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CreateManga.Application.Data;
    using CreateManga.Application.Data.Models;
    using CreateManga.Application.Models.Chapters.BindingModels;
    using CreateManga.Application.Models.Chapters.ViewModels;
    using CreateManga.Application.Models.Mangas.ViewModels;
    using CreateManga.Application.Services.Interfaces;

    public class ChaptersService : IChaptersService
    {
        private readonly ApplicationDbContext dbContext;

        public ChaptersService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<GetAllChaptersViewModel> GetAll()
        {
            IEnumerable<GetAllChaptersViewModel> chapters = this.dbContext.Chapter
                .Select(chapters => new GetAllChaptersViewModel
                {
                    Id = chapters.Id,
                    Title = chapters.Title,
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

        public async Task CreateAsync(CreateChaptersBindingModel model)
        {
            Chapter chapter = new Chapter();
            chapter.Title = model.Title;
            chapter.Story = model.Story;
            chapter.MangaId = model.MangaId;

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
            chapter.MangaId = model.MangaId;

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
