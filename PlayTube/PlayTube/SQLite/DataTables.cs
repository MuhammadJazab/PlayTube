using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;
using Xamarin.Forms;

namespace PlayTube.SQLite
{
    public class DataTables
    {
        public class LoginTB
        {
            [PrimaryKey, AutoIncrement]
            public int ID { get; set; }

            public string UserID { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public string Session { get; set; }
            public string Cookie { get; set; }
            public string Company_id { get; set; }
            public string Email { get; set; }
            public string Status { get; set; }
        }

        public class CatigoriesTB
        {
            [PrimaryKey, AutoIncrement]
            public int ID { get; set; }

            public string Catigories_Id { get; set; }
            public string Catigories_Name { get; set; }
        }

        public class ChannelTB
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
        }

        public class MySettingsTB
        {
            [PrimaryKey, AutoIncrement]
            public int ID { get; set; }

            public string S_theme { get; set; }
            public string S_censored_words { get; set; }
            public string S_title { get; set; }
            public string S_name { get; set; }
            public string S_keyword { get; set; }
            public string S_email { get; set; }
            public string S_description { get; set; }
            public string S_validation { get; set; }
            public string S_recaptcha { get; set; }
            public string S_language { get; set; }
            public string S_seo_link { get; set; }
            public string S_comment_system { get; set; }
            public string S_delete_account { get; set; }
            public string S_total_videos { get; set; }
            public string S_total_views { get; set; }
            public string S_total_users { get; set; }
            public string S_total_subs { get; set; }
            public string S_total_comments { get; set; }
            public string S_total_likes { get; set; }
            public string S_total_dislikes { get; set; }
            public string S_total_saved { get; set; }
            public string S_upload_system { get; set; }
            public string S_import_system { get; set; }
            public string S_autoplay_system { get; set; }
            public string S_history_system { get; set; }
            public string S_user_registration { get; set; }
            public string S_verification_badge { get; set; }
            public string S_comments_default_num { get; set; }
            public string S_fb_login { get; set; }
            public string S_tw_login { get; set; }
            public string S_plus_login { get; set; }
            public string S_go_pro { get; set; }
            public string S_user_ads { get; set; }
            public string S_max_upload { get; set; }
            public string S_theme_url { get; set; }
            public string S_site_url { get; set; }
            public string S_script_version { get; set; }
            //style
            public string S_AddShortcutIcon { get; set; }

        }

        public class LikedVideosTB
        {
            [PrimaryKey, AutoIncrement]
            public int ID { get; set; }

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

        public class HistoryVideosTB
        {
            [PrimaryKey, AutoIncrement]
            public int ID { get; set; }

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

        public class PlayListsVideosTB
        {
            [PrimaryKey, AutoIncrement]
            public int ID { get; set; }

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
            public string dv_Long_description { get; set; }
            public string SV_Type_video { get; set; }
            public string SV_BackgroundColor { get; set; }
            public string SV_TextColor { get; set; }
        }

        public class WatchLaterVideosTB
        {
            [PrimaryKey, AutoIncrement]
            public int ID { get; set; }

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

        public class SubscriptionsChannelTB
        {
            [PrimaryKey, AutoIncrement]
            public int ID { get; set; }

            //Owner
            public string Owner_id { get; set; }
            public string Owner_username { get; set; } // Channel_Name
            public string Owner_email { get; set; }
            public string Owner_first_name { get; set; }
            public string Owner_last_name { get; set; }
            public string Owner_gender { get; set; }
            public string Owner_language { get; set; }
            public ImageSource Owner_avatar { get; set; } //Image
            public ImageSource Owner_cover { get; set; } //Image
            public string Owner_about { get; set; }
            public string Owner_google { get; set; }
            public string Owner_facebook { get; set; }
            public string Owner_twitter { get; set; }
            public string Owner_verified { get; set; }
            public string Owner_is_pro { get; set; }
            public string Owner_url { get; set; }
        }

    }
}