using System;
using PlayTube.Pages.Tabbes;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PlayTube.Pages.Walkthrough
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WalkthroughVariantPage : CarouselPage
    {
        public static bool LoadPage1 = false;
        public static bool LoadPage2 = false;
        public static bool LoadPage3 = false;

        public WalkthroughVariantPage()
        {
            try
            {
                NavigationPage.SetHasNavigationBar(this, false);
                InitializeComponent();
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        public void GoToStep()
        {
            try
            {
                var index = Children.IndexOf(CurrentPage);
                var moveToIndex = 0;
                if (index < Children.Count - 1)
                {
                    moveToIndex = index + 1;

                    SelectedItem = Children[moveToIndex];
                }
                else
                {
                    Close();
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }

        }

        public async void Close()
        {
            try
            {
                await Navigation.PopModalAsync();
            }
            catch (Exception ex)
            {
                await Navigation.PopAsync(true);
                var exception = ex.ToString();
            }
        }

        public void FinshTheWalkThroutPages()
        {
            try
            {
                if (Settings.VimeoStyle)
                {
                    var navigationPage = new NavigationPage(new MainTabbed_Page()) { };
                    navigationPage.BarBackgroundColor = Color.FromHex(Settings.MainColor);
                    navigationPage.BarTextColor = Color.FromHex("#ffff");
                    navigationPage.Title = Settings.Application_Name;

                    navigationPage.Padding = new Thickness(0, 0, 0, 0);
                    Application.Current.MainPage = navigationPage;
                }

                if (Settings.YoutubeStyle)
                {
                    var navigationPage = new NavigationPage(new BottomTabbedPage()) { };
                    navigationPage.BarBackgroundColor = Color.FromHex(Settings.MainColor);
                    navigationPage.BarTextColor = Color.FromHex("#ffff");
                    navigationPage.Title = Settings.Application_Name;

                    navigationPage.Padding = new Thickness(0, 0, 0, 0);
                    Application.Current.MainPage = navigationPage;
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }
    }
}