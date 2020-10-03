using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PlayTube.Controls;
using PlayTube.CustomRenders;
using PlayTube.Languish;
using PlayTube.Pages.Custom_View;
using PlayTube.Pages.Default;
using Plugin.Connectivity;
using Refractored.XamForms.PullToRefresh;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PlayTube.Pages.Tabbes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home_Page : ContentPage
    {
        #region Event Refresh

        public class RefreshMVVM : INotifyPropertyChanged
        {
            public ObservableCollection<string> Items { get; set; }
            Home_Page pageChosen;
            public RefreshMVVM(Home_Page page)
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
                get { return refreshCommand ?? (refreshCommand = new Command(async () => await ExecuteRefreshCommand())); }
            }

            async Task ExecuteRefreshCommand()
            {
                if (IsBusy)
                {
                    return;
                }

                IsBusy = true;

                //Run code
                pageChosen.Get_Video();

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

        public Home_Page()
        {
            try
            {
                InitializeComponent();

                BindingContext = new RefreshMVVM(this);
                PullToRefreshLayoutView.SetBinding<RefreshMVVM>(PullToRefreshLayout.IsRefreshingProperty, vm => vm.IsBusy, BindingMode.OneWay);
                PullToRefreshLayoutView.SetBinding<RefreshMVVM>(PullToRefreshLayout.RefreshCommandProperty, vm => vm.RefreshCommand);

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
                }
                
                StyleThemeChanger();

                if (Classes.CatigoryList.Count > 0)
                {
                    //Add Top Category
                    Add_Category_Section(Classes.CatigoryList);
                }

                Get_Video();
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        //Get_Video
        public async void Get_Video()
        {
            try
            {
                if (!CrossConnectivity.Current.IsConnected)
                {
                    await DisplayAlert(AppResources.Label_Error, AppResources.Label_Check_Your_Internet, AppResources.Label_OK);
                }
                else
                {
                    PullToRefreshLayoutView.IsRefreshing = true;

                    if (Classes.VideoList.Count > 0)
                    {
                        Classes.VideoList.Clear();
                    }

                    var Videos_data = await API_Request.Get_Videos_Http("0", "0", "0", "15").ConfigureAwait(false);
                    if (Videos_data != null)
                    {
                        List<string> listOfvideos = new List<string>();
                        listOfvideos.Add("featured");
                        listOfvideos.Add("top");
                        listOfvideos.Add("latest");

                        foreach (var typevideos in listOfvideos)
                        {
                            var v_data = JObject.Parse(Videos_data.ToString(Formatting.None)).SelectToken(typevideos).ToString();
                            JArray dataVideos = JArray.Parse(v_data);

                            foreach (var All in dataVideos)
                            {
                                Classes.Video V = new Classes.Video();

                                try //featured - top - latest
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
                                    V.SV_Type_video = typevideos;
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
                                    V.dv_description = Functions.SubStringCutOf(Functions.DecodeStringWithEnter(Functions.DecodeString(description)), 50);
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
                                    //Add list All data Video
                                    var checker = Classes.VideoList.FirstOrDefault(a => a.dv_video_id == V.dv_video_id);
                                    if (checker != null)
                                    {
                                        checker.SV_Type_video += " " + typevideos;
                                    }
                                    else
                                    {
                                        Classes.VideoList.Add(V);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    var exception = ex.ToString();
                                }
                            }
                        }

                        //Add Featured video
                        Device.BeginInvokeOnMainThread(() => 
                        {
                            Classes.FeaturedVideoList = new ObservableCollection<Classes.Video>(Classes.VideoList.Where(a => a.SV_Type_video.Contains("featured")));
                            Add_Featured_video_Section(Classes.FeaturedVideoList);
                        });

                        Device.BeginInvokeOnMainThread(() =>
                        {
                            //Task.Delay(2000);
                            PopulateNewsLists(Classes.VideoList);
                            PullToRefreshLayoutView.IsRefreshing = false;
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
                PullToRefreshLayoutView.IsRefreshing = false;

                //Add Featured video
                Add_Featured_video_Section(Classes.FeaturedVideoList);
                PopulateNewsLists(Classes.VideoList);
            }
        }

        //Templet populate  
        public  async void PopulateNewsLists(ObservableCollection<Classes.Video> VideoList)
        {
            try
            {
                for (var i = 0; i < VideoList.Count; i++)
                {
                    if (i < 4)
                    {
                        //Add_Top_BigVideo(VideoList[i]);
                        //Add_Overlaout_Video(VideoList[i]);
                    }
                }

                AddSection(AppResources.Label_Top_videos);
                Classes.TopVideoList = new ObservableCollection<Classes.Video>(VideoList.Where(a => a.SV_Type_video.Contains("top")));
                if (Classes.TopVideoList.Count > 0)
                {
                    for (var i = 0; i < Classes.TopVideoList.Count; i++)
                    {
                        if (i < 7)
                        {
                            Add_Horizontal_Video(Classes.TopVideoList[i]);
                        }
                    }
                }

                AddSection(AppResources.Label_Latest_videos);
                Classes.LatestVideoList = new ObservableCollection<Classes.Video>(VideoList.Where(a => a.SV_Type_video.Contains("latest")));
                if (Classes.LatestVideoList.Count > 0)
                {
                    for (var i = 0; i < Classes.LatestVideoList.Count; i++)
                    {
                        if (i < 7)
                        {
                            Add_Horizontal_Video(Classes.LatestVideoList[i]);
                        }
                        if (i == 6)
                        {
                            if (Settings.Show_ADMOB_On_Timeline)
                            {
                                var item = new AdMob_ViewTemplate();
                                item.BindingContext = Classes.LatestVideoList[i];
                                item.GestureRecognizers.Add(new ClickGestureRecognizer());
                                TopColumn.Children.Add(item);
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

        #region AddSection

        public void Add_Category_Section(ObservableCollection<Classes.Catigory> CatigoryList)
        {
            try
            {
                var scrollableContent = new StackLayout()
                {
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.Fill
                };

                for (var i = 0; i < CatigoryList.Count; i++)
                {
                    var TapGestureRecognizer = new TapGestureRecognizer();
                    TapGestureRecognizer.Tapped += OnCatigoryTapped;
                    var item = new Category_Item_Template();
                    item.BindingContext = CatigoryList[i];
                    item.GestureRecognizers.Add(TapGestureRecognizer);
                    scrollableContent.Children.Add(item);
                }

                var ScrolHorizinal = new ScrollViewModifiedRender()
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Orientation = ScrollOrientation.Horizontal,
                    Content = scrollableContent,
                    Padding = new Thickness(7, 0, 7, 5),
                    BackgroundColor = Color.White,
                };

                var SectionTapGestureRecognizer = new TapGestureRecognizer();
                SectionTapGestureRecognizer.Tapped += OnSectionTapped;
                var itemSection = new Section_View_Template();
                Classes.Catigory Ca = new Classes.Catigory();
                Ca.Name = AppResources.Label_Categories;
                itemSection.BindingContext = Ca;
                itemSection.GestureRecognizers.Add(SectionTapGestureRecognizer);

                if (Settings.Show_Cutsom_Logo_And_Header_On_the_Top)
                {
                    MainStackPanel.Children.Insert(1, itemSection);
                    MainStackPanel.Children.Insert(2, ScrolHorizinal);
                }
                else
                {
                    MainStackPanel.Children.Insert(0, itemSection);
                    MainStackPanel.Children.Insert(1, ScrolHorizinal);
                }

                if (Settings.DarkTheme)
                {
                    ScrolHorizinal.BackgroundColor = Color.FromHex("#444");
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        public async void Add_Featured_video_Section(ObservableCollection<Classes.Video> FeaturedVideoList)
        {
            try
            {
                var scrollableContent = new StackLayout()
                {
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.Fill
                };
                var itemBigVideo = new Big_Video_Template();
                for (var i = 0; i < FeaturedVideoList.Count; i++)
                {
                    if (i == 0 && Settings.ShowBigFeatured_video)
                    {
                        var VideoTapGestureRecognizer = new TapGestureRecognizer();
                        VideoTapGestureRecognizer.Tapped += OnVideoBigTapped;
                        itemBigVideo.BindingContext = FeaturedVideoList[i];
                        itemBigVideo.GestureRecognizers.Add(VideoTapGestureRecognizer);

                    }
                    else
                    {
                        var TapGestureRecognizer = new TapGestureRecognizer();
                        TapGestureRecognizer.Tapped += OnVideoOverlayoutTapped;
                        var item = new Video_Overlayout_Template();
                        item.BindingContext = FeaturedVideoList[i];
                        item.GestureRecognizers.Add(TapGestureRecognizer);
                        scrollableContent.Children.Add(item);
                    }

                }

                var ScrolHorizinal = new ScrollViewModifiedRender()
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Orientation = ScrollOrientation.Horizontal,
                    Content = scrollableContent,
                    Padding = new Thickness(7, 0, 7, 0),
                    TranslationY = -3
                };

                var SectionTapGestureRecognizer = new TapGestureRecognizer();
                SectionTapGestureRecognizer.Tapped += OnSectionTapped;
                var itemSection = new Section_View_Template();
                itemSection.Margin = new Thickness(0, 5, 0, 0);
                Classes.Catigory Ca = new Classes.Catigory();
                Ca.Name = AppResources.Label_Featured_Video;
                itemSection.BindingContext = Ca;
                itemSection.GestureRecognizers.Add(SectionTapGestureRecognizer);

                if (Settings.Show_Cutsom_Logo_And_Header_On_the_Top)
                {
                    MainStackPanel.Children.Insert(3, itemSection);
                    MainStackPanel.Children.Insert(4, ScrolHorizinal);
                    if (Settings.ShowBigFeatured_video)
                    {
                        MainStackPanel.Children.Insert(5, itemBigVideo);
                    }
                }
                else
                {
                    MainStackPanel.Children.Insert(2, itemSection);
                    MainStackPanel.Children.Insert(3, ScrolHorizinal);
                    if (Settings.ShowBigFeatured_video)
                    {
                        MainStackPanel.Children.Insert(4, itemBigVideo);
                    }
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        public void Add_Horizontal_Video(Classes.Video VideoItem)
        {
            try
            {
                var VideoTapGestureRecognizer = new TapGestureRecognizer();
                VideoTapGestureRecognizer.Tapped += OnVideoHorizontalTapped;
                var item = new Horizontal_Video_Template();
                item.BindingContext = VideoItem;
                item.GestureRecognizers.Add(VideoTapGestureRecognizer);
                TopColumn.Children.Add(item);
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        public void Add_Top_BigVideo(Classes.Video VideoItem)
        {
            try
            {
                var VideoTapGestureRecognizer = new TapGestureRecognizer();
                VideoTapGestureRecognizer.Tapped += OnVideoBigTapped;
                var item = new Big_Video_Template();
                item.BindingContext = VideoItem;
                item.GestureRecognizers.Add(VideoTapGestureRecognizer);
                TopColumn.Children.Add(item);
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        public void AddSection(string Section_name)
        {
            try
            {
                var SectionTapGestureRecognizer = new TapGestureRecognizer();
                SectionTapGestureRecognizer.Tapped += OnSectionTapped;
                var itemSection = new Section_View_Template();
                Classes.Catigory Ca = new Classes.Catigory();
                Ca.Name = Section_name;
                itemSection.BindingContext = Ca;
                itemSection.GestureRecognizers.Add(SectionTapGestureRecognizer);
                TopColumn.Children.Add(itemSection);
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        #endregion

        #region Event

        //Event click Item in Big_Video_Template >> open Video_Player_Page
        private async void OnVideoBigTapped(object sender, EventArgs e)
        {
            try
            {
                if (!CrossConnectivity.Current.IsConnected)
                {
                    await DisplayAlert(AppResources.Label_Error, AppResources.Label_Check_Your_Internet, AppResources.Label_OK);
                }
                else
                {
                    var selectedItem = (Classes.Video) ((Big_Video_Template) sender).BindingContext;
                    if (selectedItem != null)
                    {
                        await Navigation.PushAsync(new Video_Player_Page(selectedItem));
                    }
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        //Event click Item in Video_Overlayout_Template >> open Video_Player_Page
        private async void OnVideoOverlayoutTapped(object sender, EventArgs e)
        {
            try
            {
                if (!CrossConnectivity.Current.IsConnected)
                {
                    await DisplayAlert(AppResources.Label_Error, AppResources.Label_Check_Your_Internet, AppResources.Label_OK);
                }
                else
                {
                    var selectedItem = (Classes.Video) ((Video_Overlayout_Template) sender).BindingContext;
                    if (selectedItem != null)
                    {
                        await Navigation.PushAsync(new Video_Player_Page(selectedItem));
                    }
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        //Event click Item in Horizontal_Video_Template >> open Video_Player_Page
        private async void OnVideoHorizontalTapped(object sender, EventArgs e)
        {
            try
            {
                if (!CrossConnectivity.Current.IsConnected)
                {
                    await DisplayAlert(AppResources.Label_Error, AppResources.Label_Check_Your_Internet, AppResources.Label_OK);
                }
                else
                {
                    var selectedItem = (Classes.Video) ((Horizontal_Video_Template) sender).BindingContext;
                    if (selectedItem != null)
                    {
                        await Navigation.PushAsync(new Video_Player_Page(selectedItem));
                    }
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        //Event click Item in Catigory >> open Category_Page Or Videos_ByType_Page
        private async void OnSectionTapped(object sender, EventArgs e)
        {
            try
            {
                if (!CrossConnectivity.Current.IsConnected)
                {
                    await DisplayAlert(AppResources.Label_Error, AppResources.Label_Check_Your_Internet, AppResources.Label_OK);
                }
                else
                {
                    var selectedItem = (Classes.Catigory) ((Section_View_Template) sender).BindingContext;
                    if (selectedItem != null)
                    {
                        if (selectedItem.Name.Contains(AppResources.Label_Categories))
                        {
                            await Navigation.PushAsync(new Category_Page());
                        }
                        else if (selectedItem.Name.Contains(AppResources.Label_Featured_Video))
                        {
                            await Navigation.PushAsync(new Videos_ByType_Page("featured", Classes.FeaturedVideoList));
                        }
                        else if (selectedItem.Name.Contains(AppResources.Label_Top_videos))
                        {
                            await Navigation.PushAsync(new Videos_ByType_Page("top", Classes.TopVideoList));
                        }
                        else if (selectedItem.Name.Contains(AppResources.Label_Latest_videos))
                        {
                            await Navigation.PushAsync(new Videos_ByType_Page("latest", Classes.LatestVideoList));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        //Event click Item in Section_View_Template => Catigory >> open Videos_ByCategory_Page
        private async void OnCatigoryTapped(object sender, EventArgs e)
        {
            try
            {
                var selectedItem = (Classes.Catigory) ((Category_Item_Template) sender).BindingContext;
                if (selectedItem != null)
                {
                    await Navigation.PushAsync(new Videos_ByCategory_Page(selectedItem.Id, selectedItem.Name));
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

        #endregion

        public void StyleThemeChanger()
        {
            try
            {
                if (Settings.DarkTheme)
                {
                    ImageLogo.Source = "logolight.png";
                    MainStackPanel.BackgroundColor = Color.FromHex("#444");
                    TopHeaderBAR.BackgroundColor = Color.FromHex("#2f2f2f");
                    TopHeaderBAR.OutlineColor = Color.FromHex("#2f2f2f");
                    SearchIcon.TextColor = Color.FromHex("#ffff");
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

    }
}