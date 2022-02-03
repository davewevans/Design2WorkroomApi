using Design2WorkroomApi.Data;
using Design2WorkroomApi.Models;
using Design2WorkroomApi.Repository.Contracts;

namespace Design2WorkroomApi.Repository
{
    public class ClientDesignRepository : IClientDesignRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ClientDesignRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<(bool IsSuccess, string? ErrorMessage)> CreateClientDesignAsync(ClientDesignModel design)
        {
            try
            {
                await _dbContext.ClientDesigns.AddAsync(design);
                await _dbContext.SaveChangesAsync();

                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, "Error adding Client Design!");
            }
        }

        public async Task<(bool IsSuccess, ClientDesignModel? ClientDesign, string? ErrorMessage)> GetClientDesignByIdAsync(Guid Id)
        {
            try
            {
                ClientDesignModel design = _dbContext.ClientDesigns.Where(a => a.Id == Id).FirstOrDefault();

                if (design == null)
                {
                    return (false, design, null);
                }

                return (true, design, null);
            }
            catch(Exception ex)
            {
                return (false, null, "Error");
            }
        }

        public async Task<(bool IsSuccess, List<ClientDesignModel>? ClientDesigns, string? ErrorMessage)> GetAllClientDesignsAsync()
        {
            try
            {
                List<ClientDesignModel> ClientDesigns = new List<ClientDesignModel>();
                ClientDesigns = _dbContext.ClientDesigns.ToList();

                return (true, ClientDesigns, null);
            }
            catch(Exception ex)
            {
                return (true, new List<ClientDesignModel>(), null);
            }
        }
    }
}