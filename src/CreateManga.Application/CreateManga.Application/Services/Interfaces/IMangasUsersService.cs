namespace CreateManga.Application.Services.Interfaces
{
    using System.Threading.Tasks;

    using CreateManga.Application.Data.Models;

    public interface IMangasUsersService
    {
        Task<bool> EnrollUserToVoteAsync(string userId, int mangaId);

        Task<bool> RemoveTheUserVoteAsync(string userId, int mangaId);

        bool IsAlreadyVoted(string userId, int mangaId);
    }
}
