using PropertyChanged;
using System.Collections.ObjectModel;
using PlayTube.SQLite;
using Xamarin.Forms;

namespace PlayTube.Controls
{
    public class Classes
    {
        //############# DONT'T MODIFY HERE #############
        //List Items Declaration 
        //*********************************************************
        public static ObservableCollection<MySettings> MySettingsList = new ObservableCollection<MySettings>();
        public static ObservableCollection<Catigory> CatigoryList = new ObservableCollection<Catigory>();
        public static ObservableCollection<DataTables.LoginTB> DataUserLoginList = new ObservableCollection<DataTables.LoginTB>();
        public static ObservableCollection<PageItems> ListPage = new ObservableCollection<PageItems>();
        public static ObservableCollection<SeconderyPageItems> SeconderyListPage = new ObservableCollection<SeconderyPageItems>();

        public static ObservableCollection<Channelprofiletems> ChannelprofileListItems = new ObservableCollection<Channelprofiletems>();
        public static ObservableCollection<Channelprofiletems> ChannelUsersprofileListItems = new ObservableCollection<Channelprofiletems>();

        public static ObservableCollection<Video> VideoList = new ObservableCollection<Video>();
        public static ObservableCollection<Video> FeaturedVideoList = new ObservableCollection<Video>();
        public static ObservableCollection<Video> TopVideoList = new ObservableCollection<Video>();
        public static ObservableCollection<Video> LatestVideoList = new ObservableCollection<Video>();
        public static ObservableCollection<Video> TrendingList = new ObservableCollection<Video>();
        public static ObservableCollection<Video> SearchList = new ObservableCollection<Video>();
        public static ObservableCollection<Video> ViewsVideosList = new ObservableCollection<Video>();

        public static ObservableCollection<Video> VideoByMyChannelList = new ObservableCollection<Video>();
        public static ObservableCollection<Video> VideoByChannelList = new ObservableCollection<Video>();
        public static ObservableCollection<Video> VideoByChannelUsersList = new ObservableCollection<Video>();
        public static ObservableCollection<Video> VideoByCatigoryList = new ObservableCollection<Video>();

        public static ObservableCollection<Channel> MyChannelList = new ObservableCollection<Channel>();
        public static ObservableCollection<Channel> ChannelUsersList = new ObservableCollection<Channel>();

        public static ObservableCollection<Channel> SubscriptionsChannelList = new ObservableCollection<Channel>();
        public static ObservableCollection<Subscriptions> SubscriptionsVideoesList = new ObservableCollection<Subscriptions>();

        public static ObservableCollection<LikedVideos> LikedVideosList = new ObservableCollection<LikedVideos>();
        public static ObservableCollection<HistoryVideos> HistoryVideosList = new ObservableCollection<HistoryVideos>();
        public static ObservableCollection<WatchLaterVideos> WatchLaterVideosList = new ObservableCollection<WatchLaterVideos>();
        public static ObservableCollection<DataTables.HistoryVideosTB> HistoryList = new ObservableCollection<DataTables.HistoryVideosTB>();

        public static ObservableCollection<PlayListsVideos> PlayListsVideosList = new ObservableCollection<PlayListsVideos>();
        public static ObservableCollection<PlayListsVideos.SubPlayListsVideos> SubPlayListsVideosList = new ObservableCollection<PlayListsVideos.SubPlayListsVideos>();

        //Classes 
        //*********************************************************

        [AddINotifyPropertyChangedInterface]
        public class Video
        {
            //Data video
            public string dv_id { get; set; }
            public string dv_video_id { get; set; }
            public string dv_user_id { get; set; }
            public string dv_title { get; set; }
            public string dv_description { get; set; }
            public string dv_thumbnail { get; set; }
            public string dv_video_location { get; set; }
            public string dv_youtube { get; set; }
            public string dv_vimeo { get; set; }
            public string dv_daily { get; set; }
            public string dv_time { get; set; }
            public string dv_time_date { get; set; }
            public string dv_active { get; set; }
            public string dv_tags { get; set; }
            public string dv_duration { get; set; }
            public string dv_size { get; set; }
            public string dv_category_id { get; set; }
            public string dv_views { get; set; }
            public string dv_featured { get; set; }
            public string dv_registered { get; set; }
            public string dv_org_thumbnail { get; set; }
            public string dv_video_type { get; set; }
            public string dv_video_id_ { get; set; }
            public string dv_source { get; set; }
            public string dv_url { get; set; }
            public string dv_edit_description { get; set; }
            public string dv_markup_description { get; set; }
            public string dv_is_liked { get; set; }
            public string dv_is_disliked { get; set; }
            public string dv_is_owner { get; set; }
            public string dv_time_alpha { get; set; }
            public string dv_time_ago { get; set; }
            public string dv_category_name { get; set; }
            //New Data
            public string dv_likes { get; set; }
            public string dv_dislikes { get; set; }
            public string dv_likes_percent { get; set; }
            public string dv_dislikes_percent { get; set; }
            public string dv_is_subscribed { get; set; }

