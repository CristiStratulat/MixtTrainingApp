using Firebase.Database;
using Firebase.Storage;
using Xamarin.Essentials;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MixtTrainingApp.Views.LoadedPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WorkoutPage : ContentPage
    {
        public int contor;
        FirebaseStorage firebaseStorage = new FirebaseStorage("mixttraining-2213f.appspot.com");
        FirebaseClient firebaseClient = new FirebaseClient("https://mixttraining-2213f-default-rtdb.firebaseio.com/");
        public WorkoutPage(string program)
        {
            InitializeComponent();
            
           Debug.Write(contor+"--------------------");
            Load(program);
        }
        public async Task<string> GetFile (string x)
        {
            contor = Preferences.Get(x,1);
            return await firebaseStorage
                .Child("Workouts")
                .Child(x)
                .Child(contor.ToString()+".jpg")
                .GetDownloadUrlAsync();
        }
        private async void Load (string p)
        {
            string path = await GetFile(p);
            if (path != null)
                Imagine.Source = ImageSource.FromUri(new Uri(path));
               
        }
        /**private async void Week()
        {
            contor = Convert.ToInt32(await firebaseClient.Child("Client/-MZDZKVOeZnfz7lhkrWh/CurrentWeek").OnceSingleAsync<int>());
            
        }*/
        private void Button_Clicked(object sender, EventArgs e)
        {
            contor = Preferences.Get("Conditioning", 1);
            contor++;
            if (contor == 3)
                contor = 1;
            Preferences.Set("Conditioning", contor);
           
        }
    }
}