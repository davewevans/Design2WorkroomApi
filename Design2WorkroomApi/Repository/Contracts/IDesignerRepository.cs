using Design2WorkroomApi.Models;
using System.Linq.Expressions;

namespace Design2WorkroomApi.Repository.Contracts
{
    public interface IDesignerRepository
    {
        Task<(bool IsSuccess, DesignerModel? Designer, string? ErrorMessage)> GetDesignerByIdAsync(Guid designer);

        Task<(bool IsSuccess, List<DesignerModel>? Designers, string? ErrorMessage)> GetAllDesignersAsync();

        Task<(bool IsSuccess, List<DesignerModel>? Designers, string? ErrorMessage)> GetDesignersByConditionAsync(Expression<Func<AppUserBase, bool>> expression);

        Task<(bool IsSuccess, string? ErrorMessage, string? UserId)> CreateDesignerAsync(DesignerModel designer);

        Task<(bool IsSuccess, string? ErrorMessage)> UpdateDesignerAsync(DesignerModel designer);

        Task<(bool IsSuccess, string? ErrorMessage)> DeleteDesignerAsync(Guid id);

        Task<(bool IsSuccess, bool Exists, string? ErrorMessage)> DesignerExistsAsync(Guid id);

        Task<(bool IsSuccess, bool Exists, string? ErrorMessage)> DesignerExistsAsync(string email);

        Task<(bool IsSuccess, string? ErrorMessage)> UpdateClientActiveStatusAsync(Guid designerId, Guid clientId, bool isActive);

        Task<(bool IsSuccess, string? ErrorMessage)> UpdateWorkroomActiveStatusAsync(Guid designerId, Guid workroomId, bool isActive);

        Task<(bool IsSuccess, bool IsActive, string? ErrorMessage)> GetDesignerClientActiveStatusAsync(Guid designerId, Guid clientId);

        Task<(bool IsSuccess, bool IsActive, string? ErrorMessage)> GetDesignerWorkroomActiveStatusAsync(Guid designerId, Guid workroomId);

        Task<(bool IsSuccess, string? ErrorMessage)> AddClientToDesignerAsync(Guid designerId, Guid clientId);

        Task<(bool IsSuccess, string? ErrorMessage)> AddWorkroomToDesignerAsync(Guid designerId, Guid workroomId);

        Task<(bool IsSuccess, string? ErrorMessage)> RemoveDesignerClientAsync(Guid designerId, Guid clientId);

        Task<(bool IsSuccess, string? ErrorMessage)> RemoveDesignerWorkroomAsync(Guid designerId, Guid workroomId);
    }
}
