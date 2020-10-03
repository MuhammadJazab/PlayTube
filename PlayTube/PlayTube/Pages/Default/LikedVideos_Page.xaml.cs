using System;
using PlayTube.Controls;
using PlayTube.Languish;
using PlayTube.SQLite;
using Plugin.Share;
using Plugin.Share.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PlayTube.Pages.Default
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LikedVideos_Page : ContentPage
    {
        public LikedVideos_Page()
        {
            try
            {
                InitializeComponent();

                Lbl_Dont_have.Text = AppResources.Label_Dont_have_any_videos;
                Lbl_Go_back.Text = AppResources.Label_Go_back_and_liked_any_video;
                
                if (Classes.LikedVideosList.Count == 0)
                {
                    LikedVideosListView.BeginRefresh();
                }
                LikedVideosListView.ItemsSource = Classes.LikedVideosList;

                StyleThemeChanger();
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        //Get Liked Videos in Database
        public void LikedVideos()
        {
            try
            {
                Classes.LikedVideosList.Clear();

                var LikedVideos = SQL_Commander.Get_LikedVideos_Credentials();
                if (Classes.LikedVideosList != null)
                {
                    foreach (var Video in LikedVideos)
                    {
                        Classes.LikedVideos Liked = new Classes.LikedVideos();

                        Liked.SV_Type_video = "Liked";
                        if (Settings.DarkTheme)
                        {
                            Liked.SV_BackgroundColor = "#bcbcbc";
                            Liked.SV_TextColor = "#ffff";
                        }
                        else
                        {
                            Liked.SV_TextColor = "#444";
                            Liked.SV_BackgroundColor = "#ffff";
                        }
                        //Data video
                        Liked.dv_id = Video.dv_id;
                        Liked.dv_video_id = Video.dv_video_id;
                        Liked.dv_user_id = Video.dv_user_id;
                        Liked.dv_title = Video.dv_title;
                        Liked.dv_description = Video.dv_description;
                        Liked.dv_Long_title = Video.dv_Long_title;
                        Liked.dv_Long_description = Video.dv_Long_description;
                        Liked.dv_thumbnail = Video.dv_thumbnail;
                        Liked.dv_video_location = Video.dv_video_location;
                        Liked.dv_youtube = Video.dv_youtube;
                        Liked.dv_vimeo = Video.dv_vimeo;
                        Liked.dv_daily = Video.dv_daily;
                        Liked.dv_time = Video.dv_time;
                        Liked.dv_time_date = Video.dv_time_date;
                        Liked.dv_active = Video.dv_active;
                        Liked.dv_tags = Video.dv_tags;
                        Liked.dv_duration = Video.dv_duration;
                        Liked.dv_size = Video.dv_size;
                        Liked.dv_category_id = Video.dv_category_id;
                        Liked.dv_views = Video.dv_views;
                        Liked.dv_featured = Video.dv_featured;
                        Liked.dv_registered = Video.dv_registered;
                        Liked.dv_org_thumbnail = Video.dv_org_thumbnail;
                        Liked.dv_video_type = Video.dv_video_type;
                        Liked.dv_video_id_ = Video.dv_video_id_;
                        Liked.dv_source = Video.dv_source;
                        Liked.dv_url = Video.dv_url;
                        Liked.dv_edit_description = Video.dv_edit_description;
                        Liked.dv_markup_description = Video.dv_markup_description;
                        Liked.dv_is_liked = Video.dv_is_liked;
                        Liked.dv_is_disliked = Video.dv_is_disliked;
                        Liked.dv_is_owner = Video.dv_is_owner;
                        Liked.dv_time_alpha = Video.dv_time_alpha;
                        Liked.dv_time_ago = Video.dv_time_ago;
                        Liked.dv_category_name = Video.dv_category_name;

                        //Owner
                        Liked.Owner_id = Video.Owner_id;
                        Liked.Owner_username = Video.Owner_username;
                        Liked.Owner_email = Video.Owner_email;
                        Liked.Owner_first_name = Video.Owner_first_name;
                        Liked.Owner_last_name = Video.Owner_last_name;
                        Liked.Owner_gender = Video.Owner_gender;
                        Liked.Owner_language = Video.Owner_language;
                        Liked.Owner_avatar = Video.Owner_avatar;
                        Liked.Owner_cover = Video.Owner_cover;
                        Liked.Owner_about = Video.Owner_about;
                        Liked.Owner_google = Video.Owner_google;
                        Liked.Owner_facebook = Video.Owner_facebook;
                        Liked.Owner_twitter = Video.Owner_twitter;
                        Liked.Owner_verified = Video.Owner_verified;
                        Liked.Owner_is_pro = Video.Owner_is_pro;
                        Liked.Owner_url = Video.Owner_url;

                        Classes.LikedVideosList.Add(Liked);
                    }
                    LikedVideosListView.EndRefresh();
                    LikedVideosListView.IsVisible = true;
                    EmptyPage.IsVisible = false;
                    LikedVideosListView.ItemsSource = Classes.LikedVideosList;
                }
                else
                {
                    LikedVideosListView.EndRefresh();
                    EmptyPage.IsVisible = true;
                    LikedVideosListView.IsVisible = false;
                }
            }
            catch (Exception ex)
            {
                LikedVideosListView.EndRefresh();
                var exception = ex.ToString();
                LikedVideosListView.IsVisible = false;
                EmptyPage.IsVisible = true;
            }
        }

        private void LikedVideosListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                LikedVideosListView.SelectedItem = null;
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        //Event click Item >> open Video_Player_Page
        private async void LikedVideosListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                var selectedItem = e.Item as Classes.LikedVideos;
                if (selectedItem != null)
                {
                    //Convert From Classes.LikedVideos
                    Classes.Video Liked = new Classes.Video();

                    //Data video
                    Liked.dv_id = selectedItem.dv_id;
                    Liked.dv_video_id = selectedItem.dv_video_id;
                    Liked.dv_user_id = selectedItem.dv_user_id;
                    Liked.dv_title = selectedItem.dv_title;
                    Liked.dv_description = selectedItem.dv_description;
                    Liked.dv_Long_title = selectedItem.dv_Long_title;
                    Liked.dv_Long_description = selectedItem.dv_Long_description;
                    Liked.dv_thumbnail = selectedItem.dv_thumbnail;
                    Liked.dv_video_location = selectedItem.dv_video_location;
                    Liked.dv_youtube = selectedItem.dv_youtube;
                    Liked.dv_vimeo = selectedItem.dv_vimeo;
                    Liked.dv_daily = selectedItem.dv_daily;
                    Liked.dv_time = selectedItem.dv_time;
                    Liked.dv_time_date = selectedItem.dv_time_date;
                    Liked.dv_active = selectedItem.dv_active;
                    Liked.dv_tags = selectedItem.dv_tags;
                    Liked.dv_duration = selectedItem.dv_duration;
                    Liked.dv_size = selectedItem.dv_size;
                    Liked.dv_category_id = selectedItem.dv_category_id;
                    Liked.dv_views = selectedItem.dv_views;
                    Liked.dv_featured = selectedItem.dv_featured;
                    Liked.dv_registered = selectedItem.dv_registered;
                    Liked.dv_org_thumbnail = selectedItem.dv_org_thumbnail;
                    Liked.dv_video_type = selectedItem.dv_video_type;
                    Liked.dv_video_id_ = selectedItem.dv_video_id_;
                    Liked.dv_source = selectedItem.dv_source;
                    Liked.dv_url = selectedItem.dv_url;
                    Liked.dv_edit_description = selectedItem.dv_edit_description;
                    Liked.dv_markup_description = selectedItem.dv_markup_description;
                    Liked.dv_is_liked = selectedItem.dv_is_liked;
                    Liked.dv_is_disliked = selectedItem.dv_is_disliked;
                    Liked.dv_is_owner = selectedItem.dv_is_owner;
                    Liked.dv_time_alpha = selectedItem.dv_time_alpha;
                    Liked.dv_time_ago = selectedItem.dv_time_ago;
                    Liked.dv_category_name = selectedItem.dv_category_name;

                    //Owner
                    Liked.Owner_id = selectedItem.Owner_id;
                    Liked.Owner_username = selectedItem.Owner_username;
                    Liked.Owner_email = selectedItem.Owner_email;
                    Liked.Owner_first_name = selectedItem.Owner_first_name;
                    Liked.Owner_last_name = selectedItem.Owner_last_name;
                    Liked.Owner_gender = selectedItem.Owner_gender;
                    Liked.Owner_language = selectedItem.Owner_language;
                    Liked.Owner_avatar = selectedItem.Owner_avatar;
                    Liked.Owner_cover = selectedItem.Owner_cover;
                    Liked.Owner_about = selectedItem.Owner_about;
                    Liked.Owner_google = selectedItem.Owner_google;
                    Liked.Owner_facebook = selectedItem.Owner_facebook;
                    Liked.Owner_twitter = selectedItem.Owner_twitter;
                    Liked.Owner_verified = selectedItem.Owner_verified;
                    Liked.Owner_is_pro = selectedItem.Owner_is_pro;
                    Liked.Owner_url = selectedItem.Owner_url;

                    await Navigation.PushAsync(new Video_Player_Page(Liked));
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        //Event Refreshing LikedVideosListView 
        private void LikedVideosListView_OnRefreshing(object sender, EventArgs e)
        {
            try
            {
                LikedVideos();
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }
        
        private void LikedVideos_Page_OnAppearing(object sender, EventArgs e)
        {
            try
            {
                LikedVideos();
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        private async void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            try
            {
                var Item = (Classes.Video)((Label)sender).BindingContext;
                if (Item != null)
                {
                    var action = await App.Current.MainPage.DisplayActionSheet("Choose your action", "Cancel", null, "Delete", "Share");

                    if (action == "Share")
                    {
                        if (!CrossShare.IsSupported)
                        {
                            return;
                        }
                        await CrossShare.Current.Share(new ShareMessage
                        {
                            Title = Item.dv_title,
                            Text = Item.dv_description,
                            Url = Item.dv_url
                        });
                    }
                    else if (action == "Delete")
                    {
                        
                    }
                }
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
                LikedVideosListView.BackgroundColor = Color.FromHex("#444");
                MainStackPanel.BackgroundColor = Color.FromHex("#444");
                Lbl_Dont_have.TextColor = Color.FromHex("#ffff");
                Lbl_Go_back.TextColor = Color.FromHex("#ffff");
            }
        }
    }
}