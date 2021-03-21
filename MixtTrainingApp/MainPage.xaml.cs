using System;
using Xamarin.Forms;

namespace MixtTrainingApp
{
    public partial class MainPage : ContentPage
    {
        IFirebaseAuthenticaton auth;
        public MainPage()
        {
            InitializeComponent();
            auth = DependencyService.Get<IFirebaseAuthenticaton>();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            auth.SignOut();
            App.Current.MainPage = new NavigationPage(new IntroPage());
        }
    }
}
