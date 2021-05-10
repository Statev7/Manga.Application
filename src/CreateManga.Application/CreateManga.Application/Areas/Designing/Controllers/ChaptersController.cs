namespace CreateManga.Application.Area.Designing.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using CreateManga.Application.Areas.Designing.Chapters.ViewModels;
    using CreateManga.Application.Services.Interfaces;
    using CreateManga.Application.Areas.Designing.Mangas.ViewModels;
    using CreateManga.Application.Areas.Designing.Chapters.BindingModels;
    using CreateManga.Application.Areas.Designing.Controllers;

    public class ChaptersController : DesigningController
    {
        private readonly IChaptersService chapterService;

        public ChaptersController(IChaptersService chapterService)
        {
            this.chapterService = chapterService;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<GetAllChaptersViewModel> chapter = this.chapterService.GetAll();

            return this.View(chapter);
        }

        [Authorize]
        public IActionResult Details(int id)
        {
            DetailsChaptersViewModel chapter = this.chapterService.GetDetailsById(id);

            bool isCharacterNull = chapter == null;
            if (isCharacterNull)
            {
                return this.RedirectToAction("index");
            }

            return this.View(chapter);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            IEnumerable<MangasIdNameViewModel> mangas = this.chapterService.GetByName();

            bool areMangasEmpty = mangas.Count() == 0;
            if (areMangasEmpty)
            {
                this.RedirectToAction("index");
            }

            ViewBag.mangas = mangas;

            return this.View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateChaptersBindingModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.RedirectToAction("create");
            }

            await this.chapterService.CreateAsync(model);

            return this.RedirectToAction("index");
        }

        [Authorize]
        [HttpGet]
        public IActionResult Update(int id)
        {
            UpdateChaptersBindingModel chapter = this.chapterService.UpdateById(id);

            IEnumerable<MangasIdNameViewModel> mangas = this.chapterService.GetByName();

            bool isCharacterNull = chapter == null;
            bool areMangasEmpty = mangas.Count() == 0;
            if (isCharacterNull || areMangasEmpty)
            {
                return this.RedirectToAction("index");
            }

            ViewBag.mangas = mangas;

            return this.View(chapter);
        }

        [Authorize]
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(UpdateChaptersBindingModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.View(model);
            }

            await this.chapterService.UpdateAsync(model);

            return this.RedirectToAction("index");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await this.chapterService.DeleteAsync(id);

            return this.RedirectToAction("index");
        }
    }
}
