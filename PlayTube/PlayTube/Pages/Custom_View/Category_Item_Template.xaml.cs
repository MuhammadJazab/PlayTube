using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace PlayTube.Pages.Custom_View
{
    public partial class Category_Item_Template : ContentView
    {
        public Category_Item_Template()
        {
            InitializeComponent();

            if (Settings.DarkTheme)
            {
                MainStackPanel.BackgroundColor = Color.FromHex("#2f2f2f");
                Name.TextColor = Color.FromHex("#ffff");
                LayoutofLabel.BackgroundColor = Color.FromHex("#2f2f2f");
            }
        }
    }
}
