namespace CreateManga.Application.Areas.Designing.Chapters.BindingModels
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class UpdateChaptersBindingModel
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(64)]
        public string Title { get; set; }

        [Required]
        public string Story { get; set; }

        [Required]
        [DisplayName("Manga")]
        public int MangaId { get; set; }
    }
}
