namespace CreateManga.Application.Areas.Designing.Mangas.BindingModels
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Microsoft.AspNetCore.Http;

    using CreateManga.Application.Data.Constants;

    public class UpdateMangaBiningModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(MangasConstants.MIN_LENGHT_NAME, ErrorMessage = MangasConstants.MIN_LENGHT_NAME_ERROR_MESSAGE)]
        [MaxLength(MangasConstants.MAX_LENGHT_NAME, ErrorMessage = MangasConstants.MAX_LENGHT_NAME_ERROR_MESSAGE)]
        [DisplayName("Manga name")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Release date")]
        public DateTime StartDate { get; set; }

        [DisplayName("End date")]
        public DateTime? EndDate { get; set; }

        [Required]
        [MaxLength(MangasConstants.MAX_DESCRIPTION_LENGHT, ErrorMessage = MangasConstants.DESCRIPTION_ERROR_MESSAGE)]
        public string Description { get; set; }

        public string ImageName { get; set; }

        [DisplayName("Upload image")]
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
