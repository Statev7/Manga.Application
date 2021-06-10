namespace CreateManga.Application.Areas.Designing.Chapters.BindingModels
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Microsoft.AspNetCore.Http;

    using CreateManga.Application.Constants.DataConstants;

    public class UpdateChaptersBindingModel
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(ChaptersConstants.MIN_LENGHT_NAME, ErrorMessage = ChaptersConstants.MIN_LENGHT_NAME_ERROR_MESSAGE)]
        [MaxLength(ChaptersConstants.MAX_LENGHT_NAME, ErrorMessage = ChaptersConstants.MAX_LENGHT_NAME_ERROR_MESSAGE)]
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
