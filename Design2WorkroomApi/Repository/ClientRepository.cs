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
    public class ClientRepository : IClientRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ClientRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<(bool IsSuccess, ClientModel? Client, string? ErrorMessage)> GetClientByIdAsync(Guid id)
        {
            var client = await _dbContext.AppUsers
                .Include(x => x.Profile)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (client is not null)
            {
                return (true, (ClientModel)client, null);
            }
            return (false, null, "No client found");
        }

        public async Task<(bool IsSuccess, List<ClientModel>? Clients, string? ErrorMessage)> GetAllClientsAsync()
        {
            try
            {
                var clients = await _dbContext.AppUsers
                    .Include(x => x.Profile)
                    .Where(x => x.AppUserRole == AppUserRole.Client)
                    .Select(x => (ClientModel)x)
                    .AsNoTracking()
                    .ToListAsync();

                if (clients.Any())
                {
                    return (true, clients, null);
                }
                return (false, null, "No clients found");
            }
            catch (Exception ex)
            {
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, List<ClientModel>? Clients, string? ErrorMessage)> GetClientsByConditionAsync(Expression<Func<AppUserBase, bool>> expression)
        {
            try
            {
                var clients = await _dbContext.AppUsers
                    .Include(x => x.Profile)
                    .Where(expression)
                    .Select(x => (ClientModel)x)
                    .AsNoTracking()
                    .ToListAsync();

                if (clients.Any())
                {
                    return (true, clients, null);
                }
                return (false, null, "No clients found");
            }
            catch (Exception ex)
            {
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, bool IsActive, string? ErrorMessage)> GetDesignerClientActiveStatusAsync(Guid designerId, Guid clientId)
        {
            var designerClient = await _dbContext.AppUserAppUsers
                .FirstOrDefaultAsync(x => x.AppUserParentId.Equals(designerId) && x.AppUserChildId.Equals(clientId));

            return designerClient is null ? (false, false, "Record not found") : (true, designerClient.Active, null);
        }

        public async Task<(bool IsSuccess, string? ErrorMessage)> CreateClientAsync(ClientModel client)
        {
            try
            {
                await _dbContext.AppUsers.AddAsync(client);
                await _dbContext.SaveChangesAsync();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, ClientModel? Client, string? ErrorMessage)> CreateClientReturnClientAsync(ClientModel client)
        {
            try
            {
                await _dbContext.AppUsers.AddAsync(client);
                await _dbContext.SaveChangesAsync();
                return (true, client, null);
            }
            catch (Exception ex)
            {
                return (false, new ClientModel("", ""), ex.Message);
            }
        }

        public async Task<(bool IsSuccess, string? ErrorMessage)> UpdateClientAsync(ClientModel client)
        {
            try
            {
                _dbContext.AppUsers.Update(client);
                await _dbContext.SaveChangesAsync();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);

            }
        }

        public async Task<(bool IsSuccess, string? ErrorMessage)> DeleteClientAsync(Guid id)
        {
            try
            {
                var recordToDelete = await _dbContext.AppUsers.FirstOrDefaultAsync(x => x.Id == id);
                if (recordToDelete is null) return (false, "Client not found");
                _dbContext.AppUsers.Remove(recordToDelete);
                await _dbContext.SaveChangesAsync();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, bool Exists, string? ErrorMessage)> ClientExistsAsync(Guid id)
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

        public async Task<(bool IsSuccess, bool Exists, string? ErrorMessage)> ClientExistsAsync(string email)
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

        public async Task<(bool IsSuccess, List<ClientModel>? Clients, string? ErrorMessage)> GetClientsByDesignerIdAsync(Guid designerId)
        {
            var clients = await _dbContext.AppUserAppUsers
                .Where(x => x.AppUserParentId.Equals(designerId)
                            && x.ChildAppUser.AppUserRole == AppUserRole.Client)
                .Include(x => x.ChildAppUser.Profile)
                .Select(x => (ClientModel)x.ChildAppUser)
                .AsNoTracking()
                .ToListAsync();

            if (clients.Any())
            {
                return (true, clients, null);
            }
            return (false, null, "No clients found");
        }

    }
}
