using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PlayTube.Controls;
using PlayTube.Languish;
using PlayTube.Pages.Custom_View;
using Plugin.Connectivity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PlayTube.Pages.Default
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlayListsVideos_Page : ContentPage
    {
        public PlayListsVideos_Page()
        {
            try
            {
                InitializeComponent();


                if (Classes.PlayListsVideosList.Count == 0)
                {
                    PlayListsVideosListView.BeginRefresh();
                }
                PlayListsVideosListView.ItemsSource = Classes.PlayListsVideosList;
                
                StyleThemeChanger();
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        //Get PlayLists Videos in API
        public async void PlayListsVideos_API()
        {
            try
            {
                if (!CrossConnectivity.Current.IsConnected)
                {
                    // await DisplayAlert(AppResources.Label_Error, AppResources.Label_Check_Your_Internet,AppResources.Label_OK);
                    PlayListsVideosListView.EndRefresh();
                    EmptyPage.IsVisible = true;
                    PlayListsVideosListView.IsVisible = false;

                    Icon_page.Text = "\uf119";
                    Label_Empty.Text = AppResources.Label_Offline_Mode;
                    Label_Go_back.Text = AppResources.Label_Check_Your_Internet;
                }
                else
                {
                    Classes.PlayListsVideosList.Clear();
                    Classes.SubPlayListsVideosList.Clear();

                    var playlists = await API_Request.Get_My_Playlists_Http("0", "10");
                    if (playlists != null)
                    {
                        EmptyPage.IsVisible = false;
                        PlayListsVideosListView.IsVisible = true;

                        foreach (var Items in playlists)
                        {
                            try // Data PlayLists
                            {
                                Classes.PlayListsVideos PlayLists = new Classes.PlayListsVideos();

                                string data_id = Items["id"].ToString();
                                string data_list_id = Items["list_id"].ToString();
                                string data_user_id = Items["user_id"].ToString();
                                string data_name = Items["name"].ToString();
                                string data_description = Items["description"].ToString();
                                string data_privacy = Items["privacy"].ToString();
                                string data_views = Items["views"].ToString();
                                string data_icon = Items["icon"].ToString();
                                string data_time = Items["time"].ToString();

                                PlayLists.dp_id = data_id;
                                PlayLists.dp_list_id = data_list_id;
                                PlayLists.dp_user_id = data_user_id;
                                PlayLists.dp_name = data_name;
                                PlayLists.dp_description = Functions.DecodeStringWithEnter(Functions.DecodeString(data_description));
                                PlayLists.dp_privacy = data_privacy;
                                PlayLists.dp_views = Functions.FormatPriceValue(Convert.ToInt32(data_views)) + " " + AppResources.Label_Views;
                                PlayLists.dp_icon = data_icon;
                                PlayLists.dp_time = data_time;
                                if (Settings.DarkTheme)
                                {
                                    PlayLists.dp_BackgroundColor = "#bcbcbc";
                                    PlayLists.dp_TextColor = "#ffff";
                                }
                                else
                                {
                                    PlayLists.dp_TextColor = "#444";
                                    PlayLists.dp_BackgroundColor = "#ffff";
                                }
                                var videos_data = JObject.Parse(Items.ToString(Formatting.None)).SelectToken("videos").ToString();
                                JArray dataVideos = JArray.Parse(videos_data);

                                foreach (var All in dataVideos)
                                {
                                    Classes.PlayListsVideos.SubPlayListsVideos sub = new Classes.PlayListsVideos.SubPlayListsVideos();
                                   
                                    try //videos
                                    {
                                        var id = All["id"].ToString();
                                        var video_id = All["video_id"].ToString();
                                        var user_id = All["user_id"].ToString();
                                        var title = All["title"].ToString();
                                        var thumbnail = All["thumbnail"].ToString();
                                        var time = All["time"].ToString();
                                        var time_date = All["time_date"].ToString();
                                        var duration = All["duration"].ToString();
                                        var views = All["views"].ToString();
                                        var url = All["url"].ToString();

                                        //style
                                        sub.SV_Type_video = data_list_id; // Type video by Id PlayLists
                                        if (Settings.DarkTheme)
                                        {
                                            sub.SV_BackgroundColor = "#bcbcbc";
                                            sub.SV_TextColor = "#ffff";
                                        }
                                        else
                                        {
                                            sub.SV_TextColor = "#444";
                                            sub.SV_BackgroundColor = "#ffff";
                                        }
                                        //Data video
                                        sub.dv_id = id;
                                        sub.dv_video_id = video_id;
                                        sub.dv_user_id = user_id;
                                        sub.dv_title = Functions.SubStringCutOf(Functions.DecodeStringWithEnter(Functions.DecodeString(title)), 30);
                                        sub.dv_Long_title = Functions.DecodeStringWithEnter(Functions.DecodeString(title));
                                        sub.dv_thumbnail = thumbnail;
                                        sub.dv_time = time;
                                        sub.dv_time_date = time_date;
                                        sub.dv_duration = duration;
                                        sub.dv_views = Functions.FormatPriceValue(Convert.ToInt32(views)) + " " + AppResources.Label_Views;
                                        sub.dv_url = url;

                                        try //owner
                                        {
                                            JObject Owner = JObject.FromObject(All["owner"]);

                                            var Owner_id = Owner["id"].ToString();
                                            var Owner_username = Owner["username"].ToString();
                                            var Owner_email = Owner["email"].ToString();
                                            var Owner_first_name = Owner["first_name"].ToString();
                                            var Owner_last_name = Owner["last_name"].ToString();
                                            var Owner_gender = Owner["gender"].ToString();
                                            var Owner_language = Owner["language"].ToString();
                                            var Owner_avatar = Owner["avatar"].ToString();
                                            var Owner_cover = Owner["cover"].ToString();
                                            string Owner_about = "";
                                            try
                                            {
                                                Owner_about = Functions.StringNullRemover(Functions.DecodeStringWithEnter(Functions.DecodeString(Owner["about"].ToString())));
                                            }
                                            catch (Exception ex)
                                            {
                                                var exception = ex.ToString();
                                            }
                                            var Owner_google = Owner["google"].ToString();
                                            var Owner_facebook = Owner["facebook"].ToString();
                                            var Owner_twitter = Owner["twitter"].ToString();
                                            var Owner_verified = Owner["verified"].ToString();
                                            var Owner_is_pro = Owner["is_pro"].ToString();
                                            var Owner_url = Owner["url"].ToString();

                                            sub.Owner_id = Owner_id;
                                            sub.Owner_username = Owner_username;
                                            sub.Owner_email = Owner_email;
                                            sub.Owner_first_name = Owner_first_name;
                                            sub.Owner_last_name = Owner_last_name;
                                            sub.Owner_gender = Owner_gender;
                                            sub.Owner_language = Owner_language;
                                            sub.Owner_avatar = Owner_avatar;
                                            sub.Owner_cover = Owner_cover;
                                            sub.Owner_about = Owner_about;
                                            sub.Owner_google = Owner_google;
                                            sub.Owner_facebook = Owner_facebook;
                                            sub.Owner_twitter = Owner_twitter;
                                            if (Owner_verified == "0")
                                                sub.Owner_verified = "false";
                                            else
                                                sub.Owner_verified = "true";
                                            sub.Owner_is_pro = Owner_is_pro;
                                            sub.Owner_url = Owner_url;
                                        }
                                        catch (Exception ex)
                                        {
                                            var exception = ex.ToString();
                                        }
                                        //Add list All data sub PlayLists Videos
                                        Classes.SubPlayListsVideosList.Add(sub);
                                    }
                                    catch (Exception ex)
                                    {
                                        var exception = ex.ToString();
                                    }
                                }

                                //Get count Sub Videos at PlayLists
                                var checker = Classes.SubPlayListsVideosList.Where(a => a.SV_Type_video == data_list_id).ToList();
                                if (checker != null)
                                {
                                    PlayLists.dp_totalSubVideo = checker.Count.ToString();
                                    PlayLists.dp_totalSubVideo_String = checker.Count.ToString() + " " + AppResources.Label_Videos;
                                    PlayLists.dp_Image = checker[0].dv_thumbnail;
                                }

                                //Add list All data PlayLists Videos
                                Classes.PlayListsVideosList.Add(PlayLists);
                            }
                            catch (Exception ex)
                            {
                                var exception = ex.ToString();
                            }
                        }
                        PlayListsVideosListView.EndRefresh();
                        PlayListsVideosListView.ItemsSource = Classes.PlayListsVideosList;
                    }
                    else
                    {
                        PlayListsVideosListView.EndRefresh();
                        EmptyPage.IsVisible = true;
                        PlayListsVideosListView.IsVisible = false;

                        Icon_page.Text = "\uf001";
                        Label_Empty.Text = AppResources.Label_Empty_PlayLists;
                        Label_Go_back.Text = AppResources.Label_Go_back_and_watched_any_video;
                    }
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();

                PlayListsVideosListView.EndRefresh();
                EmptyPage.IsVisible = true;
                PlayListsVideosListView.IsVisible = false;

                Icon_page.Text = "\uf001";
                Label_Empty.Text = AppResources.Label_Empty_PlayLists;
                Label_Go_back.Text = AppResources.Label_Go_back_and_watched_any_video;
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

        //Event click Item in PlayList_Item_View >> open SubPlayListsVideos_Page
        private async void PlayListsVideosListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                var selectedItem = e.Item as Classes.PlayListsVideos;
                if (selectedItem != null)
                {
                    await Navigation.PushAsync(new SubPlayListsVideos_Page(selectedItem));
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        private void PlayListsVideosListView_OnRefreshing(object sender, EventArgs e)
        {
            try
            {
                if (Classes.PlayListsVideosList.Count > 0)
                {
                    Classes.PlayListsVideosList.Clear();
                    Classes.SubPlayListsVideosList.Clear();
                }

                PlayListsVideos_API();
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        //Event click Try again #Get_PlayListsVideos
        private void TryagainButton_OnClicked(object sender, EventArgs e)
        {
            try
            {
                PlayListsVideos_API();
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        public async void OnPlayListsTapped(Object sender, EventArgs e)
        {
            try
            {
                var selectedItem = (Classes.PlayListsVideos)((PlayList_Item_View)sender).BindingContext;
                if (selectedItem != null)
                {
                    await Navigation.PushAsync(new SubPlayListsVideos_Page(selectedItem));
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
                PlayListsVideosListView.BackgroundColor = Color.FromHex("#444");
                EmptyPage.BackgroundColor = Color.FromHex("#444");
                Label_Empty.TextColor = Color.FromHex("#444");
                Label_Go_back.TextColor = Color.FromHex("#444");
            }
        }
    }
}