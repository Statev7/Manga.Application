namespace CreateManga.Application.Models.Mangas.ViewModels
{
    using System;
    using System.ComponentModel;

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
    }
}
