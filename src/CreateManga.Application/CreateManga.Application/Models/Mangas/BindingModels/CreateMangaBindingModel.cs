namespace CreateManga.Application.Models.Mangas.BindingModels
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class CreateMangaBindingModel
    {
        [Required]
        [MinLength(2)]
        [MaxLength(32)]
        [DisplayName("Manga name")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Release date")]
        public DateTime StartDate { get; set; }

        [DisplayName("End date")]
        public DateTime? EndDate { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
