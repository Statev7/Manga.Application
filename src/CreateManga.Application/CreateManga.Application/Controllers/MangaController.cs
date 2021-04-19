namespace CreateManga.Application.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using CreateManga.Application.Data;
    using CreateManga.Application.Data.Models;
    using CreateManga.Application.Models;

    public class MangaController : Controller
    {
        private ApplicationDbContext dbContext;

        public MangaController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Index()
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

            MangasViewModel mangasViewModel = new MangasViewModel();

            mangasViewModel.Mangas = mangas;
            
            return this.View(mangasViewModel);
        }

        public IActionResult Details(int id)
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

            bool isMangaNull = manga == null;
            if (isMangaNull)
            {
                return this.RedirectToRoute("index");
            }

            return this.View(manga);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(CreateMangaBindingModel model)
        {
            if (ModelState.IsValid == false)
            {
                return this.View("create", model);
            }

            Manga mangaFromDb = this.dbContext.Mangas
                .Where(manga => manga.Name == model.Name)
                .SingleOrDefault();
                

            bool isMangaAlreadyInDb = mangaFromDb != null;
            if (isMangaAlreadyInDb)
            {
                return this.RedirectToAction("index");
            }

            Manga manga = new Manga();

            manga.Name = model.Name;
            manga.StartDate = model.StartDate;
            manga.EndDate = model.EndDate;
            manga.Description = model.Description;

            this.dbContext.Mangas.Add(manga);
            this.dbContext.SaveChanges();

            return this.RedirectToAction("index");
        }

        [HttpGet]
        public IActionResult Update(int id)
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

            bool isMangaNull = manga == null;
            if (isMangaNull)
            {
                return this.RedirectToAction("index");
            }

            return this.View(manga);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Update(UpdateMangaBiningModel model)
        {
            Manga manga = this.dbContext.Mangas
                .Find(model.Id);

            bool isMangaNull = manga == null;
            if (isMangaNull)
            {
                return this.View(model);
            }

            manga.Name = model.Name;
            manga.StartDate = model.StartDate;
            manga.EndDate = model.EndDate;
            manga.Description = model.Description;

            this.dbContext.Mangas.Update(manga);
            this.dbContext.SaveChanges();

            return this.RedirectToAction("index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Manga manga = new Manga();
            manga = this.dbContext.Mangas
                   .Find(id);

            bool isMangaNull = manga == null;
            if (isMangaNull)
            {
                return this.RedirectToAction("index");
            }

            this.dbContext.Mangas.Remove(manga);
            this.dbContext.SaveChanges();

            return this.RedirectToAction("index");
        }
    }
}
