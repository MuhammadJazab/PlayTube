using System;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.CSharp.RuntimeBinder;
using Newtonsoft.Json.Linq;
using PlayTube.Controls;
using PlayTube.Languish;
using Plugin.Connectivity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PlayTube.Pages.Default
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Videos_ByCategory_Page : ContentPage
    {
        #region Variables

        public string Id_Category, Name_Category;

        #endregion

        public Videos_ByCategory_Page(string id, string name)
        {
            try
            {
                InitializeComponent();
                VideoByCatListView.IsRefreshing = true;
                Id_Category = id;
                Name_Category = name;

                Title = Name_Category;

                StyleThemeChanger();

                var cheker = Classes.VideoByCatigoryList.Where(a => a.dv_category_id == id).ToList();

                if (cheker.Count == 0)
                {
                   
                    VideoByCatListView.BeginRefresh();
                }
                else
                {
                    
                    VideoByCatPage.IsVisible = true;
                    EmptyPage.IsVisible = false;
                }
                VideoByCatListView.ItemsSource = cheker;
                VideoByCatListView.IsRefreshing = false;
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
                VideoByCatListView.IsRefreshing = false;
            }
        }

        public async void Videos_By_Category_API(string offset)
        {
            try
            {
                if (!CrossConnectivity.Current.IsConnected)
                {
                    await DisplayAlert(AppResources.Label_Error, AppResources.Label_Check_Your_Internet,
                        AppResources.Label_OK);
                }
                else
                {
                   
                    
                    var Videos_data = await API_Request.Get_Videos_By_Category_Http(Id_Category, "20", offset);
                    if (Videos_data != null)
                    {
                        VideoByCatPage.IsVisible = true;
                        EmptyPage.IsVisible = false;
                        VideoByCatListView.IsRefreshing = true;

                        foreach (var All in Videos_data)
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
                                V.SV_Type_video = "bycat";
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
                                        about =Functions.StringNullRemover(Functions.DecodeStringWithEnter(Functions.DecodeString(Owner["about"].ToString())));
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
                                var checker =Classes.VideoByCatigoryList.FirstOrDefault(a => a.dv_video_id == V.dv_video_id);
                                if (checker == null)
                                {
                                    Classes.VideoByCatigoryList.Add(V);
                                }
                            }
                            catch (Exception ex)
                            {
                                var exception = ex.ToString();
                            }
                        }
                        VideoByCatListView.EndRefresh();

                        //Add video by catigory
                        ObservableCollection<Classes.Video> listsCollection = new ObservableCollection<Classes.Video>(
                            Classes.VideoByCatigoryList.Where(a => a.dv_category_id == Id_Category && a.dv_category_name == Name_Category));
                        if (listsCollection.Count > 0)
                        {
                            VideoByCatListView.EndRefresh();

                            VideoByCatPage.IsVisible = true;
                            EmptyPage.IsVisible = false;

                            VideoByCatListView.ItemsSource = Classes.VideoByCatigoryList;
                        }
                        else
                        {
                            VideoByCatListView.EndRefresh();

                            EmptyPage.IsVisible = true;
                            VideoByCatPage.IsVisible = false;
                        }
                    }
                    else
                    {
                        VideoByCatListView.EndRefresh();

                        EmptyPage.IsVisible = true;
                        VideoByCatPage.IsVisible = false;
                    }
                }
                VideoByCatListView.IsRefreshing = false;
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
                VideoByCatListView.EndRefresh();
                VideoByCatListView.IsRefreshing = false;
                //Add video by catigory
                VideoByCatListView.ItemsSource = Classes.VideoByCatigoryList;
            }
        }

        #region Event Tapped

        private void VideoByCatListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                VideoByCatListView.SelectedItem = null;
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        //Event click Item >> open Video_Player_Page
        private async void VideoByCatListView_OnItemTapped(object sender, ItemTappedEventArgs e)
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

        //Event click Random Videos
        private void Btn_Random_OnClicked(object sender, EventArgs e)
        {
            try
            {
                VideoByCatPage.IsVisible = true;
                EmptyPage.IsVisible = false;

                Title = AppResources.Label_Random_Videos;

                if (Classes.VideoList.Count > 0)
                {
                    VideoByCatListView.ItemsSource = Classes.VideoList;
                }
                else
                {
                    VideoByCatListView.ItemsSource = Classes.VideoByCatigoryList;
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        #endregion

        //Event Item Appearing >> Get More video By Category
        private async void VideoByCatListView_OnItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            try
            {
                var currentItem = e.Item as Classes.Video;
                var beforelastItem = Classes.VideoByCatigoryList[Classes.VideoByCatigoryList.Count - 2];
                if (currentItem == beforelastItem)
                {
                    if (!CrossConnectivity.Current.IsConnected)
                    {
                        await DisplayAlert(AppResources.Label_Error, AppResources.Label_Check_Your_Internet, AppResources.Label_OK);
                    }
                    else
                    {
                       // Title = AppResources.Label_Loading;
                        var lastItem = Classes.VideoByCatigoryList[Classes.VideoByCatigoryList.Count - 1];
                        if (lastItem != null)
                        {
                            Videos_By_Category_API(lastItem.dv_id);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        //Event Refreshing VideoByCatListView
        private void VideoByCatListView_OnRefreshing(object sender, EventArgs e)
        {
            try
            {
                if (Classes.VideoByCatigoryList.Count > 0)
                {
                    Classes.VideoByCatigoryList.Clear();
                }

                Videos_By_Category_API("0");
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
                VideoByCatListView.BackgroundColor = Color.FromHex("#444");
            }
        }
    }
}