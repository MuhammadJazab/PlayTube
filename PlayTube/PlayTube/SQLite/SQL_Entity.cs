using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayTube.Dependencies;
using SQLite.Net;
using Xamarin.Forms;

namespace PlayTube.SQLite
{
    public class SQL_Entity
    {
        public static SQLiteConnection Connection;

        //Open Connection in Database
        public static void Connect()
        {
            try
            {
                var config = DependencyService.Get<SQLitePCL>();
                Connection = new SQLiteConnection(config.Platform, Path.Combine(config.DirectoriDB, "PlayTube.db3"));

                Connection.CreateTable<DataTables.MySettingsTB>();
                Connection.CreateTable<DataTables.LoginTB>();
                Connection.CreateTable<DataTables.CatigoriesTB>();
                Connection.CreateTable<DataTables.LikedVideosTB>();
                Connection.CreateTable<DataTables.ChannelTB>(); 
                Connection.CreateTable<DataTables.HistoryVideosTB>();
                Connection.CreateTable<DataTables.PlayListsVideosTB>();
                Connection.CreateTable<DataTables.WatchLaterVideosTB>();
                Connection.CreateTable<DataTables.SubscriptionsChannelTB>();
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        //Close Connection in Database
        public static void Close()
        {
            try
            {
                Connection.Close();
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        //Delete table 
        public static void DropAll()
        {
            try
            {
                Connection.DropTable<DataTables.MySettingsTB>();
                Connection.DropTable<DataTables.LoginTB>();
                Connection.DropTable<DataTables.CatigoriesTB>();
                Connection.DropTable<DataTables.LikedVideosTB>();
                Connection.DropTable<DataTables.ChannelTB>();
                Connection.DropTable<DataTables.HistoryVideosTB>();
                Connection.DropTable<DataTables.PlayListsVideosTB>();
                Connection.DropTable<DataTables.WatchLaterVideosTB>();
                Connection.DropTable<DataTables.SubscriptionsChannelTB>();
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }
    }
}