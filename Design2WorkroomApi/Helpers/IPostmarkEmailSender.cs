using Design2WorkroomApi.Models;

namespace Design2WorkroomApi.Helpers
{
    public interface IPostmarkEmailSender
    {
        Task SendEmailPostmarkAsync(PostmarkEmailModel email, List<FileAttachment> fileAttachments = null);

        Task SendTestEmailPostmark();
    }
}
