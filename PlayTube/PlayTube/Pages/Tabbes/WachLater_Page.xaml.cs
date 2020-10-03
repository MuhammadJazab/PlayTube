using System;
using PlayTube.Controls;
using PlayTube.Languish;
using PlayTube.Pages.Default;
using PlayTube.SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PlayTube.Pages.Tabbes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WachLater_Page : ContentPage
    {
        public WachLater_Page()
        {
            InitializeComponent();

            Title = AppResources.Label_Watch_Later_Videos;

            Lbl_Dont_have.Text = AppResources.Label_Dont_have_any_videos;
            Lbl_Go_back.Text = AppResources.Label_Go_back_and_watched_any_video;

            if (Settings.VimeoStyle)
            {
                TopHeaderBAR.IsVisible = false;
                Title = "";
            }

            if (Hamburg_Page.IsLogin)
            {
                if (MyChannel_Page.Avatar != null)
                    AvatarImage.Source = ImageSource.FromUri(new Uri(MyChannel_Page.Avatar));
                else
                    AvatarImage.Source = "NoProfileImage.png";
            }

            if (Classes.WatchLaterVideosList.Count == 0)
            {
                WatchLaterListView.BeginRefresh();
            }
            WatchLaterListView.ItemsSource = Classes.WatchLaterVideosList;

            StyleThemeChanger();
        }

        //Get WatchLater Videos in Database
        public void WatchLater()
        {
            try
            {
                Classes.WatchLaterVideosList.Clear();

                var WatchLater = SQL_Commander.Get_WatchLaterVideos_Credentials();
                if (Classes.WatchLaterVideosList != null)
                {
                    foreach (var Video in WatchLater)
                    {
                        Classes.WatchLaterVideos watch = new Classes.WatchLaterVideos();

                        watch.SV_Type_video = "WatchLater";
                        if (Settings.DarkTheme)
                        {
                            watch.SV_BackgroundColor = "#bcbcbc";
                            watch.SV_TextColor = "#ffff";
                        }
                        else
                        {
                            watch.SV_TextColor = "#444";
                            watch.SV_BackgroundColor = "#ffff";
                        }
                        //Data video
                        watch.dv_id = Video.dv_id;
                        watch.dv_video_id = Video.dv_video_id;
                        watch.dv_user_id = Video.dv_user_id;
                        watch.dv_title = Video.dv_title;
                        watch.dv_description = Video.dv_description;
                        watch.dv_Long_title = Video.dv_Long_title;
                        watch.dv_Long_description = Video.dv_Long_description;
                        watch.dv_thumbnail = Video.dv_thumbnail;
                        watch.dv_video_location = Video.dv_video_location;
                        watch.dv_youtube = Video.dv_youtube;
                        watch.dv_vimeo = Video.dv_vimeo;
                        watch.dv_daily = Video.dv_daily;
                        watch.dv_time = Video.dv_time;
                        watch.dv_time_date = Video.dv_time_date;
                        watch.dv_active = Video.dv_active;
                        watch.dv_tags = Video.dv_tags;
                        watch.dv_duration = Video.dv_duration;
                        watch.dv_size = Video.dv_size;
                        watch.dv_category_id = Video.dv_category_id;
                        watch.dv_views = Video.dv_views;
                        watch.dv_featured = Video.dv_featured;
                        watch.dv_registered = Video.dv_registered;
                        watch.dv_org_thumbnail = Video.dv_org_thumbnail;
                        watch.dv_video_type = Video.dv_video_type;
                        watch.dv_video_id_ = Video.dv_video_id_;
                        watch.dv_source = Video.dv_source;
                        watch.dv_url = Video.dv_url;
                        watch.dv_edit_description = Video.dv_edit_description;
                        watch.dv_markup_description = Video.dv_markup_description;
                        watch.dv_is_liked = Video.dv_is_liked;
                        watch.dv_is_disliked = Video.dv_is_disliked;
                        watch.dv_is_owner = Video.dv_is_owner;
                        watch.dv_time_alpha = Video.dv_time_alpha;
                        watch.dv_time_ago = Video.dv_time_ago;
                        watch.dv_category_name = Video.dv_category_name;

                        //Owner
                        watch.Owner_id = Video.Owner_id;
                        watch.Owner_username = Video.Owner_username;
                        watch.Owner_email = Video.Owner_email;
                        watch.Owner_first_name = Video.Owner_first_name;
                        watch.Owner_last_name = Video.Owner_last_name;
                        watch.Owner_gender = Video.Owner_gender;
                        watch.Owner_language = Video.Owner_language;
                        watch.Owner_avatar = Video.Owner_avatar;
                        watch.Owner_cover = Video.Owner_cover;
                        watch.Owner_about = Video.Owner_about;
                        watch.Owner_google = Video.Owner_google;
                        watch.Owner_facebook = Video.Owner_facebook;
                        watch.Owner_twitter = Video.Owner_twitter;
                        watch.Owner_verified = Video.Owner_verified;
                        watch.Owner_is_pro = Video.Owner_is_pro;
                        watch.Owner_url = Video.Owner_url;

                        Classes.WatchLaterVideosList.Add(watch);
                    }
                    WatchLaterListView.EndRefresh();
                    WatchLaterListView.IsVisible = true;
                    EmptyPage.IsVisible = false;
                    WatchLaterListView.ItemsSource = Classes.WatchLaterVideosList;
                }
                else
                {
                    WatchLaterListView.EndRefresh();
                    EmptyPage.IsVisible = true;
                    WatchLaterListView.IsVisible = false;
                }
            }
            catch (Exception ex)
            {
                WatchLaterListView.EndRefresh();
                var exception = ex.ToString();
                WatchLaterListView.IsVisible = false;
                EmptyPage.IsVisible = true;
            }
        }

        private void WatchLaterListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                WatchLaterListView.SelectedItem = null;
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        //Event click Item >> open Video_Player_Page
        private async void WatchLaterListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                var selectedItem = e.Item as Classes.WatchLaterVideos;
                if (selectedItem != null)
                {
                    //Convert From Classes.WatchLaterVideos
                    Classes.Video WatchLater = new Classes.Video();

                    //Data video
                    WatchLater.dv_id = selectedItem.dv_id;
                    WatchLater.dv_video_id = selectedItem.dv_video_id;
                    WatchLater.dv_user_id = selectedItem.dv_user_id;
                    WatchLater.dv_title = selectedItem.dv_title;
                    WatchLater.dv_description = selectedItem.dv_description;
                    WatchLater.dv_Long_title = selectedItem.dv_Long_title;
                    WatchLater.dv_Long_description = selectedItem.dv_Long_description;
                    WatchLater.dv_thumbnail = selectedItem.dv_thumbnail;
                    WatchLater.dv_video_location = selectedItem.dv_video_location;
                    WatchLater.dv_youtube = selectedItem.dv_youtube;
                    WatchLater.dv_vimeo = selectedItem.dv_vimeo;
                    WatchLater.dv_daily = selectedItem.dv_daily;
                    WatchLater.dv_time = selectedItem.dv_time;
                    WatchLater.dv_time_date = selectedItem.dv_time_date;
                    WatchLater.dv_active = selectedItem.dv_active;
                    WatchLater.dv_tags = selectedItem.dv_tags;
                    WatchLater.dv_duration = selectedItem.dv_duration;
                    WatchLater.dv_size = selectedItem.dv_size;
                    WatchLater.dv_category_id = selectedItem.dv_category_id;
                    WatchLater.dv_views = selectedItem.dv_views;
                    WatchLater.dv_featured = selectedItem.dv_featured;
                    WatchLater.dv_registered = selectedItem.dv_registered;
                    WatchLater.dv_org_thumbnail = selectedItem.dv_org_thumbnail;
                    WatchLater.dv_video_type = selectedItem.dv_video_type;
                    WatchLater.dv_video_id_ = selectedItem.dv_video_id_;
                    WatchLater.dv_source = selectedItem.dv_source;
                    WatchLater.dv_url = selectedItem.dv_url;
                    WatchLater.dv_edit_description = selectedItem.dv_edit_description;
                    WatchLater.dv_markup_description = selectedItem.dv_markup_description;
                    WatchLater.dv_is_liked = selectedItem.dv_is_liked;
                    WatchLater.dv_is_disliked = selectedItem.dv_is_disliked;
                    WatchLater.dv_is_owner = selectedItem.dv_is_owner;
                    WatchLater.dv_time_alpha = selectedItem.dv_time_alpha;
                    WatchLater.dv_time_ago = selectedItem.dv_time_ago;
                    WatchLater.dv_category_name = selectedItem.dv_category_name;

                    //Owner
                    WatchLater.Owner_id = selectedItem.Owner_id;
                    WatchLater.Owner_username = selectedItem.Owner_username;
                    WatchLater.Owner_email = selectedItem.Owner_email;
                    WatchLater.Owner_first_name = selectedItem.Owner_first_name;
                    WatchLater.Owner_last_name = selectedItem.Owner_last_name;
                    WatchLater.Owner_gender = selectedItem.Owner_gender;
                    WatchLater.Owner_language = selectedItem.Owner_language;
                    WatchLater.Owner_avatar = selectedItem.Owner_avatar;
                    WatchLater.Owner_cover = selectedItem.Owner_cover;
                    WatchLater.Owner_about = selectedItem.Owner_about;
                    WatchLater.Owner_google = selectedItem.Owner_google;
                    WatchLater.Owner_facebook = selectedItem.Owner_facebook;
                    WatchLater.Owner_twitter = selectedItem.Owner_twitter;
                    WatchLater.Owner_verified = selectedItem.Owner_verified;
                    WatchLater.Owner_is_pro = selectedItem.Owner_is_pro;
                    WatchLater.Owner_url = selectedItem.Owner_url;

                    await Navigation.PushAsync(new Video_Player_Page(WatchLater));
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        //Event Refreshing WatchLaterListView 
        private void WatchLaterListView_OnRefreshing(object sender, EventArgs e)
        {
            try
            {
                WatchLater();
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

        private void WatchLaterVideos_Page_OnAppearing(object sender, EventArgs e)
        {
            try
            {
                WatchLater();
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
                MainStackPanel.BackgroundColor = Color.FromHex("#444");
                WatchLaterListView.BackgroundColor = Color.FromHex("#444");
                Lbl_Dont_have.TextColor = Color.FromHex("#ffff");
                Lbl_Go_back.TextColor = Color.FromHex("#ffff");
            }
        }
    }
}
