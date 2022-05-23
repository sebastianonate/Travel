using System.Threading.Tasks;
using Travel.Core.Application.DTOs.Mail;

namespace Travel.Core.Application.Interfaces.Shared
{
    public interface IMailService
    {
        Task SendAsync(MailRequest request);
    }
}