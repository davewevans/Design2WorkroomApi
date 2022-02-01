using Design2WorkroomApi.Models;

namespace Design2WorkroomApi.Repository.Contracts
{
    public interface IWorkOrdersRepository
    {
        Task<(bool IsSuccess, string? ErrorMessage)> CreateWorkOrderAsync(WorkOrderModel WorkOrder);

        Task<(bool IsSuccess, WorkOrderModel? Client, string? ErrorMessage)> GetWorkOrderByIdAsync(Guid id);
    }
}
