using Xamarin.Forms;
using Xamarin.Essentials;
using Newtonsoft.Json;
using System.Reflection;
using System.IO;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using System.Threading.Tasks;
namespace MixtTrainingApp
{
    public partial class App : Xamarin.Forms.Application
    {
        static public string UserUID { get; set; }
        static public MixtTraining_Configuration conf;
        public App()
        {
            checkPreviousInstall();
            LoadJson();
            InitializeComponent();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(conf.syncfusion);
            Xamarin.Forms.Application.Current.On<Xamarin.Forms.PlatformConfiguration.Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
            if (CheckConnection())
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                checkUserLogInAsync();
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
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
        public static void notSignedIn()
        {
            App.Current.MainPage = new NavigationPage(new IntroPage());
            App.Current.Properties.Remove("App.UserUID");
            App.Current.SavePropertiesAsync();
            App.UserUID = "";
        }
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
         async Task checkUserLogInAsync()
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            App.UserUID = App.Current.Properties.ContainsKey("App.UserUID") ? App.Current.Properties["App.UserUID"] as string : "";
            if (!string.IsNullOrEmpty(App.UserUID) && !string.IsNullOrWhiteSpace(App.UserUID) )//user exists
            {
                App.Current.MainPage = new NavigationPage(new MainPage());
            }
            else
            {
                notSignedIn();
            }
        }
        
    }
}
