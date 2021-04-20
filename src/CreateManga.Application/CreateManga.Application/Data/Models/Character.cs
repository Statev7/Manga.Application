namespace CreateManga.Application.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    public class Character
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(64)]
        public string Name { get; set; }

        [Required]
        [Range(1, 100000)]
        public int Age { get; set; }

        [Required]
        public char Gender { get; set; }

        [Required]
        public string Ability { get; set; }

        [Required]
        public int MangaId { get; set; }

        public virtual Manga Manga { get; set; }
    }
}
