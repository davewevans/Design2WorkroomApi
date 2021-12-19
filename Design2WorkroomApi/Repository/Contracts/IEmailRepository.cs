using Design2WorkroomApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Design2WorkroomApi.Repository.Contracts
{
    public interface IEmailRepository
    {
        ICollection<EmailModel> GetEmails();

        Task<ICollection<EmailModel>> GetEmailsAsync();

        ICollection<EmailModel> GetEmailsByCondition(Expression<Func<EmailModel, bool>> expression);

        Task<ICollection<EmailModel>> GetEmailsByConditionAsync(Expression<Func<EmailModel, bool>> expression);

        EmailModel GetEmailById(int EmailId);

        Task<EmailModel> GetEmailByIdAsync(int EmailId);

        bool EmailExists(int id);

        Task<bool> EmailExistsAsync(int id);

        bool CreateEmail(EmailModel Email);

        Task<bool> CreateEmailAsync(EmailModel Email);

        bool UpdateEmail(EmailModel Email);

        Task<bool> UpdateEmailAsync(EmailModel Email);

        bool DeleteEmail(int EmailId);

        Task<bool> DeleteEmailAsync(int EmailId);

        bool Save();

        Task<bool> SaveAsync();
    }
}
