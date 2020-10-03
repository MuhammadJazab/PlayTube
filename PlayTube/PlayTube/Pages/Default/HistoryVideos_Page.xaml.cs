using System;
using System.Linq;
using PlayTube.Controls;
using PlayTube.SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PlayTube.Pages.Default
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryVideos_Page : ContentPage
    {
        public HistoryVideos_Page()
        {
            try
            {
                InitializeComponent();

                if (Classes.HistoryVideosList.Count == 0)
                {
                    HistoryVideosListView.BeginRefresh();
                }
                HistoryVideosListView.ItemsSource = Classes.HistoryVideosList;

                StyleThemeChanger();
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        //Get History Videos in Database
        public void HistoryVideos()
        {
            try
            {
                Classes.HistoryVideosList.Clear();

                var HistoryVideos = SQL_Commander.Get_HistoryVideos_Credentials();
                if (Classes.HistoryVideosList != null)
                {
                    Classes.HistoryList = HistoryVideos;

                    int x = 0;
                    foreach (var Video in HistoryVideos)
                    {
                        if (x < 16)
                        {
                            Classes.HistoryVideos History = new Classes.HistoryVideos();

                            History.SV_Type_video = "History";
                            if (Settings.DarkTheme)
                            {
                                History.SV_BackgroundColor = "#bcbcbc";
                                History.SV_TextColor = "#ffff";
                            }
                            else
                            {
                                History.SV_TextColor = "#444";
                                History.SV_BackgroundColor = "#ffff";
                            }
                            //Data video
                            History.dv_id = Video.dv_id;
                            History.dv_video_id = Video.dv_video_id;
                            History.dv_user_id = Video.dv_user_id;
                            History.dv_title = Video.dv_title;
                            History.dv_description = Video.dv_description;
                            History.dv_Long_title = Video.dv_Long_title;
                            History.dv_Long_description = Video.dv_Long_description;
                            History.dv_thumbnail = Video.dv_thumbnail;
                            History.dv_video_location = Video.dv_video_location;
                            History.dv_youtube = Video.dv_youtube;
                            History.dv_vimeo = Video.dv_vimeo;
                            History.dv_daily = Video.dv_daily;
                            History.dv_time = Video.dv_time;
                            History.dv_time_date = Video.dv_time_date;
                            History.dv_active = Video.dv_active;
                            History.dv_tags = Video.dv_tags;
                            History.dv_duration = Video.dv_duration;
                            History.dv_size = Video.dv_size;
                            History.dv_category_id = Video.dv_category_id;
                            History.dv_views = Video.dv_views;
                            History.dv_featured = Video.dv_featured;
                            History.dv_registered = Video.dv_registered;
                            History.dv_org_thumbnail = Video.dv_org_thumbnail;
                            History.dv_video_type = Video.dv_video_type;
                            History.dv_video_id_ = Video.dv_video_id_;
                            History.dv_source = Video.dv_source;
                            History.dv_url = Video.dv_url;
                            History.dv_edit_description = Video.dv_edit_description;
                            History.dv_markup_description = Video.dv_markup_description;
                            History.dv_is_liked = Video.dv_is_liked;
                            History.dv_is_disliked = Video.dv_is_disliked;
                            History.dv_is_owner = Video.dv_is_owner;
                            History.dv_time_alpha = Video.dv_time_alpha;
                            History.dv_time_ago = Video.dv_time_ago;
                            History.dv_category_name = Video.dv_category_name;

                            //Owner
                            History.Owner_id = Video.Owner_id;
                            History.Owner_username = Video.Owner_username;
                            History.Owner_email = Video.Owner_email;
                            History.Owner_first_name = Video.Owner_first_name;
                            History.Owner_last_name = Video.Owner_last_name;
                            History.Owner_gender = Video.Owner_gender;
                            History.Owner_language = Video.Owner_language;
                            History.Owner_avatar = Video.Owner_avatar;
                            History.Owner_cover = Video.Owner_cover;
                            History.Owner_about = Video.Owner_about;
                            History.Owner_google = Video.Owner_google;
                            History.Owner_facebook = Video.Owner_facebook;
                            History.Owner_twitter = Video.Owner_twitter;
                            History.Owner_verified = Video.Owner_verified;
                            History.Owner_is_pro = Video.Owner_is_pro;
                            History.Owner_url = Video.Owner_url;

                            Classes.HistoryVideosList.Add(History);
                            x++;
                        }
                    }
                    HistoryVideosListView.EndRefresh();
                    HistoryVideosListView.IsVisible = true;
                    EmptyPage.IsVisible = false;
                    HistoryVideosListView.ItemsSource = Classes.HistoryVideosList;
                }
                else
                {
                    HistoryVideosListView.EndRefresh();
                    EmptyPage.IsVisible = true;
                    HistoryVideosListView.IsVisible = false;
                }
            }
            catch (Exception ex)
            {
                HistoryVideosListView.EndRefresh();
                var exception = ex.ToString();
                HistoryVideosListView.IsVisible = false;
                EmptyPage.IsVisible = true;
            }
        }

        private void HistoryVideosListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                HistoryVideosListView.SelectedItem = null;
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        //Event click Item >> open Video_Player_Page
        private async void HistoryVideosListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                var selectedItem = e.Item as Classes.HistoryVideos;
                if (selectedItem != null)
                {
                    //Convert From Classes.HistoryVideos
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
       
        //Event Refreshing HistoryVideosListView 
        private void HistoryVideosListView_OnRefreshing(object sender, EventArgs e)
        {
            try
            {
                HistoryVideos();
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        private void HistoryVideosListView_OnItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            try
            {
                if (Classes.HistoryList.Count > 0)
                {
                    int x = 0;
                    foreach (var Video in Classes.HistoryList)
                    {
                        var chk = Classes.HistoryVideosList.FirstOrDefault(a => a.dv_id == Video.dv_id);
                        if (chk == null)
                        {
                            if (x < 16)
                            {
                                Classes.HistoryVideos History = new Classes.HistoryVideos();

                                History.SV_Type_video = "History";
                                if (Settings.DarkTheme)
                                {
                                    History.SV_BackgroundColor = "#bcbcbc";
                                    History.SV_TextColor = "#ffff";
                                }
                                else
                                {
                                    History.SV_TextColor = "#444";
                                    History.SV_BackgroundColor = "#ffff";
                                }
                                //Data video
                                History.dv_id = Video.dv_id;
                                History.dv_video_id = Video.dv_video_id;
                                History.dv_user_id = Video.dv_user_id;
                                History.dv_title = Video.dv_title;
                                History.dv_description = Video.dv_description;
                                History.dv_Long_title = Video.dv_Long_title;
                                History.dv_Long_description = Video.dv_Long_description;
                                History.dv_thumbnail = Video.dv_thumbnail;
                                History.dv_video_location = Video.dv_video_location;
                                History.dv_youtube = Video.dv_youtube;
                                History.dv_vimeo = Video.dv_vimeo;
                                History.dv_daily = Video.dv_daily;
                                History.dv_time = Video.dv_time;
                                History.dv_time_date = Video.dv_time_date;
                                History.dv_active = Video.dv_active;
                                History.dv_tags = Video.dv_tags;
                                History.dv_duration = Video.dv_duration;
                                History.dv_size = Video.dv_size;
                                History.dv_category_id = Video.dv_category_id;
                                History.dv_views = Video.dv_views;
                                History.dv_featured = Video.dv_featured;
                                History.dv_registered = Video.dv_registered;
                                History.dv_org_thumbnail = Video.dv_org_thumbnail;
                                History.dv_video_type = Video.dv_video_type;
                                History.dv_video_id_ = Video.dv_video_id_;
                                History.dv_source = Video.dv_source;
                                History.dv_url = Video.dv_url;
                                History.dv_edit_description = Video.dv_edit_description;
                                History.dv_markup_description = Video.dv_markup_description;
                                History.dv_is_liked = Video.dv_is_liked;
                                History.dv_is_disliked = Video.dv_is_disliked;
                                History.dv_is_owner = Video.dv_is_owner;
                                History.dv_time_alpha = Video.dv_time_alpha;
                                History.dv_time_ago = Video.dv_time_ago;
                                History.dv_category_name = Video.dv_category_name;

                                //Owner
                                History.Owner_id = Video.Owner_id;
                                History.Owner_username = Video.Owner_username;
                                History.Owner_email = Video.Owner_email;
                                History.Owner_first_name = Video.Owner_first_name;
                                History.Owner_last_name = Video.Owner_last_name;
                                History.Owner_gender = Video.Owner_gender;
                                History.Owner_language = Video.Owner_language;
                                History.Owner_avatar = Video.Owner_avatar;
                                History.Owner_cover = Video.Owner_cover;
                                History.Owner_about = Video.Owner_about;
                                History.Owner_google = Video.Owner_google;
                                History.Owner_facebook = Video.Owner_facebook;
                                History.Owner_twitter = Video.Owner_twitter;
                                History.Owner_verified = Video.Owner_verified;
                                History.Owner_is_pro = Video.Owner_is_pro;
                                History.Owner_url = Video.Owner_url;

                                Classes.HistoryVideosList.Add(History);
                                x++;
                            }
                        }
                    }
                    HistoryVideosListView.ItemsSource = Classes.HistoryVideosList;
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
                MainStackPanel.BackgroundColor = Color.FromHex("#444");
                Lbl_Dont_have.TextColor = Color.FromHex("#ffff");
                Lbl_Go_back.TextColor = Color.FromHex("#ffff");
                HistoryVideosListView.BackgroundColor = Color.FromHex("#444");
                EmptyPage.BackgroundColor = Color.FromHex("#444");
            }
        }
    }
}