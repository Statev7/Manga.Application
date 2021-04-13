namespace CreateManga.Application.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using CreateManga.Application.Data;
    using CreateManga.Application.Data.Models;

    public class MangaController : Controller
    {
        private ApplicationDbContext dbContext;

        public MangaController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            Manga manga = new Manga();

            manga.MangaName = "One Piece";

            dbContext.Mangas.Add(manga);

            dbContext.SaveChanges();

            return RedirectToAction("index");
        }
    }
}
