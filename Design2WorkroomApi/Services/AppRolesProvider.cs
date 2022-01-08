using Design2WorkroomApi.Data;
using Design2WorkroomApi.Models;
using Design2WorkroomApi.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Design2WorkroomApi.Services
{
    public class AppRolesProvider : IAppRolesProvider
    {
        private readonly ApplicationDbContext _dbContext;

        public AppRolesProvider(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<(bool IsSuccess, string? AppUserRole, string? ErrorMessage)> GetAppRolesAsync(string email,string objectId)
        {
            var designer = await _dbContext.AppUsers
                .Include(x => x.Profile)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserName == email && x.B2CObjectId == objectId);

            if (designer is not null)
            {
                var designerData = designer as DesignerModel;
                if (designerData != null)
                {
                    var userRole = designerData.AppUserRole.ToString();
                    return (true, userRole, null);
                }
                
            }
            return (false, null, "No designer found");
        }
    }
}
