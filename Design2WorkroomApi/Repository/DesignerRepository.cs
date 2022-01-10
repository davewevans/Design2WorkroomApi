using Design2WorkroomApi.Data;
using Design2WorkroomApi.Models;
using Design2WorkroomApi.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;
using System.Linq;
using System.Linq.Expressions;
using Design2WorkroomApi.Enums;
using TinifyAPI;

namespace Design2WorkroomApi.Repository
{
    public class DesignerRepository : IDesignerRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public DesignerRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<(bool IsSuccess, DesignerModel? Designer, string? ErrorMessage)> GetDesignerByIdAsync(Guid id)
        {
            var designer = await _dbContext.AppUsers
                .Include(x => x.Profile)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (designer is not null)
            {
                return (true, designer as DesignerModel, null);
            }
            return (false, null, "No designer found");
        }

        public async Task<(bool IsSuccess, List<DesignerModel>? Designers, string? ErrorMessage)> GetAllDesignersAsync()
        {
            try
            {
                var designers = await _dbContext.AppUsers
                    .Include(x => x.Profile)
                    .Where(x => x.AppUserRole == AppUserRole.Designer)
                    .Select(x => (DesignerModel)x)
                    .AsNoTracking()
                    .ToListAsync();

                if (designers.Any())
                {
                    return (true, designers, null);
                }
                return (false, null, "No designers found");
            }
            catch (Exception ex)
            {
                return (false, null, ex.Message);
            }

        }

        public async Task<(bool IsSuccess, List<DesignerModel>? Designers, string? ErrorMessage)> GetDesignersByConditionAsync(Expression<Func<AppUserBase, bool>> expression)
        {
            try
            {
                var designers = await _dbContext.AppUsers
                    .Include(x => x.Profile)
                    .Where(expression)
                    .Select(x => (DesignerModel)x)
                    .AsNoTracking()
                    .ToListAsync();

                if (designers.Any())
                {
                    return (true, designers, null);
                }
                return (false, null, "No designers found");
            }
            catch (Exception ex)
            {
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, string? ErrorMessage, string? UserId)> CreateDesignerAsync(DesignerModel designer)
        {
            try
            {
                await _dbContext.AppUsers.AddAsync(designer);
                await _dbContext.SaveChangesAsync();
                return (true, null, designer.Id.ToString());
            }
            catch (Exception ex)
            {
                return (false, ex.Message, "");
            }
        }

        public async Task<(bool IsSuccess, string? ErrorMessage)> UpdateDesignerAsync(DesignerModel designer)
        {
            try
            {
                _dbContext.AppUsers.Update(designer);
                await _dbContext.SaveChangesAsync();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, string? ErrorMessage)> DeleteDesignerAsync(Guid id)
        {
            try
            {
                var recordToDelete = await _dbContext.AppUsers.FirstOrDefaultAsync(x => x.Id == id);
                if (recordToDelete is null) return (false, "Designer not found");
                _dbContext.AppUsers.Remove(recordToDelete);
                await _dbContext.SaveChangesAsync();
                return (true, null);

            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, bool Exists, string? ErrorMessage)> DesignerExistsAsync(Guid id)
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

        public async Task<(bool IsSuccess, bool Exists, string? ErrorMessage)> DesignerExistsAsync(string email)
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

        public async Task<(bool IsSuccess, string? ErrorMessage)> AddClientToDesignerAsync(Guid designerId, Guid clientId)
        {
            try
            {
                var exists = await _dbContext.AppUserAppUsers.AnyAsync(x =>
                    x.AppUserParentId.Equals(designerId) && x.AppUserChildId.Equals(clientId));

                if (exists) return (false, "Record already exists");
                var designerClient = new AppUserAppUser
                {
                    AppUserParentId = designerId,
                    AppUserChildId = clientId
                };
                await _dbContext.AppUserAppUsers.AddAsync(designerClient);
                await _dbContext.SaveChangesAsync();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, string? ErrorMessage)> AddWorkroomToDesignerAsync(Guid designerId, Guid workroomId)
        {
            try
            {
                var exists = await _dbContext.AppUserAppUsers.AnyAsync(x =>
                    x.AppUserParentId.Equals(designerId) && x.AppUserChildId.Equals(workroomId));

                if (exists) return (false, "Record already exists");
                var designerWorkroom = new AppUserAppUser
                {
                    AppUserParentId = designerId,
                    AppUserChildId = workroomId
                };
                await _dbContext.AppUserAppUsers.AddAsync(designerWorkroom);
                await _dbContext.SaveChangesAsync();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, bool IsActive, string? ErrorMessage)> GetDesignerClientActiveStatusAsync(Guid designerId, Guid clientId)
        {
            var designerClient = await _dbContext.AppUserAppUsers
                .FirstOrDefaultAsync(x => x.AppUserParentId.Equals(designerId) && x.AppUserChildId.Equals(clientId));

            return designerClient is null ? (false, false, "Record not found") : (true, designerClient.Active, null);
        }

        public async Task<(bool IsSuccess, bool IsActive, string? ErrorMessage)> GetDesignerWorkroomActiveStatusAsync(Guid designerId, Guid workroomId)
        {
            var designerWorkroom = await _dbContext.AppUserAppUsers
                .FirstOrDefaultAsync(x => x.AppUserParentId.Equals(designerId) && x.AppUserChildId.Equals(workroomId));

            return designerWorkroom is null ? (false, false, "Record not found") : (true, designerWorkroom.Active, null);
        }

        public async Task<(bool IsSuccess, string? ErrorMessage)> RemoveDesignerClientAsync(Guid designerId, Guid clientId)
        {
            try
            {
                var recordToDelete = _dbContext.AppUserAppUsers
                    .FirstOrDefaultAsync(x => x.AppUserParentId == designerId && x.AppUserChildId == clientId);
                _dbContext.Remove(recordToDelete);
                await _dbContext.SaveChangesAsync();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, string? ErrorMessage)> RemoveDesignerWorkroomAsync(Guid designerId, Guid workroomId)
        {
            try
            {
                var recordToDelete = _dbContext.AppUserAppUsers
                    .FirstOrDefaultAsync(x => x.AppUserParentId == designerId && x.AppUserChildId == workroomId);
                _dbContext.Remove(recordToDelete);
                await _dbContext.SaveChangesAsync();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, string? ErrorMessage)> UpdateClientActiveStatusAsync(Guid designerId, Guid clientId, bool isActive)
        {
            try
            {
                var designerClient = await  _dbContext.AppUserAppUsers
                    .FirstOrDefaultAsync(x => x.AppUserParentId.Equals(designerId) && x.AppUserChildId.Equals(clientId));
                if (designerClient is null) return (false, "Record not found");
                designerClient.Active = isActive;
                await _dbContext.SaveChangesAsync();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, string? ErrorMessage)> UpdateWorkroomActiveStatusAsync(Guid designerId, Guid workroomId, bool isActive)
        {
            try
            {
                var designerWorkroom = await _dbContext.AppUserAppUsers
                    .FirstOrDefaultAsync(x => x.AppUserParentId.Equals(designerId) && x.AppUserChildId.Equals(workroomId));
                if (designerWorkroom is null) return (false, "Record not found");
                designerWorkroom.Active = isActive;
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
