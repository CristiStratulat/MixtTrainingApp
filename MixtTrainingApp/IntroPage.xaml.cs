using System;
using MixtTrainingApp.Views.Login;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MixtTrainingApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IntroPage : ContentPage
    {
        public IntroPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SimpleLoginPage());
        }
    }
}