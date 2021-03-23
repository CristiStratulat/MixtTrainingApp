using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using System;
using Xamarin.Forms;
using System.Net.Mail;
using MixtTrainingApp.Views.Login;
using MixtTrainingApp.Views.P;
using MixtTrainingApp.Helper;
namespace MixtTrainingApp.Views.SignUp
{
    /// <summary>
    /// Page to sign in with user details.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SimpleSignUpPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleSignUpPage" /> class.
        /// </summary>
        IFirebaseRegister auth;
        readonly FireBaseHelperClient firebaseClient = new FireBaseHelperClient();
        public SimpleSignUpPage()
        {
            InitializeComponent();
            auth = DependencyService.Get<IFirebaseRegister>();
        }

        private async void SfButton_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PopAsync();
        }
        private async void Register(object sender, System.EventArgs e)
        {
            bool ok = true;
            if (App.CheckConnection())
            {
                //NameError.IsVisible = false;
                EmailError.IsVisible = false;
                PasswordError.IsVisible = false;
                ConfirmError.IsVisible = false;

               /* if (String.IsNullOrEmpty(NameEntry.Text) || String.IsNullOrWhiteSpace(NameEntry.Text))
                {
                    NameError.IsVisible = true;
                    ok = false;
                }*/

                if (ok && (String.IsNullOrEmpty(SignUpEmailEntry.Text) || String.IsNullOrWhiteSpace(SignUpEmailEntry.Text)))
                {
                    EmailError.IsVisible = true;
                    ok = false;
                }
                if (ok)
                {
                    string email = SignUpEmailEntry.Text.Trim();
                    if (!IsValidEmail(email))
                    {
                        EmailError.IsVisible = true;
                        ok = false;
                    }
                }
                if( ok && (String.IsNullOrEmpty(PasswordEntry.Text)|| String.IsNullOrWhiteSpace(PasswordEntry.Text)))
                {
                    ok = false;
                    PasswordError.IsVisible = true;
                }
                if (ok && !passwordMatch(PasswordEntry.Text, ConfirmPasswordEntry.Text))
                {
                    ok = false;
                    ConfirmError.IsVisible = true;
                }

                if (ok)
                {
                    await Navigation.PushAsync(new AddProfilePage(SignUpEmailEntry.Text,PasswordEntry.Text));
                }
            }
            else
                await DisplayAlert("No Internet Connection","No active Internet Connection Detected","Ok");

        }
        private bool passwordMatch (string password1, string password2)
        {
            return password1 == password2;
        }
        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch (FormatException)
            {
                return false;
            }
        }
       
    }
}