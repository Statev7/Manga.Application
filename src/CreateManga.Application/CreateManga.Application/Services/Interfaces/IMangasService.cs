namespace CreateManga.Application.Services.Interfaces
{
    using CreateManga.Application.Data.Models;
    using CreateManga.Application.Models.Mangas.BindingModels;
    using CreateManga.Application.Models.Mangas.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IMangasService
    {
        IEnumerable<MangaViewModel> GetAll();

        MangaViewModel GetDetailsById(int id);

        Manga GetByModelName(string modelName);

        Task CreateAsync(CreateMangaBindingModel model);

        UpdateMangaBiningModel UpdateById(int id);

        Task UpdateAsync(UpdateMangaBiningModel model);

        Task DeleteAsync(int id);
    }
}
