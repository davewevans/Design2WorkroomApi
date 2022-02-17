using Design2WorkroomApi.Models;
using System.Linq.Expressions;

namespace Design2WorkroomApi.Repository.Contracts
{
    public interface IWorkroomRepository
    { 
        Task<(bool IsSuccess, WorkroomModel? Workroom, string? ErrorMessage)> CreateWorkroomReturnWorkroomAsync(WorkroomModel workroom);

        Task<(bool IsSuccess, WorkroomModel? Workroom, string? ErrorMessage)> GetWorkroomByIdAsync(Guid id);

        Task<(bool IsSuccess, List<WorkroomModel>? Workrooms, string? ErrorMessage)> GetAllWorkroomsAsync();

        Task<(bool IsSuccess, List<WorkroomModel>? Workrooms, string? ErrorMessage)> GetWorkroomsByConditionAsync(Expression<Func<AppUserBase, bool>> expression);

        Task<(bool IsSuccess, List<WorkroomModel>? Workrooms, string? ErrorMessage)> GetWorkroomsByDesignerIdAsync(Guid designerId);

        Task<(bool IsSuccess, bool IsActive, string? ErrorMessage)> GetDesignerWorkroomActiveStatusAsync(Guid designerId, Guid workroomId);

        Task<(bool IsSuccess, string? ErrorMessage)> CreateWorkroomAsync(WorkroomModel workroom);

        Task<(bool IsSuccess, string? ErrorMessage)> UpdateWorkroomAsync(WorkroomModel workroom);

        Task<(bool IsSuccess, string? ErrorMessage)> DeleteWorkroomAsync(Guid id);

        Task<(bool IsSuccess, bool Exists, string? ErrorMessage)> WorkroomExistsAsync(Guid id);

        Task<(bool IsSuccess, bool Exists, string? ErrorMessage)> WorkroomExistsAsync(string email);
    }
}
