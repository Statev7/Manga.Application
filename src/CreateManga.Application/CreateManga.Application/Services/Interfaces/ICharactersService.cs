namespace CreateManga.Application.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CreateManga.Application.Areas.Designing.Characters.BindingModels;
    using CreateManga.Application.Areas.Designing.Characters.ViewModels;
    using CreateManga.Application.Areas.Designing.Mangas.ViewModels;
    using CreateManga.Application.Data.Models;

    public interface ICharactersService
    {
        IEnumerable<GetAllCharactersViewModel> GetAll();

        DetailsCharactersViewModel GetDetailsById(int id);

        IEnumerable<MangasIdNameViewModel> GetByName();

        Character GetByModelName(string modelName);

        Task CreateAsync(CreateCharactersBindingModel model);

        UpdateCharactersBindingModel UpdateById(int id);

        Task UpdateAsync(UpdateCharactersBindingModel model);

        Task DeleteAsync(int id);
    }
}
