using Design2WorkroomApi.Models;
using Design2WorkroomApi.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Design2WorkroomApi.Repository
{
    public class SMSMessageRepository : ISMSMessageRepository
    {
        public bool CreateSMSMessage(SmsMessageModel SMSMessage)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateSMSMessageAsync(SmsMessageModel SMSMessage)
        {
            throw new NotImplementedException();
        }

        public bool DeleteSMSMessage(int SMSMessageId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteSMSMessageAsync(int SMSMessageId)
        {
            throw new NotImplementedException();
        }

        public SmsMessageModel GetSMSMessageById(int SMSMessageId)
        {
            throw new NotImplementedException();
        }

        public Task<SmsMessageModel> GetSMSMessageByIdAsync(int SMSMessageId)
        {
            throw new NotImplementedException();
        }

        public ICollection<SmsMessageModel> GetSMSMessages()
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<SmsMessageModel>> GetSMSMessagesAsync()
        {
            throw new NotImplementedException();
        }

        public ICollection<SmsMessageModel> GetSMSMessagesByCondition(Expression<Func<SmsMessageModel, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<SmsMessageModel>> GetSMSMessagesByConditionAsync(Expression<Func<SmsMessageModel, bool>> expression)
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

        public bool SMSMessageExists(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SMSMessageExistsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateSMSMessage(SmsMessageModel SMSMessage)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateSMSMessageAsync(SmsMessageModel SMSMessage)
        {
            throw new NotImplementedException();
        }
    }
}