            //Owner
            public string Owner_id { get; set; }
            public string Owner_username { get; set; } // Channel_Name
            public string Owner_email { get; set; }
            public string Owner_first_name { get; set; }
            public string Owner_last_name { get; set; }
            public string Owner_gender { get; set; }
            public string Owner_language { get; set; }
            public string Owner_avatar { get; set; } //Image
            public string Owner_cover { get; set; } //Image
            public string Owner_about { get; set; }
            public string Owner_google { get; set; }
            public string Owner_facebook { get; set; }
            public string Owner_twitter { get; set; }
            public string Owner_verified { get; set; }
            public string Owner_is_pro { get; set; }
            public string Owner_url { get; set; }

            //style
            public string SV_Type_video { get; set; }
            public string dv_Long_title { get; set; }
            public string dv_Long_description { get; set; }
            public string SV_BackgroundColor { get; set; }
            public string SV_TextColor { get; set; }
        }

        [AddINotifyPropertyChangedInterface]
        public class LikedVideos
        {
            //Data video
            public string dv_id { get; set; }
            public string dv_video_id { get; set; }
            public string dv_user_id { get; set; }
            public string dv_title { get; set; }
            public string dv_description { get; set; }
            public string dv_thumbnail { get; set; }
            public string dv_video_location { get; set; }
            public string dv_youtube { get; set; }
            public string dv_vimeo { get; set; }
            public string dv_daily { get; set; }
            public string dv_time { get; set; }
            public string dv_time_date { get; set; }
            public string dv_active { get; set; }
            public string dv_tags { get; set; }
            public string dv_duration { get; set; }
            public string dv_size { get; set; }
            public string dv_category_id { get; set; }
            public string dv_views { get; set; }
            public string dv_featured { get; set; }
            public string dv_registered { get; set; }
            public string dv_org_thumbnail { get; set; }
            public string dv_video_type { get; set; }
            public string dv_video_id_ { get; set; }
            public string dv_source { get; set; }
            public string dv_url { get; set; }
            public string dv_edit_description { get; set; }
            public string dv_markup_description { get; set; }
            public string dv_is_liked { get; set; }
            public string dv_is_disliked { get; set; }
            public string dv_is_owner { get; set; }
            public string dv_time_alpha { get; set; }
            public string dv_time_ago { get; set; }
            public string dv_category_name { get; set; }
            //New Data
            public string dv_likes { get; set; }
            public string dv_dislikes { get; set; }
            public string dv_likes_percent { get; set; }
            public string dv_dislikes_percent { get; set; }
            public string dv_is_subscribed { get; set; }

            //Owner
            public string Owner_id { get; set; }
            public string Owner_username { get; set; } // Channel_Name
            public string Owner_email { get; set; }
            public string Owner_first_name { get; set; }
            public string Owner_last_name { get; set; }
            public string Owner_gender { get; set; }
            public string Owner_language { get; set; }
            public string Owner_avatar { get; set; } //Image
            public string Owner_cover { get; set; } //Image
            public string Owner_about { get; set; }
            public string Owner_google { get; set; }
            public string Owner_facebook { get; set; }
            public string Owner_twitter { get; set; }
            public string Owner_verified { get; set; }
            public string Owner_is_pro { get; set; }
            public string Owner_url { get; set; }

            //style
            public string SV_Type_video { get; set; }
            public string dv_Long_title { get; set; }
            public string dv_Long_description { get; set; }
            public string SV_BackgroundColor { get; set; }
            public string SV_TextColor { get; set; }
        }

