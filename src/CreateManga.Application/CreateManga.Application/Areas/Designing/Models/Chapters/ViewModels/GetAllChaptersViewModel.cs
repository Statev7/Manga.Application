namespace CreateManga.Application.Areas.Designing.Chapters.ViewModels
{
    using Microsoft.AspNetCore.Http;

    public class GetAllChaptersViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ImageName { get; set; }

        public IFormFile ImageFile { get; set; }
    }
}
