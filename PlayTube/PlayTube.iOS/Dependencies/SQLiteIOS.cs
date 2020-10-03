using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using PlayTube.Dependencies;
using SQLite.Net.Interop;
using SQLite.Net.Platform.XamarinIOS;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(PlayTube.iOS.Dependencies.SQLiteIOS))]
namespace PlayTube.iOS.Dependencies
{
    class SQLiteIOS : SQLitePCL
    {
        private string DirectoriPrivate;
        private ISQLitePlatform Platforma;

        public string DirectoriDB
        {
            get
            {
                if (string.IsNullOrEmpty(DirectoriPrivate))
                {
                    var der = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                    DirectoriPrivate = System.IO.Path.Combine(der, "..", "Library");
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
                    Platforma = new SQLitePlatformIOS();
                }
                return Platforma;
            }
        }
    }
}