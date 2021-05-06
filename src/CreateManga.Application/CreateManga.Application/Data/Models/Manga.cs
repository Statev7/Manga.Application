namespace CreateManga.Application.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CreateManga.Application.Data.Constants;

    public class Manga
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(MangasConstants.MIN_LENGHT_NAME)]
        [MaxLength(MangasConstants.MAX_LENGHT_NAME)]
        public string Name { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Required]
        public string Description { get; set; }

        public virtual ICollection<Character> Characters { get; set; }

        public virtual ICollection<Chapter> Chapters { get; set; }
    }
}
