namespace CreateManga.Application.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;

    using CreateManga.Application.Data;
    using CreateManga.Application.Data.Models;
    using CreateManga.Application.Services.Interfaces;
    using CreateManga.Application.Constants.ExeptionConstants;

    public class MangasUserService : IMangasUsersService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMangasService mangasService;

        public MangasUserService(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, IMangasService mangasService)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.mangasService = mangasService;
        }

        public async Task<bool> EnrollUserToVoteAsync(string userId, int mangaId)
        {
            await CheckIfUserAndMangaExistAsync(userId, mangaId);

            if (this.IsAlreadyVoted(userId, mangaId))
            {
                return false;
            }

            MangaUser mangaUser = GetVoteted(userId, mangaId);

            await this.dbContext.MangasUsers.AddAsync(mangaUser);
            await this.dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RemoveTheUserVoteAsync(string userId, int mangaId)
        {
            await CheckIfUserAndMangaExistAsync(userId, mangaId);

            if (this.IsAlreadyVoted(userId, mangaId) == false)
            {
                return false;
            }

            MangaUser mangaUser = GetVoteted(userId, mangaId);

            this.dbContext.MangasUsers.Remove(mangaUser);
            await this.dbContext.SaveChangesAsync();

            return true;
        }

        public bool IsAlreadyVoted(string userId, int mangaId)
        {
            MangaUser mangauser = this.dbContext.MangasUsers
                .Where(mu => mu.UserId == userId && mu.MangaId == mangaId)
                .FirstOrDefault();

            bool isAlreadyVoted = mangauser != null;

            return isAlreadyVoted;
        }

        private MangaUser GetVoteted(string userId, int mangaId)
        {
            return this.dbContext.MangasUsers
                            .Where(mu => mu.UserId == userId && mu.MangaId == mangaId)
                            .FirstOrDefault();
        }

        private async Task CheckIfUserAndMangaExistAsync(string userId, int mangaId)
        {
            if (this.mangasService.CheckIfMangaExist(mangaId) == false)
            {
                throw new ArgumentException(ExeptionConstants.NOT_EXISTING_MANGA_ERROR_MESSAGE);
            }

            if (await this.userManager.FindByIdAsync(userId) == null)
            {
                throw new ArgumentException(ExeptionConstants.NOT_EXISTING_USER_ERROR_MESSAGE);
            }
        }
    }
}
