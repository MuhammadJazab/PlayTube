using System;
using PlayTube.Pages.Default;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace PlayTube.Pages.Tabbes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainTabbed_Page : TabbedPage
    {
        public MainTabbed_Page()
        {
            try
            {
                InitializeComponent();
                Title = Settings.Application_Name;
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        private async void Search_OnClicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new Search_Page());
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
                await Navigation.PushModalAsync(new Search_Page());
            }
        }
    }
}
