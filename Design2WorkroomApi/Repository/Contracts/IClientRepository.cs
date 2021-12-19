using Design2WorkroomApi.Models;
using System.Linq.Expressions;

namespace Design2WorkroomApi.Repository.Contracts
{
    public interface IClientRepository
    {
        Task<(bool IsSuccess, ClientModel? Client, string? ErrorMessage)> GetClientByIdAsync(Guid id);

        Task<(bool IsSuccess, List<ClientModel>? Clients, string? ErrorMessage)> GetAllClientsAsync();

        Task<(bool IsSuccess, List<ClientModel>? Clients, string? ErrorMessage)> GetClientsByConditionAsync(Expression<Func<AppUserBase, bool>> expression);

        Task<(bool IsSuccess, List<ClientModel>? Clients, string? ErrorMessage)> GetClientsByDesignerIdAsync(Guid designerId);

        Task<(bool IsSuccess, bool IsActive, string? ErrorMessage)> GetDesignerClientActiveStatusAsync(Guid designerId, Guid clientId);

        Task<(bool IsSuccess, string? ErrorMessage)> CreateClientAsync(ClientModel client);

        Task<(bool IsSuccess, string? ErrorMessage)> UpdateClientAsync(ClientModel client);

        Task<(bool IsSuccess, string? ErrorMessage)> DeleteClientAsync(Guid id);

        Task<(bool IsSuccess, bool Exists, string? ErrorMessage)> ClientExistsAsync(Guid id);

        Task<(bool IsSuccess, bool Exists, string? ErrorMessage)> ClientExistsAsync(string email);
    }
}
