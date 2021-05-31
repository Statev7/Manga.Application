namespace CreateManga.Application.Areas.Designing.Mangas.ViewModels
{
    using System;
    using System.ComponentModel;

    using Microsoft.AspNetCore.Http;

    public class MangaViewModel
    {
        public int Id { get; set; }

        [DisplayName("Manga name")]
        public string MangaName { get; set; }

        [DisplayName("Release date")]
        public DateTime StartDate { get; set; }

        [DisplayName("End date")]
        public DateTime? EndDate { get; set; }

        public string Description { get; set; }

        [DisplayName("Upload image")]
        public string ImageName { get; set; }

        public IFormFile ImageFile { get; set; }

        public bool CurrentUserIsVoted { get; set; }

    }
}
