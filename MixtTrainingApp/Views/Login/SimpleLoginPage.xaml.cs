using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using System;
using MixtTrainingApp.Views.SignUp;

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
                string token = await auth.LoginWithEmailAndPassword(EmailEntry.Text, PasswordEntry.Text);
                if (token != String.Empty)
                    await Navigation.PushAsync(new MainPage());
                else
                    ShowError();
            }
            else
                await DisplayAlert("No Internet Connection", "No active Internet Connection Detected", "Ok");
        }

        private async void ShowError()
        {
            await DisplayAlert("Authentication Failed","Wrong Email or Password", "OK");
        }

        private async void SfButton_Clicked_1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SimpleSignUpPage());
        }
    }
}