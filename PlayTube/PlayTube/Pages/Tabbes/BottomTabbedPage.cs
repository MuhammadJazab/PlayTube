using BottomBar.XamarinForms;
using PlayTube.Languish;
using Xamarin.Forms;

namespace PlayTube.Pages.Tabbes
{
    public class BottomTabbedPage : BottomBarPage
    {
        public BottomTabbedPage()
        {
            var HomePage = new Home_Page()
            {
                Icon = "Home.png",
                Title = AppResources.Label_Home,
                BackgroundColor = Color.White,
            };

            var Trending_Page = new Trending_Page()
            {
                Icon = "Trending.png",
                Title = AppResources.Label_Trending,
                BackgroundColor = Color.White,
            };

            var WachLater_Page = new WachLater_Page()
            {
                Icon = "T_WachLater.png",
                Title = AppResources.Label_Watch_Later,
                BackgroundColor = Color.White,
            };

            var Subscriptions_Page = new Subscriptions_Page()
            {
                Icon = "T_Category.png",
                Title = AppResources.Label_Subscriptions,
                BackgroundColor = Color.White,
            };

            var Hamburg_Page = new Hamburg_Page()
            {
                Icon = "T_Hamburg.png",
                Title = AppResources.Label_More,
                BackgroundColor = Color.White,
            };

            if (Settings.DarkTheme)
            {
                BarTheme = BarThemeTypes.DarkWithoutAlpha;
            }

            if (Settings.LightTheme)
            {
                this.BarBackgroundColor = Color.FromHex("#f3f3f3");
                BottomBarPageExtensions.SetTabColor(HomePage, Color.FromHex("#f3f3f3"));
                BottomBarPageExtensions.SetTabColor(Trending_Page, Color.FromHex("#f3f3f3"));
                BottomBarPageExtensions.SetTabColor(WachLater_Page, Color.FromHex("#f3f3f3"));
                BottomBarPageExtensions.SetTabColor(Subscriptions_Page, Color.FromHex("#f3f3f3"));
                BottomBarPageExtensions.SetTabColor(Hamburg_Page, Color.FromHex("#f3f3f3"));
                BarTextColor = Color.Black;
                FixedMode = true;
            }

            if (Settings.DefaultTheme)
            {
                BottomBarPageExtensions.SetTabColor(HomePage, Color.FromHex(Settings.MainColor));
                BottomBarPageExtensions.SetTabColor(Trending_Page, Color.FromHex(Settings.MainColor));
                BottomBarPageExtensions.SetTabColor(WachLater_Page, Color.FromHex(Settings.MainColor));
                BottomBarPageExtensions.SetTabColor(Subscriptions_Page, Color.FromHex(Settings.MainColor));
                BottomBarPageExtensions.SetTabColor(Hamburg_Page, Color.FromHex(Settings.MainColor));
            }

            Children.Add(HomePage);
            Children.Add(Trending_Page);
            Children.Add(WachLater_Page);
            Children.Add(Subscriptions_Page);
            Children.Add(Hamburg_Page);


            if (Settings.Show_Cutsom_Logo_And_Header_On_the_Top)
            {
                NavigationPage.SetHasNavigationBar(this, false);
            }

            //ToolbarItems.Add(new ToolbarItem()
            //{
            //    Text = "Something"
            //});
        }
    }
}