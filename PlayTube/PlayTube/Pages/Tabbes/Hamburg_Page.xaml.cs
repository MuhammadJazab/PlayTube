using System;
using System.Linq;
using PlayTube.Controls;
using PlayTube.Languish;
using PlayTube.Pages.Default;
using PlayTube.SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PlayTube.Pages.Tabbes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Hamburg_Page : ContentPage
    {
        public static bool IsLogin = false;

        public Hamburg_Page()
        {
            try
            {
                InitializeComponent();

                if (Settings.VimeoStyle)
                {
                    TopHeaderBAR.IsVisible = false;
                }

                Item_list();

                StyleThemeChanger();

                //Check User Status and Get data
                if (IsLogin)
                {
                    var s = Classes.SeconderyListPage.FirstOrDefault(a => a.Name_page == AppResources.Label_Login);
                    if (s != null)
                    {
                        s.Name_page = AppResources.Label_Logout;
                        s.Icon_page = IoniciconsFont.LogOut;
                    }
                    PagesListView.HeightRequest = 260;

                    if (MyChannel_Page.Avatar != null)
                        AvatarImage.Source = ImageSource.FromUri(new Uri(MyChannel_Page.Avatar));
                    else
                        AvatarImage.Source = "NoProfileImage.png";
                }

                if (Settings.Show_ADMOB_On_ItemList)
                    AdmobBanner.IsVisible = true;
                else
                    AdmobBanner.IsVisible = false;

            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        //Add Item Page 
        public void Item_list()
        {
            try
            {
                if (IsLogin)
                {
                    Classes.ListPage.Add(new Classes.PageItems()
                    {
                        Name_page =AppResources.Label_My_Channel,
                        Icon_page = IoniciconsFont.Person,
                        BackgroundColor = "#696969",
                        TextColor = "#444"
                    });
                }
                // First List Page
                Classes.ListPage.Add(new Classes.PageItems()
                {
                    Name_page = AppResources.Label_History,
                    Icon_page = IoniciconsFont.AndroidTime,
                    BackgroundColor = "#696969",
                    TextColor = "#444"
                });
                if (IsLogin)
                {
                    Classes.ListPage.Add(new Classes.PageItems()
                    {
                        Name_page = AppResources.Label_PlayLists,
                        Icon_page = IoniciconsFont.Play,
                        BackgroundColor = "#696969",
                        TextColor = "#444"
                    });

                    Classes.ListPage.Add(new Classes.PageItems()
                    {
                        Name_page = AppResources.Label_Liked_Videos,
                        Icon_page = IoniciconsFont.Thumbsup,
                        BackgroundColor = "#696969",
                        TextColor = "#444"
                    });

                    Classes.ListPage.Add(new Classes.PageItems()
                    {
                        Name_page = AppResources.Label_Upload,
                        Icon_page = IoniciconsFont.IosCloudUpload,
                        BackgroundColor = "#696969",
                        TextColor = "#444"
                    });
                }
                if (Classes.ListPage.Count == 1)
                {
                    PagesListView.HeightRequest = 60;
                }
                else
                {
                    PagesListView.HeightRequest = 260;
                }


                // Secondery List Page
                Classes.SeconderyListPage.Add(new Classes.SeconderyPageItems()
                {
                    Name_page = AppResources.Label_Login,
                    Icon_page = IoniciconsFont.LogIn,
                    BackgroundColor = "#696969",
                    TextColor = "#444"
                });
                Classes.SeconderyListPage.Add(new Classes.SeconderyPageItems()
                {
                    Name_page = AppResources.Label_About,
                    Icon_page = IoniciconsFont.IosInformationOutline,
                    BackgroundColor = "#696969",
                    TextColor = "#444"
                });
                PagesListView.ItemsSource = Classes.ListPage;
                SeconderyListView.ItemsSource = Classes.SeconderyListPage;
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        private void PagesListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                PagesListView.SelectedItem = null;
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        //Event click Item in list Pages >> open page 
        private async void PagesListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                var item = e.Item as Classes.PageItems;
                if (item != null)
                {
                    if (item.Name_page == AppResources.Label_My_Channel)
                    {
                        await Navigation.PushAsync(new MyChannel_Page());
                    }
                    else if (item.Name_page == AppResources.Label_History)
                    {
                        await Navigation.PushAsync(new HistoryVideos_Page()); 
                    }
                    else if (item.Name_page == AppResources.Label_PlayLists)
                    {
                        await Navigation.PushAsync(new PlayListsVideos_Page());
                    }
                    else if (item.Name_page == AppResources.Label_Liked_Videos)
                    {
                        await Navigation.PushAsync(new LikedVideos_Page());
                    }
                    else if (item.Name_page == AppResources.Label_Upload)
                    {
                        await Navigation.PushAsync(new Import_Page());
                    }
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        private void SeconderyListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                SeconderyListView.SelectedItem = null;
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        //Event click Item in Secondery List Page >> open page 
        private async void SeconderyListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                var item = e.Item as Classes.SeconderyPageItems;
                if (item != null)
                {
                    if (item.Name_page == AppResources.Label_Login)
                    {
                        await Navigation.PushModalAsync(new Login_Page());
                    }
                    else if(item.Name_page == AppResources.Label_Logout)
                    {
                        var user = Classes.SeconderyListPage.FirstOrDefault(a => a.Name_page == AppResources.Label_Logout);
                        if (user != null)
                        {
                            user.Name_page = AppResources.Label_Login;
                            user.Icon_page = IoniciconsFont.LogIn;
                        }

                        var query = Classes.ListPage.FirstOrDefault(a => a.Name_page == AppResources.Label_My_Channel);
                        if (query != null)
                        {
                            Classes.ListPage.Remove(query);
                        }
                        var query2 = Classes.ListPage.FirstOrDefault(a => a.Name_page == AppResources.Label_Liked_Videos);
                        if (query2 != null)
                        {
                            Classes.ListPage.Remove(query2);
                        }
                        var query3 = Classes.ListPage.FirstOrDefault(a => a.Name_page == AppResources.Label_PlayLists);
                        if (query3 != null)
                        {
                            Classes.ListPage.Remove(query3);
                        }
                        var query4 = Classes.ListPage.FirstOrDefault(a => a.Name_page == AppResources.Label_Upload);
                        if (query4 != null)
                        {
                            Classes.ListPage.Remove(query4);
                        }

                        PagesListView.ItemsSource = Classes.ListPage;
                        PagesListView.HeightRequest = 50;

                        API_Request.User_logout_Http();
                        SQL_Commander.Delete_Login_Credentials();
                    }
                    else if (item.Name_page == AppResources.Label_About)
                    {
                        var URI = new Uri(Settings.WebsiteUrl + "/terms/about-us");
                        Device.OpenUri(URI);
                    }
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        //Event click Icon Search =>  open Search_Page
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

        public void StyleThemeChanger()
        {
            if (Settings.DarkTheme)
            {
                ImageLogo.Source = ImageSource.FromResource("logolight.png");
               
                MainStackPanel.BackgroundColor = Color.FromHex("#767676");
                TopHeaderBAR.BackgroundColor = Color.FromHex("#2f2f2f");
                TopHeaderBAR.OutlineColor = Color.FromHex("#2f2f2f");
                SearchIcon.TextColor = Color.FromHex("#ffff");
                PagesListView.BackgroundColor = Color.FromHex("#444");
                SeconderyListView.BackgroundColor = Color.FromHex("#444");

                foreach (var Item in Classes.ListPage)
                {
                    Item.BackgroundColor = "#bcbcbc";
                    Item.TextColor = "#ffff";
                }
                foreach (var Item in Classes.SeconderyListPage)
                {
                    Item.BackgroundColor = "#bcbcbc";
                    Item.TextColor = "#ffff";
                }
            }
        }

        private void Hamburg_Page_OnAppearing(object sender, EventArgs e)
        {
            try
            {
                if (IsLogin)
                {
                    if (Classes.ListPage.Count == 1)
                    {
                        PagesListView.HeightRequest = 60;
                    }
                    else
                    {
                        PagesListView.HeightRequest = 260;
                    }
                }
                else
                {
                    PagesListView.HeightRequest = 60;
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }
    }
}
