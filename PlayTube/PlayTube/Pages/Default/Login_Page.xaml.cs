using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Acr.UserDialogs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PlayTube.Controls;
using PlayTube.Languish;
using PlayTube.Pages.Tabbes;
using PlayTube.SQLite;
using Plugin.Connectivity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PlayTube.Pages.Default
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login_Page : ContentPage
    {
        public Login_Page()
        {
            try
            {
                InitializeComponent();

                Txt_Username.Completed += (s, a) => Txt_password.Focus();
                //Txt_Password.Completed += (s, a) => Btn_Login_OnClicked(s, a);

                Lbl_NameApp.Text = Settings.Application_Name;
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        //Click Button Login
        private async void Btn_Login_OnClicked(object sender, EventArgs e)
        {
            try
            {
                if (!CrossConnectivity.Current.IsConnected)
                {
                    await DisplayAlert(AppResources.Label_Error, AppResources.Label_Check_Your_Internet, AppResources.Label_OK);
                }
                else
                {
                    UserDialogs.Instance.ShowLoading(AppResources.Label_Please_wait, MaskType.None);

                    //API_Request.Session = Functions.RandomString(30);

                    using (var client = new HttpClient())
                    {
                        var formContent = new FormUrlEncodedContent(new[]
                        {
                            new KeyValuePair<string, string>("username", Txt_Username.Text),
                            new KeyValuePair<string, string>("password", Txt_password.Text),
                            //new KeyValuePair<string, string>("s", API_Request.Session),
                        });

                        var response = await client.PostAsync(API_Request.API_Login, formContent);
                        response.EnsureSuccessStatusCode();
                        var Response_json = await response.Content.ReadAsStringAsync();
                        var data = JsonConvert.DeserializeObject<Dictionary<string, object>>(Response_json);
                        string ApiStatus = data["api_status"].ToString();

                        if (ApiStatus == "200")
                        {
                            JObject Login_data = JObject.FromObject(data["data"]);

                            API_Request.User_id = Login_data["user_id"].ToString();
                            API_Request.Session = Login_data["session_id"].ToString();
                            API_Request.Cookie = Login_data["cookie"].ToString();

                            //Insert user data to database
                            DataTables.LoginTB user = new DataTables.LoginTB();
                            user.UserID = API_Request.User_id;
                            user.Session = API_Request.Session;
                            user.Cookie = API_Request.Cookie;
                            user.Username = Txt_Username.Text;
                            user.Password = Txt_password.Text;
                            user.Status = "Active";

                            Classes.DataUserLoginList.Add(user);
                            SQL_Commander.Insert_Login_Credentials(user);
                           
                            //Messeges is login user 
                            Hamburg_Page.IsLogin = true;
                            var Status_user = Classes.SeconderyListPage.FirstOrDefault(a => a.Name_page == AppResources.Label_Login);
                            if (Status_user != null)
                            {
                                Status_user.Name_page = AppResources.Label_Logout;
                                Status_user.Icon_page = IoniciconsFont.LogOut;

                                var query = Classes.ListPage.FirstOrDefault(a => a.Name_page == AppResources.Label_My_Channel);
                                if (query == null)
                                {
                                    Classes.ListPage.Insert(0, new Classes.PageItems()
                                    {
                                        Name_page = AppResources.Label_My_Channel,
                                        Icon_page = IoniciconsFont.Person,
                                        BackgroundColor = "#696969",
                                        TextColor = "#444"
                                    });
                                    Classes.ListPage.Insert(2, new Classes.PageItems()
                                    {
                                        Name_page = AppResources.Label_PlayLists,
                                        Icon_page = IoniciconsFont.Play,
                                        BackgroundColor = "#696969",
                                        TextColor = "#444"
                                    });
                                    Classes.ListPage.Insert(3, new Classes.PageItems()
                                    {
                                        Name_page = AppResources.Label_Liked_Videos,
                                        Icon_page = IoniciconsFont.Thumbsup,
                                        BackgroundColor = "#696969",
                                        TextColor = "#444"
                                    });
                                    Classes.ListPage.Add(new Classes.PageItems()
                                    {
                                        Name_page = AppResources.Label_Upload,
                                        Icon_page = IoniciconsFont.IosCloudUpload,
                                        BackgroundColor = "#696969",
                                        TextColor = "#444"
                                    });
                                    if (Settings.DarkTheme)
                                    {
                                        foreach (var Item in Classes.ListPage)
                                        {
                                            Item.BackgroundColor = "#bcbcbc";
                                            Item.TextColor = "#ffff";
                                        }
                                    }
                                }
                            }
                            //Check User Status and Get data
                            SQL_Commander.Get_data_Login_Credentials();

                            UserDialogs.Instance.HideLoading();
                            await Navigation.PopModalAsync(true);
                        }
                        else if (ApiStatus == "304")
                        {
                            JObject errors = JObject.FromObject(data["errors"]);
                            string errorID = errors["error_id"].ToString();
                            var errortext = errors["error_text"].ToString();
                        
                            UserDialogs.Instance.ShowError(errortext);
                        }
                        else
                        {
                            UserDialogs.Instance.HideLoading();
                            await DisplayAlert(AppResources.Label_Login, AppResources.Label_Login_Not_Correct, AppResources.Label_OK);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
                await DisplayAlert(AppResources.Label_Error,AppResources.Label_Session_Dead, AppResources.Label_OK);
                UserDialogs.Instance.HideLoading();
            }
        }

        //Click Icon this close
        private void Lbl_IconClose_OnTapped(object sender, EventArgs e)
        {
            try
            {
                Navigation.PopAsync(false);
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
                Navigation.PopModalAsync(true);
            }
        }

        //Click Button Register
        private async void Btn_Register_OnClicked(object sender, EventArgs e)
        {
            try
            {
                //await Navigation.PopAsync(true);
                await Navigation.PushAsync(new Registration_Page());
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
                //await Navigation.PopModalAsync(true);
                await Navigation.PushModalAsync(new Registration_Page());
            }
        }

        //Click Button ForgetPassword
        private async void Btn_ForgetPassword_OnClicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new ForgetPassword_Page());
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
                await Navigation.PushModalAsync(new ForgetPassword_Page());
            }
        }
    }
}