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

            ViewBag.mangas = mangas;

            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateCharacterBindingModel model)
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

            this.dbContext.Characters.Add(character);
            this.dbContext.SaveChanges();

            return this.RedirectToAction("index");
        }
    }
}
