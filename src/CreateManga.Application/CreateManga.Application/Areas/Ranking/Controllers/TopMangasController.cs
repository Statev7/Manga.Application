namespace CreateManga.Application.Areas.Ranking.Controllers
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using CreateManga.Application.Areas.Ranking.Models.TopMangas.ViewModels;
    using CreateManga.Application.Services.Interfaces;

    public class TopMangasController : RankingController
    {
        private readonly ITopMangasService topMangasService;

        public TopMangasController(ITopMangasService topMangasService)
        {
            this.topMangasService = topMangasService;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<TopMangaViewModel> mangas = this.topMangasService.GetAll();

            GetAllTopMangasViewModel allMangas = new GetAllTopMangasViewModel();
            allMangas.Mangas = mangas;

            return this.View(allMangas);
        }
    }
}
