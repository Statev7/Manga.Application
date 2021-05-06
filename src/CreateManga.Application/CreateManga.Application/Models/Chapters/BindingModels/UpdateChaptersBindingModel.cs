using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CreateManga.Application.Models.Chapters.BindingModels
{
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
