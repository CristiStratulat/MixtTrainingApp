using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using System;

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
            string token = await auth.LoginWithEmailAndPassword(EmailEntry.Text, PasswordEntry.Text);
            if (token != String.Empty)
                await Navigation.PushAsync(new MainPage());
            else
                ShowError();
        }

        private async void ShowError()
        {
            await DisplayAlert("Authentication Failed","Wrong Email or Password", "OK");
        }
    }
}