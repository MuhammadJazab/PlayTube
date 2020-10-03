using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayTube.Languish;
using Xamarin.Forms;

namespace PlayTube.Pages.Custom_View
{
    public partial class Section_View_Template : ContentView
    {
        public Section_View_Template()
        {
            InitializeComponent();

            Lbl_SEE_ALL.Text= AppResources.Label_SEE_ALL;

            if (Settings.DarkTheme)
            {
                MainStackPanel.BackgroundColor = Color.FromHex("#444");
                Liner.BackgroundColor = Color.FromHex("#767676");
                Name.TextColor = Color.FromHex("#ffff");
            }
        }

       
     }
}
