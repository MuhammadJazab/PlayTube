using System;
using PlayTube.Controls;
using PlayTube.Languish;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PlayTube.Pages.Default
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Category_Page : ContentPage
    {
        public Category_Page()
        {
            try
            {
                InitializeComponent();

                Title = AppResources.Label_Videos_Categories;

                if (Classes.CatigoryList.Count > 0)
                {
                    CategoryListView.ItemsSource = Classes.CatigoryList;
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        private void CategoryListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                CategoryListView.SelectedItem = null;
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        //Event click Item in Videos_ByCategory_Page >> open Videos_ByCategory_Page
        private async void CategoryListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                var item = e.Item as Classes.Catigory;
                if (item != null)
                {
                    await Navigation.PushAsync(new Videos_ByCategory_Page(item.Id, item.Name)); 
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }
    }
}