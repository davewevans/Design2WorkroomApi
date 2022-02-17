using Design2WorkroomApi.Models;

namespace Design2WorkroomApi.Repository.Contracts
{
    public interface IUserRepository
    {
        Task<(bool IsSuccess, string? ErrorMessage)> CreateAppUserAppUserAsync(AppUserAppUser user);
    }
}
