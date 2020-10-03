using System;
using PlayTube.Controls;
using PlayTube.Dependencies;
using PlayTube.Languish;
using PlayTube.SQLite;
using Plugin.Share;
using Plugin.Share.Abstractions;
using Xamarin.Forms;

namespace PlayTube.Pages.Custom_View
{
    public partial class Big_Video_Template : ContentView
    {
        public Big_Video_Template()
        {
            InitializeComponent();
        }

       
        private async void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            try
            {
                var ItemVideo = (Classes.Video)((Label)sender).BindingContext;
                if (ItemVideo != null)
                { /*AppResources.Label_Add_to_play_list *//*AppResources.Label_Not_interested*/
                    var action = await App.Current.MainPage.DisplayActionSheet(AppResources.Label_Choose_your_action, AppResources.Label_Cancel, null, AppResources.Label_Add_to_watch_later, AppResources.Label_Share);

                    if (action == AppResources.Label_Share)
                    {
                        try
                        {
                            if (!CrossShare.IsSupported)
                            {
                                return;
                            }
                            await CrossShare.Current.Share(new ShareMessage
                            {
                                Title = ItemVideo.dv_title,
                                Text = ItemVideo.dv_description,
                                Url = ItemVideo.dv_url
                            });
                        }
                        catch (Exception ex)
                        {
                            var exception = ex.ToString();
                        }
                    }
                    else if (action == AppResources.Label_Not_interested)
                    {
                        Classes.VideoList.Remove(ItemVideo);
                    }
                    else if (action == AppResources.Label_Add_to_watch_later)
                    {
                        try
                        {
                            //Add to watch Later Database
                            SQL_Commander.Insert_WatchLaterVideos_Credentials(ItemVideo);

                            DependencyService.Get<IMethods>().LongAlert(AppResources.Label_Add_The_Videos_to_watch_Later);
                        }
                        catch (Exception ex)
                        {
                            var exception = ex.ToString();
                        }
                    }
                    else if (action == AppResources.Label_Add_to_play_list)
                    {
                        //if (Hamburg_Page.IsLogin)
                        //{
                        //    //Add to PlayList Database
                        //    SQL_Commander.Insert_PlayListsVideos_Credentials(Item);

                        //    DependencyService.Get<IMethods>().LongAlert(AppResources.Label_Add_The_Videos_To_PlayList);
                        //}
                        //var ErrorMSG = await DisplayAlert(AppResources.Label_Error, AppResources.Label_Please_sign_in_Playlist, AppResources.Label_Yes, AppResources.Label_No);
                        //if (!ErrorMSG)
                        //{
                        //    await Navigation.PushModalAsync(new Login_Page());
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }
    }
}
