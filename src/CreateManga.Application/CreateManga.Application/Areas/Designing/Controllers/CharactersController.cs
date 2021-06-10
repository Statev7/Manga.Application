namespace CreateManga.Application.Area.Designing.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using CreateManga.Application.Areas.Designing.Mangas.ViewModels;
    using CreateManga.Application.Areas.Designing.Characters.BindingModels;
    using CreateManga.Application.Areas.Designing.Characters.ViewModels;
    using CreateManga.Application.Services.Interfaces;
    using CreateManga.Application.Areas.Designing.Controllers;
    using CreateManga.Application.Constants.RolesConstants;
    using CreateManga.Application.Constants.NotificationsConstants;
    using CreateManga.Application.Data.Models;

    public class CharactersController : DesigningController
    {
        private readonly ICharactersService charactersService;

        public CharactersController(ICharactersService charactersService)
        {
            this.charactersService = charactersService;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<GetAllCharactersViewModel> characters = this.charactersService.GetAll();

            return this.View(characters);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Details(int id)
        {
            DetailsCharactersViewModel character = this.charactersService.GetDetailsById(id);

            bool isCharacterNull = character == null;
            if (isCharacterNull)
            {
                this.TempData[NotificationsConstants.ERROR_NOTIFICATION] = NotificationsConstants.CANNOT_GET_A_DETAILS_ABOUT_EMPTY_CHARACTER;
                return this.RedirectToAction("index");
            }

            return this.View(character);
        }

        [Authorize(Roles = RolesConstants.ADMIN_OR_AUTHOR)]
        [HttpGet]
        public IActionResult Create()
        {
            IEnumerable<MangasIdNameViewModel> mangas = this.charactersService.GetByName();

            bool areMangasEmpty = mangas.Count() == 0;
            if (areMangasEmpty)
            {
                this.TempData[NotificationsConstants.ERROR_NOTIFICATION] = NotificationsConstants.CANNOT_CRAETE_CHARACTER_WITHOUT_MANGA;
                return this.RedirectToAction("index");
            }

            ViewBag.mangas = mangas;

            return this.View();
        }

        [Authorize(Roles = RolesConstants.ADMIN_OR_AUTHOR)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCharactersBindingModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.RedirectToAction("create");
            }

            Character chapterFromDb = this.charactersService.GetByModelName(model.Name);

            bool isMangaAlreadyInDb = chapterFromDb != null;
            if (isMangaAlreadyInDb)
            {
                this.TempData[NotificationsConstants.WARNING_NOTIFICATION] = NotificationsConstants.CHARACTER_ALREADY_EXIST;
                return this.RedirectToAction("index");
            }

            await this.charactersService.CreateAsync(model);
            this.TempData[NotificationsConstants.SUCCESS_NOTIFICATION] = string.Format(NotificationsConstants.SUCCESSFUL_CREATED_A_CHARACTER, model.Name);

            return this.RedirectToAction("index");
        }

        [Authorize(Roles = RolesConstants.ADMIN_OR_AUTHOR_OR_EDITOR)]
        [HttpGet]
        public IActionResult Update(int id)
        {
            UpdateCharactersBindingModel character = this.charactersService.UpdateById(id);

            IEnumerable<MangasIdNameViewModel> mangas = this.charactersService.GetByName();

            bool isCharacterNull = character == null;
            bool areMangasEmpty = mangas.Count() == 0;
            if (isCharacterNull || areMangasEmpty)
            {
                this.TempData[NotificationsConstants.ERROR_NOTIFICATION] = NotificationsConstants.CANNOT_GET_A_DETAILS_ABOUT_EMPTY_CHARACTER;
                return this.RedirectToAction("index");
            }

            ViewBag.mangas = mangas;

            return this.View(character);
        }

        [Authorize(Roles = RolesConstants.ADMIN_OR_AUTHOR_OR_EDITOR)]
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(UpdateCharactersBindingModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.View(model);
            }

            await this.charactersService.UpdateAsync(model);
            this.TempData[NotificationsConstants.SUCCESS_NOTIFICATION] = string.Format(NotificationsConstants.SUCCESSFUL_UPDATE_A_CHARACTER, model.Name);

            return this.RedirectToAction("index");
        }

        [Authorize(Roles = RolesConstants.ADMIN_OR_AUTHOR)]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await this.charactersService.DeleteAsync(id);

            return this.RedirectToAction("index");
        }
    }
}
