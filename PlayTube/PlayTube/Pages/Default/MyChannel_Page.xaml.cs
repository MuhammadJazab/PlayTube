using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PlayTube.Controls;
using PlayTube.Dependencies;
using PlayTube.Languish;
using PlayTube.SQLite;
using Plugin.Connectivity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ValueChangedEventArgs = SegmentedControl.FormsPlugin.Abstractions.ValueChangedEventArgs;

namespace PlayTube.Pages.Default
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyChannel_Page : ContentPage
    {
        #region Variables

        private string URl , Id_MyChannel;
        public static string Avatar;

        #endregion

        public MyChannel_Page()
        {
            try
            {
                InitializeComponent();

                if (Device.OS == TargetPlatform.iOS)
                    NavigationPage.SetHasNavigationBar(this, true);
                else
                    NavigationPage.SetHasNavigationBar(this, false);

                if (Classes.MyChannelList.Count > 0)
                {
                    AddlistItems();
                }
                else
                {
                    var data = SQL_Commander.Get_DataMyChanne_Credentials();
                    if (data != "true")
                    {
                       // Get_MyChanne_Info_Api();
                    }
                    else
                    {
                        AddlistItems();
                    }
                }
                StyleThemeChanger();
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        public async void Get_MyChanne_Info_Api()
        {
            try
            {
                if (!CrossConnectivity.Current.IsConnected)
                {
                    await DisplayAlert(AppResources.Label_Error, AppResources.Label_Check_Your_Internet, AppResources.Label_OK);
                }
                else
                {
                    Classes.MyChannelList.Clear();
                    Classes.VideoByMyChannelList.Clear();

                    var data_Channel = await API_Request.Get_Channel_Info_Http(API_Request.User_id);
                    if (data_Channel != null)
                    {
                        JObject Items = JObject.FromObject(data_Channel);

                        Classes.Channel ch = new Classes.Channel();

                        string id = Items["id"].ToString();
                        string username = Items["username"].ToString();
                        string email = Items["email"].ToString();
                        string first_name = Items["first_name"].ToString();
                        string last_name = Items["last_name"].ToString();
                        string gender = Items["gender"].ToString();
                        string language = Items["language"].ToString();
                        string avatar = Items["avatar"].ToString();
                        string cover = Items["cover"].ToString();
                        string about = "";
                        try
                        {
                            about = Functions.StringNullRemover(Functions.DecodeStringWithEnter(Functions.DecodeString(Items["about"].ToString())));
                        }
                        catch (Exception ex)
                        {
                            var exception = ex.ToString();
                        }
                        string google = Functions.StringNullRemover(Items["google"].ToString());
                        string facebook = Functions.StringNullRemover(Items["facebook"].ToString());
                        string twitter = Functions.StringNullRemover(Items["twitter"].ToString());
                        string verified = Items["verified"].ToString();
                        string is_pro = Items["is_pro"].ToString();
                        string url = Items["url"].ToString();

                        ch.Channel_id = id;
                        ch.Channel_username = username;
                        ch.Channel_email = email;
                        ch.Channel_first_name = first_name;
                        ch.Channel_last_name = last_name;
                        ch.Channel_gender = gender;
                        ch.Channel_language = language;
                        ch.Channel_avatar = avatar;
                        ch.Channel_cover = cover;
                        ch.Channel_about = about;
                        ch.Channel_google = google;;
                        ch.Channel_facebook =facebook;
                        ch.Channel_twitter = twitter;
                        ch.Channel_verified = verified;
                        ch.Channel_is_pro = is_pro;
                        ch.Channel_url = url;


                        if (Settings.DarkTheme)
                        {
                            ch.SC_BackgroundColor = "#bcbcbc";
                            ch.SC_TextColor = "#ffff";
                        }
                        else
                        {
                            ch.SC_TextColor = "#444";
                            ch.SC_BackgroundColor = "#ffff";
                        }
                      
                        Avatar = avatar;
                        URl = url;
                        Id_MyChannel = id;

                        Classes.MyChannelList.Add(ch);

                        //Insert Or Update data at database
                        SQL_Commander.InsertOrUpdate_DataMyChanne_Credentials(ch);

                        AddlistItems();
                        MyVidoesListView.EndRefresh();
                    }
                    else
                    {
                        MyVidoesListView.EndRefresh();
                    }
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
                MyVidoesListView.EndRefresh();
            }
        }

        //Add Items data at ChannelprofileList
        public  void AddlistItems()
        {
            try
            {
                var dataMyChannel = Classes.MyChannelList.FirstOrDefault();
                if (dataMyChannel != null)
                {
                    CoverImage.Source = dataMyChannel.Channel_cover;
                    AvatarImage.Source = dataMyChannel.Channel_avatar;

                    if (!String.IsNullOrEmpty(dataMyChannel.Channel_first_name) &&!String.IsNullOrEmpty(dataMyChannel.Channel_last_name))
                        Lbl_Username.Text = dataMyChannel.Channel_first_name + " " + dataMyChannel.Channel_last_name;
                    else
                        Lbl_Username.Text = dataMyChannel.Channel_username;

                    URl = dataMyChannel.Channel_url;
                    Id_MyChannel = dataMyChannel.Channel_id;

                    if (Classes.ChannelUsersprofileListItems.Count > 0)
                    {
                        Classes.ChannelUsersprofileListItems.Clear();
                    }

                    Classes.ChannelUsersprofileListItems.Add(new Classes.Channelprofiletems()
                    {
                        Label = dataMyChannel.Channel_about,
                        Icon = "\uf040",
                        Color = "#34465d",
                        TextColor = "#444"
                    });
                    Classes.ChannelUsersprofileListItems.Add(new Classes.Channelprofiletems()
                    {
                        Label = dataMyChannel.Channel_facebook,
                        Icon = "\uf09a",
                        Color = "#3b5999",
                        TextColor = "#444"
                    });
                    Classes.ChannelUsersprofileListItems.Add(new Classes.Channelprofiletems()
                    {
                        Label = dataMyChannel.Channel_google,
                        Icon = "\uf0d5",
                        Color = "#dd4b39",
                        TextColor = "#444"
                    });
                    Classes.ChannelUsersprofileListItems.Add(new Classes.Channelprofiletems()
                    {
                        Label = dataMyChannel.Channel_twitter,
                        Icon = "\uf099",
                        Color = "#55acee",
                        TextColor = "#444"
                    });

                    UserInfoListView.ItemsSource = Classes.ChannelUsersprofileListItems;
                }

                if (Classes.VideoByMyChannelList.Count > 0)
                {
                    EmptyVideos.IsVisible = false;
                    MyVidoesListView.IsVisible = true;

                    MyVidoesListView.ItemsSource = Classes.VideoByMyChannelList;
                }
                else
                {
                    GetVideoByMyChannel_Api("0");
                }

                if (Classes.PlayListsVideosList.Count > 0)
                {
                    EmptyPlayLists.IsVisible = false;
                    PlayListsVideosListView.IsVisible = true;

                    PlayListsVideosListView.ItemsSource = Classes.PlayListsVideosList;
                }
                else
                {
                    PlayListsVideos_API();
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        //Get My Video By my Channel Api
        public async void GetVideoByMyChannel_Api(string offset)
        {
            try
            {
                var data = await API_Request.Get_Videos_By_Channel_Http(Id_MyChannel, "20", offset);
                if (data != null)
                {
                    foreach (var All in data)
                    {
                        EmptyVideos.IsVisible = false;
                        MyVidoesListView.IsVisible = true;

                        Classes.Video V = new Classes.Video();

                        try
                        {
                            var id = All["id"].ToString();
                            var video_id = All["video_id"].ToString();
                            var user_id = All["user_id"].ToString();
                            var title = All["title"].ToString();
                            var description = All["description"].ToString();
                            var thumbnail = All["thumbnail"].ToString();
                            var video_location = All["video_location"].ToString();
                            var youtube = All["youtube"].ToString();
                            var vimeo = All["vimeo"].ToString();
                            var daily = All["daily"].ToString();
                            var time = All["time"].ToString();
                            var time_date = All["time_date"].ToString();
                            var active = All["active"].ToString();
                            var tags = All["tags"].ToString();
                            var duration = All["duration"].ToString();
                            var size = All["size"].ToString();
                            var category_id = All["category_id"].ToString();
                            var views = All["views"].ToString();
                            var featured = All["featured"].ToString();
                            var registered = All["registered"].ToString();
                            var org_thumbnail = All["org_thumbnail"].ToString(); //Video_Image
                            var video_type = All["video_type"].ToString();
                            var video_id_ = All["video_id_"].ToString();
                            var source = All["source"].ToString();
                            var url = All["url"].ToString();
                            var edit_description = All["edit_description"].ToString();
                            var markup_description = All["markup_description"].ToString();
                            var is_liked = All["is_liked"].ToString();
                            var is_disliked = All["is_disliked"].ToString();
                            var is_owner = All["is_owner"].ToString();
                            var time_alpha = All["time_alpha"].ToString();
                            var time_ago = All["time_ago"].ToString();
                            var category_name = All["category_name"].ToString();

                            //style
                            V.SV_Type_video = "";
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
                            V.dv_title =Functions.SubStringCutOf(Functions.DecodeStringWithEnter(Functions.DecodeString(title)),30);
                            V.dv_description =Functions.SubStringCutOf(Functions.DecodeStringWithEnter(Functions.DecodeString(description)), 60);
                            V.dv_Long_title = Functions.DecodeStringWithEnter(Functions.DecodeString(title));
                            V.dv_Long_description =Functions.DecodeStringWithEnter(Functions.DecodeString(description));
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
                            V.dv_views = Functions.FormatPriceValue(Convert.ToInt32(views)) + " " + AppResources.Label_Views;
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
                                JObject Owner = JObject.FromObject(All["owner"]);

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
                                    about = Functions.StringNullRemover(Owner["about"].ToString());
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
                            //Add list All data myVideo By My Channel
                            Classes.VideoByMyChannelList.Add(V);
                        }
                        catch (Exception ex)
                        {
                            var exception = ex.ToString();
                        }
                    }
                    MyVidoesListView.ItemsSource = Classes.VideoByMyChannelList;
                }
                else
                {
                    EmptyVideos.IsVisible = true;
                    MyVidoesListView.IsVisible = false;
                }

            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
                EmptyVideos.IsVisible = true;
                MyVidoesListView.IsVisible = false;
            }
        }

        //Get PlayLists Videos in API
        public async void PlayListsVideos_API()
        {
            try
            {
                Classes.PlayListsVideosList.Clear();
                Classes.SubPlayListsVideosList.Clear();

                var playlists = await API_Request.Get_My_Playlists_Http("0", "10");
                if (playlists != null)
                {
                    EmptyPlayLists.IsVisible = false;
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
                            PlayLists.dp_description =Functions.DecodeStringWithEnter(Functions.DecodeString(data_description));
                            PlayLists.dp_privacy = data_privacy;
                            PlayLists.dp_views = data_views;
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
                                Classes.PlayListsVideos.SubPlayListsVideos sub =
                                    new Classes.PlayListsVideos.SubPlayListsVideos();

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
                                    sub.SV_Type_video = data_list_id; // Type ideo by Id PlayLists
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
                                    sub.dv_title =Functions.SubStringCutOf(Functions.DecodeStringWithEnter(Functions.DecodeString(title)), 30);
                                    sub.dv_Long_title = Functions.DecodeStringWithEnter(Functions.DecodeString(title));
                                    sub.dv_thumbnail = thumbnail;
                                    sub.dv_time = time;
                                    sub.dv_time_date = time_date;
                                    sub.dv_duration = duration;
                                    sub.dv_views = Functions.FormatPriceValue(Convert.ToInt32(views)) + " " +
                                                   AppResources.Label_Views;
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
                                            Owner_about =
                                                Functions.StringNullRemover(Functions.DecodeStringWithEnter(
                                                    Functions.DecodeString(Owner["about"].ToString())));
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
                            var checker = Classes.SubPlayListsVideosList.Where(a => a.SV_Type_video == data_list_id)
                                .ToList();
                            if (checker != null)
                            {
                                PlayLists.dp_totalSubVideo = checker.Count.ToString();
                                PlayLists.dp_totalSubVideo_String =
                                    checker.Count.ToString() + " " + AppResources.Label_Videos;
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
                    PlayListsVideosListView.ItemsSource = Classes.PlayListsVideosList;
                }
                else
                {
                    EmptyPlayLists.IsVisible = true;
                    PlayListsVideosListView.IsVisible = false;
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
                EmptyPlayLists.IsVisible = true;
                PlayListsVideosListView.IsVisible = false;
            }
        }

        #region Event

        //Event Copy To URL post
        private void CopyUrlButton_OnClicked(object sender, EventArgs e)
        {
            try
            {
                LblCopyUrl.Text = "\uf00c";
                DependencyService.Get<IMethods>().CopyToClipboard(URl);
                DependencyService.Get<IMethods>().LongAlert(AppResources.Label_Url_Copyed);
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        private void UserInfoListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                UserInfoListView.SelectedItem = null;
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        //Event click Item >> open *** null ***
        private void UserInfoListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        private void MyVidoesListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                MyVidoesListView.SelectedItem = null;
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        //Event click Item >> open Video_Player_Page
        private async void MyVidoesListView_OnItemTapped(object sender, ItemTappedEventArgs e)
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

        //Event click Refreshing >> Get More My Video By my Channel
        private void MyVidoesListView_OnRefreshing(object sender, EventArgs e)
        {
            if (Classes.MyChannelList.Count > 0)
            {
              
            }
            else
            {
                var data = SQL_Commander.Get_DataMyChanne_Credentials();
                if (data != "true")
                {
                    Get_MyChanne_Info_Api();
                }
                else
                {
                    AddlistItems();
                }
            }
        }

        //Event Item Appearing >> Get More My Video By my Channel  
        private async void MyVidoesListView_OnItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            try
            {
                var currentItem = e.Item as Classes.Video;
                var beforelastItem = Classes.VideoByMyChannelList[Classes.VideoByMyChannelList.Count - 2];
                if (currentItem == beforelastItem)
                {
                    if (!CrossConnectivity.Current.IsConnected)
                    {
                        await DisplayAlert(AppResources.Label_Error, AppResources.Label_Check_Your_Internet, AppResources.Label_OK);
                    }
                    else
                    {
                        Title = AppResources.Label_Loading;
                        var lastItem = Classes.VideoByMyChannelList[Classes.VideoByMyChannelList.Count - 1];
                        if (lastItem != null)
                        {
                            GetVideoByMyChannel_Api(lastItem.dv_id);
                        }
                    }
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

        //Event click Item >> open Video_Player_Page
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

        //Event Icon back
        private void OnbackIconTapped(object sender, EventArgs e)
        {
            try
            {
                Navigation.PopAsync(true);
            }
            catch (Exception ex)
            {
                Navigation.PopModalAsync(true);
                var exception = ex.ToString();
            }
        }

        #endregion

        //TOOLBAR
        private void SegControl_OnValueChanged(object sender, ValueChangedEventArgs e)
        {
            try
            {
                switch (e.NewValue)
                {
                    case 0:
                        VideosStack.IsVisible = true;
                        PlayListStack.IsVisible = false;
                        AboutStack.IsVisible = false;
                        break;
                    case 1:
                        VideosStack.IsVisible = false;
                        PlayListStack.IsVisible = true;
                        AboutStack.IsVisible = false;
                        break;
                    case 2:
                        VideosStack.IsVisible = false;
                        PlayListStack.IsVisible = false;
                        AboutStack.IsVisible = true;
                        break;
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        public void StyleThemeChanger()
        {
            try
            {
                if (Settings.DarkTheme)
                {
                    MyVidoesListView.BackgroundColor = Color.FromHex("#444");
                    PlayListsVideosListView.BackgroundColor = Color.FromHex("#444");
                    UserInfoListView.BackgroundColor = Color.FromHex("#444");
                    EmptyVideos.BackgroundColor = Color.FromHex("#444");
                    Label_No_videos.TextColor = Color.FromHex("#ffff");
                    EmptyPlayLists.BackgroundColor = Color.FromHex("#444");
                    Label_Empty_PlayLists.TextColor = Color.FromHex("#ffff");
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

    }
}