using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using PlayTube.Controls;
using PlayTube.SQLite;
using Plugin.Connectivity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PlayTube.Pages.Walkthrough
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WalkThrough_Page1 : ContentPage
    {
        public WalkThrough_Page1()
        {
            try
            {
                NavigationPage.SetHasNavigationBar(this, false);
                InitializeComponent();

                var Asd = new TapGestureRecognizer();
                Asd.Tapped += OnPrimaryActionButtonClicked;
                GetSettings_Api();
                NextLabel.GestureRecognizers.Add(Asd);
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        //Get Settings Api
        public async void GetSettings_Api()
        {
            try
            {
                ObservableCollection<DataTables.CatigoriesTB> ListCatigories_Names = new ObservableCollection<DataTables.CatigoriesTB>();

                if (!CrossConnectivity.Current.IsConnected)
                {
                    //  await DisplayAlert("Error", "Check Your Internet Connection", "OK");
                }
                else
                {
                    var data = await API_Request.Get_Settings_Http();
                    if (data != null)
                    {
                        //site_settings
                        try
                        {
                            JObject Items_settings = JObject.FromObject(data["site_settings"]);

                            DataTables.MySettingsTB ms = new DataTables.MySettingsTB();

                            var theme = Items_settings["theme"].ToString();
                            var censored_words = Items_settings["censored_words"].ToString();
                            var title = Items_settings["title"].ToString();
                            var name = Items_settings["name"].ToString();
                            var keyword = Items_settings["keyword"].ToString();
                            var email = Items_settings["email"].ToString();
                            var description = Items_settings["description"].ToString();
                            var validation = Items_settings["validation"].ToString();
                            var recaptcha = Items_settings["recaptcha"].ToString();
                            var language = Items_settings["language"].ToString();
                            var seo_link = Items_settings["seo_link"].ToString();
                            var comment_system = Items_settings["comment_system"].ToString();
                            var delete_account = Items_settings["delete_account"].ToString();
                            var total_videos = Items_settings["total_videos"].ToString();
                            var total_views = Items_settings["total_views"].ToString();
                            var total_users = Items_settings["total_users"].ToString();
                            var total_subs = Items_settings["total_subs"].ToString();
                            var total_comments = Items_settings["total_comments"].ToString();
                            var total_likes = Items_settings["total_likes"].ToString();
                            var total_dislikes = Items_settings["total_dislikes"].ToString();
                            var total_saved = Items_settings["total_saved"].ToString();
                            var upload_system = Items_settings["upload_system"].ToString();
                            var import_system = Items_settings["import_system"].ToString();
                            var autoplay_system = Items_settings["autoplay_system"].ToString();
                            var history_system = Items_settings["history_system"].ToString();
                            var user_registration = Items_settings["user_registration"].ToString();
                            var verification_badge = Items_settings["verification_badge"].ToString();
                            var comments_default_num = Items_settings["comments_default_num"].ToString();
                            var fb_login = Items_settings["fb_login"].ToString();
                            var tw_login = Items_settings["tw_login"].ToString();
                            var plus_login = Items_settings["plus_login"].ToString();
                            var go_pro = Items_settings["go_pro"].ToString();
                            var user_ads = Items_settings["user_ads"].ToString();
                            var max_upload = Items_settings["max_upload"].ToString();
                            var theme_url = Items_settings["theme_url"].ToString();
                            var site_url = Items_settings["site_url"].ToString();
                            var script_version = Items_settings["script_version"].ToString();


                            ms.S_theme = theme;
                            ms.S_censored_words = censored_words;
                            ms.S_title = title;
                            ms.S_name = name;
                            ms.S_keyword = keyword;
                            ms.S_email = email;
                            ms.S_description = description;
                            ms.S_validation = validation;
                            ms.S_recaptcha = recaptcha;
                            ms.S_language = language;
                            ms.S_seo_link = seo_link;
                            ms.S_comment_system = comment_system;
                            ms.S_delete_account = delete_account;
                            ms.S_total_videos = total_videos;
                            ms.S_total_views = total_views;
                            ms.S_total_users = total_users;
                            ms.S_total_subs = total_subs;
                            ms.S_total_comments = total_comments;
                            ms.S_total_likes = total_likes;
                            ms.S_total_dislikes = total_dislikes;
                            ms.S_total_saved = total_saved;
                            ms.S_upload_system = upload_system;
                            ms.S_import_system = import_system;
                            ms.S_autoplay_system = autoplay_system;
                            ms.S_history_system = history_system;
                            ms.S_user_registration = user_registration;
                            ms.S_verification_badge = verification_badge;
                            ms.S_comments_default_num = comments_default_num;
                            ms.S_fb_login = fb_login;
                            ms.S_tw_login = tw_login;
                            ms.S_plus_login = plus_login;
                            ms.S_go_pro = go_pro;
                            ms.S_user_ads = user_ads;
                            ms.S_max_upload = max_upload;
                            ms.S_theme_url = theme_url;
                            ms.S_site_url = site_url;
                            ms.S_script_version = script_version;
                            ms.S_AddShortcutIcon = "true";

                            //Insert MySettings in Database
                            SQL_Commander.Insert_Settings_Credentials(ms);
                        }
                        catch (Exception ex)
                        {
                            var exception = ex.ToString();
                        }

                        //categories
                        try
                        {
                            JObject Items_categories = JObject.FromObject(data["categories"]);
                            if (Items_categories != null)
                            {
                                foreach (var pair in Items_categories)
                                {
                                    DataTables.CatigoriesTB cat = new DataTables.CatigoriesTB();

                                    var Id = pair.Key;
                                    var Name = pair.Value;

                                    cat.Catigories_Id = Id;
                                    cat.Catigories_Name = Name.ToString();

                                    ListCatigories_Names.Add(cat);
                                }
                                //Insert Catigories in Database
                                SQL_Commander.Insert_Catigories_Credentials(ListCatigories_Names);
                            }
                        }
                        catch (Exception ex)
                        {
                            var exception = ex.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        private void OnPrimaryActionButtonClicked(object sender, EventArgs e)
        {
            var parent = (WalkthroughVariantPage)Parent;
            parent.GoToStep();
        }

        public async Task AnimateIn()
        {
            await Task.WhenAll(new[] {
                AnimateItem (IconLabel, 500),
                AnimateItem (HeaderLabel, 600),
                AnimateItem (DescriptionLabel, 700)
            });
        }

        private async Task AnimateItem(View uiElement, uint duration)
        {
            await Task.WhenAll(new Task[] {
                uiElement.ScaleTo(1.5, duration, Easing.CubicIn),
                uiElement.FadeTo(1, duration/2, Easing.CubicInOut).ContinueWith(
                    _ =>
                    {
                        // Queing on UI to workaround an issue with Forms 2.1
                        Device.BeginInvokeOnMainThread(() => {
                            uiElement.ScaleTo(1, duration, Easing.CubicOut);
                        });
                    })
            });
        }

        //private void ResetAnimation()
        //{
        //    IconLabel.Opacity = 0;
        //    HeaderLabel.Opacity = 0;
        //    DescriptionLabel.Opacity = 0;
        //    OverlapedButtonContainer.BackgroundColor = BackgroundColor;
        //}

        private void WalkThrough_Page1_OnAppearing(object sender, EventArgs e)
        {
            AnimateIn();
        }
    }
}