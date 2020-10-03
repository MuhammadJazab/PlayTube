using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace PlayTube.Pages.Custom_View
{
    public partial class Watch_Later_Video_Template : ContentView
    {
        public Watch_Later_Video_Template()
        {
            InitializeComponent();

            if (Settings.DarkTheme)
            {
                MainStackPanel.BackgroundColor = Color.FromHex("#444");
                Liner.BackgroundColor = Color.FromHex("#767676");
                Video_Title.TextColor = Color.FromHex("#ffff");
                Channel_Name.TextColor = Color.FromHex("#ffff");
                Views.TextColor = Color.FromHex("#ffff");
                Time.TextColor = Color.FromHex("#ffff");
            }
        }
    }
}
