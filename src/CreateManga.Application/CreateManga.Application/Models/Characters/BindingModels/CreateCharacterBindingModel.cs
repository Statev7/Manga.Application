namespace CreateManga.Application.Models.Characters.BindingModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    public class CreateCharacterBindingModel
    {
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
    }
}
