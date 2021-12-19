using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Design2WorkroomApi.Models
{
    public class ClientModel : AppUserBase
    {
        public ClientModel(string userName, string b2CObjectId) : base(userName, b2CObjectId)
        {
        }
    }
}
