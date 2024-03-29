﻿using Design2WorkroomApi.Data;
using Design2WorkroomApi.DTOs;
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
        public async Task<(bool IsSuccess, string? AppUserRole,string? UserId, string? ErrorMessage)> GetAppRolesAsync(string email,string objectId)
        {
            var user = await _dbContext.AppUsers
                .Include(x => x.Profile)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserName == email && x.B2CObjectId == objectId);

            if (user is not null)
            {
                var userRole = user.AppUserRole.ToString();
                var user_Id = user.Id;
                return (true, userRole, user_Id.ToString(), null);
            }
            else
            {
                var data = await _dbContext.AppUsers.Where(a => a.UserName == email).FirstOrDefaultAsync();
                if(data is not null)
                {
                    data.B2CObjectId = objectId;

                    _dbContext.Update(data);

                    await _dbContext.SaveChangesAsync();

                    var userRole = data.AppUserRole.ToString();
                    var user_Id = data.Id;
                    return (true, userRole, user_Id.ToString(), null);
                }
                
            }
            return (false, null,"", "No designer found");
        }

        public async Task<(bool IsSuccess, User? userData, string? ErrorMessage)> UserExistsAsync(string email)
        {
            try
            {
                var _user = await _dbContext.AppUsers
                .Include(x => x.Profile)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserName == email);
                if(_user != null)
                {
                    var userData = new User();
                    userData.Id = _user.Id;
                    userData.UserName = _user.UserName;
                    userData.B2CObjectId = _user.B2CObjectId;
                    userData.Profile = _user.Profile;
                    userData.AppUserRole = _user.AppUserRole;
                    return (true, userData, null);
                }
                return (true, null, "No designer Found");
            }
            catch (Exception ex)
            {
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, User? userData, string? ErrorMessage)> updateUserObjectId(User user)
        {
            try
            {
                var _user = await _dbContext.AppUsers
                .Include(x => x.Profile)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserName == user.UserName);

                if (user is not null)
                {
                    _user.B2CObjectId = user.B2CObjectId;
                    _dbContext.AppUsers.Update(_user);
                    await _dbContext.SaveChangesAsync();
                    var userData = new User();
                    userData.Id = _user.Id;
                    userData.UserName = _user.UserName;
                    userData.B2CObjectId = _user.B2CObjectId;
                    userData.Profile = _user.Profile;
                    userData.AppUserRole = _user.AppUserRole;
                    if (userData != null)
                    {
                        return (true, userData, null);
                    }

                }
                return (false, null, "No User found");
            }
            catch (Exception ex)
            {
                return (false, null, ex.ToString());
            }
        }

        public async Task<(bool IsSuccess, User? userData, string? ErrorMessage)> GetAppRolesByobjectId(string objectId)
        {
            var user = await _dbContext.AppUsers
                .Include(x => x.Profile)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.B2CObjectId == objectId);

            if (user is not null)
            {
                var userData = new User();
                userData.Id = user.Id;
                userData.UserName = user.UserName;
                userData.B2CObjectId = user.B2CObjectId;
                userData.Profile = user.Profile;
                userData.AppUserRole = user.AppUserRole;
                if (userData != null)
                {
                    return (true, userData, null);
                }

            }
            return (false, null, "No User found");
        }
    }
}
