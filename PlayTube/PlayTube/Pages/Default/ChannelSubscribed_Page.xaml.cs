using System;
using System.Collections.ObjectModel;
using PlayTube.Controls;
using PlayTube.Pages.Custom_View;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PlayTube.Pages.Default
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ChannelSubscribed_Page : ContentPage
	{
	    private ObservableCollection<Classes.Channel> AllChannelList = new ObservableCollection<Classes.Channel>();

	    public ChannelSubscribed_Page(ObservableCollection<Classes.Channel> SubChannelList)
	    {
	        try
	        {
	            InitializeComponent();

	            if (SubChannelList.Count > 0)
	            {
	                ItemsGrid.IsVisible = true;
	                EmptyPage.IsVisible = false;
                    AllChannelList = SubChannelList;
                }
	            else
	            {
	                ItemsGrid.IsVisible = false;
	                EmptyPage.IsVisible = true;
                }
	            PopulateChannelSubscribed(AllChannelList);
	        }
	        catch (Exception ex)
	        {
	            var exception = ex.ToString();
	        }
	    }

	    //Templet populate  
	    private async void PopulateChannelSubscribed(ObservableCollection<Classes.Channel> ChannelList)
	    {
	        try
	        {
	            if (ChannelList.Count == 0)
	            {

	            }
	            else
	            {

	            }

	            TopColumn.Children.Clear();

	            var column = TopColumn;

	            for (var i = 0; i < ChannelList.Count; i++)
	            {
	                var NewsTapGestureRecognizer = new TapGestureRecognizer();

	                NewsTapGestureRecognizer.Tapped += ChannelSubscribedTapped;
	                var item = new ChannelSubscribed_ViewTemplate();
	                item.BindingContext = ChannelList[i];
	                item.GestureRecognizers.Add(NewsTapGestureRecognizer);
	                column.Children.Add(item);

	                //productsList[i].ThumbnailHeight = lastHeight;
	            }
	        }
	        catch (Exception ex)
	        {
	            var exception = ex.ToString();
	        }
	    }

        //Event click Item >> open 
        private async void ChannelSubscribedTapped(Object sender, EventArgs e)
	    {
	        try
	        {
	            var selectedItem = (Classes.Channel) ((ChannelSubscribed_ViewTemplate) sender).BindingContext;
	            if (selectedItem != null)
	            {
	                await Navigation.PushAsync(new ChannelUsers_Page(null, null, selectedItem));
	            }
	        }
	        catch (Exception ex)
	        {
	            var exception = ex.ToString();
	        }
	    }
    }
}