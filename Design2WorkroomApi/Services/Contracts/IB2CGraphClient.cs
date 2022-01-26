﻿using Design2WorkroomApi.Models;
using Microsoft.Graph;

namespace Design2WorkroomApi.Services.Contracts
{
    public interface IB2CGraphClient
    {
        Task<(bool IsSuccess, string? userObjectId, string? ErrorMessage)> CreateUser(Microsoft.Graph.User User);
    }
}
