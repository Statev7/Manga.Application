namespace CreateManga.Application.Areas.Designing.Characters.BindingModels
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using CreateManga.Application.Data.Constants;

    public class UpdateCharactersBindingModel
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(CharactersConstants.MIN_LENGHT_NAME, ErrorMessage = CharactersConstants.MIN_LENGHT_NAME_ERROR_MESSAGE)]
        [MaxLength(CharactersConstants.MAX_LENGHT_NAME, ErrorMessage = CharactersConstants.MAX_LENGHT_NAME_ERROR_MESSAGE)]
        public string Name { get; set; }

        [Required]
        [Range(CharactersConstants.MIN_AGE_VALUE, CharactersConstants.MAX_AGE_VALUE)]
        public int Age { get; set; }

        [Required]
        public char Gender { get; set; }

        [Required]
        public string Ability { get; set; }

        [Required]
        [DisplayName("Manga")]
        public int MangaId { get; set; }
    }
}
