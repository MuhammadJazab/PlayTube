using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json.Linq;
using PlayTube.Controls;
using PlayTube.Dependencies;
using PlayTube.Languish;
using Plugin.Connectivity;
using Refractored.XamForms.PullToRefresh;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PlayTube.Pages.Default
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChannelUsers_Page : ContentPage
    {

        #region Event Refresh

        public class RefreshMVVM : INotifyPropertyChanged
        {
            public ObservableCollection<string> Items { get; set; }
            ChannelUsers_Page pageChosen;

            public RefreshMVVM(ChannelUsers_Page page)
            {
                this.pageChosen = page;
                Items = new ObservableCollection<string>();
            }

            bool isBusy;

            public bool IsBusy
            {
                get { return isBusy; }
                set
                {
                    if (isBusy == value)
                        return;

                    isBusy = value;
                    OnPropertyChanged("IsBusy");
                }
            }

            ICommand refreshCommand;

            public ICommand RefreshCommand
            {
                get
                {
                    return refreshCommand ?? (refreshCommand = new Command(async () => await ExecuteRefreshCommand()));
                }
            }

            async Task ExecuteRefreshCommand()
            {
                if (IsBusy)
                {
                    return;
                }

                IsBusy = true;

                //Run code
                pageChosen.Get_ChannelUsers_Info_Api();

                IsBusy = false;
            }

            #region INotifyPropertyChanged implementation

            public event PropertyChangedEventHandler PropertyChanged;

            #endregion

            public void OnPropertyChanged(string propertyName)
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region Variables

        private string URl, Id_Channel;
        private Classes.Subscriptions _Subscriptions;
        private Classes.Video _Video_Data;
        private Classes.Channel _Channel;
        #endregion

        public ChannelUsers_Page(Classes.Subscriptions subscriptions , Classes.Video Video_Data , Classes.Channel Channel)
        {
            try
            {
                InitializeComponent();

                if (Device.OS == TargetPlatform.iOS)
                    NavigationPage.SetHasNavigationBar(this, true);
                else
                    NavigationPage.SetHasNavigationBar(this, false);

                BindingContext = new RefreshMVVM(this);
                PullToRefreshLayoutView.SetBinding<RefreshMVVM>(PullToRefreshLayout.IsRefreshingProperty,vm => vm.IsBusy, BindingMode.OneWay);
                PullToRefreshLayoutView.SetBinding<RefreshMVVM>(PullToRefreshLayout.RefreshCommandProperty,vm => vm.RefreshCommand);

                if (subscriptions != null)
                {
                    _Subscriptions = subscriptions;
                    URl = subscriptions.Owner_url;
                    Id_Channel = subscriptions.Owner_id;
                }
                else if (Video_Data != null)
                {
                    _Video_Data = Video_Data;
                    URl = Video_Data.Owner_url;
                    Id_Channel = Video_Data.Owner_id;
                }
                else
                {
                    _Channel = Channel;
                    URl = Channel.Channel_url;
                    Id_Channel = Channel.Channel_id;
                }

                Get_ChannelUsers_Info_Api();
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        public async void Get_ChannelUsers_Info_Api()
        {
            try
            {
                if (!CrossConnectivity.Current.IsConnected)
                {
                    await DisplayAlert(AppResources.Label_Error, AppResources.Label_Check_Your_Internet,AppResources.Label_OK);
                }
                else
                {
                    PullToRefreshLayoutView.IsRefreshing = true;
                    Classes.ChannelUsersList.Clear();
                    Classes.VideoByChannelUsersList.Clear();

                    var data_Channel = await API_Request.Get_Channel_Info_Http(Id_Channel);
                    if (data_Channel != null)
                    {

                        JObject Items = JObject.FromObject(data_Channel);

                        Classes.Channel ch = new Classes.Channel();

                        var id = Items["id"].ToString();
                        var username = Items["username"].ToString();
                        var email = Items["email"].ToString();
                        var first_name = Items["first_name"].ToString();
                        var last_name = Items["last_name"].ToString();
                        var gender = Items["gender"].ToString();
                        var language = Items["language"].ToString();
                        var avatar = Items["avatar"].ToString();
                        var cover = Items["cover"].ToString();
                        var about = "";
                        try
                        {
                            about = Functions.StringNullRemover(Functions.DecodeStringWithEnter(Functions.DecodeString(Items["about"].ToString())));
                        }
                        catch (Exception ex)
                        {
                            var exception = ex.ToString();
                        }
                        var google = Functions.StringNullRemover(Items["google"].ToString());
                        var facebook = Functions.StringNullRemover(Items["facebook"].ToString());
                        var twitter = Functions.StringNullRemover(Items["twitter"].ToString());
                        var verified = Items["verified"].ToString();
                        var is_pro = Items["is_pro"].ToString();
                        var url = Items["url"].ToString();

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

                        Classes.ChannelUsersList.Add(ch);

                        AddlistItems();
                        PullToRefreshLayoutView.IsRefreshing = false;
                    }
                    else
                    {
                        PullToRefreshLayoutView.IsRefreshing = false;

                    }
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        //Add Items data at ChannelUsersprofileListItems
        public  void AddlistItems()
        {
            try
            {
                var dataMyChannel = Classes.ChannelUsersList.FirstOrDefault(a => a.Channel_id == Id_Channel);
                if (dataMyChannel != null)
                {
                    CoverImage.Source = dataMyChannel.Channel_cover;
                    AvatarImage.Source = dataMyChannel.Channel_avatar;

                    if (!String.IsNullOrEmpty(dataMyChannel.Channel_first_name) &&
                        !String.IsNullOrEmpty(dataMyChannel.Channel_last_name))
                        Lbl_Username.Text = dataMyChannel.Channel_first_name + " " + dataMyChannel.Channel_last_name;
                    else
                        Lbl_Username.Text = dataMyChannel.Channel_username;

                    URl = dataMyChannel.Channel_url;
                    Id_Channel = dataMyChannel.Channel_id;

                    if (Classes.ChannelUsersprofileListItems.Count > 0)
                    {
                        Classes.ChannelUsersprofileListItems.Clear();
                    }

                    Classes.ChannelUsersprofileListItems.Add(new Classes.Channelprofiletems()
                    {
                        Label = dataMyChannel.Channel_about,
                        Icon = "\uf040",
                        Color = "#34465d",
                        TextColor = dataMyChannel.SC_TextColor
                    });
                    Classes.ChannelUsersprofileListItems.Add(new Classes.Channelprofiletems()
                    {
                        Label = dataMyChannel.Channel_facebook,
                        Icon = "\uf09a",
                        Color = "#3b5999",
                        TextColor = dataMyChannel.SC_TextColor
                    });
                    Classes.ChannelUsersprofileListItems.Add(new Classes.Channelprofiletems()
                    {
                        Label = dataMyChannel.Channel_google,
                        Icon = "\uf0d5",
                        Color = "#dd4b39",
                        TextColor = dataMyChannel.SC_TextColor
                    });
                    Classes.ChannelUsersprofileListItems.Add(new Classes.Channelprofiletems()
                    {
                        Label = dataMyChannel.Channel_twitter,
                        Icon = "\uf099",
                        Color = "#55acee",
                        TextColor = dataMyChannel.SC_TextColor
                    });

                    UserInfoListView.ItemsSource = Classes.ChannelUsersprofileListItems;
                }
                if (Classes.VideoByChannelUsersList.Count > 0)
                {
                    EmptyVideos.IsVisible = false;
                    VidoesListView.IsVisible = true;

                    VidoesListView.ItemsSource = Classes.VideoByChannelUsersList;
                }
                else
                {
                    GetVideoByChannelUsers_Api("0");
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        //Get Video By Channel Api
        public async void GetVideoByChannelUsers_Api(string offset)
        {
            try
            {
                if (!CrossConnectivity.Current.IsConnected)
                {
                    //await DisplayAlert(AppResources.Label_Error, AppResources.Label_Check_Your_Internet,AppResources.Label_OK);

                }
                else
                {
                    var data = await API_Request.Get_Videos_By_Channel_Http(Id_Channel, "20", offset);
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
                                V.dv_title =Functions.SubStringCutOf(Functions.DecodeStringWithEnter(Functions.DecodeString(title)), 30);
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
                                V.dv_views = Functions.FormatPriceValue(Convert.ToInt32(views)) + " " +AppResources.Label_Views;
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

                                //Add list All data Video By ChannelUsers
                                var checker = Classes.VideoByChannelUsersList.FirstOrDefault(a => a.dv_id == V.dv_id);
                                if (checker == null)
                                {
                                    Classes.VideoByChannelUsersList.Add(V);
                                }
                            }
                            catch (Exception ex)
                            {
                                var exception = ex.ToString();
                            }
                        }
                        VidoesListView.ItemsSource = Classes.VideoByChannelUsersList;
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

        //TOOLBAR
        private void SegControl_OnValueChanged(object sender,SegmentedControl.FormsPlugin.Abstractions.ValueChangedEventArgs e)
        {
            switch (e.NewValue)
            {
                case 0:
                    VideosStack.IsVisible = true;
                    AboutStack.IsVisible = false;
                    break;
                case 1:
                    VideosStack.IsVisible = false;
                    AboutStack.IsVisible = true;
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

        //Event click Item >> open Video_Player_Page
        private async void VidoesListView_OnItemTapped(object sender, ItemTappedEventArgs e)
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

        //Event Item Appearing >> Get More video By ChannelUsers
        private async void VidoesListView_OnItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            try
            {
                var currentItem = e.Item as Classes.Video;
                var beforelastItem = Classes.VideoByChannelUsersList[Classes.VideoByChannelUsersList.Count - 2];
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
                        var lastItem = Classes.VideoByChannelUsersList[Classes.VideoByChannelUsersList.Count - 1];
                        if (lastItem != null)
                        {
                            GetVideoByChannelUsers_Api(lastItem.dv_id);
                        }
                    }
                }
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

        //Event click Item >> open ******
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

        public void StyleThemeChanger()
        {
            if (Settings.DarkTheme)
            {
                VidoesListView.BackgroundColor = Color.FromHex("#444");
                UserInfoListView.BackgroundColor = Color.FromHex("#444");
                EmptyVideos.BackgroundColor = Color.FromHex("#444");
                Label_No_videos.TextColor = Color.FromHex("#ffff");
            }
        }
    }
}