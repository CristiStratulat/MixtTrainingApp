using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using MixtTrainingApp.Helper;
using System;
using Xamarin.Essentials;
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
        private string pass;
        private IFirebaseRegister auth;
        readonly FireBaseHelperClient firebaseClient = new FireBaseHelperClient();
        public AddProfilePage(string email, string password)
        {
            InitializeComponent();
            mail = email;
            pass = password;
            auth = DependencyService.Get<IFirebaseRegister>();
        }
        
        private async void SfButton_Clicked(object sender, EventArgs e)
        {
            FirstNameError.IsVisible = false;
            LastNameError.IsVisible = false;
            DatePickerError.IsVisible = false;
            HeightEntryError.IsVisible = false;
            WeightEntryError.IsVisible = false;
            SexPickerError.IsVisible = false;
            bool ok = true;
            if (String.IsNullOrEmpty(FirstNameEntry.Text) || String.IsNullOrWhiteSpace(FirstNameEntry.Text))
            {
                ok = false;
                FirstNameError.IsVisible = true;
            }
            if (ok && (String.IsNullOrWhiteSpace(LastNameEntry.Text) || String.IsNullOrEmpty(LastNameEntry.Text)))
            {
                ok = false;
                LastNameError.IsVisible = true;
            }
            if (ok && Birthday.Date == DateTime.Today)
            {
                ok = false;
                DatePickerError.IsVisible = true;
            }
            if (ok && (String.IsNullOrWhiteSpace(HeightEntry.Text) || String.IsNullOrEmpty(HeightEntry.Text)))
            {
                ok = false;
                HeightEntryError.IsVisible = true;
            }
            if (ok && (String.IsNullOrWhiteSpace(WeightEntry.Text) || String.IsNullOrEmpty(WeightEntry.Text)))
            {
                ok = false;
                WeightEntryError.IsVisible = true;
            }
            if (ok && Sex.SelectedItem == null)
            {
                ok = false;
                SexPickerError.IsVisible = true;
            }
            if (ok)
            {
                string Token = "";
                bool connection = true;
                try
                {
                    Token = await auth.RegisterWithEmailAndPassword(mail, pass);
                }
                catch
                {
                    connection = false;
                }
                if (connection)
                {
                    if (!String.IsNullOrEmpty(Token) && Token != "exsisting" && Token != "not")
                    {
                        AddUserDetails(mail, FirstNameEntry.Text, LastNameEntry.Text, ageCalculator(Birthday.Date), Convert.ToInt32(HeightEntry.Text), Convert.ToInt32(WeightEntry.Text), Sex.SelectedItem.ToString());
                        App.UserUID = "";
                        App.Current.Properties["App.UserUID"] = "";
                        await App.Current.SavePropertiesAsync();
                        await DisplayAlert("Congratulations", "Your account has been created", "OK");
                        Preferences.Set("Conditioning", 1);
                        App.Current.MainPage = new NavigationPage(new SimpleLoginPage());
                    }
                    else if (Token == "existing")
                    {
                        await DisplayAlert("Attention", "An account using this email already exists", "OK");
                    }
                }
            }
        }
        private async void AddUserDetails(string Email, string FirstName, string LastName, int Age, int Height, int Weight, string Sex)
        {
            await firebaseClient.AddClient(App.UserUID, Email, FirstName, LastName, Age, Height, Weight, Sex);
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