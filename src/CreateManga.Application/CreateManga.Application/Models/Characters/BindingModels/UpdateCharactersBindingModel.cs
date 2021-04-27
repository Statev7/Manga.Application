﻿namespace CreateManga.Application.Models.Characters.BindingModels
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class UpdateCharactersBindingModel
    {
        [Required]
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
        [DisplayName("Manga")]
        public int MangaId { get; set; }
    }
}