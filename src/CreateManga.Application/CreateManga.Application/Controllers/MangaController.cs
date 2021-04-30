﻿namespace CreateManga.Application.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using CreateManga.Application.Data.Models;
    using CreateManga.Application.Models.Mangas.ViewModels;
    using CreateManga.Application.Models.Mangas.BindingModels;
    using CreateManga.Application.Services;

    public class MangaController : Controller
    {
        private readonly MangasService mangasService;

        public MangaController(MangasService mangasService)
        {
            this.mangasService = mangasService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<MangaViewModel> mangas = this.mangasService.GetAll();

            MangasViewModel mangasViewModel = new MangasViewModel();
            mangasViewModel.Mangas = mangas;
            
            return this.View(mangasViewModel);
        }

        public IActionResult Details(int id)
        {
            MangaViewModel manga = this.mangasService.GetDetailsById(id);

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
        public async Task<IActionResult> Create(CreateMangaBindingModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.View("create", model);
            }

            Manga mangaFromDb = this.mangasService.GetByModelName(model.Name);
                
            bool isMangaAlreadyInDb = mangaFromDb != null;
            if (isMangaAlreadyInDb)
            {
                return this.RedirectToAction("index");
            }

            await this.mangasService.CreateAsync(model);

            return this.RedirectToAction("index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            UpdateMangaBiningModel manga = this.mangasService.UpdateById(id);

            bool isMangaNull = manga == null;
            if (isMangaNull)
            {
                return this.RedirectToAction("index");
            }

            return this.View(manga);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(UpdateMangaBiningModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.View("create", model);
            }

            await this.mangasService.UpdateAsync(model);

            return this.RedirectToAction("index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await this.mangasService.DeleteAsync(id);

            return this.RedirectToAction("index");
        }
    }
}
