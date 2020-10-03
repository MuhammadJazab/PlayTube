using System;
using Android.Gms.Ads;
using PlayTube.CustomRenders;
using PlayTube.Droid.CustomRenders;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(AdMobView), typeof(AdMobViewRenderer))]
namespace PlayTube.Droid.CustomRenders
{
    public class AdMobViewRenderer : ViewRenderer<AdMobView, AdView>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<AdMobView> elementChangedEventArgs)
        {
            try
            {
                base.OnElementChanged(elementChangedEventArgs);

                if (Control == null)
                {
                    var ad = new AdView(Forms.Context);
                    ad.AdSize = AdSize.Banner;
                    ad.AdUnitId = Settings.Ad_Unit_ID;

                    var requestbuilder = new AdRequest.Builder();
                    ad.LoadAd(requestbuilder.Build());

                    SetNativeControl(ad);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}