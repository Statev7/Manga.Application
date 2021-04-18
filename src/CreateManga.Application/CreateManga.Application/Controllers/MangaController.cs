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
            List<MangaViewModel> mangasNames = dbContext.Mangas
                .Select(manga => new MangaViewModel
                {
                    MangaName = manga.MangaName,
                })
                .ToList();

            MangasViewModel mangasViewModel = new MangasViewModel();

            mangasViewModel.Mangas = mangasNames;

            return View(mangasViewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
           
            return View();
        }

        [HttpPost]
        public IActionResult Create(MangaBindingModel model)
        {
            Manga manga = new Manga();

            if (ModelState.IsValid == false)
            {
                return View("create", model);
            }

            manga.MangaName = model.Name;

            dbContext.Mangas.Add(manga);
            dbContext.SaveChanges();

            return RedirectToAction("index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Manga manga = new Manga();

            bool isIdLessOrEqualToZero = id <= 0;
            bool isMangaAreNull = manga == null;

            if (isIdLessOrEqualToZero || isMangaAreNull)
            {
                return RedirectToAction("index");
            }

            manga = dbContext.Mangas
                   .Find(id);

            dbContext.Mangas.Remove(manga);
            dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            Manga manga = dbContext.Mangas
                .Where(manga => manga.MangaId == id)
                .SingleOrDefault();

            return RedirectToAction("index");
        }
    }
}
