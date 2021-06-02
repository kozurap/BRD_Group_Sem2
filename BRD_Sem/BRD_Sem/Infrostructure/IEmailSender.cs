using System.Threading.Tasks;

namespace BRD_Sem.Infrostructure
{
    public interface IEmailSender
    {
        Task<bool> SendEmailAsync(string email, string subject, string message);
    }
}