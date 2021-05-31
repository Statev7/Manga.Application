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

        public MangasUserService(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        public async Task<bool> EnrollUserToVoteAsync(string userId, int mangaId)
        {
            await CheckIfUserAndMangaExistAsync(userId, mangaId);

            if (this.IsAlreadyVoted(userId, mangaId))
            {
                return false;
            }

            MangaUser mangaUser = new MangaUser()
            {
                UserId = userId,
                MangaId = mangaId,
            };

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

            MangaUser mangaUser = GetVoted(userId, mangaId);

            this.dbContext.MangasUsers.Remove(mangaUser);
            await this.dbContext.SaveChangesAsync();

            return true;
        }

        public bool IsAlreadyVoted(string userId, int mangaId)
        {
            MangaUser mangaUser = GetVoted(userId, mangaId);

            bool isAlreadyVoted = mangaUser != null;

            return isAlreadyVoted;
        }

        private MangaUser GetVoted(string userId, int mangaId)
        {
            MangaUser mangaUser = this.dbContext.MangasUsers
                .Where(mu => mu.UserId == userId && mu.MangaId == mangaId)
                .FirstOrDefault();

            return mangaUser;
        }

        private async Task CheckIfUserAndMangaExistAsync(string userId, int mangaId)
        {
            Manga manga = this.dbContext.Manga
                .Where(m => m.Id == mangaId)
                .SingleOrDefault();

            bool isMangaNotExists = manga == null;

            if (isMangaNotExists)
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
