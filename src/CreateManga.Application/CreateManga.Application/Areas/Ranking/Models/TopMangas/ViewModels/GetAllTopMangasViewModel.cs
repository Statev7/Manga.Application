namespace CreateManga.Application.Areas.Ranking.Models.TopMangas.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CreateManga.Application.Data.Models;

    public class GetAllTopMangasViewModel
    {
        public IEnumerable<TopMangaViewModel> Mangas { get; set; }
    }
}
