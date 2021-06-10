namespace CreateManga.Application.Services.Interfaces
{
    using System.Collections.Generic;

    using CreateManga.Application.Areas.Ranking.Models.TopMangas.ViewModels;

    public interface ITopMangasService
    {
        public IEnumerable<TopMangaViewModel> GetAll();
    }
}
