namespace CreateManga.Application.Data.Models
{
    using System.ComponentModel.DataAnnotations;


    public class Chapter
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(64)]
        public string Title { get; set; }

        [Required]
        public string Story { get; set; }

        [Required]
        public int MangaId { get; set; }

        public virtual Manga Manga { get; set; }
    }
}
