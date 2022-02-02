using Design2WorkroomApi.Models;

namespace Design2WorkroomApi.Repository.Contracts
{
    public interface IWorkOrdersRepository
    {
        Task<(bool IsSuccess, string? ErrorMessage)> CreateWorkOrderAsync(WorkOrderModel WorkOrder);

        Task<(bool IsSuccess, WorkOrderModel? WorkOrder, string? ErrorMessage)> GetWorkOrderByIdAsync(Guid id);

        Task<(bool IsSuccess, List<WorkOrderModel>? WorkOrders, string? ErrorMessage)> GetAllWorkOrdersAsync();

        Task<(bool IsSuccess, string? ErrorMessage)> UpdateWorkOrderAsync(WorkOrderModel WorkOrder);

        Task<(bool IsSuccess, string? ErrorMessage)> DeleteWorkOrderAsync(Guid id);
    }
}
