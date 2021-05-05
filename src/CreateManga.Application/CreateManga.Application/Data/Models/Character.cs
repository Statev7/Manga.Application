namespace CreateManga.Application.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using CreateManga.Application.Data.Constants;

    public class Character
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(CharactersConstants.MIN_LENGHT_NAME)]
        [MaxLength(CharactersConstants.MAX_LENGHT_NAME)]
        public string Name { get; set; }

        [Required]
        [Range(CharactersConstants.MIN_AGE_VALUE, CharactersConstants.MAX_AGE_VALUE)]
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
