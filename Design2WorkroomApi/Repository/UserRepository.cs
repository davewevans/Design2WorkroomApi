using Design2WorkroomApi.Data;
using Design2WorkroomApi.Models;
using Design2WorkroomApi.Repository.Contracts;

namespace Design2WorkroomApi.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<(bool IsSuccess, string? ErrorMessage)> CreateAppUserAppUserAsync(AppUserAppUser user)
        {
            try
            {
                await _dbContext.AppUserAppUsers.AddAsync(user);
                await _dbContext.SaveChangesAsync();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
    }
}
