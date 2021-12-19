using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Design2WorkroomApi.Models
{
    [Table("Reviews")]
    public class ReviewModel : Entity
    {
        public ReviewModel(string? comment = null)
        {
            Comment = comment;
        }
        public int DesignerId { get; set; }

        public int? PlanId { get; set; }

        public int Rating { get; set; }

        public string? Comment { get; set; } 
    }
}
