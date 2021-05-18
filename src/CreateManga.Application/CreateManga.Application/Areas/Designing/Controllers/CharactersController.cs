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
                this.RedirectToAction("index");
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

            await this.charactersService.CreateAsync(model);

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
