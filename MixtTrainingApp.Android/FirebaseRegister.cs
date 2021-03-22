using System;
using System.Threading.Tasks;
using Firebase.Auth;
using Xamarin.Forms;
using MixtTrainingApp.Droid;
[assembly: Dependency(typeof(FirebaseRegister))]
namespace MixtTrainingApp.Droid
{
    public class FirebaseRegister :IFirebaseRegister
    {
        public async Task<string> RegisterWithEmailAndPassword(string email, string password)
        {
            try
            {
                var user = await FirebaseAuth.Instance.CreateUserWithEmailAndPasswordAsync(email, password);
                return user.User.Uid;
            }
            catch (FirebaseAuthWeakPasswordException)
            {
                return "not";
            }
            catch (FirebaseAuthUserCollisionException)
            {
                return "exsisting";
            }
            catch (Exception)
            {
                return "";
            }

        }
    }
}