        [AddINotifyPropertyChangedInterface]
        public class HistoryVideos
        {
            //Data video
            public string dv_id { get; set; }
            public string dv_video_id { get; set; }
            public string dv_user_id { get; set; }
            public string dv_title { get; set; }
            public string dv_description { get; set; }
            public string dv_thumbnail { get; set; }
            public string dv_video_location { get; set; }
            public string dv_youtube { get; set; }
            public string dv_vimeo { get; set; }
            public string dv_daily { get; set; }
            public string dv_time { get; set; }
            public string dv_time_date { get; set; }
            public string dv_active { get; set; }
            public string dv_tags { get; set; }
            public string dv_duration { get; set; }
            public string dv_size { get; set; }
            public string dv_category_id { get; set; }
            public string dv_views { get; set; }
            public string dv_featured { get; set; }
            public string dv_registered { get; set; }
            public string dv_org_thumbnail { get; set; }
            public string dv_video_type { get; set; }
            public string dv_video_id_ { get; set; }
            public string dv_source { get; set; }
            public string dv_url { get; set; }
            public string dv_edit_description { get; set; }
            public string dv_markup_description { get; set; }
            public string dv_is_liked { get; set; }
            public string dv_is_disliked { get; set; }
            public string dv_is_owner { get; set; }
            public string dv_time_alpha { get; set; }
            public string dv_time_ago { get; set; }
            public string dv_category_name { get; set; }
            //New Data
            public string dv_likes { get; set; }
            public string dv_dislikes { get; set; }
            public string dv_likes_percent { get; set; }
            public string dv_dislikes_percent { get; set; }
            public string dv_is_subscribed { get; set; }

            //Owner
            public string Owner_id { get; set; }
            public string Owner_username { get; set; } // Channel_Name
            public string Owner_email { get; set; }
            public string Owner_first_name { get; set; }
            public string Owner_last_name { get; set; }
            public string Owner_gender { get; set; }
            public string Owner_language { get; set; }
            public string Owner_avatar { get; set; } //Image
            public string Owner_cover { get; set; } //Image
            public string Owner_about { get; set; }
            public string Owner_google { get; set; }
            public string Owner_facebook { get; set; }
            public string Owner_twitter { get; set; }
            public string Owner_verified { get; set; }
            public string Owner_is_pro { get; set; }
            public string Owner_url { get; set; }

            //style
            public string SV_Type_video { get; set; }
            public string dv_Long_title { get; set; }
            public string dv_Long_description { get; set; }
            public string SV_BackgroundColor { get; set; }
            public string SV_TextColor { get; set; }
        }

        [AddINotifyPropertyChangedInterface]
        public class PlayListsVideos
        {
            //Data PlayLists
            public string dp_id { get; set; }
            public string dp_list_id { get; set; }
            public string dp_user_id { get; set; }
            public string dp_name { get; set; }
            public string dp_description { get; set; }
            public string dp_privacy { get; set; }
            public string dp_views { get; set; }
            public string dp_icon { get; set; }
            public string dp_time { get; set; }

            //style
            public string dp_totalSubVideo { get; set; }
            public string dp_totalSubVideo_String { get; set; }
            public string dp_Image { get; set; }
            public string dp_BackgroundColor { get; set; }
            public string dp_TextColor { get; set; }


            public class SubPlayListsVideos
            {
                //Data Video
                public string dv_id { get; set; }
                public string dv_video_id { get; set; }
                public string dv_user_id { get; set; }
                public string dv_title { get; set; }
                public string dv_thumbnail { get; set; }
                public string dv_time { get; set; }
                public string dv_time_date { get; set; }
                public string dv_duration { get; set; }
                public string dv_views { get; set; }
                public string dv_url { get; set; }

                //Owner
                public string Owner_id { get; set; }
                public string Owner_username { get; set; } // Channel_Name
                public string Owner_email { get; set; }
                public string Owner_first_name { get; set; }
                public string Owner_last_name { get; set; }
                public string Owner_gender { get; set; }
                public string Owner_language { get; set; }
                public string Owner_avatar { get; set; } //Image
                public string Owner_cover { get; set; } //Image
                public string Owner_about { get; set; }
                public string Owner_google { get; set; }
                public string Owner_facebook { get; set; }
                public string Owner_twitter { get; set; }
                public string Owner_verified { get; set; }
                public string Owner_is_pro { get; set; }
                public string Owner_url { get; set; }

