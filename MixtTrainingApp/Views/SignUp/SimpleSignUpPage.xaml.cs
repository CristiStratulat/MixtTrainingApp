using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using System;
using Xamarin.Forms;

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
            if (App.CheckConnection())
            {
                string Token = await auth.RegisterWithEmailAndPassword(SignUpEmailEntry.Text, PasswordEntry.Text);
                Console.WriteLine(Token);
            }
            else
                await DisplayAlert("No Internet Connection","No active Internet Connection Detected","Ok");

        }
        private bool passwordMatch (string password1, string password2)
        {
            return password1 == password2;
        }
    }
}