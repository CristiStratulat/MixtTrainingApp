using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using MixtTrainingApp.Droid;
using Firebase.Auth;
using Android.Gms.Extensions;

[assembly: Dependency(typeof(FirebaseAuthentication))]
namespace MixtTrainingApp.Droid
{
    public class FirebaseAuthentication : IFirebaseAuthenticaton
    {
        public bool IsSignIn()
        {
            var user = FirebaseAuth.Instance.CurrentUser;
            return user != null;
        }
        public async Task<string> LoginWithEmailAndPassword(string email, string password)
        {
            try
            {
                var user = await FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(email, password);
                var token = await user.User.GetIdToken(false);
                return user.User.Uid;
                
            }
            catch (Exception ex)
            {
                if (ex is FirebaseAuthInvalidUserException)
                {
                    return "noUserFound";
                }
                else
                {
                    return "";
                }
            }
        }
        public bool SignOut()
        {
            try
            {
                FirebaseAuth.Instance.SignOut();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}