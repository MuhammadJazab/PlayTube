using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PlayTube.Droid;

namespace PlayTube.Android
{
    [Activity(NoHistory = true, Theme = "@style/Theme.Splash", MainLauncher = true)]
    public class SplashActivity : Activity
    {
        private Bundle savedInstanceState;

        protected override void OnCreate(Bundle bundle)
        {
            RequestWindowFeature(WindowFeatures.NoTitle);
            this.Window.AddFlags(WindowManagerFlags.Fullscreen);
            this.Window.AddFlags(WindowManagerFlags.KeepScreenOn);

            base.OnCreate(savedInstanceState);



            StartActivity(typeof(MainActivity));
        }
    }
}