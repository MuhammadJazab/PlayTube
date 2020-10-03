using System;
using System.Linq;
using Newtonsoft.Json.Linq;
using PlayTube.Controls;
using PlayTube.Languish;
using Plugin.Connectivity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PlayTube.Pages.Default
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Search_Page : ContentPage
    {
        public Search_Page()
        {
            try
            {
                InitializeComponent();

                if (Device.OS == TargetPlatform.iOS)
                    NavigationPage.SetHasNavigationBar(this, true);
                else
                    NavigationPage.SetHasNavigationBar(this, false);

                //if (Classes.SearchList.Count == 0)
                //{
                //    SearchListView.BeginRefresh();
                //}
                //SearchListView.ItemsSource = Classes.SearchList;

                StyleThemeChanger();
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        //Get Search Api
        public async void GetSearch_Result(string key , string offset)
        {
            try
            {
                if (!CrossConnectivity.Current.IsConnected)
                {
                    await DisplayAlert(AppResources.Label_Error, AppResources.Label_Check_Your_Internet, AppResources.Label_OK);
                }
                else
                {
                    SearchListView.IsPullToRefreshEnabled = true;

                    var data = await API_Request.Search_Videos_Http(key, "20", offset);
                    if (data != null)
                    {
                        PostSearchPage.IsVisible = true;
                        EmptySearchPage.IsVisible = false;

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
                                    SearchListView.BackgroundColor = Color.FromHex("#444");
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

                                var checker = Classes.SearchList.FirstOrDefault(a => a.dv_id == V.dv_id);
                                if (checker == null)
                                {
                                    //Add list All data Video
                                    Classes.SearchList.Add(V);
                                }
                            }
                            catch (Exception ex)
                            {
                                var exception = ex.ToString();
                            }
                        }
                        SearchListView.EndRefresh();
                        SearchListView.ItemsSource = Classes.SearchList;
                    }
                    else
                    {
                        SearchListView.EndRefresh();

                        EmptySearchPage.IsVisible = true;
                        PostSearchPage.IsVisible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
                SearchListView.EndRefresh();

                EmptySearchPage.IsVisible = false;
                PostSearchPage.IsVisible = true;
                SearchListView.ItemsSource = Classes.SearchList;
            }
        }

        //Event Run #GetSearch_Result
        private void SearchBar_OnSearchButtonPressed(object sender, EventArgs e)
        {
            try
            {
                EmptySearchPage.IsVisible = false;
                PostSearchPage.IsVisible = true;

                if (Classes.SearchList.Count > 0)
                {
                    Classes.SearchList.Clear();
                }

                GetSearch_Result(SearchBarCo.Text,"0");
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        //Event Refreshing SearchListView
        private void SearchListView_OnRefreshing(object sender, EventArgs e)
        {
            try
            {
                EmptySearchPage.IsVisible = false;
                PostSearchPage.IsVisible = true;

                if (Classes.SearchList.Count > 0)
                {
                    Classes.SearchList.Clear();
                }

                GetSearch_Result(SearchBarCo.Text, "0");
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        //Event Item Appearing >> Get More video 
        private async void SearchListView_OnItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            try
            {
                var currentItem = e.Item as Classes.Video;
                var beforelastItem = Classes.SearchList[Classes.SearchList.Count - 2];
                if (currentItem == beforelastItem)
                {
                    if (!CrossConnectivity.Current.IsConnected)
                    {
                        await DisplayAlert(AppResources.Label_Error, AppResources.Label_Check_Your_Internet, AppResources.Label_OK);
                    }
                    else
                    {
                        Title = AppResources.Label_Loading;
                        var lastItem = Classes.SearchList[Classes.SearchList.Count - 1];
                        if (lastItem != null)
                        {
                            GetSearch_Result(SearchBarCo.Text,lastItem.dv_id);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        private void SearchListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                SearchListView.SelectedItem = null;
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        //Event click Item >> open Video_Player_Page
        private async void SearchListView_OnItemTapped(object sender, ItemTappedEventArgs e)
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

        //Event click #Search_Random
        private void Btn_Search_OnClicked(object sender, EventArgs e)
        {
            try
            {
                if (Classes.VideoList.Count > 0)
                {
                    PostSearchPage.IsVisible = true;
                    EmptySearchPage.IsVisible = false;

                    SearchListView.ItemsSource = Classes.VideoList;
                }
                else
                {
                    EmptySearchPage.IsVisible = true;
                    PostSearchPage.IsVisible = false;
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
                EmptySearchPage.BackgroundColor = Color.FromHex("#444");
                MainStackPanel.BackgroundColor = Color.FromHex("#444");
                PostSearchPage.BackgroundColor = Color.FromHex("#444");
                SearchListView.BackgroundColor = Color.FromHex("#444");
                Lbl_Empty.TextColor = Color.FromHex("#ffff");
                Lbl_Start.TextColor = Color.FromHex("#ffff");
            }
        }
    }
}