namespace CreateManga.Application.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CreateManga.Application.Models.Characters.BindingModels;
    using CreateManga.Application.Models.Characters.ViewModels;
    using CreateManga.Application.Models.Mangas.ViewModels;

    public interface ICharactersService
    {
        IEnumerable<GetAllCharactersViewModel> GetAll();

        DetailsCharactersViewModel GetDetailsById(int id);

        IEnumerable<MangasIdNameViewModel> GetByName();

        Task CreateAsync(CreateCharactersBindingModel model);

        UpdateCharactersBindingModel UpdateById(int id);

        Task UpdateAsync(UpdateCharactersBindingModel model);

        Task DeleteAsync(int id);
    }
}
