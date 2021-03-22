using System.Threading.Tasks;

namespace MixtTrainingApp
{
    public interface IFirebaseAuthenticaton
    {
        Task<string> LoginWithEmailAndPassword(string email, string password);
        bool IsSignIn();
        Task SignOut();
    }
}
