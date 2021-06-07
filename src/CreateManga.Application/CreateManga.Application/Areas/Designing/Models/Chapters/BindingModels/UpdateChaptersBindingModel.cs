namespace CreateManga.Application.Areas.Designing.Chapters.BindingModels
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Microsoft.AspNetCore.Http;

    public class UpdateChaptersBindingModel
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(16)]
        public string Title { get; set; }

        [Required]
        public string Story { get; set; }

        [Required]
        [DisplayName("Manga")]
        public int MangaId { get; set; }

        public string ImageName { get; set; }

        [DisplayName("Upload image")]
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
