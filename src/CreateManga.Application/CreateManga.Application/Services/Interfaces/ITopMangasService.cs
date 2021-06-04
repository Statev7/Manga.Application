namespace CreateManga.Application.Services.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CreateManga.Application.Areas.Ranking.Models.TopMangas.ViewModels;

    public interface ITopMangasService
    {
        public IEnumerable<TopMangaViewModel> GetAll();
    }
}
