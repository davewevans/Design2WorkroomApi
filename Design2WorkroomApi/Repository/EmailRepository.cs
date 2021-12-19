using Design2WorkroomApi.Data;
using Design2WorkroomApi.Models;
using Design2WorkroomApi.Repository.Contracts;
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

        public EmailModel GetEmailById(int EmailId)
        {
            throw new NotImplementedException();
        }

        public Task<EmailModel> GetEmailByIdAsync(int EmailId)
        {
            throw new NotImplementedException();
        }

        public ICollection<EmailModel> GetEmails()
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<EmailModel>> GetEmailsAsync()
        {
            throw new NotImplementedException();
        }

        public bool CreateEmail(EmailModel Email)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateEmailAsync(EmailModel Email)
        {
            throw new NotImplementedException();
        }

        public bool DeleteEmail(int EmailId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteEmailAsync(int EmailId)
        {
            throw new NotImplementedException();
        }

        public bool EmailExists(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EmailExistsAsync(int id)
        {
            throw new NotImplementedException();
        }       

        public ICollection<EmailModel> GetEmailsByCondition(Expression<Func<EmailModel, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<EmailModel>> GetEmailsByConditionAsync(Expression<Func<EmailModel, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveAsync()
        {
            throw new NotImplementedException();
        }

        public bool UpdateEmail(EmailModel Email)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateEmailAsync(EmailModel Email)
        {
            throw new NotImplementedException();
        }
    }
}
