using Design2WorkroomApi.Models;

namespace Design2WorkroomApi.Repository.Contracts
{
    public interface IClientDesignRepository
    {
        Task<(bool IsSuccess, string? ErrorMessage)> CreateClientDesignAsync(ClientDesignModel design);

        Task<(bool IsSuccess, ClientDesignModel? ClientDesign, string? ErrorMessage)> GetClientDesignByIdAsync(Guid Id);

        Task<(bool IsSuccess, List<ClientDesignModel>? ClientDesigns, string? ErrorMessage)> GetAllClientDesignsAsync();
    }
}
