using Design2WorkroomApi.Models;

namespace Design2WorkroomApi.Repository.Contracts
{
    public interface IInvitationRepository
    {
        Task<(bool IsSuccess, Guid InvitationId)> CreateUserWithInvitationAsync(InvitationModel modal);

        Task<(bool IsSuccess, InvitationModel? Invitation, string? ErrorMessage)> GetInvitationByIdAsync(Guid id);

        Task<(bool IsSuccess, Guid InvitationId)> UpdateUserWithInvitationAsync(InvitationModel modal);
    }
}
