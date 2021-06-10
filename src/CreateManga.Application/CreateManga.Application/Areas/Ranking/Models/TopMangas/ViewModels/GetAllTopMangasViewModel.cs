namespace CreateManga.Application.Areas.Ranking.Models.TopMangas.ViewModels
{
    using System.Collections.Generic;

    public class GetAllTopMangasViewModel
    {
        public IEnumerable<TopMangaViewModel> Mangas { get; set; }
    }
}
