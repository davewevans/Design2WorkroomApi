using System.Linq.Expressions;
using Design2WorkroomApi.Data;
using Design2WorkroomApi.Enums;
using Design2WorkroomApi.Models;
using Design2WorkroomApi.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using TinifyAPI;

namespace Design2WorkroomApi.Repository
{
    public class DesignConceptRepository : IDesignConceptRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public DesignConceptRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<(bool IsSuccess, DesignConceptModel? DesignConcept, string? ErrorMessage)> GetDesignConceptByIdAsync(Guid id)
        {
            var designConcept = await _dbContext.DesignConcepts
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (designConcept is not null)
            {
                return (true, (DesignConceptModel)designConcept, null);
            }
            return (false, null, "No design concept found");
        }

        public async Task<(bool IsSuccess, List<DesignConceptModel>? DesignConcepts, string? ErrorMessage)> GetAllDesignConceptsAsync()
        {
            try
            {
                var designConcepts = await _dbContext.DesignConcepts
                    .Include(x => x.DesignConceptsApproval)
                    .AsNoTracking()
                    .ToListAsync();

                if (designConcepts.Any())
                {
                    return (true, designConcepts, null);
                }
                return (false, null, "No design concepts found");
            }
            catch (Exception ex)
            {
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, List<DesignConceptModel>? DesignConcepts, string? ErrorMessage)> GetDesignConceptsByConditionAsync(Expression<Func<DesignConceptModel, bool>> expression)
        {
            try
            {
                var designConcepts = await _dbContext.DesignConcepts
                    .Where(expression)
                    .AsNoTracking()
                    .ToListAsync();

                if (designConcepts.Any())
                {
                    return (true, designConcepts, null);
                }
                return (false, null, "No design concepts found");
            }
            catch (Exception ex)
            {
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, string? ErrorMessage)> CreateDesignConceptAsync(DesignConceptModel designConcept)
        {
            try
            {
                await _dbContext.DesignConcepts.AddAsync(designConcept);
                await _dbContext.SaveChangesAsync();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, string? ErrorMessage)> UpdateDesignConceptsAsync(DesignConceptModel designConcept)
        {
            try
            {
                _dbContext.DesignConcepts.Update(designConcept);
                await _dbContext.SaveChangesAsync();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, bool Exists, string? ErrorMessage)> DesignConceptExistsAsync(Guid id)
        {
            try
            {
                var exists = await _dbContext.DesignConcepts.AnyAsync(x => x.Id.Equals(id));
                return (true, exists, null);
            }
            catch (Exception ex)
            {
                return (false, false, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, string? ErrorMessage)> DeleteDesignConceptAsync(Guid id)
        {
            try
            {
                var recordToDelete = await _dbContext.DesignConcepts.FirstOrDefaultAsync(x => x.Id == id);
                if (recordToDelete is null) return (false, "Design concept not found");
                _dbContext.DesignConcepts.Remove(recordToDelete);
                await _dbContext.SaveChangesAsync();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, string? ErrorMessage)> CreateDesignConceptsApprovalAsync(DesignConceptsApprovalModel designConceptsApproval)
        {
            try
            {
                await _dbContext.DesignConceptsApprovals.AddAsync(designConceptsApproval);
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
