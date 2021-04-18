namespace CreateManga.Application.Models
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class CreateMangaBindingModel
    {
        [Required]
        [MinLength(2)]
        [MaxLength(32)]
        [DisplayName("Име")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Дата на започване")]
        public DateTime StartDate { get; set; }

        [DisplayName("Дата на приключване")]
        public DateTime? EndDate { get; set; }

        [Required]
        [DisplayName("Описание")]
        public string Description { get; set; }
    }
}
