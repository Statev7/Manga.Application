namespace CreateManga.Application.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Microsoft.AspNetCore.Http;

    using CreateManga.Application.Constants.DataConstants;

    public class Chapter
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(ChaptersConstants.MIN_LENGHT_NAME)]
        [MaxLength(ChaptersConstants.MAX_LENGHT_NAME)]
        public string Title { get; set; }

        [Required]
        public string Story { get; set; }

        [Required]
        public int MangaId { get; set; }

        public string ImageName { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }

        public virtual Manga Manga { get; set; }

    }
}
