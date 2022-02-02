using System.ComponentModel.DataAnnotations.Schema;

namespace Design2WorkroomApi.Models
{
    [Table("DesignConceptsApprovals")]
    public class DesignConceptsApprovalModel : Entity
    {
        //public DesignConceptsApprovalModel(
        //    Guid clientId,
        //    bool isApproved)
        //{
        //    ClientId = clientId;
        //    IsApproved = isApproved;
        //}
        public Guid ClientId { get; set; }

        public bool IsApproved { get; set; }

        [ForeignKey(nameof(DesignConceptModel))]
        public Guid DesignConceptId { get; set; }

        public DesignConceptModel DesignConcept { get; set; } = null!;

    }
}
