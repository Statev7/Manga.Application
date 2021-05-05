namespace CreateManga.Application.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using CreateManga.Application.Models.Mangas.ViewModels;
    using CreateManga.Application.Models.Characters.BindingModels;
    using CreateManga.Application.Models.Characters.ViewModels;
    using CreateManga.Application.Services.Interfaces;

    public class CharactersController : Controller
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

        [Authorize]
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

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCharacterBindingModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.RedirectToAction("create");
            }

            await this.charactersService.CreateAsync(model);

            return this.RedirectToAction("index");
        }

        [Authorize]
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

        [Authorize]
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

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await this.charactersService.DeleteAsync(id);

            return this.RedirectToAction("index");
        }
    }
}
