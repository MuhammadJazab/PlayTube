using System;
using System.Collections.ObjectModel;
using PlayTube.Controls;
using PlayTube.Languish;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PlayTube.Pages.Default
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Videos_ByType_Page : ContentPage
    {
        #region Lists Items and Variables

        private string TypeVideo;

        #endregion

        public Videos_ByType_Page(string type, ObservableCollection<Classes.Video> VideoByTypeList)
        {
            try
            {
                InitializeComponent();

                TypeVideo = type;
                Title = AppResources.Label_Videos_by + " " + TypeVideo;

                if (VideoByTypeList.Count > 0)
                {
                    VideoByTypePage.IsVisible = true;
                    EmptyPage.IsVisible = false;

                    VideoByTypeListView.ItemsSource = VideoByTypeList;
                }
                else
                {
                    EmptyPage.IsVisible = true;
                    VideoByTypePage.IsVisible = false;
                }

                if (Settings.DarkTheme)
                {
                    VideoByTypeListView.BackgroundColor = Color.FromHex("#444");

                    foreach (var item in VideoByTypeList)
                    {
                        item.SV_BackgroundColor = "#bcbcbc";
                        item.SV_TextColor = "#ffff";
                    }
                }
                else
                {
                    foreach (var item in VideoByTypeList)
                    {
                        item.SV_BackgroundColor = "#ffff";
                        item.SV_TextColor = "#444";
                    }
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        private void VideoByTypeListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                VideoByTypeListView.SelectedItem = null;
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        //Event click Item >> open Video_Player_Page
        private async void VideoByTypeListView_OnItemTapped(object sender, ItemTappedEventArgs e)
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

        private void VideoByTypeListView_OnItemAppearing(object sender, ItemVisibilityEventArgs e)
        {

        }

    }
}