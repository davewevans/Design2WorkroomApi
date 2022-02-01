using Design2WorkroomApi.Data;
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

        public async Task<(bool IsSuccess, string? ErrorMessage)> CreateWorkOrderAsync(WorkOrderModel WorkOrder)
        {
            try
            {
                await _dbContext.WorkOrders.AddAsync(WorkOrder);
                await _dbContext.SaveChangesAsync();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, WorkOrderModel? Client, string? ErrorMessage)> GetWorkOrderByIdAsync(Guid id)
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
    }
}
