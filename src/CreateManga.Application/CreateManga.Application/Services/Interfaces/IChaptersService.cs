﻿namespace CreateManga.Application.Services.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CreateManga.Application.Areas.Designing.Chapters.BindingModels;
    using CreateManga.Application.Areas.Designing.Chapters.ViewModels;
    using CreateManga.Application.Areas.Designing.Mangas.ViewModels;

    public interface IChaptersService
    {
        IEnumerable<GetAllChaptersViewModel> GetAll();

        DetailsChaptersViewModel GetDetailsById(int id);

        IEnumerable<MangasIdNameViewModel> GetByName();

        Task CreateAsync(CreateChaptersBindingModel model);

        UpdateChaptersBindingModel UpdateById(int id);

        Task UpdateAsync(UpdateChaptersBindingModel model);

        Task DeleteAsync(int id);
    }
}
