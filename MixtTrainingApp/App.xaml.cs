using Xamarin.Forms;
using Xamarin.Essentials;
using Newtonsoft.Json;
using System.Reflection;
using System.IO;
using System;

namespace MixtTrainingApp
{
    public partial class App : Application
    {
        static public string UserUID { get; set; }
        static public MixtTraining_Configuration conf;
        public App()
        {
            checkPreviousInstall();
            LoadJson();
            InitializeComponent();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(conf.syncfusion);
            MainPage = new NavigationPage(new IntroPage());
        }


        protected override void OnStart()
        {
            Console.WriteLine(conf.syncfusion);
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
        void LoadJson()
        {
            Assembly asm = Assembly.GetAssembly(typeof(MixtTraining_Configuration));
            Stream stream = asm.GetManifestResourceStream("MixtTrainingApp.config.json");
            using (var reader = new StreamReader(stream))
            {

                var json = reader.ReadToEnd();
               conf = JsonConvert.DeserializeObject<MixtTraining_Configuration>(json);
            }
        }
        public static bool CheckConnection()
        {
            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.Internet)
                return true;
            else
                return false;
        }
        void checkPreviousInstall()
        {
            if(!App.Current.Properties.ContainsKey("PreviousInstalled"))
            {
                App.Current.Properties.Clear();
                App.Current.Properties["PreviousInstalled"] = "true";
                App.Current.SavePropertiesAsync();
            }
        }
        
    }
}
