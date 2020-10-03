using System;
using System.Linq;
using Newtonsoft.Json.Linq;
using PlayTube.Controls;
using PlayTube.Dependencies;
using PlayTube.Languish;
using PlayTube.Pages.Tabbes;
using PlayTube.SQLite;
using Plugin.Connectivity;
using Plugin.Share;
using Plugin.Share.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PlayTube.Pages.Default
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Video_Player_Page : ContentPage
    {
        #region Variables

        private Classes.Video _Video_Data;

        #endregion

        public Video_Player_Page(Classes.Video Video_Data)
        {
            try
            {
                InitializeComponent();

                if (Device.OS == TargetPlatform.iOS)
                    NavigationPage.SetHasNavigationBar(this, true);
                else
                    NavigationPage.SetHasNavigationBar(this, false);

                StyleThemeChanger();

                if (Video_Data != null)
                {
                    _Video_Data = Video_Data;

                    GetVideoByChannel_Api(); //////////////// Delete /////////////// >> next up 

                    VideoPlayer.Source = Settings.WebsiteUrl + "/embed/" + Video_Data.dv_video_id + "?fullscreen=" + Settings.Use_Full_Screen+ "&autoplay="+Settings.Auto_play+ "&height=" + Settings.Video_height;
                    if (Hamburg_Page.IsLogin)
                    {
                        EmptyComments.IsVisible = false;
                        CommentsPage.IsVisible = true;
                        CommentWebLoader.Source = Settings.WebsiteUrl + "/get-video-comments?video_id=" + Video_Data.dv_video_id + "&cookie=" + API_Request.Cookie;
                    }
                    else
                    {
                        CommentsPage.IsVisible = false;
                        EmptyComments.IsVisible = true;
                    }

                    TitleLabel.Text = Video_Data.dv_Long_title;
                    ViewsLabel.Text = Video_Data.dv_views;
                    TimeLabel.Text = Video_Data.dv_time_ago;
                    ChannelImage.Source = Video_Data.Owner_avatar;

                    if (!String.IsNullOrEmpty(Video_Data.Owner_first_name) && !String.IsNullOrEmpty(Video_Data.Owner_last_name))
                        ChannelName.Text = Video_Data.Owner_first_name + " " + Video_Data.Owner_last_name;
                    else
                        ChannelName.Text = Video_Data.Owner_username;

                    if (Video_Data.Owner_verified == "True")
                    {
                        ChannelIsverified.IsVisible = true;
                    }

                    Get_New_Videos_Details_Api(Video_Data.dv_video_id);
                 
                    //<<<Here It must add the video information to History database>>> 
                    SQL_Commander.Insert_HistoryVideos_Credentials(_Video_Data);
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        //Get new data this Video Api
        public async void Get_New_Videos_Details_Api(string videoid)
        {
            try
            {
                if (!CrossConnectivity.Current.IsConnected)
                {
                    //await DisplayAlert(AppResources.Label_Error, AppResources.Label_Check_Your_Internet, AppResources.Label_OK);
                }
                else
                {
                    var Details = await API_Request.API_Get_Videos_Details_Http(videoid);
                    if (Details != null)
                    {
                        //updater list All data Video
                        var updater = Classes.VideoList.FirstOrDefault(a => a.dv_video_id == videoid);
                        if (updater != null)
                        {
                            JObject All = JObject.FromObject(Details);

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
                                //New data
                                var likes = All["likes"].ToString();
                                var dislikes = All["dislikes"].ToString();
                                var likes_percent = All["likes_percent"].ToString();
                                var dislikes_percent = All["dislikes_percent"].ToString();
                                var is_subscribed = All["is_subscribed"].ToString();

                                //style
                                updater.SV_Type_video = _Video_Data.SV_Type_video;

                                //Data video
                                updater.dv_id = id;
                                updater.dv_video_id = video_id;
                                updater.dv_user_id = user_id;
                                updater.dv_title =Functions.SubStringCutOf(Functions.DecodeStringWithEnter(Functions.DecodeString(title)), 30);
                                updater.dv_description =Functions.SubStringCutOf(Functions.DecodeStringWithEnter(Functions.DecodeString(description)), 60);
                                updater.dv_Long_title = Functions.DecodeStringWithEnter(Functions.DecodeString(title));
                                updater.dv_Long_description =Functions.DecodeStringWithEnter(Functions.DecodeString(description));
                                updater.dv_thumbnail = thumbnail;
                                updater.dv_video_location = video_location;
                                updater.dv_youtube = youtube;
                                updater.dv_vimeo = vimeo;
                                updater.dv_daily = daily;
                                updater.dv_time = time;
                                updater.dv_time_date = time_date;
                                updater.dv_active = active;
                                updater.dv_tags = tags;
                                updater.dv_duration = duration;
                                updater.dv_size = size;
                                updater.dv_category_id = category_id;
                                updater.dv_views = Functions.FormatPriceValue(Convert.ToInt32(views)) + " " + AppResources.Label_Views ;

                                ViewsLabel.Text = updater.dv_views;
                                TitleLabel.Text = updater.dv_Long_title;

                                updater.dv_featured = featured;
                                updater.dv_registered = registered;
                                updater.dv_org_thumbnail = org_thumbnail;
                                updater.dv_video_type = video_type;
                                updater.dv_video_id_ = video_id_;
                                updater.dv_source = source;
                                updater.dv_url = url;
                                updater.dv_edit_description = edit_description;
                                updater.dv_markup_description = markup_description;

                                if (is_liked == "1")
                                {
                                    updater.dv_is_liked = is_liked;
                                    IconLike.TextColor = Color.FromHex(Settings.MainColor);
                                    IconUnLike.TextColor = Color.FromHex("#8e8e8e");
                                }
                                else
                                {
                                    updater.dv_is_liked = is_liked;
                                    IconLike.TextColor = Color.FromHex("#8e8e8e");
                                }
                                if (is_disliked == "1")
                                {
                                    updater.dv_is_disliked = is_disliked;
                                    IconUnLike.TextColor = Color.FromHex(Settings.MainColor);
                                    IconLike.TextColor = Color.FromHex("#8e8e8e");

                                }
                                else
                                {
                                    updater.dv_is_disliked = is_disliked;
                                    IconUnLike.TextColor = Color.FromHex("#8e8e8e");
                                }
                                if (is_subscribed == "1")
                                {
                                    updater.dv_is_subscribed = is_subscribed;
                                    IconSubcribe.Text = IoniciconsFont.CheckmarkCircled;
                                    IconSubcribe.TextColor = Color.FromHex("#ffff");
                                    SubcribeLabel.TextColor = Color.FromHex("#ffff");
                                    SubcribeLabel.Text = AppResources.Label_Subscribed;
                                    SubcribeFrame.BackgroundColor = Color.FromHex(Settings.MainColor);
                                }
                                else
                                {
                                    updater.dv_is_subscribed = is_subscribed;
                                    IconSubcribe.TextColor = Color.FromHex(Settings.MainColor);
                                    SubcribeLabel.TextColor = Color.FromHex(Settings.MainColor);
                                    SubcribeFrame.BackgroundColor = Color.FromHex("#ffff");
                                    IconSubcribe.Text = IoniciconsFont.AndroidAdd;
                                    SubcribeLabel.Text = AppResources.Label_Subscribe;
                                }

                                updater.dv_is_owner = is_owner;
                                updater.dv_time_alpha = time_alpha;
                                updater.dv_time_ago = time_ago;
                                updater.dv_category_name = category_name;
                                updater.dv_likes = Functions.FormatPriceValue(Convert.ToInt32(likes));
                                updater.dv_dislikes = Functions.FormatPriceValue(Convert.ToInt32(dislikes));
                                updater.dv_likes_percent = likes_percent;
                                updater.dv_dislikes_percent = dislikes_percent;

                                LikeLabel.Text = updater.dv_likes;
                                UnLikeLabel.Text = updater.dv_dislikes;
                                TimeLabel.Text = updater.dv_time_ago;

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
                                    var google = Functions.StringNullRemover(Owner["google"].ToString());
                                    var facebook = Functions.StringNullRemover(Owner["facebook"].ToString());
                                    var twitter = Functions.StringNullRemover(Owner["twitter"].ToString());
                                    var verified = Owner["verified"].ToString();
                                    var is_pro = Owner["is_pro"].ToString();
                                    var O_url = Owner["url"].ToString();

                                    updater.Owner_id = O_id;
                                    updater.Owner_username = username;
                                    updater.Owner_email = email;
                                    updater.Owner_first_name = first_name;
                                    updater.Owner_last_name = last_name;
                                    updater.Owner_gender = gender;
                                    updater.Owner_language = language;
                                    updater.Owner_avatar = avatar;
                                    updater.Owner_cover = cover;
                                    updater.Owner_about = about;
                                    updater.Owner_google = google;
                                    updater.Owner_facebook = facebook;
                                    updater.Owner_twitter = twitter;
                                    if (verified == "0")
                                        updater.Owner_verified = "false";
                                    else
                                        updater.Owner_verified = "true";

                                    if (!String.IsNullOrEmpty(updater.Owner_first_name) &&
                                        !String.IsNullOrEmpty(updater.Owner_last_name))
                                        ChannelName.Text = updater.Owner_first_name + " " + updater.Owner_last_name;
                                    else
                                        ChannelName.Text = updater.Owner_username;

                                    ChannelIsverified.IsVisible = Convert.ToBoolean(updater.Owner_verified);

                                    updater.Owner_is_pro = is_pro;
                                    updater.Owner_url = O_url;
                                    ChannelImage.Source = updater.Owner_avatar;


                                    Classes.ViewsVideosList.Add(updater);

                                }
                                catch (Exception ex)
                                {
                                    var exception = ex.ToString();
                                }
                            }
                            catch (Exception ex)
                            {
                                var exception = ex.ToString();
                            }
                        }
                        else
                        {
                            Classes.Video addVideo = new Classes.Video();

                            JObject All = JObject.FromObject(Details);

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
                                //New data
                                var likes = All["likes"].ToString();
                                var dislikes = All["dislikes"].ToString();
                                var likes_percent = All["likes_percent"].ToString();
                                var dislikes_percent = All["dislikes_percent"].ToString();
                                var is_subscribed = All["is_subscribed"].ToString();

                                //style
                                addVideo.SV_Type_video = _Video_Data.SV_Type_video;

                                //Data video
                                addVideo.dv_id = id;
                                addVideo.dv_video_id = video_id;
                                addVideo.dv_user_id = user_id;
                                addVideo.dv_title = Functions.SubStringCutOf(Functions.DecodeStringWithEnter(Functions.DecodeString(title)), 30);
                                addVideo.dv_description = Functions.SubStringCutOf(Functions.DecodeStringWithEnter(Functions.DecodeString(description)), 60);
                                addVideo.dv_Long_title = Functions.DecodeStringWithEnter(Functions.DecodeString(title));
                                addVideo.dv_Long_description = Functions.DecodeStringWithEnter(Functions.DecodeString(description));
                                addVideo.dv_thumbnail = thumbnail;
                                addVideo.dv_video_location = video_location;
                                addVideo.dv_youtube = youtube;
                                addVideo.dv_vimeo = vimeo;
                                addVideo.dv_daily = daily;
                                addVideo.dv_time = time;
                                addVideo.dv_time_date = time_date;
                                addVideo.dv_active = active;
                                addVideo.dv_tags = tags;
                                addVideo.dv_duration = duration;
                                addVideo.dv_size = size;
                                addVideo.dv_category_id = category_id;
                                addVideo.dv_views = Functions.FormatPriceValue(Convert.ToInt32(views)) + " " + AppResources.Label_Views + " |";

                                ViewsLabel.Text = addVideo.dv_views;
                                TitleLabel.Text = addVideo.dv_Long_title;

                                addVideo.dv_featured = featured;
                                addVideo.dv_registered = registered;
                                addVideo.dv_org_thumbnail = org_thumbnail;
                                addVideo.dv_video_type = video_type;
                                addVideo.dv_video_id_ = video_id_;
                                addVideo.dv_source = source;
                                addVideo.dv_url = url;
                                addVideo.dv_edit_description = edit_description;
                                addVideo.dv_markup_description = markup_description;

                                if (is_liked == "1")
                                {
                                    addVideo.dv_is_liked = is_liked;
                                    IconLike.TextColor = Color.FromHex(Settings.MainColor);
                                    IconUnLike.TextColor = Color.FromHex("#8e8e8e");
                                }
                                else
                                {
                                    addVideo.dv_is_liked = is_liked;
                                    IconLike.TextColor = Color.FromHex("#8e8e8e");
                                }
                                if (is_disliked == "1")
                                {
                                    addVideo.dv_is_disliked = is_disliked;
                                    IconUnLike.TextColor = Color.FromHex(Settings.MainColor);
                                    IconLike.TextColor = Color.FromHex("#8e8e8e");
                                }
                                else
                                {
                                    addVideo.dv_is_disliked = is_disliked;
                                    IconUnLike.TextColor = Color.FromHex("#8e8e8e");
                                }
                                if (is_subscribed == "1")
                                {
                                    addVideo.dv_is_subscribed = is_subscribed;
                                    IconSubcribe.Text = IoniciconsFont.CheckmarkCircled;
                                    IconSubcribe.TextColor = Color.FromHex("#ffff");
                                    SubcribeLabel.TextColor = Color.FromHex("#ffff");
                                    SubcribeLabel.Text = AppResources.Label_Subscribed;
                                    SubcribeFrame.BackgroundColor = Color.FromHex(Settings.MainColor);
                                }
                                else
                                {
                                    addVideo.dv_is_subscribed = is_subscribed;
                                    IconSubcribe.TextColor = Color.FromHex(Settings.MainColor);
                                    SubcribeLabel.TextColor = Color.FromHex(Settings.MainColor);
                                    SubcribeFrame.BackgroundColor = Color.FromHex("#ffff");
                                    IconSubcribe.Text = IoniciconsFont.AndroidAdd;
                                    SubcribeLabel.Text = AppResources.Label_Subscribe;
                                }

                                addVideo.dv_is_owner = is_owner;
                                addVideo.dv_time_alpha = time_alpha;
                                addVideo.dv_time_ago = time_ago;
                                addVideo.dv_category_name = category_name;
                                addVideo.dv_likes = Functions.FormatPriceValue(Convert.ToInt32(likes));
                                addVideo.dv_dislikes = Functions.FormatPriceValue(Convert.ToInt32(dislikes));
                                addVideo.dv_likes_percent = likes_percent;
                                addVideo.dv_dislikes_percent = dislikes_percent;

                                LikeLabel.Text = addVideo.dv_likes;
                                UnLikeLabel.Text = addVideo.dv_dislikes;
                                TimeLabel.Text = addVideo.dv_time_ago;

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
                                        about = Functions.StringNullRemover(Functions.DecodeStringWithEnter(Functions.DecodeString(Owner["about"].ToString())));
                                    }
                                    catch (Exception ex)
                                    {
                                        var exception = ex.ToString();
                                    }
                                    var google = Functions.StringNullRemover(Owner["google"].ToString());
                                    var facebook = Functions.StringNullRemover(Owner["facebook"].ToString());
                                    var twitter = Functions.StringNullRemover(Owner["twitter"].ToString());
                                    var verified = Owner["verified"].ToString();
                                    var is_pro = Owner["is_pro"].ToString();
                                    var O_url = Owner["url"].ToString();

                                    addVideo.Owner_id = O_id;
                                    addVideo.Owner_username = username;
                                    addVideo.Owner_email = email;
                                    addVideo.Owner_first_name = first_name;
                                    addVideo.Owner_last_name = last_name;
                                    addVideo.Owner_gender = gender;
                                    addVideo.Owner_language = language;
                                    addVideo.Owner_avatar = avatar;
                                    addVideo.Owner_cover = cover;
                                    addVideo.Owner_about = about;
                                    addVideo.Owner_google = google;
                                    addVideo.Owner_facebook = facebook;
                                    addVideo.Owner_twitter = twitter;
                                    if (verified == "0")
                                        addVideo.Owner_verified = "false";
                                    else
                                        addVideo.Owner_verified = "true";

                                    if (!String.IsNullOrEmpty(addVideo.Owner_first_name) &&
                                        !String.IsNullOrEmpty(addVideo.Owner_last_name))
                                        ChannelName.Text = addVideo.Owner_first_name + " " + addVideo.Owner_last_name;
                                    else
                                        ChannelName.Text = addVideo.Owner_username;

                                    ChannelIsverified.IsVisible = Convert.ToBoolean(addVideo.Owner_verified);

                                    addVideo.Owner_is_pro = is_pro;
                                    addVideo.Owner_url = O_url;
                                    ChannelImage.Source = addVideo.Owner_avatar;

                                    Classes.VideoList.Add(addVideo);

                                    Classes.ViewsVideosList.Add(addVideo);
                                }
                                catch (Exception ex)
                                {
                                    var exception = ex.ToString();
                                }
                            }
                            catch (Exception ex)
                            {
                                var exception = ex.ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        //Get Video By Channel Api
        public async void GetVideoByChannel_Api()
        {
            try
            {
                if (!CrossConnectivity.Current.IsConnected)
                {
                    await DisplayAlert(AppResources.Label_Error, AppResources.Label_Check_Your_Internet, AppResources.Label_OK);
                }
                else
                {
                    Classes.VideoByChannelList.Clear();
                    var data = await API_Request.Get_Videos_By_Channel_Http(_Video_Data.Owner_id, "20", "0");
                    if (data != null)
                    {
                        EmptyVideos.IsVisible = false;
                        VidoesListView.IsVisible = true;

                        foreach (var All in data)
                        {
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
                                V.dv_title = Functions.SubStringCutOf(Functions.DecodeStringWithEnter(Functions.DecodeString(title)), 30);
                                V.dv_description = Functions.SubStringCutOf(Functions.DecodeStringWithEnter(Functions.DecodeString(description)), 60);
                                V.dv_Long_title = Functions.DecodeStringWithEnter(Functions.DecodeString(title));
                                V.dv_Long_description = Functions.DecodeStringWithEnter(Functions.DecodeString(description));
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
                                        about = Functions.StringNullRemover(Functions.DecodeStringWithEnter(Functions.DecodeString(Owner["about"].ToString())));
                                    }
                                    catch (Exception ex)
                                    {
                                        var exception = ex.ToString();
                                    }
                                    var google = Functions.StringNullRemover(Owner["google"].ToString());
                                    var facebook = Functions.StringNullRemover(Owner["facebook"].ToString());
                                    var twitter = Functions.StringNullRemover(Owner["twitter"].ToString());
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
                                //Add list All data Video By Channel
                                Classes.VideoByChannelList.Add(V);
                            }
                            catch (Exception ex)
                            {
                                var exception = ex.ToString();
                            }
                        }
                        VidoesListView.ItemsSource = Classes.VideoByChannelList;
                    }
                    else
                    {
                        EmptyVideos.IsVisible = true;
                        VidoesListView.IsVisible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();

                EmptyVideos.IsVisible = true;
                VidoesListView.IsVisible = false;
            }
        }

        private void VideoPlayer_OnOnContentLoaded(object sender, EventArgs e)
        {
            Loaderspinner.IsVisible = false;
        }

        //TOOLBAR
        private void SegControl_OnValueChanged(object sender, SegmentedControl.FormsPlugin.Abstractions.ValueChangedEventArgs e)
        {
            switch (e.NewValue)
            {
                case 0:
                    VideosStack.IsVisible = true;
                    CommentsStack.IsVisible = false;
                    break;
                case 1:
                    VideosStack.IsVisible = false;
                    CommentsStack.IsVisible = true;
                    break;
            }
        }

        private void VidoesListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                VidoesListView.SelectedItem = null;
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        //Event click Item >> new open Video_Player_Page
        private void VidoesListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                var selectedItem = e.Item as Classes.Video;
                if (selectedItem != null)
                {
                    VideoPlayer.Source = Settings.WebsiteUrl + "/embed/" + selectedItem.dv_video_id;

                    CommentWebLoader.Source = Settings.WebsiteUrl + "/get-video-comments?video_id=" + selectedItem.dv_video_id + "&cookie=" + API_Request.Cookie;

                    TitleLabel.Text = selectedItem.dv_Long_title;
                    ViewsLabel.Text = selectedItem.dv_views;
                    TimeLabel.Text = selectedItem.dv_time_ago;
                    ChannelImage.Source = selectedItem.Owner_avatar;

                    if (!String.IsNullOrEmpty(selectedItem.Owner_first_name) && !String.IsNullOrEmpty(selectedItem.Owner_last_name))
                        ChannelName.Text = selectedItem.Owner_first_name + " " + selectedItem.Owner_last_name;
                    else
                        ChannelName.Text = selectedItem.Owner_username;

                    if (selectedItem.Owner_verified == "True")
                    {
                        ChannelIsverified.IsVisible = true;
                    }

                    Get_New_Videos_Details_Api(selectedItem.dv_video_id);

                    //<<<Here It must add the video information to History database>>> 
                    SQL_Commander.Insert_HistoryVideos_Credentials(selectedItem);

                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        //Event click Channel >> new open ChannelUsers_Page
        private async void ChannelButton_Tabbed(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new ChannelUsers_Page(null, _Video_Data, null));
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        #region Event click like/dislike >> 

        private async void LikeButton_Tabbed(object sender, EventArgs e)
        {
            try
            {
                if (Hamburg_Page.IsLogin)
                {
                    //If User Liked
                    if (IconLike.TextColor == Color.FromHex("#8e8e8e"))
                    {
                        IconLike.TextColor = Color.FromHex(Settings.MainColor);
                        IconUnLike.TextColor = Color.FromHex("#8e8e8e");

                        if (!LikeLabel.Text.Contains("K") && !LikeLabel.Text.Contains("M"))
                        {
                            double x = Convert.ToDouble(LikeLabel.Text);
                            x++;
                            LikeLabel.Text = x.ToString();
                        }

                        //Add The Video to Liked Videos Database
                        SQL_Commander.Insert_LikedVideos_Credentials(_Video_Data);
                        DependencyService.Get<IMethods>().LongAlert(AppResources.Label_Add_The_Video_to_Liked);

                        //Send API Request here for Like
                        var request = await API_Request.Add_Like_Dislike_Videos_Http(_Video_Data.dv_id, "like");
                        if (request != null)
                        {
                        }
                    }
                    else
                    {
                        IconLike.TextColor = Color.FromHex("#8e8e8e");

                        if (!LikeLabel.Text.Contains("K") && !LikeLabel.Text.Contains("M"))
                        {
                            double x = Convert.ToDouble(LikeLabel.Text);
                            x--;
                            LikeLabel.Text = x.ToString();
                        }

                        //Remove The Video to Liked Videos Database
                        SQL_Commander.RemoveLikedVideos_Credentials(_Video_Data.dv_video_id);
                        DependencyService.Get<IMethods>().LongAlert(AppResources.Label_Remove_The_Video_to_Liked);

                        //Send API Request here for UNLike
                        var request =await API_Request.Add_Like_Dislike_Videos_Http(_Video_Data.dv_video_id, "dislike");
                        if (request != null)
                        {
                           
                        }
                    }
                }
                else
                {
                    var ErrorMSG = await DisplayAlert(AppResources.Label_Error, AppResources.Label_Please_sign_in_Liked, AppResources.Label_Yes, AppResources.Label_No);
                    if (ErrorMSG)
                    {
                        await Navigation.PushModalAsync(new Login_Page());
                    }
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        private async void UNLikeButton_Tabbed(object sender, EventArgs e)
        {
            try
            {
                if (Hamburg_Page.IsLogin)
                {
                    if (IconUnLike.TextColor == Color.FromHex("#8e8e8e"))
                    {
                        IconUnLike.TextColor = Color.FromHex(Settings.MainColor);
                        IconLike.TextColor = Color.FromHex("#8e8e8e");

                        if (!UnLikeLabel.Text.Contains("K") && !LikeLabel.Text.Contains("M"))
                        {
                            double x = Convert.ToDouble(UnLikeLabel.Text);
                            x++;
                            UnLikeLabel.Text = x.ToString();
                        }
                        //Add The Video to Liked Videos Database
                        SQL_Commander.Insert_LikedVideos_Credentials(_Video_Data);
                        DependencyService.Get<IMethods>().LongAlert(AppResources.Label_Add_The_Video_to_Liked);

                        //Send API Request here for Like
                        var request = await API_Request.Add_Like_Dislike_Videos_Http(_Video_Data.dv_video_id, "like");
                        if (request != null)
                        {
                          
                        }
                    }
                    else
                    {
                        IconUnLike.TextColor = Color.FromHex("#8e8e8e");

                        if (!UnLikeLabel.Text.Contains("K") && !LikeLabel.Text.Contains("M"))
                        {
                            double x = Convert.ToDouble(UnLikeLabel.Text);
                            x--;
                            UnLikeLabel.Text = x.ToString();
                        }
                        //Remove The Video to Liked Videos Database
                        SQL_Commander.RemoveLikedVideos_Credentials(_Video_Data.dv_video_id);
                        DependencyService.Get<IMethods>().LongAlert(AppResources.Label_Remove_The_Video_to_Liked);

                        //Send API Request here for UNLike
                        var request = await API_Request.Add_Like_Dislike_Videos_Http(_Video_Data.dv_video_id, "dislike");
                        if (request != null)
                        {
                          
                        }
                    }
                }
                else
                {
                    var ErrorMSG = await DisplayAlert(AppResources.Label_Error, AppResources.Label_Please_sign_in_Dislike, AppResources.Label_Yes, AppResources.Label_No);
                    if (ErrorMSG)
                    {
                        await Navigation.PushModalAsync(new Login_Page());
                    }
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        #endregion

        //Event Share this Videos
        private async void ShareButton_Tabbed(object sender, EventArgs e)
        {
            try
            {
                //Share Plugin same as flame
                if (!CrossShare.IsSupported)
                {
                    return;
                }

                await CrossShare.Current.Share(new ShareMessage
                {
                    Title = _Video_Data.dv_title,
                    Text = _Video_Data.dv_description,
                    Url = _Video_Data.dv_url
                });
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        #region Event click watchlater/playlist >>

        private async void AddToButton_Tabbed(object sender, EventArgs e)
        {
            try
            {/*, AppResources.Label_Add_to_play_list*/
                var action = await DisplayActionSheet(AppResources.Label_Choose_your_action, AppResources.Label_Cancel, null, AppResources.Label_Add_to_watch_later);

                if (action == AppResources.Label_Add_to_watch_later)
                {
                    //Add to watch Later Database
                    SQL_Commander.Insert_WatchLaterVideos_Credentials(_Video_Data);

                    DependencyService.Get<IMethods>().LongAlert(AppResources.Label_Add_The_Videos_to_watch_Later);
                }
                else if (action == AppResources.Label_Add_to_play_list)
                {
                    //if (Hamburg_Page.IsLogin)
                    //{
                    //    //Add to PlayList Database
                    //    SQL_Commander.Insert_PlayListsVideos_Credentials(_Video_Data);

                    //    DependencyService.Get<IMethods>().LongAlert(AppResources.Label_Add_The_Videos_To_PlayList);
                    //}
                    //var ErrorMSG = await DisplayAlert(AppResources.Label_Error, AppResources.Label_Please_sign_in_Playlist, AppResources.Label_Yes, AppResources.Label_No);
                    //if (!ErrorMSG)
                    //{
                    //    await Navigation.PushModalAsync(new Login_Page());
                    //}
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        #endregion

        //Event click Subcribe Button
        private async void SubcribeButton_Tabbed(object sender, EventArgs e)
        {
            try
            {
                if (Hamburg_Page.IsLogin)
                {
                    if (SubcribeLabel.Text == AppResources.Label_Subscribe)
                    {
                        IconSubcribe.Text = IoniciconsFont.CheckmarkCircled;
                        IconSubcribe.TextColor = Color.FromHex("#ffff");
                        SubcribeLabel.TextColor = Color.FromHex("#ffff");
                        SubcribeLabel.Text = AppResources.Label_Subscribed;
                        SubcribeFrame.BackgroundColor = Color.FromHex(Settings.MainColor);

                        //Add The Video to  Subcribed Videos Database
                        SQL_Commander.Insert_SubscriptionsChannel_Credentials(_Video_Data);
                        DependencyService.Get<IMethods>().LongAlert(AppResources.Label_Subcribed_Channel);

                        //Send API Request here for  Subcribe
                        var request = await API_Request.Add_Subscribe_To_Channel_Http(_Video_Data.Owner_id);
                        if (request != null)
                        {
                        }
                    }
                    else
                    {
                        IconSubcribe.TextColor = Color.FromHex(Settings.MainColor);
                        SubcribeLabel.TextColor = Color.FromHex(Settings.MainColor);
                        SubcribeFrame.BackgroundColor = Color.FromHex("#ffff");
                        IconSubcribe.Text = IoniciconsFont.AndroidAdd;
                        SubcribeLabel.Text = AppResources.Label_Subscribe;

                        //Remove The Video to Subcribed Videos Database
                        SQL_Commander.RemoveSubscriptionsChannel_Credentials(_Video_Data.Owner_id);
                        DependencyService.Get<IMethods>().LongAlert(AppResources.Label_Remove_Subcribed_Channel);

                        //Send API Request here for UnSubcribe
                        var request = await API_Request.Add_Subscribe_To_Channel_Http(_Video_Data.Owner_id);
                        if (request != null)
                        {
                        }
                    }
                }
                else
                {
                    var ErrorMSG = await DisplayAlert(AppResources.Label_Error, AppResources.Label_Please_sign_in_Subcribed, AppResources.Label_Yes, AppResources.Label_No);
                    if (ErrorMSG)
                    {
                        await Navigation.PushModalAsync(new Login_Page());
                    }
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        //Event click Add Comment
        private async void Btn_AddComment_OnClicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new Comment_Page(_Video_Data.dv_video_id));
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        //Event click Back
        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();
            try
            {
                var checker_Count = Classes.ViewsVideosList.Count;
                if (checker_Count > 1)
                {
                    var checker = Classes.ViewsVideosList.LastOrDefault();
                    if (checker != null)
                    {
                        VideoPlayer.Source = Settings.WebsiteUrl + "/embed/" + checker.dv_video_id;
                        CommentWebLoader.Source = Settings.WebsiteUrl + "/get-video-comments?video_id=" +checker.dv_video_id + "&cookie=" + API_Request.Cookie;
                        ChannelImage.Source = checker.Owner_avatar;
                        LikeLabel.Text = checker.dv_likes;
                        UnLikeLabel.Text = checker.dv_dislikes;
                        TimeLabel.Text = checker.dv_time_ago;
                        ChannelName.Text = checker.Owner_username;
                        ViewsLabel.Text = checker.dv_views;
                        TitleLabel.Text = checker.dv_Long_title;
                        if (checker.dv_is_liked == "1")
                        {
                            IconLike.TextColor = Color.FromHex(Settings.MainColor);
                            IconUnLike.TextColor = Color.FromHex("#8e8e8e");
                        }
                        else
                        {
                            IconLike.TextColor = Color.FromHex("#8e8e8e");
                        }
                        if (checker.dv_is_disliked == "1")
                        {
                            IconUnLike.TextColor = Color.FromHex(Settings.MainColor);
                            IconLike.TextColor = Color.FromHex("#8e8e8e");
                        }
                        else
                        {
                            IconUnLike.TextColor = Color.FromHex("#8e8e8e");
                        }
                        if (checker.dv_is_subscribed == "1")
                        {
                            IconSubcribe.Text = IoniciconsFont.CheckmarkCircled;
                            IconSubcribe.TextColor = Color.FromHex("#ffff");
                            SubcribeLabel.TextColor = Color.FromHex("#ffff");
                            SubcribeLabel.Text = AppResources.Label_Subscribed;
                            SubcribeFrame.BackgroundColor = Color.FromHex(Settings.MainColor);
                        }
                        else
                        {
                            IconSubcribe.TextColor = Color.FromHex(Settings.MainColor);
                            SubcribeLabel.TextColor = Color.FromHex(Settings.MainColor);
                            SubcribeFrame.BackgroundColor = Color.FromHex("#ffff");
                            IconSubcribe.Text = IoniciconsFont.AndroidAdd;
                            SubcribeLabel.Text = AppResources.Label_Subscribe;
                        }
                        if (checker.Owner_verified == "True")
                        {
                            ChannelIsverified.IsVisible = true;
                        }
                        Classes.ViewsVideosList.Remove(checker);
                    }
                }
                else if (checker_Count == 0 || checker_Count == 1)
                {
                    Classes.ViewsVideosList.Clear();

                    try
                    {
                        Navigation.PopAsync(false);
                    }
                    catch (Exception ex)
                    {
                        var exception = ex.ToString();
                        Navigation.PopModalAsync(true);
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
                return true;
            }
            return true;
        }

        public void StyleThemeChanger()
        {
            if (Settings.DarkTheme)
            {
                MainStackPanel.BackgroundColor = Color.FromHex("#444");
                Liner.BackgroundColor = Color.FromHex("#767676");
                Liner2.BackgroundColor = Color.FromHex("#767676");
                //Liner3.BackgroundColor = Color.FromHex("#767676");
                IconLike.TextColor = Color.FromHex("#bcbcbc");
                IconUnLike.TextColor = Color.FromHex("#bcbcbc");
                IconShare.TextColor = Color.FromHex("#bcbcbc");
                IconAddTo.TextColor = Color.FromHex("#bcbcbc");
                AddToLabel.TextColor = Color.FromHex("#ffff");
                LikeLabel.TextColor = Color.FromHex("#ffff");
                UnLikeLabel.TextColor = Color.FromHex("#ffff");
                ShareLabel.TextColor = Color.FromHex("#ffff");
                TitleLabel.TextColor = Color.FromHex("#ffff");
                ChannelName.TextColor = Color.FromHex("#ffff");
                //UpnextLabel.TextColor = Color.FromHex("#ffff");
                SubcribeFrame.BackgroundColor = Color.FromHex("#2f2f2f");
                //LayoutVideoHeader.BackgroundColor = Color.FromHex("#2f2f2f");
                VidoesListView.BackgroundColor = Color.FromHex("#444");
                Label_Empty.TextColor = Color.FromHex("#ffff");
                Label_Go_back.TextColor = Color.FromHex("#ffff");
                EmptyVideos.BackgroundColor = Color.FromHex("#444");
                Label_No_videos.TextColor = Color.FromHex("#ffff");
            }
        } 
    }
}