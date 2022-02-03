using Design2WorkroomApi.Data;
using Design2WorkroomApi.DTOs;
using Design2WorkroomApi.Models;
using Design2WorkroomApi.Repository.Contracts;

namespace Design2WorkroomApi.Repository
{
    public class WorkOrdersRepository : IWorkOrdersRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public WorkOrdersRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<(bool IsSuccess, Guid? Id, string? ErrorMessage)> CreateWorkOrderAsync(WorkOrderModel WorkOrder)
        {
            try
            {
                await _dbContext.WorkOrders.AddAsync(WorkOrder);
                await _dbContext.SaveChangesAsync();
                return (true, WorkOrder.Id, null);
            }
            catch (Exception ex)
            {
                return (false, Guid.Empty, null);
            }
        }

        public async Task<(bool IsSuccess, WorkOrderModel? WorkOrder, string? ErrorMessage)> GetWorkOrderByIdAsync(Guid id)
        {
            try
            {
                var WorkOrder = _dbContext.WorkOrders.Where(a => a.Id == id).FirstOrDefault();

                if (WorkOrder is not null)
                {
                    return (true, (WorkOrderModel)WorkOrder, null);
                }
                return (false, null, "No work order found");
            }
            catch(Exception ex)
            {
                return (false, null, "Error");
            }
        }

        public async Task<(bool IsSuccess, List<WorkOrderModel>? WorkOrders, string? ErrorMessage)> GetAllWorkOrdersAsync()
        {
            try
            {
                var WorkOrders = _dbContext.WorkOrders.ToList();

                if (WorkOrders is not null)
                {
                    return (true, (List<WorkOrderModel>)WorkOrders, null);
                }
                return (false, null, "No work order found");
            }
            catch (Exception ex)
            {
                return (false, null, "Error");
            }
        }

        public async Task<(bool IsSuccess, string? ErrorMessage)> UpdateWorkOrderAsync(WorkOrderModel WorkOrder)
        {
            try
            {
                _dbContext.WorkOrders.Update(WorkOrder);
                await _dbContext.SaveChangesAsync();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, string? ErrorMessage)> DeleteWorkOrderAsync(Guid id)
        {
            try
            {
                WorkOrderModel WorkOrder = new WorkOrderModel();
                WorkOrder = _dbContext.WorkOrders.Where(a => a.Id == id).FirstOrDefault();
                
                List<WorkOrderItemModel> WorkOrderItem = new List<WorkOrderItemModel>();
                WorkOrderItem = _dbContext.WorkOrderItems.Where(a => a.WorkOrderId == id).ToList();

                _dbContext.WorkOrderItems.RemoveRange(WorkOrderItem);
                _dbContext.WorkOrders.Remove(WorkOrder);
                
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