                //style
                public string dv_Long_title { get; set; }
                public string SV_Type_video { get; set; }
                public string SV_BackgroundColor { get; set; }
                public string SV_TextColor { get; set; }
            }
        }

        [AddINotifyPropertyChangedInterface]
        public class WatchLaterVideos
        {
            //Data video
            public string dv_id { get; set; }
            public string dv_video_id { get; set; }
            public string dv_user_id { get; set; }
            public string dv_title { get; set; }
            public string dv_description { get; set; }
            public string dv_thumbnail { get; set; }
            public string dv_video_location { get; set; }
            public string dv_youtube { get; set; }
            public string dv_vimeo { get; set; }
            public string dv_daily { get; set; }
            public string dv_time { get; set; }
            public string dv_time_date { get; set; }
            public string dv_active { get; set; }
            public string dv_tags { get; set; }
            public string dv_duration { get; set; }
            public string dv_size { get; set; }
            public string dv_category_id { get; set; }
            public string dv_views { get; set; }
            public string dv_featured { get; set; }
            public string dv_registered { get; set; }
            public string dv_org_thumbnail { get; set; }
            public string dv_video_type { get; set; }
            public string dv_video_id_ { get; set; }
            public string dv_source { get; set; }
            public string dv_url { get; set; }
            public string dv_edit_description { get; set; }
            public string dv_markup_description { get; set; }
            public string dv_is_liked { get; set; }
            public string dv_is_disliked { get; set; }
            public string dv_is_owner { get; set; }
            public string dv_time_alpha { get; set; }
            public string dv_time_ago { get; set; }
            public string dv_category_name { get; set; }
            //New Data
            public string dv_likes { get; set; }
            public string dv_dislikes { get; set; }
            public string dv_likes_percent { get; set; }
            public string dv_dislikes_percent { get; set; }
            public string dv_is_subscribed { get; set; }

            //Owner
            public string Owner_id { get; set; }
            public string Owner_username { get; set; } // Channel_Name
            public string Owner_email { get; set; }
            public string Owner_first_name { get; set; }
            public string Owner_last_name { get; set; }
            public string Owner_gender { get; set; }
            public string Owner_language { get; set; }
            public string Owner_avatar { get; set; } //Image
            public string Owner_cover { get; set; } //Image
            public string Owner_about { get; set; }
            public string Owner_google { get; set; }
            public string Owner_facebook { get; set; }
            public string Owner_twitter { get; set; }
            public string Owner_verified { get; set; }
            public string Owner_is_pro { get; set; }
            public string Owner_url { get; set; }

            //style
            public string SV_Type_video { get; set; }
            public string dv_Long_title { get; set; }
            public string dv_Long_description { get; set; }
            public string SV_BackgroundColor { get; set; }
            public string SV_TextColor { get; set; }

        }

        [AddINotifyPropertyChangedInterface]
        public class Catigory
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Icon { get; set; }
            public string BackgroundColor { get; set; }
            public string Image { get; set; }
        }

        [AddINotifyPropertyChangedInterface]
        public class Channel
        {
            public string Channel_id { get; set; } //userId
            public string Channel_username { get; set; }
            public string Channel_email { get; set; }
            public string Channel_first_name { get; set; }
            public string Channel_last_name { get; set; }
            public string Channel_gender { get; set; }
            public string Channel_language { get; set; }
            public string Channel_avatar { get; set; }
            public string Channel_cover { get; set; }
            public string Channel_about { get; set; }
            public string Channel_google { get; set; }
            public string Channel_facebook { get; set; }
            public string Channel_twitter { get; set; }
            public string Channel_verified { get; set; }
            public string Channel_is_pro { get; set; }
            public string Channel_url { get; set; }

            //style
            public string SC_BackgroundColor { get; set; }
            public string SC_TextColor { get; set; }
        }

