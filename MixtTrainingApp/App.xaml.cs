using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MixtTrainingApp.Views.Login;
namespace MixtTrainingApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mzg5NDg4QDMxMzgyZTM0MmUzMEs0T2F5QXBTMEg4UDVhek9GejM3YkhBeDRDV01ucytsdW5JSzlScWhFZEU9");
            MainPage = new NavigationPage(new IntroPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
        
    }
}
