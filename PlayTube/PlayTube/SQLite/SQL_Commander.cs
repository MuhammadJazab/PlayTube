using System;
using System.Collections.ObjectModel;
using System.Linq;
using PlayTube.Controls;
using PlayTube.Languish;
using PlayTube.Pages.Default;
using PlayTube.Pages.Tabbes;

namespace PlayTube.SQLite
{
    public class SQL_Commander
    {
        //Insert data Settings
        public static void Insert_Settings_Credentials(DataTables.MySettingsTB Data)
        {
            try
            {
                SQL_Entity.Connection.Insert(Data);
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        //Get data setting 
        public static string Get_Settings()
        {
            try
            {
                var data = SQL_Entity.Connection.Table<DataTables.MySettingsTB>().FirstOrDefault();
                if (data != null)
                {
                    Classes.MySettings ms = new Classes.MySettings();

                    ms.theme = data.S_theme;
                    ms.censored_words = data.S_censored_words;
                    ms.title = data.S_title;
                    ms.name = data.S_name;
                    ms.keyword = data.S_keyword;
                    ms.email = data.S_email;
                    ms.description = data.S_description;
                    ms.validation = data.S_validation;
                    ms.recaptcha = data.S_recaptcha;
                    ms.language = data.S_language;
                    ms.seo_link = data.S_seo_link;
                    ms.comment_system = data.S_comment_system;
                    ms.delete_account = data.S_delete_account;
                    ms.total_videos = data.S_total_videos;
                    ms.total_views = data.S_total_views;
                    ms.total_users = data.S_total_users;
                    ms.total_subs = data.S_total_subs;
                    ms.total_comments = data.S_total_comments;
                    ms.total_likes = data.S_total_likes;
                    ms.total_dislikes = data.S_total_dislikes;
                    ms.total_saved = data.S_total_saved;
                    ms.upload_system = data.S_upload_system;
                    ms.import_system = data.S_import_system;
                    ms.autoplay_system = data.S_autoplay_system;
                    ms.history_system = data.S_history_system;
                    ms.user_registration = data.S_user_registration;
                    ms.verification_badge = data.S_verification_badge;
                    ms.comments_default_num = data.S_comments_default_num;
                    ms.fb_login = data.S_fb_login;
                    ms.tw_login = data.S_tw_login;
                    ms.plus_login = data.S_plus_login;
                    ms.go_pro = data.S_go_pro;
                    ms.user_ads = data.S_user_ads;
                    ms.max_upload = data.S_max_upload;
                    ms.theme_url = data.S_theme_url;
                    ms.site_url = data.S_site_url;
                    ms.script_version = data.S_script_version;

                    API_Request.Lang = data.S_language;

                    if (Classes.MySettingsList.Count == 0)
                    {
                        Classes.MySettingsList.Add(ms);
                    }

                    Get_CatigoriesList_Credentials();

                    Get_data_Login_Credentials();

                    if (Hamburg_Page.IsLogin)
                    {
                        Get_DataMyChanne_Credentials();
                    }

                    return "true";
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
                return "false";
            }
            return "false";
        }

        //Update data Settings
        public static void Update_Settings_Credentials(DataTables.MySettingsTB credentials)
        {
            try
            {
                var data = SQL_Entity.Connection.Table<DataTables.MySettingsTB>().FirstOrDefault();
                if (data != null)
                {
                    if (data.S_theme != credentials.S_theme)
                    {
                        data.S_theme = credentials.S_theme;
                    }
                    if (data.S_censored_words != credentials.S_censored_words)
                    {
                        data.S_censored_words = credentials.S_censored_words;
                    }
                    if (data.S_title != credentials.S_title)
                    {
                        data.S_title = credentials.S_title;
                    }
                    if (data.S_name != credentials.S_name)
                    {
                        data.S_name = credentials.S_name;
                    }
                    if (data.S_keyword != credentials.S_keyword)
                    {
                        data.S_keyword = credentials.S_keyword;
                    }
                    if (data.S_email != credentials.S_email)
                    {
                        data.S_email = credentials.S_email;
                    }
                    if (data.S_description != credentials.S_description)
                    {
                        data.S_description = credentials.S_description;
                    }
                    if (data.S_validation != credentials.S_validation)
                    {
                        data.S_validation = credentials.S_validation;
                    }
                    if (data.S_recaptcha != credentials.S_recaptcha)
                    {
                        data.S_recaptcha = credentials.S_recaptcha;
                    }
                    if (data.S_language != credentials.S_language)
                    {
                        data.S_language = credentials.S_language;
                    }
                    if (data.S_seo_link != credentials.S_seo_link)
                    {
                        data.S_seo_link = credentials.S_seo_link;
                    }
                    if (data.S_comment_system != credentials.S_comment_system)
                    {
                        data.S_comment_system = credentials.S_comment_system;
                    }
                    if (data.S_delete_account != credentials.S_delete_account)
                    {
                        data.S_delete_account = credentials.S_delete_account;
                    }
                    if (data.S_total_videos != credentials.S_total_videos)
                    {
                        data.S_total_videos = credentials.S_total_videos;
                    }
                    if (data.S_total_views != credentials.S_total_views)
                    {
                        data.S_total_views = credentials.S_total_views;
                    }
                    if (data.S_total_users != credentials.S_total_users)
                    {
                        data.S_total_users = credentials.S_total_users;
                    }
                    if (data.S_total_subs != credentials.S_total_subs)
                    {
                        data.S_total_subs = credentials.S_total_subs;
                    }
                    if (data.S_total_comments != credentials.S_total_comments)
                    {
                        data.S_total_comments = credentials.S_total_comments;
                    }
                    if (data.S_total_likes != credentials.S_total_likes)
                    {
                        data.S_total_likes = credentials.S_total_likes;
                    }
                    if (data.S_total_dislikes != credentials.S_total_dislikes)
                    {
                        data.S_total_dislikes = credentials.S_total_dislikes;
                    }
                    if (data.S_total_saved != credentials.S_total_saved)
                    {
                        data.S_total_saved = credentials.S_total_saved;
                    }
                    if (data.S_upload_system != credentials.S_upload_system)
                    {
                        data.S_upload_system = credentials.S_upload_system;
                    }
                    if (data.S_import_system != credentials.S_import_system)
                    {
                        data.S_import_system = credentials.S_import_system;
                    }
                    if (data.S_autoplay_system != credentials.S_autoplay_system)
                    {
                        data.S_autoplay_system = credentials.S_autoplay_system;
                    }
                    if (data.S_history_system != credentials.S_history_system)
                    {
                        data.S_history_system = credentials.S_history_system;
                    }
                    if (data.S_user_registration != credentials.S_user_registration)
                    {
                        data.S_user_registration = credentials.S_user_registration;
                    }
                    if (data.S_verification_badge != credentials.S_verification_badge)
                    {
                        data.S_verification_badge = credentials.S_verification_badge;
                    }
                    if (data.S_comments_default_num != credentials.S_comments_default_num)
                    {
                        data.S_comments_default_num = credentials.S_comments_default_num;
                    }
                    if (data.S_fb_login != credentials.S_fb_login)
                    {
                        data.S_fb_login = credentials.S_fb_login;
                    }
                    if (data.S_tw_login != credentials.S_tw_login)
                    {
                        data.S_tw_login = credentials.S_tw_login;
                    }
                    if (data.S_plus_login != credentials.S_plus_login)
                    {
                        data.S_plus_login = credentials.S_plus_login;
                    }
                    if (data.S_go_pro != credentials.S_go_pro)
                    {
                        data.S_go_pro = credentials.S_go_pro;
                    }
                    if (data.S_user_ads != credentials.S_user_ads)
                    {
                        data.S_user_ads = credentials.S_user_ads;
                    }
                    if (data.S_max_upload != credentials.S_max_upload)
                    {
                        data.S_max_upload = credentials.S_max_upload;
                    }
                    if (data.S_theme_url != credentials.S_theme_url)
                    {
                        data.S_theme_url = credentials.S_theme_url;
                    }
                    if (data.S_site_url != credentials.S_site_url)
                    {
                        data.S_site_url = credentials.S_site_url;
                    }
                    if (data.S_script_version != credentials.S_script_version)
                    {
                        data.S_script_version = credentials.S_script_version;
                    }
                    SQL_Entity.Connection.Update(credentials);
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        //Insert data Catigories
        public static void Insert_Catigories_Credentials(ObservableCollection<DataTables.CatigoriesTB> List_Data)
        {
            try
            {
                SQL_Entity.Connection.InsertAll(List_Data);
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        //Get List Catigories 
        public static void Get_CatigoriesList_Credentials()
        {
            try
            {
                if (Classes.CatigoryList.Count == 0)
                {
                    var Result = SQL_Entity.Connection.Table<DataTables.CatigoriesTB>().ToList();
                    if (Result != null)
                    {
                        foreach (var item in Result)
                        {
                            Classes.Catigory cat = new Classes.Catigory();
                            cat.Id = item.Catigories_Id;
                            cat.Name = item.Catigories_Name;

                            if (API_Request.Lang == "english")
                            {
                                if (cat.Name.Contains("Film & Animation"))
                                {
                                    cat.Icon = IoniciconsFont.FilmMarker;
                                    cat.Image = "Cat_Film.jpg";
                                    cat.BackgroundColor = "#44bec7";
                                }
                                else if (cat.Name.Contains("Cars & Vehicles"))
                                {
                                    cat.Icon = IoniciconsFont.ModelS;
                                    cat.Image = "Cat_Cars.jpg";
                                    cat.BackgroundColor = "#ffc300";
                                }
                                else if (cat.Name.Contains("Music"))
                                {
                                    cat.Icon = IoniciconsFont.MusicNote;
                                    cat.Image = "Cat_Music.jpg";
                                    cat.BackgroundColor = "#fa3c4c";
                                }
                                else if (cat.Name.Contains("Pets & Animals"))
                                {
                                    cat.Icon = IoniciconsFont.SocialTux;
                                    cat.Image = "Cat_Animals.jpg";
                                    cat.BackgroundColor = "#d696bb";
                                }
                                else if (cat.Name.Contains("Sports"))
                                {
                                    cat.Icon = IoniciconsFont.IosBody;
                                    cat.Image = "Cat_Sport.jpg";
                                    cat.BackgroundColor = "#6699cc";
                                }
                                else if (cat.Name.Contains("Travel & Events"))
                                {
                                    cat.Icon = IoniciconsFont.IosSpeedometer;
                                    cat.Image = "Cat_Travel.jpg";
                                    cat.BackgroundColor = "#67b868";
                                }
                                else if (cat.Name.Contains("Gaming"))
                                {
                                    cat.Icon = IoniciconsFont.Bug;
                                    cat.Image = "Cat_Gaming.jpg";
                                    cat.BackgroundColor = "#331489";
                                }
                                else if (cat.Name.Contains("People & Blogs"))
                                {
                                    cat.Icon = IoniciconsFont.IosPeople;
                                    cat.Image = "Cat_People.jpg";
                                    cat.BackgroundColor = "#a84849";
                                }
                                else if (cat.Name.Contains("Comedy"))
                                {
                                    cat.Icon = IoniciconsFont.SocialReddit;
                                    cat.Image = "Cat_Comedy.jpg";
                                    cat.BackgroundColor = "#d4a88c";
                                }
                                else if (cat.Name.Contains("Entertainment"))
                                {
                                    cat.Icon = IoniciconsFont.Star;
                                    cat.Image = "Cat_Entertainment.jpg";
                                    cat.BackgroundColor = "#86a65b";
                                }
                                else if (cat.Name.Contains("News & Politics"))
                                {
                                    cat.Icon = IoniciconsFont.DocumentText;
                                    cat.Image = "Cat_News.jpg";
                                    cat.BackgroundColor = "#20cef5";
                                }
                                else if (cat.Name.Contains("How-to & Style"))
                                {
                                    cat.Icon = IoniciconsFont.HelpCircled;
                                    cat.Image = "Cat_Help.jpg";
                                    cat.BackgroundColor = "#732249";
                                }
                                else if (cat.Name.Contains("Non-profits & Activism"))
                                {
                                    cat.Icon = IoniciconsFont.AndroidVolumeUp;
                                    cat.Image = "Cat_Activism.jpg";
                                    cat.BackgroundColor = "#b68268";
                                }
                                else
                                {
                                    cat.Icon = IoniciconsFont.AndroidFilm;
                                    cat.Image = "Cat_Other.jpg";
                                    cat.BackgroundColor = Functions.RandomColor();
                                }
                            }
                            else
                            {
                                if (cat.Name.Contains(AppResources.Label_Film_Animation))
                                {
                                    cat.Icon = IoniciconsFont.FilmMarker;
                                    cat.Image = "Cat_Film.jpg";
                                    cat.BackgroundColor = "#44bec7";
                                }
                                else if (cat.Name.Contains(AppResources.Label_Cars_Vehicles))
                                {
                                    cat.Icon = IoniciconsFont.ModelS;
                                    cat.Image = "Cat_Cars.jpg";
                                    cat.BackgroundColor = "#ffc300";
                                }
                                else if (cat.Name.Contains(AppResources.Label_Music))
                                {
                                    cat.Icon = IoniciconsFont.MusicNote;
                                    cat.Image = "Cat_Music.jpg";
                                    cat.BackgroundColor = "#fa3c4c";
                                }
                                else if (cat.Name.Contains(AppResources.Label_Pets_Animals))
                                {
                                    cat.Icon = IoniciconsFont.SocialTux;
                                    cat.Image = "Cat_Animals.jpg";
                                    cat.BackgroundColor = "#d696bb";
                                }
                                else if (cat.Name.Contains(AppResources.Label_Sports))
                                {
                                    cat.Icon = IoniciconsFont.IosBody;
                                    cat.Image = "Cat_Sport.jpg";
                                    cat.BackgroundColor = "#6699cc";
                                }
                                else if (cat.Name.Contains(AppResources.Label_Travel_Events))
                                {
                                    cat.Icon = IoniciconsFont.IosSpeedometer;
                                    cat.Image = "Cat_Travel.jpg";
                                    cat.BackgroundColor = "#67b868";
                                }
                                else if (cat.Name.Contains(AppResources.Label_Gaming))
                                {
                                    cat.Icon = IoniciconsFont.Bug;
                                    cat.Image = "Cat_Gaming.jpg";
                                    cat.BackgroundColor = "#331489";
                                }
                                else if (cat.Name.Contains(AppResources.Label_People_Blogs))
                                {
                                    cat.Icon = IoniciconsFont.IosPeople;
                                    cat.Image = "Cat_People.jpg";
                                    cat.BackgroundColor = "#a84849";
                                }
                                else if (cat.Name.Contains(AppResources.Label_Comedy))
                                {
                                    cat.Icon = IoniciconsFont.SocialReddit;
                                    cat.Image = "Cat_Comedy.jpg";
                                    cat.BackgroundColor = "#d4a88c";
                                }
                                else if (cat.Name.Contains(AppResources.Label_Entertainment))
                                {
                                    cat.Icon = IoniciconsFont.Star;
                                    cat.Image = "Cat_Entertainment.jpg";
                                    cat.BackgroundColor = "#86a65b";
                                }
                                else if (cat.Name.Contains(AppResources.Label_News_Politics))
                                {
                                    cat.Icon = IoniciconsFont.DocumentText;
                                    cat.Image = "Cat_News.jpg";
                                    cat.BackgroundColor = "#20cef5";
                                }
                                else if (cat.Name.Contains(AppResources.Label_How_to_Style))
                                {
                                    cat.Icon = IoniciconsFont.HelpCircled;
                                    cat.Image = "Cat_Help.jpg";
                                    cat.BackgroundColor = "#732249";
                                }
                                else if (cat.Name.Contains(AppResources.Label_Non_profits_Activism))
                                {
                                    cat.Icon = IoniciconsFont.AndroidVolumeUp;
                                    cat.Image = "Cat_Activism.jpg";
                                    cat.BackgroundColor = "#b68268";
                                }
                                else
                                {
                                    cat.Icon = IoniciconsFont.AndroidFilm;
                                    cat.Image = "Cat_Other.jpg";
                                    cat.BackgroundColor = Functions.RandomColor();
                                }
                            }

                            Classes.CatigoryList.Add(cat);
                        }
                    }
                }
           }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        //Insert data Login
        public static void Insert_Login_Credentials(DataTables.LoginTB Data)
        {
            try
            {
                SQL_Entity.Connection.Insert(Data);
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        //Get data Login
        public static bool Get_data_Login_Credentials()
        {
            var Result = SQL_Entity.Connection.Table<DataTables.LoginTB>().FirstOrDefault();
            if (Result != null)
            {
                API_Request.Session = Result.Session;
                API_Request.User_id = Result.UserID;
                API_Request.Username = Result.Username;
                API_Request.Email = Result.Email;
                API_Request.Status = Result.Status;
                API_Request.Cookie = Result.Cookie;
                Hamburg_Page.IsLogin = true;
                return true;
            }
            else
            {
                return false;
            }
        }

        //Delete data Login
        public static void Delete_Login_Credentials()
        {
            try
            {
                var Data = SQL_Entity.Connection.Table<DataTables.LoginTB>().FirstOrDefault(c => c.Status == "Active");
                if (Data != null)
                {
                    SQL_Entity.Connection.Delete(Data);
                    Delete_DataMyChanne_Credentials();
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        //Insert Or Update data MyChanne
        public static void InsertOrUpdate_DataMyChanne_Credentials(Classes.Channel credentials)
        {
            try
            {
                var data = SQL_Entity.Connection.Table<DataTables.ChannelTB>().FirstOrDefault();
                if (data != null)
                {
                    if (data.Channel_id != credentials.Channel_id)
                    {
                        data.Channel_id = credentials.Channel_id;
                    }
                    if (data.Channel_username != credentials.Channel_username)
                    {
                        data.Channel_username = credentials.Channel_username;
                    }
                    if (data.Channel_email != credentials.Channel_email)
                    {
                        data.Channel_email = credentials.Channel_email;
                    }
                    if (data.Channel_first_name != credentials.Channel_first_name)
                    {
                        data.Channel_first_name = credentials.Channel_first_name;
                    }
                    if (data.Channel_last_name != credentials.Channel_last_name)
                    {
                        data.Channel_last_name = credentials.Channel_last_name;
                    }
                    if (data.Channel_gender != credentials.Channel_gender)
                    {
                        data.Channel_gender = credentials.Channel_gender;
                    }
                    if (data.Channel_language != credentials.Channel_language)
                    {
                        data.Channel_language = credentials.Channel_language;
                    }
                    if (data.Channel_avatar != credentials.Channel_avatar)
                    {
                        data.Channel_avatar = credentials.Channel_avatar;
                    }
                    if (data.Channel_cover != credentials.Channel_cover)
                    {
                        data.Channel_cover = credentials.Channel_cover;
                    }
                    if (data.Channel_about != credentials.Channel_about)
                    {
                        data.Channel_about = credentials.Channel_about;
                    }
                    if (data.Channel_google != credentials.Channel_google)
                    {
                        data.Channel_google = credentials.Channel_google;
                    }
                    if (data.Channel_facebook != credentials.Channel_facebook)
                    {
                        data.Channel_facebook = credentials.Channel_facebook;
                    }
                    if (data.Channel_twitter != credentials.Channel_twitter)
                    {
                        data.Channel_twitter = credentials.Channel_twitter;
                    }
                    if (data.Channel_verified != credentials.Channel_verified)
                    {
                        data.Channel_verified = credentials.Channel_verified;
                    }
                    if (data.Channel_is_pro != credentials.Channel_is_pro)
                    {
                        data.Channel_is_pro = credentials.Channel_is_pro;
                    }
                    if (data.Channel_url != credentials.Channel_url)
                    {
                        data.Channel_url = credentials.Channel_url;
                    }
                    SQL_Entity.Connection.Update(credentials);
                }
                else
                {
                    DataTables.ChannelTB ch = new DataTables.ChannelTB();

                    ch.Channel_id = credentials.Channel_id;
                    ch.Channel_username = credentials.Channel_username;
                    ch.Channel_email = credentials.Channel_email;
                    ch.Channel_first_name = credentials.Channel_first_name;
                    ch.Channel_last_name = credentials.Channel_last_name;
                    ch.Channel_gender = credentials.Channel_gender;
                    ch.Channel_language = credentials.Channel_language;
                    ch.Channel_avatar = credentials.Channel_avatar;
                    ch.Channel_cover = credentials.Channel_cover;
                    ch.Channel_about = credentials.Channel_about;
                    ch.Channel_google = credentials.Channel_google;
                    ch.Channel_facebook = credentials.Channel_facebook;
                    ch.Channel_twitter = credentials.Channel_twitter;
                    ch.Channel_verified = credentials.Channel_verified;
                    ch.Channel_is_pro = credentials.Channel_is_pro;
                    ch.Channel_url = credentials.Channel_url;

                    SQL_Entity.Connection.Insert(ch);
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        //Get data MyChanne
        public static string Get_DataMyChanne_Credentials()
        {
            try
            {
                var Result = SQL_Entity.Connection.Table<DataTables.ChannelTB>().FirstOrDefault(a => a.Channel_id == API_Request.User_id);
                if (Result != null)
                {
                    Classes.Channel ch = new Classes.Channel();

                    ch.Channel_id = Result.Channel_id;
                    ch.Channel_username = Result.Channel_username;
                    ch.Channel_email = Result.Channel_email;
                    ch.Channel_first_name = Result.Channel_first_name;
                    ch.Channel_last_name = Result.Channel_last_name;
                    ch.Channel_gender = Result.Channel_gender;
                    ch.Channel_language = Result.Channel_language;
                    ch.Channel_avatar = Result.Channel_avatar;
                    ch.Channel_cover = Result.Channel_cover;
                    ch.Channel_about = Result.Channel_about;
                    ch.Channel_google = Result.Channel_google;
                    ch.Channel_facebook = Result.Channel_facebook;
                    ch.Channel_twitter = Result.Channel_twitter;
                    ch.Channel_verified = Result.Channel_verified;
                    ch.Channel_is_pro = Result.Channel_is_pro;
                    ch.Channel_url = Result.Channel_url;

                    MyChannel_Page.Avatar = Result.Channel_avatar;
                    Classes.MyChannelList.Add(ch);

                    return "true";
                }
                else
                {
                    return "false";
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
                return "false";
            }
        }

        //Delete data MyChanne
        public static void Delete_DataMyChanne_Credentials()
        {
            try
            {
                var Data = SQL_Entity.Connection.Table<DataTables.ChannelTB>().ToList();
                if (Data.Count > 0)
                {
                    SQL_Entity.Connection.DeleteAll<DataTables.ChannelTB>();
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        //Insert Liked Videos
        public static void Insert_LikedVideos_Credentials(Classes.Video Video)
        {
            try
            {
                if (Video != null)
                {
                    var select = SQL_Entity.Connection.Table<DataTables.LikedVideosTB>().FirstOrDefault(a => a.dv_video_id == Video.dv_video_id);
                    if (select == null)
                    {
                        DataTables.LikedVideosTB Liked = new DataTables.LikedVideosTB();

                        Liked.SV_Type_video = "Liked";
                        //Data video
                        Liked.dv_id = Video.dv_id;
                        Liked.dv_video_id = Video.dv_video_id;
                        Liked.dv_user_id = Video.dv_user_id;
                        Liked.dv_title = Video.dv_title;
                        Liked.dv_description = Video.dv_description;
                        Liked.dv_Long_title = Video.dv_Long_title;
                        Liked.dv_Long_description = Video.dv_Long_description;
                        Liked.dv_thumbnail = Video.dv_thumbnail;
                        Liked.dv_video_location = Video.dv_video_location;
                        Liked.dv_youtube = Video.dv_youtube;
                        Liked.dv_vimeo = Video.dv_vimeo;
                        Liked.dv_daily = Video.dv_daily;
                        Liked.dv_time = Video.dv_time;
                        Liked.dv_time_date = Video.dv_time_date;
                        Liked.dv_active = Video.dv_active;
                        Liked.dv_tags = Video.dv_tags;
                        Liked.dv_duration = Video.dv_duration;
                        Liked.dv_size = Video.dv_size;
                        Liked.dv_category_id = Video.dv_category_id;
                        Liked.dv_views = Video.dv_views;
                        Liked.dv_featured = Video.dv_featured;
                        Liked.dv_registered = Video.dv_registered;
                        Liked.dv_org_thumbnail = Video.dv_org_thumbnail;
                        Liked.dv_video_type = Video.dv_video_type;
                        Liked.dv_video_id_ = Video.dv_video_id_;
                        Liked.dv_source = Video.dv_source;
                        Liked.dv_url = Video.dv_url;
                        Liked.dv_edit_description = Video.dv_edit_description;
                        Liked.dv_markup_description = Video.dv_markup_description;
                        Liked.dv_is_liked = Video.dv_is_liked;
                        Liked.dv_is_disliked = Video.dv_is_disliked;
                        Liked.dv_is_owner = Video.dv_is_owner;
                        Liked.dv_time_alpha = Video.dv_time_alpha;
                        Liked.dv_time_ago = Video.dv_time_ago;
                        Liked.dv_category_name = Video.dv_category_name;
                        Liked.dv_likes = Video.dv_likes;
                        Liked.dv_dislikes = Video.dv_dislikes;
                        Liked.dv_likes_percent = Video.dv_likes_percent;
                        Liked.dv_dislikes_percent = Video.dv_dislikes_percent;
                        Liked.dv_is_subscribed = Video.dv_is_subscribed;

                        //Owner
                        Liked.Owner_id = Video.Owner_id;
                        Liked.Owner_username = Video.Owner_username;
                        Liked.Owner_email = Video.Owner_email;
                        Liked.Owner_first_name = Video.Owner_first_name;
                        Liked.Owner_last_name = Video.Owner_last_name;
                        Liked.Owner_gender = Video.Owner_gender;
                        Liked.Owner_language = Video.Owner_language;
                        Liked.Owner_avatar = Video.Owner_avatar;
                        Liked.Owner_cover = Video.Owner_cover;
                        Liked.Owner_about = Video.Owner_about;
                        Liked.Owner_google = Video.Owner_google;
                        Liked.Owner_facebook = Video.Owner_facebook;
                        Liked.Owner_twitter = Video.Owner_twitter;
                        Liked.Owner_verified = Video.Owner_verified;
                        Liked.Owner_is_pro = Video.Owner_is_pro;
                        Liked.Owner_url = Video.Owner_url;

                        SQL_Entity.Connection.Insert(Liked);
                    }
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        //Remove Liked Videos
        public static void RemoveLikedVideos_Credentials(string LikedVideos_id)
        {
            try
            {
                if (!String.IsNullOrEmpty(LikedVideos_id))
                {
                    var select = SQL_Entity.Connection.Table<DataTables.LikedVideosTB>()
                        .FirstOrDefault(a => a.dv_video_id == LikedVideos_id);
                    if (select != null)
                    {
                        SQL_Entity.Connection.Delete(select);
                    }
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        //Get Liked Videos
        public static ObservableCollection<DataTables.LikedVideosTB> Get_LikedVideos_Credentials()
        {
            try
            {
                var select = SQL_Entity.Connection.Table<DataTables.LikedVideosTB>().ToList().OrderByDescending(a => a.ID);
                if (select.Count() > 0)
                {
                    return new ObservableCollection<DataTables.LikedVideosTB>(select);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
                return null;
            }
        }

        //Insert History Videos
        public static void Insert_HistoryVideos_Credentials(Classes.Video Video)
        {
            try
            {
                if (Video != null)
                {
                    var select = SQL_Entity.Connection.Table<DataTables.HistoryVideosTB>().FirstOrDefault(a => a.dv_video_id == Video.dv_video_id);
                    if (select == null)
                    {
                        DataTables.HistoryVideosTB History = new DataTables.HistoryVideosTB();
                        History.SV_Type_video = "History";
                        //Data video
                        History.dv_id = Video.dv_id;
                        History.dv_video_id = Video.dv_video_id;
                        History.dv_user_id = Video.dv_user_id;
                        History.dv_title = Video.dv_title;
                        History.dv_description = Video.dv_description;
                        History.dv_Long_title = Video.dv_Long_title;
                        History.dv_Long_description = Video.dv_Long_description;
                        History.dv_thumbnail = Video.dv_thumbnail;
                        History.dv_video_location = Video.dv_video_location;
                        History.dv_youtube = Video.dv_youtube;
                        History.dv_vimeo = Video.dv_vimeo;
                        History.dv_daily = Video.dv_daily;
                        History.dv_time = Video.dv_time;
                        History.dv_time_date = Video.dv_time_date;
                        History.dv_active = Video.dv_active;
                        History.dv_tags = Video.dv_tags;
                        History.dv_duration = Video.dv_duration;
                        History.dv_size = Video.dv_size;
                        History.dv_category_id = Video.dv_category_id;
                        History.dv_views = Video.dv_views;
                        History.dv_featured = Video.dv_featured;
                        History.dv_registered = Video.dv_registered;
                        History.dv_org_thumbnail = Video.dv_org_thumbnail;
                        History.dv_video_type = Video.dv_video_type;
                        History.dv_video_id_ = Video.dv_video_id_;
                        History.dv_source = Video.dv_source;
                        History.dv_url = Video.dv_url;
                        History.dv_edit_description = Video.dv_edit_description;
                        History.dv_markup_description = Video.dv_markup_description;
                        History.dv_is_liked = Video.dv_is_liked;
                        History.dv_is_disliked = Video.dv_is_disliked;
                        History.dv_is_owner = Video.dv_is_owner;
                        History.dv_time_alpha = Video.dv_time_alpha;
                        History.dv_time_ago = Video.dv_time_ago;
                        History.dv_category_name = Video.dv_category_name;
                        History.dv_likes = Video.dv_likes;
                        History.dv_dislikes = Video.dv_dislikes;
                        History.dv_likes_percent = Video.dv_likes_percent;
                        History.dv_dislikes_percent = Video.dv_dislikes_percent;
                        History.dv_is_subscribed = Video.dv_is_subscribed;


                        //Owner
                        History.Owner_id = Video.Owner_id;
                        History.Owner_username = Video.Owner_username;
                        History.Owner_email = Video.Owner_email;
                        History.Owner_first_name = Video.Owner_first_name;
                        History.Owner_last_name = Video.Owner_last_name;
                        History.Owner_gender = Video.Owner_gender;
                        History.Owner_language = Video.Owner_language;
                        History.Owner_avatar = Video.Owner_avatar;
                        History.Owner_cover = Video.Owner_cover;
                        History.Owner_about = Video.Owner_about;
                        History.Owner_google = Video.Owner_google;
                        History.Owner_facebook = Video.Owner_facebook;
                        History.Owner_twitter = Video.Owner_twitter;
                        History.Owner_verified = Video.Owner_verified;
                        History.Owner_is_pro = Video.Owner_is_pro;
                        History.Owner_url = Video.Owner_url;

                        SQL_Entity.Connection.Insert(History);
                    }
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        //Remove History Videos
        public static void RemoveHistoryVideos_Credentials(string HistoryVideos_id)
        {
            try
            {
                if (!String.IsNullOrEmpty(HistoryVideos_id))
                {
                    var select = SQL_Entity.Connection.Table<DataTables.HistoryVideosTB>()
                        .FirstOrDefault(a => a.dv_video_id == HistoryVideos_id);
                    if (select != null)
                    {
                        SQL_Entity.Connection.Delete(select);
                    }
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        //Get History Videos
        public static ObservableCollection<DataTables.HistoryVideosTB> Get_HistoryVideos_Credentials()
        {
            try
            {
                var select = SQL_Entity.Connection.Table<DataTables.HistoryVideosTB>().ToList().OrderByDescending(a => a.ID);
                if (select.Count() > 0)
                {
                    var collection = new ObservableCollection<DataTables.HistoryVideosTB>(select);
                    return collection;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
                return null;
            }
        }

        //Insert PlayLists Videos
        public static void Insert_PlayListsVideos_Credentials(Classes.Video Video)
        {
            try
            {
                if (Video != null)
                {
                    var select = SQL_Entity.Connection.Table<DataTables.PlayListsVideosTB>().FirstOrDefault(a => a.dv_video_id == Video.dv_video_id);

                    if (select == null)
                    {
                        DataTables.PlayListsVideosTB PlayLists = new DataTables.PlayListsVideosTB();
                        PlayLists.SV_Type_video = "PlayLists";
                        //Data video
                        PlayLists.dv_id = Video.dv_id;
                        PlayLists.dv_video_id = Video.dv_video_id;
                        PlayLists.dv_user_id = Video.dv_user_id;
                        PlayLists.dv_title = Video.dv_title;
                        PlayLists.dv_Long_title = Video.dv_Long_title;
                        PlayLists.dv_Long_description = Video.dv_Long_description;
                        PlayLists.dv_thumbnail = Video.dv_thumbnail;
                        PlayLists.dv_time = Video.dv_time;
                        PlayLists.dv_time_date = Video.dv_time_date;
                        PlayLists.dv_duration = Video.dv_duration;
                        PlayLists.dv_views = Video.dv_views;
                        PlayLists.dv_url = Video.dv_url;

                        //Owner
                        PlayLists.Owner_id = Video.Owner_id;
                        PlayLists.Owner_username = Video.Owner_username;
                        PlayLists.Owner_email = Video.Owner_email;
                        PlayLists.Owner_first_name = Video.Owner_first_name;
                        PlayLists.Owner_last_name = Video.Owner_last_name;
                        PlayLists.Owner_gender = Video.Owner_gender;
                        PlayLists.Owner_language = Video.Owner_language;
                        PlayLists.Owner_avatar = Video.Owner_avatar;
                        PlayLists.Owner_cover = Video.Owner_cover;
                        PlayLists.Owner_about = Video.Owner_about;
                        PlayLists.Owner_google = Video.Owner_google;
                        PlayLists.Owner_facebook = Video.Owner_facebook;
                        PlayLists.Owner_twitter = Video.Owner_twitter;
                        PlayLists.Owner_verified = Video.Owner_verified;
                        PlayLists.Owner_is_pro = Video.Owner_is_pro;
                        PlayLists.Owner_url = Video.Owner_url;

                        SQL_Entity.Connection.Insert(PlayLists);
                    }
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        //Remove PlayLists Videos
        public static void RemovePlayListsVideos_Credentials(string PlayListsVideos_id)
        {
            try
            {
                if (!String.IsNullOrEmpty(PlayListsVideos_id))
                {
                    var select = SQL_Entity.Connection.Table<DataTables.PlayListsVideosTB>()
                        .FirstOrDefault(a => a.dv_video_id == PlayListsVideos_id);
                    if (select != null)
                    {
                        SQL_Entity.Connection.Delete(select);
                    }
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        //Get PlayLists Videos
        public static ObservableCollection<DataTables.PlayListsVideosTB> Get_PlayListsVideos_Credentials()
        {
            try
            {
                var select = SQL_Entity.Connection.Table<DataTables.PlayListsVideosTB>().ToList().OrderByDescending(a => a.ID);
                if (select.Count() > 0)
                {
                    return new ObservableCollection<DataTables.PlayListsVideosTB>(select);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
                return null;
            }
        }

        //Insert WatchLater Videos
        public static void Insert_WatchLaterVideos_Credentials(Classes.Video Video)
        {
            try
            {
                if (Video != null )
                {
                    var select = SQL_Entity.Connection.Table<DataTables.WatchLaterVideosTB>().FirstOrDefault(a => a.dv_video_id == Video.dv_video_id);
                    if (select == null)
                    {
                        DataTables.WatchLaterVideosTB WatchLater = new DataTables.WatchLaterVideosTB();
                        WatchLater.SV_Type_video = "WatchLater";
                        //Data video
                        WatchLater.dv_id = Video.dv_id;
                        WatchLater.dv_video_id = Video.dv_video_id;
                        WatchLater.dv_user_id = Video.dv_user_id;
                        WatchLater.dv_title = Video.dv_title;
                        WatchLater.dv_description = Video.dv_description;
                        WatchLater.dv_Long_title = Video.dv_Long_title;
                        WatchLater.dv_Long_description = Video.dv_Long_description;
                        WatchLater.dv_thumbnail = Video.dv_thumbnail;
                        WatchLater.dv_video_location = Video.dv_video_location;
                        WatchLater.dv_youtube = Video.dv_youtube;
                        WatchLater.dv_vimeo = Video.dv_vimeo;
                        WatchLater.dv_daily = Video.dv_daily;
                        WatchLater.dv_time = Video.dv_time;
                        WatchLater.dv_time_date = Video.dv_time_date;
                        WatchLater.dv_active = Video.dv_active;
                        WatchLater.dv_tags = Video.dv_tags;
                        WatchLater.dv_duration = Video.dv_duration;
                        WatchLater.dv_size = Video.dv_size;
                        WatchLater.dv_category_id = Video.dv_category_id;
                        WatchLater.dv_views = Video.dv_views;
                        WatchLater.dv_featured = Video.dv_featured;
                        WatchLater.dv_registered = Video.dv_registered;
                        WatchLater.dv_org_thumbnail = Video.dv_org_thumbnail;
                        WatchLater.dv_video_type = Video.dv_video_type;
                        WatchLater.dv_video_id_ = Video.dv_video_id_;
                        WatchLater.dv_source = Video.dv_source;
                        WatchLater.dv_url = Video.dv_url;
                        WatchLater.dv_edit_description = Video.dv_edit_description;
                        WatchLater.dv_markup_description = Video.dv_markup_description;
                        WatchLater.dv_is_liked = Video.dv_is_liked;
                        WatchLater.dv_is_disliked = Video.dv_is_disliked;
                        WatchLater.dv_is_owner = Video.dv_is_owner;
                        WatchLater.dv_time_alpha = Video.dv_time_alpha;
                        WatchLater.dv_time_ago = Video.dv_time_ago;
                        WatchLater.dv_category_name = Video.dv_category_name;
                        WatchLater.dv_likes = Video.dv_likes;
                        WatchLater.dv_dislikes = Video.dv_dislikes;
                        WatchLater.dv_likes_percent = Video.dv_likes_percent;
                        WatchLater.dv_dislikes_percent = Video.dv_dislikes_percent;
                        WatchLater.dv_is_subscribed = Video.dv_is_subscribed;

                        //Owner
                        WatchLater.Owner_id = Video.Owner_id;
                        WatchLater.Owner_username = Video.Owner_username;
                        WatchLater.Owner_email = Video.Owner_email;
                        WatchLater.Owner_first_name = Video.Owner_first_name;
                        WatchLater.Owner_last_name = Video.Owner_last_name;
                        WatchLater.Owner_gender = Video.Owner_gender;
                        WatchLater.Owner_language = Video.Owner_language;
                        WatchLater.Owner_avatar = Video.Owner_avatar;
                        WatchLater.Owner_cover = Video.Owner_cover;
                        WatchLater.Owner_about = Video.Owner_about;
                        WatchLater.Owner_google = Video.Owner_google;
                        WatchLater.Owner_facebook = Video.Owner_facebook;
                        WatchLater.Owner_twitter = Video.Owner_twitter;
                        WatchLater.Owner_verified = Video.Owner_verified;
                        WatchLater.Owner_is_pro = Video.Owner_is_pro;
                        WatchLater.Owner_url = Video.Owner_url;

                        SQL_Entity.Connection.Insert(WatchLater);
                    }
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        //Remove WatchLater Videos
        public static void RemoveWatchLaterVideos_Credentials(string WatchLaterVideos_id)
        {
            try
            {
                if (!String.IsNullOrEmpty(WatchLaterVideos_id))
                {
                    var select = SQL_Entity.Connection.Table<DataTables.WatchLaterVideosTB>()
                        .FirstOrDefault(a => a.dv_video_id == WatchLaterVideos_id);
                    if (select != null)
                    {
                        SQL_Entity.Connection.Delete(select);
                    }
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        //Get WatchLater Videos
        public static ObservableCollection<DataTables.WatchLaterVideosTB> Get_WatchLaterVideos_Credentials()
        {
            try
            {
                var select = SQL_Entity.Connection.Table<DataTables.WatchLaterVideosTB>().ToList().OrderByDescending(a => a.ID);
                if (select.Count() > 0)
                {
                    return new ObservableCollection<DataTables.WatchLaterVideosTB>(select);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
                return null;
            }
        }

        //Insert SubscriptionsChannel Videos
        public static void Insert_SubscriptionsChannel_Credentials(Classes.Video Video)
        {
            try
            {
                if (Video != null)
                {
                    var select = SQL_Entity.Connection.Table<DataTables.SubscriptionsChannelTB>().FirstOrDefault(a => a.Owner_id == Video.Owner_id);
                    if (select == null)
                    {
                        DataTables.SubscriptionsChannelTB Subcribed = new DataTables.SubscriptionsChannelTB();

                        //Owner
                        Subcribed.Owner_id = Video.Owner_id;
                        Subcribed.Owner_username = Video.Owner_username;
                        Subcribed.Owner_email = Video.Owner_email;
                        Subcribed.Owner_first_name = Video.Owner_first_name;
                        Subcribed.Owner_last_name = Video.Owner_last_name;
                        Subcribed.Owner_gender = Video.Owner_gender;
                        Subcribed.Owner_language = Video.Owner_language;
                        Subcribed.Owner_avatar = Video.Owner_avatar;
                        Subcribed.Owner_cover = Video.Owner_cover;
                        Subcribed.Owner_about = Video.Owner_about;
                        Subcribed.Owner_google = Video.Owner_google;
                        Subcribed.Owner_facebook = Video.Owner_facebook;
                        Subcribed.Owner_twitter = Video.Owner_twitter;
                        Subcribed.Owner_verified = Video.Owner_verified;
                        Subcribed.Owner_is_pro = Video.Owner_is_pro;
                        Subcribed.Owner_url = Video.Owner_url;

                        SQL_Entity.Connection.Insert(Subcribed);
                    }
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        //Remove SubscriptionsChannel Videos
        public static void RemoveSubscriptionsChannel_Credentials(string SubscriptionsChannel_id)
        {
            try
            {
                if (!String.IsNullOrEmpty(SubscriptionsChannel_id))
                {
                    var select = SQL_Entity.Connection.Table<DataTables.SubscriptionsChannelTB>().FirstOrDefault(a => a.Owner_id == SubscriptionsChannel_id);
                    if (select != null)
                    {
                        SQL_Entity.Connection.Delete(select);
                    }
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        //Get SubscriptionsChannel Videos
        public static ObservableCollection<DataTables.SubscriptionsChannelTB> Get_SubscriptionsChannel_Credentials()
        {
            try
            {
                var select = SQL_Entity.Connection.Table<DataTables.SubscriptionsChannelTB>().ToList().OrderByDescending(a => a.ID);
                if (select.Count() > 0)
                {
                    return new ObservableCollection<DataTables.SubscriptionsChannelTB>(select);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
                return null;
            }
        }

    }
}