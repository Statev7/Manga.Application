namespace CreateManga.Application.Models
{
    using System;
    public class MangaViewModel
    {
        public int Id { get; set; }

        public string MangaName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string Description { get; set; }
    }
}
