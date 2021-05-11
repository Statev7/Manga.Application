namespace CreateManga.Application.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Microsoft.AspNetCore.Http;

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
        [MaxLength(MangasConstants.MAX_DESCRIPTION_LENGHT)]
        public string Description { get; set; }

        public string ImageName { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }

        public virtual ICollection<Character> Characters { get; set; }

        public virtual ICollection<Chapter> Chapters { get; set; }
    }
}
