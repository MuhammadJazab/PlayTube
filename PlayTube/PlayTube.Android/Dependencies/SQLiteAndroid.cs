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
using PlayTube.Dependencies;
using SQLite.Net.Interop;
using SQLite.Net.Platform.XamarinAndroid;
using Xamarin.Forms;

[assembly: Dependency(typeof(PlayTube.Droid.Dependencies.SQLiteAndroid))]
namespace PlayTube.Droid.Dependencies
{
   
   public class SQLiteAndroid : SQLitePCL
    {
        private string DirectoriPrivate;
        private ISQLitePlatform Platforma;


        public string DirectoriDB
        {
            get
            {
                if (string.IsNullOrEmpty(DirectoriPrivate))
                {
                    DirectoriPrivate = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

                }
                return DirectoriPrivate;
            }
        }

        public ISQLitePlatform Platform
        {
            get
            {
                if (Platforma == null)
                {
                    Platforma = new SQLitePlatformAndroid();

                }
                return Platforma;
            }
        }
    }
}