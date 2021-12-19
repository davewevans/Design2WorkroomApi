using System.ComponentModel.Design;
using System.Linq;
using System.Linq.Expressions;
using Design2WorkroomApi.Data;
using Design2WorkroomApi.Enums;
using Design2WorkroomApi.Models;
using Design2WorkroomApi.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Design2WorkroomApi.Repository
{
    public class WorkroomRepository : IWorkroomRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public WorkroomRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<(bool IsSuccess, WorkroomModel? Workroom, string? ErrorMessage)> GetWorkroomByIdAsync(Guid id)
        {
            var workroom = await _dbContext.AppUsers
                .Include(x => x.Profile)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (workroom is not null)
            {
                return (true, (WorkroomModel)workroom, null);
            }
            return (false, null, "No workroom found");
        }

        public async Task<(bool IsSuccess, List<WorkroomModel>? Workrooms, string? ErrorMessage)> GetAllWorkroomsAsync()
        {
            try
            {
                var workrooms = await _dbContext.AppUsers
                    .Include(x => x.Profile)
                    .Where(x => x.AppUserRole == AppUserRole.Workroom)
                    .Select(x => (WorkroomModel)x)
                    .AsNoTracking()
                    .ToListAsync();

                if (workrooms.Any())
                {
                    return (true, workrooms, null);
                }
                return (false, null, "No workrooms found");
            }
            catch (Exception ex)
            {
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, List<WorkroomModel>? Workrooms, string? ErrorMessage)> GetWorkroomsByConditionAsync(Expression<Func<AppUserBase, bool>> expression)
        {
            try
            {
                var workrooms = await _dbContext.AppUsers
                    .Include(x => x.Profile)
                    .Where(expression)
                    .Select(x => (WorkroomModel)x)
                    .AsNoTracking()
                    .ToListAsync();

                if (workrooms.Any())
                {
                    return (true, workrooms, null);
                }
                return (false, null, "No workrooms found");
            }
            catch (Exception ex)
            {
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, bool IsActive, string? ErrorMessage)> GetDesignerWorkroomActiveStatusAsync(Guid designerId, Guid workroomId)
        {
            var designerWorkroom = await _dbContext.AppUserAppUsers
                .FirstOrDefaultAsync(x => x.AppUserParentId.Equals(designerId) && x.AppUserChildId.Equals(workroomId));

            return designerWorkroom is null ? (false, false, "Record not found") : (true, designerWorkroom.Active, null);
        }

        public async Task<(bool IsSuccess, string? ErrorMessage)> CreateWorkroomAsync(WorkroomModel workroom)
        {
            try
            {
                await _dbContext.AppUsers.AddAsync(workroom);
                await _dbContext.SaveChangesAsync();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
        public async Task<(bool IsSuccess, string? ErrorMessage)> UpdateWorkroomAsync(WorkroomModel workroom)
        {
            try
            {
                _dbContext.AppUsers.Update(workroom);
                await _dbContext.SaveChangesAsync();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);

            }
        }

        public async Task<(bool IsSuccess, string? ErrorMessage)> DeleteWorkroomAsync(Guid id)
        {
            try
            {
                var recordToDelete = await _dbContext.AppUsers.FirstOrDefaultAsync(x => x.Id == id);
                if (recordToDelete is null) return (false, "Workroom not found");
                _dbContext.AppUsers.Remove(recordToDelete);
                await _dbContext.SaveChangesAsync();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, bool Exists, string? ErrorMessage)> WorkroomExistsAsync(Guid id)
        {
            try
            {
                var exists = await _dbContext.AppUsers.AnyAsync(x => x.Id.Equals(id));
                return (true, exists, null);
            }
            catch (Exception ex)
            {
                return (false, false, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, bool Exists, string? ErrorMessage)> WorkroomExistsAsync(string email)
        {
            try
            {
                var exists = await _dbContext.AppUsers.AnyAsync(x => x.UserName.Equals(email));
                return (true, exists, null);
            }
            catch (Exception ex)
            {
                return (false, false, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, List<WorkroomModel>? Workrooms, string? ErrorMessage)> GetWorkroomsByDesignerIdAsync(Guid designerId)
        {
            var workrooms = await _dbContext.AppUserAppUsers
                .Where(x => x.AppUserParentId.Equals(designerId)
                            && x.ChildAppUser.AppUserRole == AppUserRole.Workroom)
                .Include(x => x.ChildAppUser.Profile)
                .Select(x => (WorkroomModel)x.ChildAppUser)
                .AsNoTracking()
                .ToListAsync();

            if (workrooms.Any())
            {
                return (true, workrooms, null);
            }
            return (false, null, "No clients found");
        }
    }
}