        [AddINotifyPropertyChangedInterface]
        public class Subscriptions
        {
            //Data video
            public string dv_id { get; set; }
            public string dv_video_id { get; set; }
            public string dv_user_id { get; set; }
            public string dv_title { get; set; }
            public string dv_description { get; set; }
            public string dv_thumbnail { get; set; }
            public string dv_video_location { get; set; }
            public string dv_youtube { get; set; }
            public string dv_vimeo { get; set; }
            public string dv_daily { get; set; }
            public string dv_time { get; set; }
            public string dv_time_date { get; set; }
            public string dv_active { get; set; }
            public string dv_tags { get; set; }
            public string dv_duration { get; set; }
            public string dv_size { get; set; }
            public string dv_category_id { get; set; }
            public string dv_views { get; set; }
            public string dv_featured { get; set; }
            public string dv_registered { get; set; }
            public string dv_org_thumbnail { get; set; }
            public string dv_video_type { get; set; }
            public string dv_video_id_ { get; set; }
            public string dv_source { get; set; }
            public string dv_url { get; set; }
            public string dv_edit_description { get; set; }
            public string dv_markup_description { get; set; }
            public string dv_is_liked { get; set; }
            public string dv_is_disliked { get; set; }
            public string dv_is_owner { get; set; }
            public string dv_time_alpha { get; set; }
            public string dv_time_ago { get; set; }
            public string dv_category_name { get; set; }
            //New Data
            public string dv_likes { get; set; }
            public string dv_dislikes { get; set; }
            public string dv_likes_percent { get; set; }
            public string dv_dislikes_percent { get; set; }
            public string dv_is_subscribed { get; set; }


            //Owner
            public string Owner_id { get; set; }
            public string Owner_username { get; set; } // Channel_Name
            public string Owner_email { get; set; }
            public string Owner_first_name { get; set; }
            public string Owner_last_name { get; set; }
            public string Owner_gender { get; set; }
            public string Owner_language { get; set; }
            public string Owner_avatar { get; set; } //Image
            public string Owner_cover { get; set; } //Image
            public string Owner_about { get; set; }
            public string Owner_google { get; set; }
            public string Owner_facebook { get; set; }
            public string Owner_twitter { get; set; }
            public string Owner_verified { get; set; }
            public string Owner_is_pro { get; set; }
            public string Owner_url { get; set; }

            //style
            public string SV_Type_video { get; set; }
            public string dv_Long_title { get; set; }
            public string dv_Long_description { get; set; }
        }

        [AddINotifyPropertyChangedInterface]
        public class PageItems
        {
            public string Name_page { get; set; }
            public string Icon_page { get; set; }
            public string BackgroundColor { get; set; }
            public string TextColor { get; set; }

        }

        [AddINotifyPropertyChangedInterface]
        public class SeconderyPageItems
        {
            public string Name_page { get; set; }
            public string Icon_page { get; set; }
            public string BackgroundColor { get; set; }
            public string TextColor { get; set; }
        }

        [AddINotifyPropertyChangedInterface]
        public class Channelprofiletems
        {
            public string Label { get; set; }
            public string Icon { get; set; }
            public string Color { get; set; }
            public string TextColor { get; set; }
        }

        public class MySettings
        {
            public string theme { get; set; }
            public string censored_words { get; set; }
            public string title { get; set; }
            public string name { get; set; }
            public string keyword { get; set; }
            public string email { get; set; }
            public string description { get; set; }
            public string validation { get; set; }
            public string recaptcha { get; set; }
            public string language { get; set; }
            public string seo_link { get; set; }
            public string comment_system { get; set; }
            public string delete_account { get; set; }
            public string total_videos { get; set; }
            public string total_views { get; set; }
            public string total_users { get; set; }
            public string total_subs { get; set; }
            public string total_comments { get; set; }
            public string total_likes { get; set; }
            public string total_dislikes { get; set; }
            public string total_saved { get; set; }
            public string upload_system { get; set; }
            public string import_system { get; set; }
            public string autoplay_system { get; set; }
            public string history_system { get; set; }
            public string user_registration { get; set; }
            public string verification_badge { get; set; }
            public string comments_default_num { get; set; }
            public string fb_login { get; set; }
            public string tw_login { get; set; }
            public string plus_login { get; set; }
            public string go_pro { get; set; }
            public string user_ads { get; set; }
            public string max_upload { get; set; }
            public string theme_url { get; set; }
            public string site_url { get; set; }
            public string script_version { get; set; }
        }
    }
}
