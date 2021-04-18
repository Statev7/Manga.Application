namespace CreateManga.Application.Models
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class MangaBindingModel
    {
        [Required]
        [MinLength(2)]
        [MaxLength(32)]
        [DisplayName("Manga name")]
        public string Name { get; set; }
    }
}
