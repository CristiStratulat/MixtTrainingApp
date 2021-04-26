using MixtTrainingApp.DataService;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using MixtTrainingApp.Views.LoadedPage;
using MixtTrainingApp.Helper;

namespace MixtTrainingApp.Views.WorkoutsNavigation
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NavigationListCardPage
    {
        IFirebaseAuthenticaton auth;
        readonly FireBaseHelperClient firebaseClient = new FireBaseHelperClient();
        public NavigationListCardPage()
        {
            InitializeComponent();
            auth = DependencyService.Get<IFirebaseAuthenticaton>();
            this.BindingContext = NavigationDataService.Instance.NavigationViewModel;
        }

        private void Button_Clicked(object sender, System.EventArgs e)
        {
            auth.SignOut();
            App.Current.MainPage = new NavigationPage(new IntroPage());
            App.Current.Properties.Remove("App.UserUID");
            App.Current.SavePropertiesAsync();
        }

        private async void SfListView_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
             if (e.ItemData == NavigationDataService.Instance.NavigationViewModel.NavigationList[0])
                await Navigation.PushAsync(new WorkoutPage("Strength"));
             else if(e.ItemData == NavigationDataService.Instance.NavigationViewModel.NavigationList[1])
                await Navigation.PushAsync(new WorkoutPage("Bodybuilding"));
            else if (e.ItemData == NavigationDataService.Instance.NavigationViewModel.NavigationList[2])
                await Navigation.PushAsync(new WorkoutPage("Circuit"));
            else if (e.ItemData == NavigationDataService.Instance.NavigationViewModel.NavigationList[3])
                await Navigation.PushAsync(new WorkoutPage("Isometric"));
            else if (e.ItemData == NavigationDataService.Instance.NavigationViewModel.NavigationList[4])
                await Navigation.PushAsync(new WorkoutPage("Conditioning"));
        }
    }
}