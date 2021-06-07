namespace CreateManga.Application.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Microsoft.AspNetCore.Http;

    public class Chapter
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(16)]
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
