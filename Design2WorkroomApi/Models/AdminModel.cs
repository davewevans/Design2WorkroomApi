using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Design2WorkroomApi.Models
{
    public class AdminModel : AppUserBase
    {
        public AdminModel(string userName, string b2CObjectId) : base(userName, b2CObjectId)
        {
        }
    }
}
