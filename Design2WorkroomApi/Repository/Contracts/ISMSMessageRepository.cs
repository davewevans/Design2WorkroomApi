using Design2WorkroomApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Design2WorkroomApi.Repository.Contracts
{
    public interface ISMSMessageRepository
    {
        ICollection<SmsMessageModel> GetSMSMessages();

        Task<ICollection<SmsMessageModel>> GetSMSMessagesAsync();

        ICollection<SmsMessageModel> GetSMSMessagesByCondition(Expression<Func<SmsMessageModel, bool>> expression);

        Task<ICollection<SmsMessageModel>> GetSMSMessagesByConditionAsync(Expression<Func<SmsMessageModel, bool>> expression);

        SmsMessageModel GetSMSMessageById(int SMSMessageId);

        Task<SmsMessageModel> GetSMSMessageByIdAsync(int SMSMessageId);

        bool SMSMessageExists(int id);

        Task<bool> SMSMessageExistsAsync(int id);

        bool CreateSMSMessage(SmsMessageModel SMSMessage);

        Task<bool> CreateSMSMessageAsync(SmsMessageModel SMSMessage);

        bool UpdateSMSMessage(SmsMessageModel SMSMessage);

        Task<bool> UpdateSMSMessageAsync(SmsMessageModel SMSMessage);

        bool DeleteSMSMessage(int SMSMessageId);

        Task<bool> DeleteSMSMessageAsync(int SMSMessageId);

        bool Save();

        Task<bool> SaveAsync();
    }
}
