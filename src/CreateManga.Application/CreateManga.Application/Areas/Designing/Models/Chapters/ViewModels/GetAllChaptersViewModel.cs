namespace CreateManga.Application.Areas.Designing.Chapters.ViewModels
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Http;

    using CreateManga.Application.Data.Models;

    public class GetAllChaptersViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ImageName { get; set; }

        public IFormFile ImageFile { get; set; }
    }
}
