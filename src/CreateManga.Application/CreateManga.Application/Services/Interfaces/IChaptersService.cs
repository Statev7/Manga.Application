namespace CreateManga.Application.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CreateManga.Application.Areas.Designing.Chapters.BindingModels;
    using CreateManga.Application.Areas.Designing.Chapters.ViewModels;
    using CreateManga.Application.Areas.Designing.Mangas.ViewModels;
    using CreateManga.Application.Data.Models;

    public interface IChaptersService
    {
        IEnumerable<GetAllChaptersViewModel> GetAll();

        DetailsChaptersViewModel GetDetailsById(int id);

        IEnumerable<MangasIdNameViewModel> GetByName();

        Chapter GetByModelName(string modelName);

        Task CreateAsync(CreateChaptersBindingModel model);

        UpdateChaptersBindingModel UpdateById(int id);

        Task UpdateAsync(UpdateChaptersBindingModel model);

        Task DeleteAsync(int id);

    }
}
