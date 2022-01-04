﻿using Design2WorkroomApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Design2WorkroomApi.Repository.Contracts
{
    public interface IEmailRepository
    {
        Task<(bool IsSuccess, EmailModel? Email, string? ErrorMessage)> GetEmailByIdAsync(Guid id);

        Task<(bool IsSuccess, List<EmailModel>? Emails, string? ErrorMessage)> GetAllEmailsAsync();

        Task<(bool IsSuccess, List<EmailModel>? Emails, string? ErrorMessage)> GetEmailsByConditionAsync(Expression<Func<EmailModel, bool>> expression);

        Task<(bool IsSuccess, bool Exists, string? ErrorMessage)> EmailExistsAsync(Guid id);

        Task<(bool IsSuccess, string? ErrorMessage)> CreateEmailAsync(EmailModel email);

        Task<(bool IsSuccess, string? ErrorMessage)> UpdateEmailAsync(EmailModel email);

        Task<(bool IsSuccess, string? ErrorMessage)> DeleteEmailAsync(Guid id);
    }
}
