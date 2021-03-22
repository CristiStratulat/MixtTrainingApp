using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using MixtTrainingApp.Helper;
using System;
using MixtTrainingApp.Views.Login;
namespace MixtTrainingApp.Views.P
{
    /// <summary>
    /// This page used for adding Profile image with Name and phone number.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddProfilePage : ContentPage
    {
        private string mail;
        readonly FireBaseHelperClient firebaseClient = new FireBaseHelperClient();
        public AddProfilePage(string email)
        {
            InitializeComponent();
            mail = email;
        }

        private async void SfButton_Clicked(object sender, System.EventArgs e)
        {
            AddUserDetails(mail,FirstNameEntry.Text,LastNameEntry.Text,ageCalculator(Birthday.Date),Convert.ToInt32(HeightEntry.Text),Convert.ToInt32(WeightEntry.Text),Sex.SelectedItem.ToString(),PhoneNoEntry.Text);
            App.UserUID = "";
            App.Current.Properties["App.UserUID"] = "";
            await App.Current.SavePropertiesAsync();
            await DisplayAlert("Congratulations", "Your account has been created", "OK");
            App.Current.MainPage = new NavigationPage(new SimpleLoginPage());
        }
        private async void AddUserDetails(string Email, string FirstName, string LastName, int Age, int Height, int Weight, string Sex, string phonenumber)
        {
            await firebaseClient.AddClient(App.UserUID, Email, FirstName, LastName, Age, Height, Weight, Sex, phonenumber);
        }
        private int ageCalculator (DateTime date)
        {
            int year = Convert.ToInt32(DateTime.Today.Year - date.Year);
            if (DateTime.Today.Month < date.Month)
                year--;
            else if (DateTime.Today.Month == date.Month)
            {
                if (DateTime.Today.Day < date.Day)
                    year--;

            }
            return year;
        }
    }
}