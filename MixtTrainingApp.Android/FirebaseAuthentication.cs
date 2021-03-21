using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using MixtTrainingApp.Droid;
using Firebase.Auth;

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
                //var token = await user.User.GetIdToken(false);
                return user.User.Uid;

            }
            catch (FirebaseAuthInvalidUserException e)
            {
                e.PrintStackTrace();
                return string.Empty;
            }
            catch (FirebaseAuthInvalidCredentialsException e)
            {
                e.PrintStackTrace();
                return string.Empty;
            }
        }
        public async Task SignOut()
        {
            try
            {
                FirebaseAuth.Instance.SignOut();
                App.Current.Properties.Remove("App.UserUID");
                await App.Current.SavePropertiesAsync();
            }
            catch (Exception)
            {

            }
        }
    }
}