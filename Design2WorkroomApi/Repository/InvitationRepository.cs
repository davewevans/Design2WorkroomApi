using Design2WorkroomApi.Data;
using Design2WorkroomApi.Models;
using Design2WorkroomApi.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Design2WorkroomApi.Repository
{
    public class InvitationRepository : IInvitationRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public InvitationRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<(bool IsSuccess, Guid InvitationId)> CreateUserWithInvitationAsync(InvitationModel modal)
        {
            try
            {
                await _dbContext.Invitations.AddAsync(modal);
                await _dbContext.SaveChangesAsync();
                return (true, modal.Id);
            }
            catch (Exception ex)
            {
                return (false, Guid.Empty);
            }
        }

        public async Task<(bool IsSuccess, InvitationModel? Invitation, string? ErrorMessage)> GetInvitationByIdAsync(Guid id)
        {
            InvitationModel invitation = _dbContext.Invitations.Where(a => a.Id == id).FirstOrDefault();

            if (invitation is not null)
            {
                return (true, (InvitationModel)invitation, null);
            }
            return (false, null, "No invitation found");
        }

        public async Task<(bool IsSuccess, Guid InvitationId)> UpdateUserWithInvitationAsync(InvitationModel modal)
        {
            try
            {
                _dbContext.Entry(modal).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                return (true, Guid.Empty);
            }
            catch (Exception ex)
            {
                return (false, Guid.Empty);
            }
        }
    }
}