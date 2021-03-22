using Firebase.Database;
using MixtTrainingApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Database.Query;
using System;
namespace MixtTrainingApp.Helper
{
    class FireBaseHelperClient
    {
        readonly FirebaseClient firebaseClient = new FirebaseClient(App.conf.firebase);
        private readonly string UserType="Client";
        /*public async Task<List<Client>> GetAllClients()
        {
           
        }
        */
        public async Task AddClient(string UID, string email, string firstName, string lastName, int age, int height, int weight, string sex)
        {
            await firebaseClient.Child(UserType).Child(UID).PostAsync(new Client()
            {
                Email=email,
                FirstName=firstName,
                LastName=lastName,
                Age=age,
                Height=height,
                Weight=weight,
                Sex=sex,
            });
        }

    }
}
