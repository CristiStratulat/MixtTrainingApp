using MixtTrainingApp.DataService;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace MixtTrainingApp.Views.WorkoutsNavigation
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NavigationListCardPage
    {
        public NavigationListCardPage()
        {
            InitializeComponent();
            this.BindingContext = NavigationDataService.Instance.NavigationViewModel;
        }
    }
}