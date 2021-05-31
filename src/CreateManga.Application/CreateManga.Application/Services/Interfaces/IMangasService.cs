namespace CreateManga.Application.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CreateManga.Application.Data.Models;
    using CreateManga.Application.Areas.Designing.Mangas.BindingModels;
    using CreateManga.Application.Areas.Designing.Mangas.ViewModels;

    public interface IMangasService
    {
        IEnumerable<MangaViewModel> GetAll();

        MangaViewModel GetDetailsById(int id);

        Manga GetByModelName(string modelName);

        bool CheckIfMangaExist(int id);

        Task CreateAsync(CreateMangaBindingModel model);

        UpdateMangaBiningModel UpdateById(int id);

        Task UpdateAsync(UpdateMangaBiningModel model);

        Task DeleteAsync(int id);
    }
}
