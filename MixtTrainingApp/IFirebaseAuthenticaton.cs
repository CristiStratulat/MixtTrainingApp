using System.Threading.Tasks;

namespace MixtTrainingApp
{
    public interface IFirebaseAuthenticaton
    {
        Task<string> LoginWithEmailAndPassword(string email, string password);
        bool SignOut();
        bool IsSignIn();
    }
}
