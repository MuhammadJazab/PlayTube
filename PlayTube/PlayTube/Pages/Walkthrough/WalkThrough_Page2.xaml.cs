using System;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PlayTube.Pages.Walkthrough
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WalkThrough_Page2 : ContentPage
    {
        public WalkThrough_Page2()
        {
            try
            {
                NavigationPage.SetHasNavigationBar(this, false);
                InitializeComponent();
                var Asd = new TapGestureRecognizer();
                Asd.Tapped += OnPrimaryActionButtonClicked;

                NextLabel.GestureRecognizers.Add(Asd);
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
            await Task.WhenAll(new[]
            {
                AnimateItem(IconLabel, 500),
                AnimateItem(HeaderLabel, 600),
                AnimateItem(DescriptionLabel, 700)
            });
        }

        private async Task AnimateItem(View uiElement, uint duration)
        {
            await Task.WhenAll(new Task[]
            {
                uiElement.ScaleTo(1.5, duration, Easing.CubicIn),
                uiElement.FadeTo(1, duration / 2, Easing.CubicInOut)
                    .ContinueWith(
                        _ =>
                        {
                            // Queing on UI to workaround an issue with Forms 2.1
                            Device.BeginInvokeOnMainThread(() =>
                            {
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


        private void WalkThrough_Page2_OnAppearing(object sender, EventArgs e)
        {
            AnimateIn();
        }
    }
}