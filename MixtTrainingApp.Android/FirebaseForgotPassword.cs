using Firebase.Auth;
using MixtTrainingApp.Droid;
using MixtTrainingApp.Helpers;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(FirebaseForgotPassword))]
namespace MixtTrainingApp.Droid
{
    class FirebaseForgotPassword :IFirebaseForgotPassword
    {
        public async Task<string> ForgotPassword(string email)
        {
            try
            {
                await FirebaseAuth.Instance.SendPasswordResetEmailAsync(email);
                return "emailSent";
            }
            catch (Exception ex)
            {
                if (ex is FirebaseAuthInvalidUserException)
                {
                    return "emailNotFound";
                }
                else
                {
                    return "";
                }
            }
        }
    }
}