namespace CreateManga.Application.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CreateManga.Application.Data;
    using CreateManga.Application.Data.Models;
    using CreateManga.Application.Areas.Designing.Characters.BindingModels;
    using CreateManga.Application.Areas.Designing.Characters.ViewModels;
    using CreateManga.Application.Areas.Designing.Mangas.ViewModels;
    using CreateManga.Application.Services.Interfaces;

    public class CharactersService : ICharactersService
    {
        private readonly ApplicationDbContext dbContext;

        public CharactersService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<GetAllCharactersViewModel> GetAll()
        {
            IEnumerable<GetAllCharactersViewModel> characters = this.dbContext.Character
               .Select(characters => new GetAllCharactersViewModel
               {
                   Id = characters.Id,
                   Name = characters.Name,
               })
               .ToList();

            return characters;
        }

        public DetailsCharactersViewModel GetDetailsById(int id)
        {
            DetailsCharactersViewModel character = this.dbContext.Character
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

            return character;
        }

        public IEnumerable<MangasIdNameViewModel> GetByName()
        {
            IEnumerable<MangasIdNameViewModel> mangas = this.dbContext.Manga
                .Select(mangas => new MangasIdNameViewModel
                {
                    Id = mangas.Id,
                    Name = mangas.Name,
                })
                .OrderBy(mangas => mangas.Name)
                .ToList();

            return mangas;
        }

        public async Task CreateAsync(CreateCharactersBindingModel model)
        {
            Character character = new Character();
            character.Name = model.Name;
            character.Age = model.Age;
            character.Gender = model.Gender;
            character.Ability = model.Ability;
            character.MangaId = model.MangaId;

            await this.dbContext.Character.AddAsync(character);
            await this.dbContext.SaveChangesAsync();
        }

        public UpdateCharactersBindingModel UpdateById(int id)
        {
            UpdateCharactersBindingModel character = this.dbContext.Character
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

            return character;
        }

        public async Task UpdateAsync(UpdateCharactersBindingModel model)
        {
            Character character = this.dbContext.Character
                .Find(model.Id);

            bool isCharacterNull = character == null;
            if (isCharacterNull)
            {
                return;
            }

            character.Name = model.Name;
            character.Age = model.Age;
            character.Gender = model.Gender;
            character.Ability = model.Ability;
            character.MangaId = model.MangaId;

            this.dbContext.Character.Update(character);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Character character = new Character();
            character = this.dbContext.Character
                   .Find(id);

            bool isCharacterNull = character == null;
            if (isCharacterNull)
            {
                return;
            }

            this.dbContext.Character.Remove(character);
            await this.dbContext.SaveChangesAsync();
        }

    }
}
