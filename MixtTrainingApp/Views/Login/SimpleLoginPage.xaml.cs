using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using System;
using System.Net.Mail;
using MixtTrainingApp.Views.SignUp;
using MixtTrainingApp.Views.ForgotPassword;
namespace MixtTrainingApp.Views.Login
{
    /// <summary>
    /// Page to login with user name and password
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SimpleLoginPage
    {
        IFirebaseAuthenticaton auth;
        public ICommand LoginCommand;
        public SimpleLoginPage()
        {
            InitializeComponent();
            auth =DependencyService.Get<IFirebaseAuthenticaton>();
        }

        public async void SfButton_Clicked(object sender, EventArgs e)
        {
            if (App.CheckConnection())
            {
                EmailError.IsVisible = false;
                PasswordError.IsVisible = false;
                bool ok = true;
                string userEmail = "";
                //  Checks the Email Entry if empty or null 
                if (!string.IsNullOrEmpty(EmailEntry.Text) && !string.IsNullOrWhiteSpace(EmailEntry.Text))
                {
                    userEmail = EmailEntry.Text.Trim();
                // Checks if the email format is a valid format
                    if(!IsValidEmail(userEmail))
                    {
                        ok = false;
                        EmailError.IsVisible = true;
                    }
                }
                else
                {
                    EmailError.IsVisible = true;
                    ok = false;
                }
                // If email field is validated checks the password entry
                if(ok)
                {
                    // Checks if the email field is empty or null 
                    if (string.IsNullOrEmpty(PasswordEntry.Text) || string.IsNullOrWhiteSpace(PasswordEntry.Text))
                    {
                        ok = false;
                        PasswordError.IsVisible = true;
                    }
                }
                // If email and password entry work, try to log in 
                if(ok) 
                {
                    App.UserUID = await auth.LoginWithEmailAndPassword(EmailEntry.Text, PasswordEntry.Text);

                    if (auth.IsSignIn() && !string.IsNullOrEmpty(App.UserUID) && !string.IsNullOrWhiteSpace(App.UserUID))
                    {
                        App.Current.MainPage = new NavigationPage(new MainPage());
                        Application.Current.Properties["App.UserUID"] = App.UserUID;
                        await App.Current.SavePropertiesAsync();

                    }
                    else
                    {
                        App.UserUID = "";
                        App.Current.Properties["App.UserUID"] = "";
                        await App.Current.SavePropertiesAsync();
                        await DisplayAlert("Login Failed", "Invalid email or password", "OK");
                    }
                }
            }
            else
                await DisplayAlert("No Internet Connection", "No active Internet Connection Detected", "Ok");
        }
        bool IsValidEmail (string email)
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
        private async void ShowError()
        {
            await DisplayAlert("Authentication Failed","Wrong Email or Password", "OK");
        }

        private async void SfButton_Clicked_1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SimpleSignUpPage());
        }
        
        private async void ForgotPassword (object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SimpleForgotPasswordPage());
        }
    }
}