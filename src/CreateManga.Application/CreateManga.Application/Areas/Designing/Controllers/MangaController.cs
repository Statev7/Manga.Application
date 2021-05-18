namespace CreateManga.Application.Area.Designing.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using CreateManga.Application.Data.Models;
    using CreateManga.Application.Areas.Designing.Mangas.ViewModels;
    using CreateManga.Application.Areas.Designing.Mangas.BindingModels;
    using CreateManga.Application.Services.Interfaces;
    using CreateManga.Application.Areas.Designing.Controllers;
    using CreateManga.Application.Constants.RolesConstants;

    public class MangaController : DesigningController
    {
        private readonly IMangasService mangasService;

        public MangaController(IMangasService mangasService)
        {
            this.mangasService = mangasService;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<MangaViewModel> mangas = this.mangasService.GetAll();

            MangasViewModel mangasViewModel = new MangasViewModel();
            mangasViewModel.Mangas = mangas;
            
            return this.View(mangasViewModel);
        }

        [Authorize]
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

        [Authorize(Roles = RolesConstants.ADMIN_OR_AUTHOR)]
        [HttpGet]
        public IActionResult Create()
        {
            return this.View();
        }

        [Authorize(Roles = RolesConstants.ADMIN_OR_AUTHOR)]
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

        [Authorize(Roles = RolesConstants.ADMIN_OR_AUTHOR_OR_EDITOR)]
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

        [Authorize(Roles = RolesConstants.ADMIN_OR_AUTHOR_OR_EDITOR)]
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

        [Authorize(Roles = RolesConstants.ADMIN_OR_AUTHOR)]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await this.mangasService.DeleteAsync(id);

            return this.RedirectToAction("index");
        }
    }
}
