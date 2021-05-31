namespace CreateManga.Application.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class MangaUser
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int MangaId { get; set; }
        public virtual Manga Manga { get; set; }

        [Required]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
