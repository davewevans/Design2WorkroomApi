using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Design2WorkroomApi.Models
{
    [Table(name: "DesignSpecifications")]
    public class DesignSpecificationModel : Entity
    {
        [ForeignKey(nameof(DesignerModel))]
        public Guid DesignerId { get; set; }

        public DesignerModel Designer { get; set; } = null!;
    }
}
