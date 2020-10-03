using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using PlayTube.Controls;
using PlayTube.Languish;
using PlayTube.Pages.Custom_View;
using PlayTube.Pages.Default;
using Plugin.Connectivity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PlayTube.Pages.Tabbes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Subscriptions_Page : ContentPage
    {
        public Subscriptions_Page()
        {
            try
            {
                InitializeComponent();

                if (Settings.VimeoStyle)
                {
                    TopHeaderBAR.IsVisible = false;
                }

                if (Hamburg_Page.IsLogin)
                {
                    if (MyChannel_Page.Avatar != null)
                        AvatarImage.Source = ImageSource.FromUri(new Uri(MyChannel_Page.Avatar));
                    else
                        AvatarImage.Source = "NoProfileImage.png";

                    if (Classes.SubscriptionsVideoesList.Count == 0)
                    {
                        SubscriptionsListView.BeginRefresh();
                    }
                    SubscriptionsListView.ItemsSource = Classes.SubscriptionsVideoesList;
                }
                else
                {
                    tryagainButton.IsVisible = false;
                    EmptyPage.IsVisible = true;
                    SubscriptionsListView.IsVisible = false;
                    TitleStackLayout.IsVisible = false;

                    SubscriptionsListView.EndRefresh();
                    Icon_page.Text = "\uf119";
                    Lbl_Dont_have.Text = AppResources.Label_Sorry_I_can_not_access;
                    Lbl_no_Videos.Text = AppResources.Label_please_login_to_view_your_subscriptions;
                }

                StyleThemeChanger();
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        //Get Subscriptions Videos Or Channel 
        public async void Get_Subscriptions_VideosOrChannel_Api()
        {
            try
            {
                if (!CrossConnectivity.Current.IsConnected)
                {
                    // await DisplayAlert(AppResources.Label_Error, AppResources.Label_Check_Your_Internet, AppResources.Label_OK);

                    SubscriptionsListView.EndRefresh();

                    Icon_page.Text = "\uf119";
                    Lbl_Dont_have.Text = AppResources.Label_Offline_Mode;
                    Lbl_no_Videos.Text = AppResources.Label_Check_Your_Internet;

                    EmptyPage.IsVisible = true;
                    SubscriptionsListView.IsVisible = false;
                    TitleStackLayout.IsVisible = false;
                }
                else
                {
                    await Task.Run(async () =>
                    {
                        await Task.Delay(5000);

                        if (Hamburg_Page.IsLogin)
                        {

                            Classes.SubscriptionsChannelList.Clear();
                            Classes.SubscriptionsVideoesList.Clear();

                            ChannelStack.Children.Clear();

                            List<string> listOfSubscriptions = new List<string>();
                            listOfSubscriptions.Add("1"); //Channel
                            listOfSubscriptions.Add("false"); //Videos

                            foreach (var subscriptions in listOfSubscriptions)
                            {
                                var dataSubscriptions = await API_Request.Get_Subscriptions_VideosOrChannel_Http(subscriptions, "0", "20");
                                if (dataSubscriptions != null)
                                {
                                    SubscriptionsListView.IsVisible = true;
                                    ChannelStack.IsVisible = true;
                                    EmptyPage.IsVisible = false;
                                   
                                    if (subscriptions == "1")
                                    {
                                        foreach (var AllChannel in dataSubscriptions)
                                        {
                                            JObject Items = JObject.FromObject(AllChannel);

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
                                            string google = Items["google"].ToString();
                                            string facebook = Items["facebook"].ToString();
                                            string twitter = Items["twitter"].ToString();
                                            string verified = Items["verified"].ToString();
                                            string is_pro = Items["is_pro"].ToString();
                                            string url = Items["url"].ToString();

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
                                            ch.Channel_google = google;
                                            ch.Channel_facebook = facebook;
                                            ch.Channel_twitter = twitter;
                                            ch.Channel_verified = verified;
                                            ch.Channel_is_pro = is_pro;
                                            ch.Channel_url = url;

                                            Device.BeginInvokeOnMainThread(() =>
                                            {
                                                var checker_Channel = Classes.SubscriptionsChannelList.FirstOrDefault(a => a.Channel_id == ch.Channel_id);
                                                if (checker_Channel == null)
                                                {
                                                    //Add list All data Channel
                                                    Classes.SubscriptionsChannelList.Add(ch);
                                                }
                                            });
                                        }
                                    }
                                    else
                                    { //Add list All data Videos
                                        foreach (var All in dataSubscriptions)
                                        {
                                            Classes.Subscriptions s = new Classes.Subscriptions();

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
                                                s.SV_Type_video = "Subcribed";

                                                //Data video
                                                s.dv_id = id;
                                                s.dv_video_id = video_id;
                                                s.dv_user_id = user_id;
                                                s.dv_title = Functions.SubStringCutOf(Functions.DecodeStringWithEnter(Functions.DecodeString(title)), 30);
                                                s.dv_description = Functions.SubStringCutOf(Functions.DecodeStringWithEnter(Functions.DecodeString(description)), 60);
                                                s.dv_Long_title = Functions.DecodeStringWithEnter(Functions.DecodeString(title));
                                                s.dv_Long_description = Functions.DecodeStringWithEnter(Functions.DecodeString(description));
                                                s.dv_thumbnail = thumbnail;
                                                s.dv_video_location = video_location;
                                                s.dv_youtube = youtube;
                                                s.dv_vimeo = vimeo;
                                                s.dv_daily = daily;
                                                s.dv_time = time;
                                                s.dv_time_date = time_date;
                                                s.dv_active = active;
                                                s.dv_tags = tags;
                                                s.dv_duration = duration;
                                                s.dv_size = size;
                                                s.dv_category_id = category_id;
                                                s.dv_views = Functions.FormatPriceValue(Convert.ToInt32(views)) + " " +AppResources.Label_Views;
                                                s.dv_featured = featured;
                                                s.dv_registered = registered;
                                                s.dv_org_thumbnail = org_thumbnail;
                                                s.dv_video_type = video_type;
                                                s.dv_video_id_ = video_id_;
                                                s.dv_source = source;
                                                s.dv_url = url;
                                                s.dv_edit_description = edit_description;
                                                s.dv_markup_description = markup_description;
                                                s.dv_is_liked = is_liked;
                                                s.dv_is_disliked = is_disliked;
                                                s.dv_is_owner = is_owner;
                                                s.dv_time_alpha = time_alpha;
                                                s.dv_time_ago = time_ago;
                                                s.dv_category_name = category_name;

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

                                                    s.Owner_id = O_id;
                                                    s.Owner_username = username;
                                                    s.Owner_email = email;
                                                    s.Owner_first_name = first_name;
                                                    s.Owner_last_name = last_name;
                                                    s.Owner_gender = gender;
                                                    s.Owner_language = language;
                                                    s.Owner_avatar = avatar;
                                                    s.Owner_cover = cover;
                                                    s.Owner_about = about;
                                                    s.Owner_google = google;
                                                    s.Owner_facebook = facebook;
                                                    s.Owner_twitter = twitter;
                                                    if (verified == "0")
                                                        s.Owner_verified = "false";
                                                    else
                                                        s.Owner_verified = "true";
                                                    s.Owner_is_pro = is_pro;
                                                    s.Owner_url = O_url;
                                                }
                                                catch (Exception ex)
                                                {
                                                    var exception = ex.ToString();
                                                }
                                                Device.BeginInvokeOnMainThread(() =>
                                                {
                                                    var checker_Video =Classes.SubscriptionsVideoesList.FirstOrDefault(a => a.dv_id == s.dv_id);
                                                    if (checker_Video == null)
                                                    {
                                                        //Add list All data Video
                                                        Classes.SubscriptionsVideoesList.Add(s);
                                                    }
                                                });
                                            }
                                            catch (Exception ex)
                                            {
                                                var exception = ex.ToString();
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    SubscriptionsListView.EndRefresh();
                                    Icon_page.Text = "\uf03d";
                                    Lbl_Dont_have.Text = AppResources.Label_Empty_Videos;
                                    Lbl_no_Videos.Text = AppResources.Label_There_are_no_subscriptions;
                                    tryagainButton.IsVisible = true;

                                    EmptyPage.IsVisible = true;
                                    SubscriptionsListView.IsVisible = false;
                                    ChannelStack.IsVisible = false;

                                }
                            }
                            Device.BeginInvokeOnMainThread(() =>
                            {
                                SubscriptionsListView.EndRefresh();
                                SubscriptionsListView.IsVisible = true;
                                ChannelStack.IsVisible = true;

                                EmptyPage.IsVisible = false;

                                Add_Channel_Users(Classes.SubscriptionsChannelList); //Add list All data Channel
                                SubscriptionsListView.ItemsSource = Classes.SubscriptionsVideoesList; //Add list All data Video
                            });
                        }
                        else
                        {
                            tryagainButton.IsVisible = false;
                            EmptyPage.IsVisible = true;
                            SubscriptionsListView.IsVisible = false;
                            ChannelStack.IsVisible = false;


                            SubscriptionsListView.EndRefresh();
                            Icon_page.Text = "\uf119";
                            Lbl_Dont_have.Text = AppResources.Label_Sorry_I_can_not_access;
                            Lbl_no_Videos.Text = AppResources.Label_please_login_to_view_your_subscriptions;
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                SubscriptionsListView.EndRefresh();

                Icon_page.Text = "\uf03d";
                Lbl_Dont_have.Text = AppResources.Label_Empty_Videos;
                Lbl_no_Videos.Text = AppResources.Label_There_are_no_subscriptions;
                tryagainButton.IsVisible = true;

                EmptyPage.IsVisible = true;
                SubscriptionsListView.IsVisible = false;
                TitleStackLayout.IsVisible = false;

                var exception = ex.ToString();
            }
        }

        public void Add_Channel_Users(ObservableCollection<Classes.Channel> ChannelList)
        {
            try
            {
                for (var i = 0; i < ChannelList.Count; i++)
                {
                    var TapGestureRecognizer = new TapGestureRecognizer();
                    TapGestureRecognizer.Tapped += OnChannelTapped;
                    var item = new Channel_View_Template();
                    item.BindingContext = ChannelList[i];
                    item.GestureRecognizers.Add(TapGestureRecognizer);
                    ChannelStack.Children.Add(item);
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        private void SubscriptionsListView_OnRefreshing(object sender, EventArgs e)
        {
            try
            {
                Get_Subscriptions_VideosOrChannel_Api();
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        //Event click Item  >> open Video_Player_Page
        private async void SubscriptionsListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                var selectedItem = e.Item as Classes.Subscriptions;
                if (selectedItem != null)
                {

                    //Convert From Classes.Subscriptions
                    Classes.Video Subscriptions = new Classes.Video();

                    //Data video
                    Subscriptions.dv_id = selectedItem.dv_id;
                    Subscriptions.dv_video_id = selectedItem.dv_video_id;
                    Subscriptions.dv_user_id = selectedItem.dv_user_id;
                    Subscriptions.dv_title = selectedItem.dv_title;
                    Subscriptions.dv_description = selectedItem.dv_description;
                    Subscriptions.dv_Long_title = selectedItem.dv_Long_title;
                    Subscriptions.dv_Long_description = selectedItem.dv_Long_description;
                    Subscriptions.dv_thumbnail = selectedItem.dv_thumbnail;
                    Subscriptions.dv_video_location = selectedItem.dv_video_location;
                    Subscriptions.dv_youtube = selectedItem.dv_youtube;
                    Subscriptions.dv_vimeo = selectedItem.dv_vimeo;
                    Subscriptions.dv_daily = selectedItem.dv_daily;
                    Subscriptions.dv_time = selectedItem.dv_time;
                    Subscriptions.dv_time_date = selectedItem.dv_time_date;
                    Subscriptions.dv_active = selectedItem.dv_active;
                    Subscriptions.dv_tags = selectedItem.dv_tags;
                    Subscriptions.dv_duration = selectedItem.dv_duration;
                    Subscriptions.dv_size = selectedItem.dv_size;
                    Subscriptions.dv_category_id = selectedItem.dv_category_id;
                    Subscriptions.dv_views = selectedItem.dv_views;
                    Subscriptions.dv_featured = selectedItem.dv_featured;
                    Subscriptions.dv_registered = selectedItem.dv_registered;
                    Subscriptions.dv_org_thumbnail = selectedItem.dv_org_thumbnail;
                    Subscriptions.dv_video_type = selectedItem.dv_video_type;
                    Subscriptions.dv_video_id_ = selectedItem.dv_video_id_;
                    Subscriptions.dv_source = selectedItem.dv_source;
                    Subscriptions.dv_url = selectedItem.dv_url;
                    Subscriptions.dv_edit_description = selectedItem.dv_edit_description;
                    Subscriptions.dv_markup_description = selectedItem.dv_markup_description;
                    Subscriptions.dv_is_liked = selectedItem.dv_is_liked;
                    Subscriptions.dv_is_disliked = selectedItem.dv_is_disliked;
                    Subscriptions.dv_is_owner = selectedItem.dv_is_owner;
                    Subscriptions.dv_time_alpha = selectedItem.dv_time_alpha;
                    Subscriptions.dv_time_ago = selectedItem.dv_time_ago;
                    Subscriptions.dv_category_name = selectedItem.dv_category_name;

                    //Owner
                    Subscriptions.Owner_id = selectedItem.Owner_id;
                    Subscriptions.Owner_username = selectedItem.Owner_username;
                    Subscriptions.Owner_email = selectedItem.Owner_email;
                    Subscriptions.Owner_first_name = selectedItem.Owner_first_name;
                    Subscriptions.Owner_last_name = selectedItem.Owner_last_name;
                    Subscriptions.Owner_gender = selectedItem.Owner_gender;
                    Subscriptions.Owner_language = selectedItem.Owner_language;
                    Subscriptions.Owner_avatar = selectedItem.Owner_avatar;
                    Subscriptions.Owner_cover = selectedItem.Owner_cover;
                    Subscriptions.Owner_about = selectedItem.Owner_about;
                    Subscriptions.Owner_google = selectedItem.Owner_google;
                    Subscriptions.Owner_facebook = selectedItem.Owner_facebook;
                    Subscriptions.Owner_twitter = selectedItem.Owner_twitter;
                    Subscriptions.Owner_verified = selectedItem.Owner_verified;
                    Subscriptions.Owner_is_pro = selectedItem.Owner_is_pro;
                    Subscriptions.Owner_url = selectedItem.Owner_url;

                    await Navigation.PushAsync(new Video_Player_Page(Subscriptions));
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        private void SubscriptionsListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                SubscriptionsListView.SelectedItem = null;
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        //Event click Item in Channel_View_Template >> open ChannelUsers_Page  
        private async void OnChannelTapped(object sender, EventArgs e)
        {
            try
            {
                var selectedItem = (Classes.Channel)((Channel_View_Template)sender).BindingContext;
                if (selectedItem != null)
                {
                    await Navigation.PushAsync(new ChannelUsers_Page( null, null,selectedItem));
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        //Event click Icon Search => open Search_Page
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

        //Event click Item => open ChannelSubscribed_Page
        private async void Subscriptions_ALL_OnClicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new ChannelSubscribed_Page(Classes.SubscriptionsChannelList));
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
                TopLabel.TextColor = Color.FromHex("#ffff");
                Liner.BackgroundColor = Color.FromHex("#767676");
                Liner2.BackgroundColor = Color.FromHex("#767676");
                Lbl_Dont_have.TextColor = Color.FromHex("#ffff");
                Lbl_no_Videos.TextColor = Color.FromHex("#ffff");
            }
        }

        private void TryagainButton_OnClicked(object sender, EventArgs e)
        {
            try
            {
                Get_Subscriptions_VideosOrChannel_Api();
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }
      
    }
}
