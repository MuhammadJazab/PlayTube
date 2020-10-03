using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Reflection;
using Acr.UserDialogs;
using Xamarin.Forms;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Gms.Ads;
using Android.Net.Http;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Util;
using FFImageLoading.Forms;
using FFImageLoading.Forms.Droid;
using FFImageLoading.Transformations;
using Java.IO;
using Xamarin.Forms.Platform.Android;
using PlayTube;
using Refractored.XamForms.PullToRefresh.Droid;
using SegmentedControl.FormsPlugin.Android;
using Xam.Plugin.WebView.Droid;
using Console = System.Console;

namespace PlayTube.Droid
{
    [Activity(Label = "PlayTube", Icon = "@drawable/icon", Theme = "@style/AppTheme", LaunchMode = LaunchMode.SingleTop, MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static ClipboardManager AndroidClipboardManager { get; private set; }
        protected override void OnCreate(Bundle bundle)
        {
            try
            {
                FormsAppCompatActivity.ToolbarResource = Resource.Layout.Toolbar;
                FormsAppCompatActivity.TabLayoutResource = Resource.Layout.Tabbar;

                base.OnCreate(bundle);

               
                if (Settings.TurnFullScreenOn)
                {
                    this.Window.AddFlags(WindowManagerFlags.Fullscreen);
                    this.Window.AddFlags(WindowManagerFlags.KeepScreenOn);
                }

                if (Settings.TurnSecurityProtocolType3072On)
                {
                    ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                    HttpClient client = new HttpClient(new Xamarin.Android.Net.AndroidClientHandler());
                }

                if (Settings.TurnTrustFailureOn_WebException)
                {
                    //If you are Getting this error >>> System.Net.WebException: Error: TrustFailure /// then Set it to true
                    System.Net.ServicePointManager.ServerCertificateValidationCallback += (o, certificate, chain, errors) => true;
                    System.Security.Cryptography.AesCryptoServiceProvider b = new System.Security.Cryptography.AesCryptoServiceProvider();
                }

                //Methods =>> Copy the text
                AndroidClipboardManager = (ClipboardManager)GetSystemService(ClipboardService);

                CachedImageRenderer.Init();
                var ignore = new CircleTransformation();

                var assembliesToInclude = new List<Assembly>()
                {
                    typeof(CachedImage).GetTypeInfo().Assembly,
                    typeof(CachedImageRenderer).GetTypeInfo().Assembly
                };
                SegmentedControlRenderer.Init();
                PullToRefreshLayoutRenderer.Init();
                FFImageLoading.Forms.Droid.CachedImageRenderer.Init();
                global::Xamarin.Forms.Forms.Init(this, bundle);

                try
                {
                    var activity = Xamarin.Forms.Forms.Context;
                    File httpCacheDir = new File(activity.CacheDir, "http");
                    long httpCacheSize = 10 * 1024 * 1024; // 10 MiB
                    HttpResponseCache.Install(httpCacheDir, httpCacheSize);
                }
                catch (IOException ce)
                {

                }

                UserDialogs.Init(this);

                MobileAds.Initialize(ApplicationContext, Settings.Ad_Unit_ID);

                FormsWebViewRenderer.Initialize();
                LoadApplication(new App());

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
           
        }

        protected void OnStop()
        {

            var cache = HttpResponseCache.Installed;
            if (cache != null)
            {
                cache.Flush();
            }
        }

}
}

