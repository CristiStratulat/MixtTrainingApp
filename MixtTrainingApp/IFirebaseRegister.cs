using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MixtTrainingApp
{
    public interface IFirebaseRegister
    {
        Task<String> RegisterWithEmailAndPassword(string email, string password);
    }
}
