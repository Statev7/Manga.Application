namespace CreateManga.Application.Area.Designing.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Identity;

    using CreateManga.Application.Data.Models;
    using CreateManga.Application.Areas.Designing.Mangas.ViewModels;
    using CreateManga.Application.Areas.Designing.Mangas.BindingModels;
    using CreateManga.Application.Services.Interfaces;
    using CreateManga.Application.Areas.Designing.Controllers;
    using CreateManga.Application.Constants.RolesConstants;
    using CreateManga.Application.Constants.NotificationsConstants;

    public class MangaController : DesigningController
    {
        private readonly IMangasService mangasService;
        private readonly IMangasUsersService mangasUsersService;
        private readonly UserManager<ApplicationUser> userManager;

        public MangaController(IMangasService mangasService, IMangasUsersService mangasUsers, UserManager<ApplicationUser> userManager)
        {
            this.mangasService = mangasService;
            this.userManager = userManager;
            this.mangasUsersService = mangasUsers;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);

            IEnumerable<MangaViewModel> mangas = this.mangasService.GetAll(currentUser.Id);

            MangasViewModel mangasViewModel = new MangasViewModel();

            mangasViewModel.Mangas = mangas;
            
            return this.View(mangasViewModel);
        }

        [Authorize]
        public IActionResult Details(int id)
        {
            MangaViewModel manga = this.mangasService.GetDetailsById(id);

            bool isMangaNull = manga == null;
            if (isMangaNull)
            {
                this.TempData[NotificationsConstants.ERROR_NOTIFICATION] = NotificationsConstants.CANNOT_GET_A_DETAILS_ABOUT_EMPTY_CHAPTER;
                return this.RedirectToRoute("index");
            }

            return this.View(manga);
        }

        [Authorize(Roles = RolesConstants.ADMIN_OR_AUTHOR)]
        [HttpGet]
        public IActionResult Create()
        {
            return this.View();
        }

        [Authorize(Roles = RolesConstants.ADMIN_OR_AUTHOR)]
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(CreateMangaBindingModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.View("create", model);
            }

            Manga mangaFromDb = this.mangasService.GetByModelName(model.Name);
                
            bool isMangaAlreadyInDb = mangaFromDb != null;
            if (isMangaAlreadyInDb)
            {
                this.TempData[NotificationsConstants.ERROR_NOTIFICATION] = NotificationsConstants.MANGA_ALREADY_EXIST;
                return this.RedirectToAction("index");
            }

            if (model.StartDate > model.EndDate)
            {
                this.TempData[NotificationsConstants.ERROR_NOTIFICATION] = NotificationsConstants.FAILED_TO_SET_DATE;
                return this.RedirectToAction("index");
            }

            await this.mangasService.CreateAsync(model);
            this.TempData[NotificationsConstants.SUCCESS_NOTIFICATION] = String.Format(NotificationsConstants.SUCCESSFUL_CREATED_A_MANGA, model.Name);

            return this.RedirectToAction("index");
        }

        [Authorize(Roles = RolesConstants.ADMIN_OR_AUTHOR_OR_EDITOR)]
        [HttpGet]
        public IActionResult Update(int id)
        {
            UpdateMangaBiningModel manga = this.mangasService.UpdateById(id);

            bool isMangaNull = manga == null;
            if (isMangaNull)
            {
                this.TempData[NotificationsConstants.ERROR_NOTIFICATION] = NotificationsConstants.CANNOT_UPDATE_EMPTY_MANGA;
                return this.RedirectToAction("index");
            }

            return this.View(manga);
        }

        [Authorize(Roles = RolesConstants.ADMIN_OR_AUTHOR_OR_EDITOR)]
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(UpdateMangaBiningModel model)
        {
            if (this.ModelState.IsValid == false)
            {
                return this.View("create", model);
            }

            await this.mangasService.UpdateAsync(model);
            this.TempData[NotificationsConstants.SUCCESS_NOTIFICATION] = String.Format(NotificationsConstants.SUCCESSFUL_UPDATE_A_MANGA, model.Name);

            return this.RedirectToAction("index");
        }

        [Authorize(Roles = RolesConstants.ADMIN_OR_AUTHOR)]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await this.mangasService.DeleteAsync(id);

            return this.RedirectToAction("index");
        }

        [Authorize]
        public async Task<IActionResult> Enroll(int id)
        {
            ApplicationUser currentUser = await this.userManager.GetUserAsync(this.User);

            bool isSuccessfullyVoted = await this.mangasUsersService.EnrollUserToVoteAsync(currentUser.Id, id);

            if (isSuccessfullyVoted)
            {
                this.TempData[NotificationsConstants.SUCCESS_NOTIFICATION] = NotificationsConstants.SUCCESSFUL_VOTING;
            }
            else
            {
                this.TempData[NotificationsConstants.WARNING_NOTIFICATION] = NotificationsConstants.ALREADY_VOTED;
            }

            return this.RedirectToAction("index");
        }

        [Authorize]
        public async Task<IActionResult> Disenroll(int id)
        {
            ApplicationUser currentUser = await this.userManager.GetUserAsync(this.User);

            bool isSuccessfullyVoted = await this.mangasUsersService.RemoveTheUserVoteAsync(currentUser.Id, id);
            if (isSuccessfullyVoted)
            {
                this.TempData[NotificationsConstants.SUCCESS_NOTIFICATION] = NotificationsConstants.SUCCESSFUL_UNVOTED;
            }
            else
            {
                this.TempData[NotificationsConstants.WARNING_NOTIFICATION] = NotificationsConstants.ALREADY_UNVOTED;
            }

            return this.RedirectToAction("index");
        }
    }
}
