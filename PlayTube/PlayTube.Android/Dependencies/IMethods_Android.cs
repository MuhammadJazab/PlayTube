using System;
using Android.Content;
using Android.App;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using PlayTube.Dependencies;
using Xamarin.Forms;
using Xam.Plugin.WebView.Abstractions;
using Xam.Plugin.WebView.Droid;
using Xamarin.Forms.Platform.Android;

[assembly: Dependency(typeof(PlayTube.Droid.Dependencies.IMethods_Android))]
namespace PlayTube.Droid.Dependencies
{
    class IMethods_Android : IMethods
    {
        public void CopyToClipboard(string text)
        {
            var clipboardManager = (ClipboardManager)Forms.Context.GetSystemService(Context.ClipboardService);

            // Create a new Clip
            ClipData clip = ClipData.NewPlainText("xxx_title", text);

            // Copy the text
            clipboardManager.PrimaryClip = clip;

            MainActivity.AndroidClipboardManager.Text = text;
        }

        //IMessage ============================

        #region IMessage

        public void LongAlert(string message)
        {
            var context = Forms.Context;
            Toast.MakeText(context, message, ToastLength.Long).Show();
        }

        public void ShortAlert(string message)
        {
            var context = Forms.Context;
            Toast.MakeText(context, message, ToastLength.Short).Show();
        }

        #endregion


        //Lang =============================
        public void SetLocale()
        {
            try
            {
                var androidLocale = Java.Util.Locale.Default; // user's preferred locale
                var netLocale = androidLocale.ToString().Replace("_", "-");
                var ci = new System.Globalization.CultureInfo(netLocale);
                Thread.CurrentThread.CurrentCulture = ci;
                Thread.CurrentThread.CurrentUICulture = ci;
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        public string GetCurrent()
        {
            try
            {
                var androidLocale = Java.Util.Locale.Default; // user's preferred locale

                // en, es, ja
                var netLanguage = androidLocale.Language.Replace("_", "-");
                // en-US, es-ES, ja-JP
                var netLocale = androidLocale.ToString().Replace("_", "-");

                var ci = new System.Globalization.CultureInfo(netLanguage);
                Thread.CurrentThread.CurrentCulture = ci;
                Thread.CurrentThread.CurrentUICulture = ci;

                if (ci.IsNeutralCulture)
                {
                    Thread.CurrentThread.CurrentCulture = ci;
                    Thread.CurrentThread.CurrentUICulture = ci;
                }
                else
                {
                    Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en");
                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
                }

                return netLanguage;
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
                return "";
            }
        }
    }
}