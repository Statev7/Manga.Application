namespace CreateManga.Application.Areas.Ranking.Models.TopMangas.ViewModels
{
    using System;

    using Microsoft.AspNetCore.Http;

    public class TopMangaViewModel
    {
        public int Id { get; set; }

        public string MangaName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string Description { get; set; }

        public string ImageName { get; set; }

        public IFormFile ImageFile { get; set; }

        public int Votes { get; set; }
    }
}
