using Design2WorkroomApi.Data;
using Design2WorkroomApi.Enums;
using Design2WorkroomApi.Models;
using Design2WorkroomApi.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Design2WorkroomApi.Repository
{
    public class EmailRepository : IEmailRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public EmailRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<(bool IsSuccess, EmailModel? Email, string? ErrorMessage)> GetEmailByIdAsync(Guid id)
        {
            var email = await _dbContext.Emails
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (email is not null)
            {
                return (true, email, null);
            }
            return (false, null, "No email found");
        }

        public async Task<(bool IsSuccess, List<EmailModel>? Emails, string? ErrorMessage)> GetAllEmailsAsync()
        {
            try
            {
                var emails = await _dbContext.Emails
                    .Select(x => (EmailModel)x)
                    .AsNoTracking()
                    .ToListAsync();

                if (emails.Any())
                {
                    return (true, emails, null);
                }
                return (false, null, "No emails found");
            }
            catch (Exception ex)
            {
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, List<EmailModel>? Emails, string? ErrorMessage)> GetEmailsByConditionAsync(Expression<Func<EmailModel, bool>> expression)
        {
            try
            {
                var emails = await _dbContext.Emails
                    .Where(expression)
                    .Select(x => x)
                    .AsNoTracking()
                    .ToListAsync();

                if (emails.Any())
                {
                    return (true, emails, null);
                }
                return (false, null, "No emails found");
            }
            catch (Exception ex)
            {
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, bool Exists, string? ErrorMessage)> EmailExistsAsync(Guid id)
        {
            try
            {
                var exists = await _dbContext.Emails.AnyAsync(x => x.Id.Equals(id));
                return (true, exists, null);
            }
            catch (Exception ex)
            {
                return (false, false, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, string? ErrorMessage)> CreateEmailAsync(EmailModel email)
        {
            try
            {
                await _dbContext.Emails.AddAsync(email);
                await _dbContext.SaveChangesAsync();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }


        public async Task<(bool IsSuccess, string? ErrorMessage)> UpdateEmailAsync(EmailModel email)
        {
            try
            {
                _dbContext.Emails.Update(email);
                await _dbContext.SaveChangesAsync();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);

            }
        }

        public async Task<(bool IsSuccess, string? ErrorMessage)> DeleteEmailAsync(Guid id)
        {
            try
            {
                var recordToDelete = await _dbContext.Emails.FirstOrDefaultAsync(x => x.Id == id);
                if (recordToDelete is null) return (false, "Email not found");
                _dbContext.Emails.Remove(recordToDelete);
                await _dbContext.SaveChangesAsync();
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
    }
}
