using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PlayTube.Controls
{
    public class API_Request
    {
        //############# DONT'T MODIFY HERE #############
        //Auto Session bindable objects 
        //*********************************************************
        public static string Session = "";
        public static string User_id = "";
        public static string Username = "";
        public static string Email = "";
        public static string Cookie = "";
        public static string Status = "";
        public static string Lang = "";
        //Main API URLS
        //*********************************************************
        private static string API_Get_Settings = Settings.WebsiteUrl + "/api/v1.0/?type=get_settings"; // Get Settings
        public static string API_Login = Settings.WebsiteUrl + "/api/v1.0/?type=login"; // Login
        private static string API_Logout = Settings.WebsiteUrl + "/api/v1.0/?type=logout"; // Logout
        public static string API_Register = Settings.WebsiteUrl + "/api/v1.0/?type=register"; // Register
        public static string API_Reset_password = Settings.WebsiteUrl + "/api/v1.0/?type=reset_password"; // Forget / Reset password
        private static string API_Get_Channel_Info = Settings.WebsiteUrl + "/api/v1.0/?type=get_channel_info"; // Get channel info
        private static string API_Get_Videos = Settings.WebsiteUrl + "/api/v1.0/?type=get_videos"; // Get videos
        private static string API_Get_Videos_Details = Settings.WebsiteUrl + "/api/v1.0/?type=get_video_details"; // Get videos details by ID
        private static string API_Get_Trending_Videos = Settings.WebsiteUrl + "/api/v1.0/?type=get_trending_videos"; // Get Trending videos
        private static string API_Get_Subscriptions = Settings.WebsiteUrl + "/api/v1.0/?type=get_subscriptions"; // Get Subscriptions
        private static string API_Search_Videos = Settings.WebsiteUrl + "/api/v1.0/?type=search_videos"; // Search videos
        private static string API_Get_Videos_By_Category = Settings.WebsiteUrl + "/api/v1.0/?type=get_videos_by_category"; // Get Videos By Category
        private static string API_Get_Videos_By_Channel = Settings.WebsiteUrl + "/api/v1.0/?type=get_videos_by_channel"; // Get Videos By Channel
        private static string API_Like_Dislike_Videos = Settings.WebsiteUrl + "/api/v1.0/?type=like_dislike_video"; // Add like/dislike Videos
        private static string API_Subscribe_To_Channel = Settings.WebsiteUrl + "/api/v1.0/?type=subscribe_to_channel"; // Add subscribe to channel
        private static string API_Get_My_Playlists = Settings.WebsiteUrl + "/api/v1.0/?type=get_my_playlists"; // Get My Playlists

        //########################## HttpClient ##########################

        #region HttpClient

        //Get Settings
        public static async Task<JObject> Get_Settings_Http()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(API_Get_Settings);
                    response.EnsureSuccessStatusCode();
                    var Response_json = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<Dictionary<string, object>>(Response_json);
                    string apiStatus = data["api_status"].ToString();
                    if (apiStatus == "200")
                    {
                        JObject cats_data = JObject.FromObject(data["data"]);
                        return cats_data;
                    }
                    else if (apiStatus == "400")
                    {
                        return null;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
                return null;
            }
        }

        //User logout
        public static async void User_logout_Http()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var formContent = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("user_id", User_id),
                        new KeyValuePair<string, string>("s", Session),
                    });
                    var Response_Api =await client.PostAsync(API_Logout, formContent);
                    Response_Api.EnsureSuccessStatusCode();
                    string Response_Api_json = await Response_Api.Content.ReadAsStringAsync();

                }              
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        // Get channel info
        public static async Task<JObject> Get_Channel_Info_Http(string channel_id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(API_Get_Channel_Info + "&channel_id=" + channel_id);
                    response.EnsureSuccessStatusCode();
                    var Response_json = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<Dictionary<string, object>>(Response_json);
                    string apiStatus = data["api_status"].ToString();
                    if (apiStatus == "200")
                    {
                        JObject Videos_data = JObject.FromObject(data["data"]);
                        return Videos_data;
                    }
                    else if (apiStatus == "400")
                    {
                        return null;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
                return null;
            }
        }

        //Get Videos
        public static async Task<JObject> Get_Videos_Http(string featured_offset, string top_offset, string latest_offset, string limit)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var formContent = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("user_id", User_id),
                        new KeyValuePair<string, string>("s", Session),
                    });
                    var response = await client.PostAsync(API_Get_Videos + "&featured_offset=" + featured_offset + "&top_offset=" + top_offset + "&latest_offset=" + latest_offset + "&limit=" + limit, formContent);

                    response.EnsureSuccessStatusCode();
                    var Response_json = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<Dictionary<string, object>>(Response_json);
                    string apiStatus = data["api_status"].ToString();
                    if (apiStatus == "200")
                    {
                        JObject Videos_data = JObject.FromObject(data["data"]);
                        return Videos_data;
                    }
                    else if (apiStatus == "400")
                    {
                        return null;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
                return null;
            }
        }

        //Get Videos details by ID
        public static async Task<JObject> API_Get_Videos_Details_Http(string video_id )
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var formContent = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("user_id", User_id),
                        new KeyValuePair<string, string>("s", Session),
                    });
                    var response = await client.PostAsync(API_Get_Videos_Details + "&video_id=" + video_id , formContent);
                    response.EnsureSuccessStatusCode();
                    var Response_json = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<Dictionary<string, object>>(Response_json);
                    string apiStatus = data["api_status"].ToString();
                    if (apiStatus == "200")
                    {
                        JObject Videos_data = JObject.FromObject(data["data"]);
                        return Videos_data;
                    }
                    else if (apiStatus == "400")
                    {
                        return null;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
                return null;
            }
        }

        //Get Trending Videos
        public static async Task<JArray> Get_Trending_Videos_Http(string offset , string limit)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(API_Get_Trending_Videos + "&offset=" + offset + "&limit=" + limit + "&s=" + Session);
                    response.EnsureSuccessStatusCode();
                    var Response_json = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<Dictionary<string, object>>(Response_json);
                    string apiStatus = data["api_status"].ToString();
                    if (apiStatus == "200")
                    {
                        var trending_data = JObject.Parse(Response_json).SelectToken("data").ToString();
                        JArray trendingData = JArray.Parse(trending_data);
                        return trendingData;
                    }
                    else if (apiStatus == "400")
                    {
                        return null;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
                return null;
            }
        }

        //Get Subscriptions Videos Or channel
        public static async Task<JArray> Get_Subscriptions_VideosOrChannel_Http(string channel, string offset, string limit)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var formContent = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("user_id", User_id),
                        new KeyValuePair<string, string>("s", Session),
                    });
                    var response = await client.PostAsync(API_Get_Subscriptions + "&channel=" + channel + "&offset=" + offset + "&limit=" + limit, formContent);
                    response.EnsureSuccessStatusCode();
                    var Response_json = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<Dictionary<string, object>>(Response_json);
                    string apiStatus = data["api_status"].ToString();
                    if (apiStatus == "200")
                    {
                        var Subscriptions_data = JObject.Parse(Response_json).SelectToken("data").ToString();
                        JArray SubscriptionsData = JArray.Parse(Subscriptions_data);
                        return SubscriptionsData;
                    }
                    else if (apiStatus == "400")
                    {
                        return null;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
                return null;
            }
        }

        //Search Videos
        public static async Task<JArray> Search_Videos_Http(string keyword, string limit, string offset)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var formContent = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("user_id", User_id),
                        new KeyValuePair<string, string>("s", Session),
                    });
                    var response = await client.PostAsync(API_Search_Videos + "&keyword=" + keyword + "&limit=" + limit + "&offset=" + offset , formContent);
                    response.EnsureSuccessStatusCode();
                    var Response_json = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<Dictionary<string, object>>(Response_json);
                    string apiStatus = data["api_status"].ToString();
                    if (apiStatus == "200")
                    {
                        var Search_data = JObject.Parse(Response_json).SelectToken("data").ToString();
                        JArray searchData = JArray.Parse(Search_data);
                        return searchData;
                    }
                    else if (apiStatus == "400")
                    {
                        return null;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
                return null;
            }
        }

        //Get Videos By Category
        public static async Task<JArray> Get_Videos_By_Category_Http(string category_id, string limit, string offset)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var formContent = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("user_id", User_id),
                        new KeyValuePair<string, string>("s", Session),
                    });
                    var response = await client.PostAsync(API_Get_Videos_By_Category + "&category_id=" + category_id + "&limit=" + limit + "&offset=" + offset , formContent);
                    response.EnsureSuccessStatusCode();
                    var Response_json = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<Dictionary<string, object>>(Response_json);
                    string apiStatus = data["api_status"].ToString();
                    if (apiStatus == "200")
                    {
                        var Videos_data = JObject.Parse(Response_json).SelectToken("data").ToString();
                        JArray VideosData = JArray.Parse(Videos_data);
                        return VideosData;
                    }
                    else if (apiStatus == "400")
                    {
                        return null;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
                return null;
            }
        }

        //Get Videos By Channel
        public static async Task<JArray> Get_Videos_By_Channel_Http(string channel_id, string limit, string offset)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var formContent = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("user_id", User_id),
                        new KeyValuePair<string, string>("s", Session),
                    });
                    var response = await client.PostAsync(API_Get_Videos_By_Channel + "&channel_id=" + channel_id + "&limit=" + limit + "&offset=" + offset, formContent);
                    response.EnsureSuccessStatusCode();
                    var Response_json = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<Dictionary<string, object>>(Response_json);
                    string apiStatus = data["api_status"].ToString();
                    if (apiStatus == "200")
                    {
                        var Videos_data = JObject.Parse(Response_json).SelectToken("data").ToString();
                        JArray VideosData = JArray.Parse(Videos_data);
                        return VideosData;
                    }
                    else if (apiStatus == "400")
                    {
                        return null;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
                return null;
            }
        }

        //Add like/dislik Videos
        public static async Task<string> Add_Like_Dislike_Videos_Http(string video_id, string like_type)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var formContent = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("video_id", video_id),
                        new KeyValuePair<string, string>("action", like_type),
                        new KeyValuePair<string, string>("user_id", User_id),
                        new KeyValuePair<string, string>("s", Session),
                    });

                    var response = await client.PostAsync(API_Like_Dislike_Videos, formContent);
                    response.EnsureSuccessStatusCode();
                    var Response_json = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<Dictionary<string, object>>(Response_json);
                    string apiStatus = data["api_status"].ToString();
                    if (apiStatus == "200")
                    {
                        var Videos_Like = data["success_type"].ToString();
                        return Videos_Like;
                    }
                    else if (apiStatus == "400")
                    {
                        return null;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
                return null;
            }
        }

        //Add Subscribe To Channel
        public static async Task<string> Add_Subscribe_To_Channel_Http(string channel_id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var formContent = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("user_id", User_id),
                        new KeyValuePair<string, string>("s", Session),
                    });
                    var response = await client.PostAsync(API_Subscribe_To_Channel + "&channel_id=" + channel_id, formContent);

                    response.EnsureSuccessStatusCode();
                    var Response_json = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<Dictionary<string, object>>(Response_json);
                    string apiStatus = data["api_status"].ToString();
                    if (apiStatus == "200")
                    {
                        return "subsribed";
                    }
                    else if (apiStatus == "304")
                    {
                        return "unsubscribed";
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
                return null;
            }
        }

        //Get My Playlists
        public static async Task<JArray> Get_My_Playlists_Http(string offset, string limit)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var formContent = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("user_id", User_id),
                        new KeyValuePair<string, string>("s", Session),
                    });
                    var response = await client.PostAsync(API_Get_My_Playlists + "&offset=" + offset + "&limit=" + limit , formContent);

                    response.EnsureSuccessStatusCode();
                    var Response_json = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<Dictionary<string, object>>(Response_json);
                    string apiStatus = data["api_status"].ToString();
                    if (apiStatus == "200")
                    {
                        var Videos_data = JObject.Parse(Response_json).SelectToken("data").ToString();
                        JArray VideosData = JArray.Parse(Videos_data);
                        return VideosData;
                    }
                    else if (apiStatus == "400")
                    {
                        return null;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
                return null;
            }
        }

        #endregion

    }
}
