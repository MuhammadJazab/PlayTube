using System;
using System.Globalization;
using PlayTube.Dependencies;
using PlayTube.Languish;
using PlayTube.Pages.Tabbes;
using PlayTube.Pages.Walkthrough;
using PlayTube.SQLite;
using Xamarin.Forms;

namespace PlayTube
{
    public partial class App : Application
    {
        public App()
        {
            try
            {
                try //Language
                {
                    L10n.SetLocale();
                    var netLanguage = DependencyService.Get<IMethods>().GetCurrent();
                    AppResources.Culture = new CultureInfo(netLanguage);
                }
                catch (Exception ex)
                {
                    var exception = ex.ToString();
                }

                InitializeComponent();

                SQL_Entity.Connect();

                var data = SQL_Commander.Get_Settings();

                if (data == "true")
                {
                    if (Settings.VimeoStyle)
                    {
                        var navigationPage = new NavigationPage(new MainTabbed_Page()) { };
                        navigationPage.BarBackgroundColor = Color.FromHex(Settings.MainColor);
                        navigationPage.BarTextColor = Color.FromHex("#ffff");
                        navigationPage.Title = Settings.Application_Name;
                        navigationPage.Padding = new Thickness(0, 0, 0, 0);
                        Settings.YoutubeStyle = false;
                        MainPage = navigationPage;
                    }

                    if (Settings.YoutubeStyle)
                    {
                        MainPage = new NavigationPage(new BottomTabbedPage());
                    }

                   //MainPage = new NavigationPage(new WalkthroughVariantPage());
                }
                else
                {
                    MainPage = new NavigationPage(new WalkthroughVariantPage());
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
