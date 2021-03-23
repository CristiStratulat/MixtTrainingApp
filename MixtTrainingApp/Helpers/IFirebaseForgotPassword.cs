using System;
using System.Threading.Tasks;

namespace MixtTrainingApp.Helpers
{
    public interface IFirebaseForgotPassword
    {
        Task<String> ForgotPassword(string email);
    }
}
