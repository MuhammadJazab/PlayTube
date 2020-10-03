using System;
using System.Linq;
using Newtonsoft.Json.Linq;
using PlayTube.Controls;
using PlayTube.Dependencies;
using PlayTube.Languish;
using PlayTube.Pages.Default;
using PlayTube.SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Connectivity;
using Plugin.Share;
using Plugin.Share.Abstractions;

namespace PlayTube.Pages.Tabbes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Trending_Page : ContentPage
    {
        public Trending_Page()
        {
            try
            {
                InitializeComponent();

                TopLabel.Text = AppResources.Label_Trending;


                if (Settings.VimeoStyle)
                {
                    TopHeaderBAR.IsVisible = false;
                }
                StyleThemeChanger();


                if (Hamburg_Page.IsLogin)
                {
                    if (MyChannel_Page.Avatar != null)
                        AvatarImage.Source = ImageSource.FromUri(new Uri(MyChannel_Page.Avatar));
                    else
                        AvatarImage.Source = "NoProfileImage.png";
                }

                if (Classes.TrendingList.Count == 0)
                {
                    TrendingListView.BeginRefresh();
                }
                TrendingListView.ItemsSource = Classes.TrendingList;


            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        //Get Trending Videos
        public async void Get_Trending_Videos(string offset)
        {
            try
            {
                if (!CrossConnectivity.Current.IsConnected)
                {
                    // await DisplayAlert(AppResources.Label_Error, AppResources.Label_Check_Your_Internet, AppResources.Label_OK);
                    EmptyPage.IsVisible = true;
                    TrendingListView.IsVisible = false;
                    TitleStackLayout.IsVisible = false;

                    TrendingListView.EndRefresh();
                    Icon_page.Text = "\uf119";
                    Lbl_Dont_have.Text = AppResources.Label_Offline_Mode;
                    Lbl_no_Videos.Text = AppResources.Label_Check_Your_Internet;
                }
                else
                {
                    var Trending_data = await API_Request.Get_Trending_Videos_Http(offset, "20");
                    if (Trending_data != null)
                    {
                        if (Trending_data.Count > 0)
                        {
                            TrendingListView.IsVisible = true;
                            TitleStackLayout.IsVisible = true;
                            EmptyPage.IsVisible = false;

                            foreach (var All in Trending_data)
                            {
                                JObject Items = JObject.FromObject(All);

                                Classes.Video V = new Classes.Video();

                                try //featured - top - latest
                                {
                                    var id = Items["id"].ToString();
                                    var video_id = Items["video_id"].ToString();
                                    var user_id = Items["user_id"].ToString();
                                    var title = Items["title"].ToString();
                                    var description = Items["description"].ToString();
                                    var thumbnail = Items["thumbnail"].ToString();
                                    var video_location = Items["video_location"].ToString();
                                    var youtube = Items["youtube"].ToString();
                                    var vimeo = Items["vimeo"].ToString();
                                    var daily = Items["daily"].ToString();
                                    var time = Items["time"].ToString();
                                    var time_date = Items["time_date"].ToString();
                                    var active = Items["active"].ToString();
                                    var tags = Items["tags"].ToString();
                                    var duration = Items["duration"].ToString();
                                    var size = Items["size"].ToString();
                                    var category_id = Items["category_id"].ToString();
                                    var views = Items["views"].ToString();
                                    var featured = Items["featured"].ToString();
                                    var registered = Items["registered"].ToString();
                                    var org_thumbnail = Items["org_thumbnail"].ToString(); //Video_Image
                                    var video_type = Items["video_type"].ToString();
                                    var video_id_ = Items["video_id_"].ToString();
                                    var source = Items["source"].ToString();
                                    var url = Items["url"].ToString();
                                    var edit_description = Items["edit_description"].ToString();
                                    var markup_description = Items["markup_description"].ToString();
                                    var is_liked = Items["is_liked"].ToString();
                                    var is_disliked = Items["is_disliked"].ToString();
                                    var is_owner = Items["is_owner"].ToString();
                                    var time_alpha = Items["time_alpha"].ToString();
                                    var time_ago = Items["time_ago"].ToString();
                                    var category_name = Items["category_name"].ToString();

                                    //style
                                    V.SV_Type_video = "Trending";
                                    if (Settings.DarkTheme)
                                    {
                                        V.SV_BackgroundColor = "#bcbcbc";
                                        V.SV_TextColor = "#ffff";
                                    }
                                    else
                                    {
                                        V.SV_TextColor = "#444";
                                        V.SV_BackgroundColor = "#ffff";
                                    }

                                    //Data video
                                    V.dv_id = id;
                                    V.dv_video_id = video_id;
                                    V.dv_user_id = user_id;
                                    V.dv_title =
                                        Functions.SubStringCutOf(
                                            Functions.DecodeStringWithEnter(Functions.DecodeString(title)), 30);
                                    V.dv_description =
                                        Functions.SubStringCutOf(
                                            Functions.DecodeStringWithEnter(Functions.DecodeString(description)), 60);
                                    V.dv_Long_title = Functions.DecodeStringWithEnter(Functions.DecodeString(title));
                                    V.dv_Long_description =
                                        Functions.DecodeStringWithEnter(Functions.DecodeString(description));
                                    V.dv_thumbnail = thumbnail;
                                    V.dv_video_location = video_location;
                                    V.dv_youtube = youtube;
                                    V.dv_vimeo = vimeo;
                                    V.dv_daily = daily;
                                    V.dv_time = time;
                                    V.dv_time_date = time_date;
                                    V.dv_active = active;
                                    V.dv_tags = tags;
                                    V.dv_duration = duration;
                                    V.dv_size = size;
                                    V.dv_category_id = category_id;
                                    V.dv_views = Functions.FormatPriceValue(Convert.ToInt32(views)) + " " +
                                                 AppResources.Label_Views;
                                    V.dv_featured = featured;
                                    V.dv_registered = registered;
                                    V.dv_org_thumbnail = org_thumbnail;
                                    V.dv_video_type = video_type;
                                    V.dv_video_id_ = video_id_;
                                    V.dv_source = source;
                                    V.dv_url = url;
                                    V.dv_edit_description = edit_description;
                                    V.dv_markup_description = markup_description;
                                    V.dv_is_liked = is_liked;
                                    V.dv_is_disliked = is_disliked;
                                    V.dv_is_owner = is_owner;
                                    V.dv_time_alpha = time_alpha;
                                    V.dv_time_ago = time_ago;
                                    V.dv_category_name = category_name;

                                    try //owner
                                    {
                                        JObject Owner = JObject.FromObject(Items["owner"]);

                                        var O_id = Owner["id"].ToString();
                                        var username = Owner["username"].ToString();
                                        var email = Owner["email"].ToString();
                                        var first_name = Owner["first_name"].ToString();
                                        var last_name = Owner["last_name"].ToString();
                                        var gender = Owner["gender"].ToString();
                                        var language = Owner["language"].ToString();
                                        var avatar = Owner["avatar"].ToString();
                                        var cover = Owner["cover"].ToString();
                                        string about = "";
                                        try
                                        {
                                            about =
                                                Functions.StringNullRemover(Functions.DecodeStringWithEnter(
                                                    Functions.DecodeString(Owner["about"].ToString())));
                                        }
                                        catch (Exception ex)
                                        {
                                            var exception = ex.ToString();
                                        }
                                        var google = Owner["google"].ToString();
                                        var facebook = Owner["facebook"].ToString();
                                        var twitter = Owner["twitter"].ToString();
                                        var verified = Owner["verified"].ToString();
                                        var is_pro = Owner["is_pro"].ToString();
                                        var O_url = Owner["url"].ToString();

                                        V.Owner_id = O_id;
                                        V.Owner_username = username;
                                        V.Owner_email = email;
                                        V.Owner_first_name = first_name;
                                        V.Owner_last_name = last_name;
                                        V.Owner_gender = gender;
                                        V.Owner_language = language;
                                        V.Owner_avatar = avatar;
                                        V.Owner_cover = cover;
                                        V.Owner_about = about;
                                        V.Owner_google = google;
                                        V.Owner_facebook = facebook;
                                        V.Owner_twitter = twitter;
                                        if (verified == "0")
                                            V.Owner_verified = "false";
                                        else
                                            V.Owner_verified = "true";
                                        V.Owner_is_pro = is_pro;
                                        V.Owner_url = O_url;
                                    }
                                    catch (Exception ex)
                                    {
                                        var exception = ex.ToString();
                                    }

                                    var dd = Classes.TrendingList.FirstOrDefault(a => a.dv_id == id);
                                    if (dd == null)
                                    {
                                        //Add list All data Video
                                        Classes.TrendingList.Add(V);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    var exception = ex.ToString();
                                }
                            }

                            TrendingListView.EndRefresh();
                            //Add_Trending_video
                            TrendingListView.ItemsSource = Classes.TrendingList;
                        }
                        else
                        {
                            EmptyPage.IsVisible = true;
                            TrendingListView.IsVisible = false;
                            TitleStackLayout.IsVisible = false;

                            TrendingListView.EndRefresh();

                            Icon_page.Text = "\uf03d";
                            Lbl_Dont_have.Text = AppResources.Label_Empty_Videos;
                            Lbl_no_Videos.Text = AppResources.Label_no_Videos_to_view;
                        }
                    }
                    else
                    {
                        EmptyPage.IsVisible = true;
                        TrendingListView.IsVisible = false;
                        TitleStackLayout.IsVisible = false;

                        TrendingListView.EndRefresh();

                        Icon_page.Text = "\uf03d";
                        Lbl_Dont_have.Text = AppResources.Label_Empty_Videos;
                        Lbl_no_Videos.Text = AppResources.Label_no_Videos_to_view;
                    }
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
                TrendingListView.EndRefresh();
                if (Classes.TrendingList.Count > 0)
                {
                    TrendingListView.IsVisible = true;
                    EmptyPage.IsVisible = false;
                    TitleStackLayout.IsVisible = true;

                    //Add_Trending_video
                    TrendingListView.ItemsSource = Classes.TrendingList;
                }
                else
                {
                    Icon_page.Text = "\uf03d";
                    Lbl_Dont_have.Text = AppResources.Label_Empty_Videos;
                    Lbl_no_Videos.Text = AppResources.Label_no_Videos_to_view;
                    EmptyPage.IsVisible = true;
                    TrendingListView.IsVisible = false;
                    TitleStackLayout.IsVisible = false;
                }
            }
        }

        private void TrendingListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                TrendingListView.SelectedItem = null;
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        //Event click Item >> open Video_Player_Page
        private async void TrendingListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                var selectedItem = e.Item as Classes.Video;
                if (selectedItem != null)
                {
                    await Navigation.PushAsync(new Video_Player_Page(selectedItem));
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        //Event Refreshing TrendingListView 
        private void TrendingListView_OnRefreshing(object sender, EventArgs e)
        {
            try
            {
                if (Classes.TrendingList.Count > 0)
                {
                    Classes.TrendingList.Clear();
                }

                Get_Trending_Videos("0");
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        //Event Item Appearing >> Get More Trending video 
        private async void TrendingListView_OnItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            try
            {
                var currentItem = e.Item as Classes.Video;
                var beforelastItem = Classes.TrendingList[Classes.TrendingList.Count - 2];
                if (currentItem == beforelastItem)
                {
                    if (!CrossConnectivity.Current.IsConnected)
                    {
                        await DisplayAlert(AppResources.Label_Error, AppResources.Label_Check_Your_Internet,
                            AppResources.Label_OK);
                    }
                    else
                    {
                        //Title = AppResources.Label_Loading;
                        var lastItem = Classes.TrendingList[Classes.TrendingList.Count - 1];
                        if (lastItem != null)
                        {
                            Get_Trending_Videos(lastItem.dv_id);
                        }
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

        //Event click Try again #Get_Trending
        private void TryagainButton_OnClicked(object sender, EventArgs e)
        {
            try
            {
                if (Classes.TrendingList.Count > 0)
                {
                    Classes.TrendingList.Clear();
                }

                Get_Trending_Videos("0");
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        public void StyleThemeChanger()
        {
            if (Settings.DarkTheme)
            {
                ImageLogo.Source = "logolight.png";
                MainStackPanel.BackgroundColor = Color.FromHex("#444");
                TopHeaderBAR.BackgroundColor = Color.FromHex("#2f2f2f");
                TopHeaderBAR.OutlineColor = Color.FromHex("#2f2f2f");
                SearchIcon.TextColor = Color.FromHex("#ffff");
                Liner.BackgroundColor = Color.FromHex("#767676");
                TopLabel.TextColor = Color.FromHex("#ffff");
                Lbl_Dont_have.TextColor = Color.FromHex("#ffff");
                Lbl_no_Videos.TextColor = Color.FromHex("#ffff");
                TrendingListView.BackgroundColor = Color.FromHex("#444");
            }
        }

        private async void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            try
            {
                var ItemVideo = (Classes.Video)((StackLayout)sender).BindingContext;
                if (ItemVideo != null)
                {

                    var action = await App.Current.MainPage.DisplayActionSheet(AppResources.Label_Choose_your_action,
                        AppResources.Label_Cancel, null, AppResources.Label_Add_to_watch_later, AppResources.Label_Share);
                    if (action == AppResources.Label_Share)
                    {
                        try
                        {
                            if (!CrossShare.IsSupported)
                            {
                                return;
                            }
                            await CrossShare.Current.Share(new ShareMessage
                            {
                                Title = ItemVideo.dv_title,
                                Text = ItemVideo.dv_description,
                                Url = ItemVideo.dv_url
                            });
                        }
                        catch (Exception ex)
                        {
                            var exception = ex.ToString();
                        }
                    }
                    else if (action == AppResources.Label_Not_interested)
                    {
                        //Classes.VideoList.Remove(ItemVideo);
                    }
                    else if (action == AppResources.Label_Add_to_watch_later)
                    {
                        try
                        {
                            //Add to watch Later Database
                            SQL_Commander.Insert_WatchLaterVideos_Credentials(ItemVideo);

                            DependencyService.Get<IMethods>().LongAlert(AppResources.Label_Add_The_Videos_to_watch_Later);
                        }
                        catch (Exception ex)
                        {
                            var exception = ex.ToString();
                        }
                    }
                    else if (action == AppResources.Label_Add_to_play_list)
                    {
                        //if (Hamburg_Page.IsLogin)
                        //{
                        //    //Add to PlayList Database
                        //    SQL_Commander.Insert_PlayListsVideos_Credentials(Item);

                        //    DependencyService.Get<IMethods>().LongAlert(AppResources.Label_Add_The_Videos_To_PlayList);
                        //}
                        //var ErrorMSG = await DisplayAlert(AppResources.Label_Error, AppResources.Label_Please_sign_in_Playlist, AppResources.Label_Yes, AppResources.Label_No);
                        //if (!ErrorMSG)
                        //{
                        //    await Navigation.PushModalAsync(new Login_Page());
                        //}
                    }
                }
            }
            catch (Exception exception)
            {
            }

       }
    }
}
