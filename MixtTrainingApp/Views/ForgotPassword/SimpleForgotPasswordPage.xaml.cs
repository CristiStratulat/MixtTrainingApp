using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using MixtTrainingApp.Helpers;
using MixtTrainingApp.Views.ForgotPassword;
using System.Net.Mail;

namespace MixtTrainingApp.Views.ForgotPassword
{
    /// <summary>
    /// Page to retrieve the password forgotten.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SimpleForgotPasswordPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleForgotPasswordPage" /> class.
        /// </summary>
        IFirebaseForgotPassword auth;
        public SimpleForgotPasswordPage()
        {
            InitializeComponent();
            auth = DependencyService.Get<IFirebaseForgotPassword>();
        }
        private async void SignUp (object sender,EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void SfButton_Clicked(object sender, EventArgs e)
        {
            bool ok = true;
            EmailEntry.IsVisible = false;
            string userEmail = "";
            //  Checks the Email Entry if empty or null 
            if (!string.IsNullOrEmpty(ForgotPasswordEmail.Text) && !string.IsNullOrWhiteSpace(ForgotPasswordEmail.Text))
            {
                userEmail = ForgotPasswordEmail.Text.Trim();
                // Checks if the email format is a valid format
                if (!IsValidEmail(userEmail))
                {
                    ok = false;
                    EmailEntry.IsVisible = true;
                }
            }
            else
            {
                EmailEntry.IsVisible = true;
                ok = false;
            }
            if (ok)
            {
                string forgotPass = await auth.ForgotPassword(ForgotPasswordEmail.Text);
                if (forgotPass == "emailSent")
                {
                    await DisplayAlert("Success", "Please verify your email to reset your password", "OK");
                }
                else if (forgotPass == "emailNotFound")
                {
                    await DisplayAlert("Email not found", "There is no account matching this email \nPlease sign up", "OK");
                }
                else
                {
                    await DisplayAlert("Error", forgotPass, "OK");
                }
            }

        }
        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}