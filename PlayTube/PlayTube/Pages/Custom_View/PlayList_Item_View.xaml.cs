using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayTube.Controls;
using Plugin.Share;
using Plugin.Share.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PlayTube.Pages.Custom_View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlayList_Item_View : ContentView
    {
        public PlayList_Item_View()
        {
            InitializeComponent();

            if (Settings.DarkTheme)
            {
                MainStackPanel.BackgroundColor = Color.FromHex("#444");
                Liner.BackgroundColor = Color.FromHex("#767676");
                PlayList_Name.TextColor = Color.FromHex("#ffff");
                PlayList_Description.TextColor = Color.FromHex("#ffff");
            }
        }
    }
}