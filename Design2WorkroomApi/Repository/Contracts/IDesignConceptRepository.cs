using System.Linq.Expressions;
using Design2WorkroomApi.Models;

namespace Design2WorkroomApi.Repository.Contracts
{
    public interface IDesignConceptRepository
    {
        Task<(bool IsSuccess, DesignConceptModel? DesignConcept, string? ErrorMessage)> GetDesignConceptByIdAsync(Guid id);

        Task<(bool IsSuccess, List<DesignConceptModel>? DesignConcepts, string? ErrorMessage)> GetAllDesignConceptsAsync();

        Task<(bool IsSuccess, List<DesignConceptModel>? DesignConcepts, string? ErrorMessage)> GetDesignConceptsByConditionAsync(Expression<Func<DesignConceptModel, bool>> expression);

        Task<(bool IsSuccess, string? ErrorMessage)> CreateDesignConceptAsync(DesignConceptModel designConcept);

        Task<(bool IsSuccess, bool Exists, string? ErrorMessage)> DesignConceptExistsAsync(Guid id);

        Task<(bool IsSuccess, string? ErrorMessage)> DeleteDesignConceptAsync(Guid id);
    }
}
