using System;
using System.Collections.ObjectModel;
using System.Linq;
using PlayTube.Controls;
using PlayTube.Languish;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PlayTube.Pages.Default
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SubPlayListsVideos_Page : ContentPage
    {
        private Classes.PlayListsVideos _PlayListsVideos;

        public SubPlayListsVideos_Page(Classes.PlayListsVideos playListsVideos)
        {
            try
            {
                InitializeComponent();
                if (playListsVideos != null)
                {
                    _PlayListsVideos = playListsVideos;

                    Title = _PlayListsVideos.dp_name + " " + AppResources.Label_PlayLists;

                    var checker = Classes.SubPlayListsVideosList.Where(a => a.SV_Type_video == _PlayListsVideos.dp_list_id).ToList();
                    if (checker.Count > 0)
                    {
                        PlayListsVideosListView.IsVisible = true;
                        EmptyPage.IsVisible = false;

                        ObservableCollection<Classes.PlayListsVideos.SubPlayListsVideos> subVideoList =new ObservableCollection<Classes.PlayListsVideos.SubPlayListsVideos>(checker);
                        PlayListsVideosListView.ItemsSource = subVideoList;
                    }
                    else
                    {
                        PlayListsVideosListView.IsVisible = false;
                        EmptyPage.IsVisible = true;
                    }
                }
                else
                {
                    PlayListsVideosListView.IsVisible = false;
                    EmptyPage.IsVisible = true;
                }              
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        private void PlayListsVideosListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                PlayListsVideosListView.SelectedItem = null;
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        private async void PlayListsVideosListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {

            try
            {
                var selectedItem = e.Item as Classes.PlayListsVideos.SubPlayListsVideos;
                if (selectedItem != null)
                {
                    //Convert From Classes.HistoryVideos
                    Classes.Video Sub = new Classes.Video();

                    //Data video
                    Sub.dv_id = selectedItem.dv_id;
                    Sub.dv_video_id = selectedItem.dv_video_id;
                    Sub.dv_user_id = selectedItem.dv_user_id;
                    Sub.dv_title = selectedItem.dv_title;
                    Sub.dv_Long_title = selectedItem.dv_Long_title;
                    Sub.dv_thumbnail = selectedItem.dv_thumbnail;
                    Sub.dv_time = selectedItem.dv_time;
                    Sub.dv_time_date = selectedItem.dv_time_date;
                    Sub.dv_duration = selectedItem.dv_duration;
                    Sub.dv_url = selectedItem.dv_url;

                    //Owner
                    Sub.Owner_id = selectedItem.Owner_id;
                    Sub.Owner_username = selectedItem.Owner_username;
                    Sub.Owner_email = selectedItem.Owner_email;
                    Sub.Owner_first_name = selectedItem.Owner_first_name;
                    Sub.Owner_last_name = selectedItem.Owner_last_name;
                    Sub.Owner_gender = selectedItem.Owner_gender;
                    Sub.Owner_language = selectedItem.Owner_language;
                    Sub.Owner_avatar = selectedItem.Owner_avatar;
                    Sub.Owner_cover = selectedItem.Owner_cover;
                    Sub.Owner_about = selectedItem.Owner_about;
                    Sub.Owner_google = selectedItem.Owner_google;
                    Sub.Owner_facebook = selectedItem.Owner_facebook;
                    Sub.Owner_twitter = selectedItem.Owner_twitter;
                    Sub.Owner_verified = selectedItem.Owner_verified;
                    Sub.Owner_is_pro = selectedItem.Owner_is_pro;
                    Sub.Owner_url = selectedItem.Owner_url;

                    await Navigation.PushAsync(new Video_Player_Page(Sub));
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }
    }
}