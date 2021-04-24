using MixtTrainingApp.DataService;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using MixtTrainingApp.Views.LoadedPage;

namespace MixtTrainingApp.Views.WorkoutsNavigation
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NavigationListCardPage
    {
        IFirebaseAuthenticaton auth;
        MixtTrainingApp.ViewModels.WorkoutsNavigation.NavigationViewModel idk = new ViewModels.WorkoutsNavigation.NavigationViewModel();
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
        }

        private async void SfListView_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
             if (e.ItemData == NavigationDataService.Instance.NavigationViewModel.NavigationList[0])
                await Navigation.PushAsync(new WorkoutPage("strength"));
             else if(e.ItemData == NavigationDataService.Instance.NavigationViewModel.NavigationList[1])
                await Navigation.PushAsync(new WorkoutPage("bodybuilding"));
            else if (e.ItemData == NavigationDataService.Instance.NavigationViewModel.NavigationList[2])
                await Navigation.PushAsync(new WorkoutPage("circuit"));
            else if (e.ItemData == NavigationDataService.Instance.NavigationViewModel.NavigationList[3])
                await Navigation.PushAsync(new WorkoutPage("isometric"));
            else if (e.ItemData == NavigationDataService.Instance.NavigationViewModel.NavigationList[4])
                await Navigation.PushAsync(new WorkoutPage("Conditioning"));
        }
    }
}