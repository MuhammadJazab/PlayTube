using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PlayTube.CustomRenders;
using PlayTube.Droid.CustomRenders;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


[assembly: ExportRenderer(typeof(RoundedEntry), typeof(RoundedEntryRendererAndroid))]
namespace PlayTube.Droid.CustomRenders
{

    public class RoundedEntryRendererAndroid : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {

                var gradientDrawable = new GradientDrawable();
                gradientDrawable.SetCornerRadius(60f);

                gradientDrawable.SetStroke(1, global::Android.Graphics.Color.ParseColor(Settings.MainColor));
                gradientDrawable.SetColor(global::Android.Graphics.Color.Transparent);

                Control.SetBackground(gradientDrawable);

                Control.SetPadding(30, Control.PaddingTop, Control.PaddingRight,
                    Control.PaddingBottom);
            }
        }
    }
}