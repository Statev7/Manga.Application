namespace CreateManga.Application.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class UpdateMangaBiningModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(32)]
        public string Name { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
