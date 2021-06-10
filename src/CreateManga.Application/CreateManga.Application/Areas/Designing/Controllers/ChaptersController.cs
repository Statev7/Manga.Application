namespace CreateManga.Application.Area.Designing.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using CreateManga.Application.Areas.Designing.Chapters.ViewModels;
    using CreateManga.Application.Services.Interfaces;
    using CreateManga.Application.Areas.Designing.Mangas.ViewModels;
    using CreateManga.Application.Areas.Designing.Chapters.BindingModels;
    using CreateManga.Application.Areas.Designing.Controllers;
    using CreateManga.Application.Constants.RolesConstants;
    using CreateManga.Application.Data.Models;
    using CreateManga.Application.Constants.NotificationsConstants;

    public class ChaptersController : DesigningController
    {
        private readonly IChaptersService chapterService;

        public ChaptersController(IChaptersService chapterService)
        {
            this.chapterService = chapterService;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<GetAllChaptersViewModel> chapter = this.chapterService.GetAll();

            return this.View(chapter);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Details(int id)
        {
            DetailsChaptersViewModel chapter = this.chapterService.GetDetailsById(id);

            bool isCharacterNull = chapter == null;
            if (isCharacterNull)
            {
                this.TempData[NotificationsConstants.ERROR_NOTIFICATION] = NotificationsConstants.CANNOT_GET_A_DETAILS_ABOUT_EMPTY_CHAPTER;
                return this.RedirectToAction("index");
            }

            return this.View(chapter);
        }

        [Authorize(Roles = RolesConstants.ADMIN_OR_AUTHOR)]
        [HttpGet]
        public IActionResult Create()
        {
            IEnumerable<MangasIdNameViewModel> mangas = this.chapterService.GetByName();

            bool areMangasEmpty = mangas.Count() == 0;
            if (areMangasEmpty)
            {
                this.TempData[NotificationsConstants.ERROR_NOTIFICATION] = NotificationsConstants.CANNOT_CRAETE_CHAPTER_WITHOUT_MANGA;
                return this.RedirectToAction("index");
            }

            ViewBag.mangas = mangas;

            return this.View();
        }

        [Authorize(Roles = RolesConstants.ADMIN_OR_AUTHOR)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateChaptersBindingModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.RedirectToAction("create");
            }

            Chapter chapterFromDb = this.chapterService.GetByModelName(model.Title);

            bool isMangaAlreadyInDb = chapterFromDb != null;
            if (isMangaAlreadyInDb)
            {
                this.TempData[NotificationsConstants.WARNING_NOTIFICATION] = NotificationsConstants.CHAPTER_ALREADY_EXIST;
                return this.RedirectToAction("index");
            }

            await this.chapterService.CreateAsync(model);
            this.TempData[NotificationsConstants.SUCCESS_NOTIFICATION] = 
                string.Format(NotificationsConstants.SUCCESSFUL_CREATED_A_CHAPTER, model.Title);

            return this.RedirectToAction("index");
        }

        [Authorize(Roles = RolesConstants.ADMIN_OR_AUTHOR_OR_EDITOR)]
        [HttpGet]
        public IActionResult Update(int id)
        {
            UpdateChaptersBindingModel chapter = this.chapterService.UpdateById(id);

            IEnumerable<MangasIdNameViewModel> mangas = this.chapterService.GetByName();

            bool isCharacterNull = chapter == null;
            bool areMangasEmpty = mangas.Count() == 0;
            if (isCharacterNull || areMangasEmpty)
            {
                this.TempData[NotificationsConstants.ERROR_NOTIFICATION] = NotificationsConstants.CANNOT_UPDATE_EMPTY_CHAPTER;
                return this.RedirectToAction("index");
            }

            ViewBag.mangas = mangas;

            return this.View(chapter);
        }

        [Authorize(Roles = RolesConstants.ADMIN_OR_AUTHOR_OR_EDITOR)]
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(UpdateChaptersBindingModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.View(model);
            }

            await this.chapterService.UpdateAsync(model);
            this.TempData[NotificationsConstants.SUCCESS_NOTIFICATION] =
                string.Format(NotificationsConstants.SUCCESSFUL_UPDATE_A_CHAPTER, model.Title);

            return this.RedirectToAction("index");
        }

        [Authorize(Roles = RolesConstants.ADMIN_OR_AUTHOR)]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await this.chapterService.DeleteAsync(id);

            return this.RedirectToAction("index");
        }
    }
}
