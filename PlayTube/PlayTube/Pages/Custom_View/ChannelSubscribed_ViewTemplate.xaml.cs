using System;
using PlayTube.Controls;
using PlayTube.Dependencies;
using PlayTube.Languish;
using PlayTube.SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PlayTube.Pages.Custom_View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ChannelSubscribed_ViewTemplate : ContentView
	{
		public ChannelSubscribed_ViewTemplate ()
		{
		    try
		    {
		        InitializeComponent();
		        if (Settings.DarkTheme)
		        {
		            BackgroundColor = Color.FromHex("#767676");
		            MainStackPanel.BackgroundColor = Color.FromHex("#444");
                }
		        else
		        {
                }

                IconSubcribe.Text = IoniciconsFont.CheckmarkCircled;
		        IconSubcribe.TextColor = Color.FromHex("#ffff");
		        SubcribeLabel.TextColor = Color.FromHex("#ffff");
		        SubcribeLabel.Text = AppResources.Label_Subscribed;
		        SubcribeFrame.BackgroundColor = Color.FromHex(Settings.MainColor);
            }
            catch (Exception ex)
		    {
		        var exception = ex.ToString();
		    }
        }

        private async void SubcribeButton_Tabbed(object sender, EventArgs e)
        {
            try
            {
                var selectedItem = (Classes.Channel)((Frame)sender).BindingContext;
                if (selectedItem != null)
                {
                    if (SubcribeLabel.Text == AppResources.Label_Subscribe)
                    {
                        IconSubcribe.Text = IoniciconsFont.CheckmarkCircled;
                        IconSubcribe.TextColor = Color.FromHex("#ffff");
                        SubcribeLabel.TextColor = Color.FromHex("#ffff");
                        SubcribeLabel.Text = AppResources.Label_Subscribed;
                        SubcribeFrame.BackgroundColor = Color.FromHex(Settings.MainColor);

                        //Convert From Classes.Channel
                        Classes.Video Channel = new Classes.Video();

                        //Owner
                        Channel.Owner_id = selectedItem.Channel_id;
                        Channel.Owner_username = selectedItem.Channel_username;
                        Channel.Owner_email = selectedItem.Channel_email;
                        Channel.Owner_first_name = selectedItem.Channel_first_name;
                        Channel.Owner_last_name = selectedItem.Channel_last_name;
                        Channel.Owner_gender = selectedItem.Channel_gender;
                        Channel.Owner_language = selectedItem.Channel_language;
                        Channel.Owner_avatar = selectedItem.Channel_avatar;
                        Channel.Owner_cover = selectedItem.Channel_cover;
                        Channel.Owner_about = selectedItem.Channel_about;
                        Channel.Owner_google = selectedItem.Channel_google;
                        Channel.Owner_facebook = selectedItem.Channel_facebook;
                        Channel.Owner_twitter = selectedItem.Channel_twitter;
                        Channel.Owner_verified = selectedItem.Channel_verified;
                        Channel.Owner_is_pro = selectedItem.Channel_is_pro;
                        Channel.Owner_url = selectedItem.Channel_url;

                        //Add The Video to  Subcribed Videos Database
                        SQL_Commander.Insert_SubscriptionsChannel_Credentials(Channel);
                        DependencyService.Get<IMethods>().LongAlert(AppResources.Label_Subcribed_Channel);

                        //Send API Request here for  Subcribe
                        var request = await API_Request.Add_Subscribe_To_Channel_Http(selectedItem.Channel_id);
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
                        SQL_Commander.RemoveSubscriptionsChannel_Credentials(selectedItem.Channel_id);
                        DependencyService.Get<IMethods>().LongAlert(AppResources.Label_Remove_Subcribed_Channel);

                        //Send API Request here for UnSubcribe
                        var request = await API_Request.Add_Subscribe_To_Channel_Http(selectedItem.Channel_id);
                        if (request != null)
                        {
                        }
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