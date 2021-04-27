namespace CreateManga.Application.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using CreateManga.Application.Data;
    using CreateManga.Application.Models.Mangas.ViewModels;
    using CreateManga.Application.Models.Characters.BindingModels;
    using CreateManga.Application.Data.Models;
    using CreateManga.Application.Models.Characters.ViewModels;

    public class CharactersController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public CharactersController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<GetAllCharactersViewModel> characters = this.dbContext.Characters
                .Select(characters => new GetAllCharactersViewModel
                {
                    Id = characters.Id,
                    Name = characters.Name,
                })
                .ToList();

            return this.View(characters);
        }

        public IActionResult Details(int id)
        {
            DetailsCharactersViewModel character = this.dbContext.Characters
               .Select(character => new DetailsCharactersViewModel
               {
                   Id = character.Id,
                   Name = character.Name,
                   Age = character.Age,
                   Gender = character.Gender,
                   Ability = character.Ability,
                   MangaName = character.Manga.Name,

               })
               .SingleOrDefault(character => character.Id == id);

            bool isCharacterNull = character == null;
            if (isCharacterNull)
            {
                return this.RedirectToAction("index");
            }

            return this.View(character);
        }

        [HttpGet]
        public IActionResult Create()
        {
            IEnumerable<MangasIdNameViewModel> mangas = this.dbContext.Mangas
                .Select(mangas => new MangasIdNameViewModel
                {
                    Id = mangas.Id,
                    Name = mangas.Name,
                })
                .OrderBy(mangas => mangas.Name)
                .ToList();

            bool areMangasEmpty = mangas.Count() == 0;
            if (areMangasEmpty)
            {
                this.RedirectToAction("index");
            }

            ViewBag.mangas = mangas;

            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCharacterBindingModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.RedirectToAction("create");
            }

            Character character = new Character();
            character.Name = model.Name;
            character.Age = model.Age;
            character.Gender = model.Gender;
            character.Ability = model.Ability;
            character.MangaId = model.MangaId;

            await this.dbContext.Characters.AddAsync(character);
            await this.dbContext.SaveChangesAsync();

            return this.RedirectToAction("index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            UpdateCharactersBindingModel character = this.dbContext.Characters
               .Select(character => new UpdateCharactersBindingModel
               {
                   Id = character.Id,
                   Name = character.Name,
                   Age = character.Age,
                   Gender = character.Gender,
                   Ability = character.Ability,
                   MangaId = character.MangaId,
               })
               .SingleOrDefault(character => character.Id == id);

            IEnumerable<MangasIdNameViewModel> mangas = this.dbContext.Mangas
                .Select(mangas => new MangasIdNameViewModel
                {
                    Id = mangas.Id,
                    Name = mangas.Name,
                })
                .OrderBy(mangas => mangas.Name)
                .ToList();

            bool isCharacterNull = character == null;
            bool areMangasEmpty = mangas.Count() == 0;
            if (isCharacterNull || areMangasEmpty)
            {
                return this.RedirectToAction("index");
            }

            ViewBag.mangas = mangas;

            return this.View(character);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Update(UpdateCharactersBindingModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.View(model);
            }

            Character character = this.dbContext.Characters
                .Find(model.Id);

            bool isCharacterNull = character == null;
            if (isCharacterNull)
            {
                return this.RedirectToAction("index");
            }

            character.Name = model.Name;
            character.Age = model.Age;
            character.Gender = model.Gender;
            character.Ability = model.Ability;
            character.MangaId = model.MangaId;

            this.dbContext.Characters.Update(character);
            this.dbContext.SaveChanges();

            return this.RedirectToAction("index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Character character = new Character();
            character = this.dbContext.Characters
                   .Find(id);

            bool isCharacterNull = character == null;
            if (isCharacterNull)
            {
                return this.RedirectToAction("index");
            }

            this.dbContext.Characters.Remove(character);
            this.dbContext.SaveChanges();

            return this.RedirectToAction("index");
        }
    }
}
