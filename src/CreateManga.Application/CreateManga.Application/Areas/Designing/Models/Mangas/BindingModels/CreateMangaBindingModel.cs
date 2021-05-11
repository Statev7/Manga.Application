namespace CreateManga.Application.Areas.Designing.Mangas.BindingModels
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Microsoft.AspNetCore.Http;

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
        [MaxLength(32)]
        public string Description { get; set; }

        public string ImageName { get; set; }

        [DisplayName("Upload image")]
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
