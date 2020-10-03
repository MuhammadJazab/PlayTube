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
    public partial class Registration_Page : ContentPage
    {
        public Registration_Page()
        {
            try
            {
                InitializeComponent();

                Txt_Email.Completed += (s, a) => Txt_Username.Focus();
                Txt_Username.Completed += (s, a) => Txt_password.Focus();
                Txt_password.Completed += (s, a) => Txt_passwordConfirm.Focus();
                //Txt_passwordConfirm.Completed += (s, a) => Btn_Register_OnClicked(s, a);
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        //Click Button Register
        private async void Btn_Register_OnClicked(object sender, EventArgs e)
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

                    //API_Request.Session = Functions.RandomString(40);

                    string Gender = "male";
                    using (var client = new HttpClient())
                    {
                        var formContent = new FormUrlEncodedContent(new[]
                        {
                            new KeyValuePair<string, string>("username", Txt_Username.Text),
                            new KeyValuePair<string, string>("password", Txt_password.Text),
                            new KeyValuePair<string, string>("confirm_password", Txt_passwordConfirm.Text),
                            new KeyValuePair<string, string>("email", Txt_Email.Text),
                            new KeyValuePair<string, string>("gender", Gender),
                            //new KeyValuePair<string, string>("s", API_Request.Session)
                        });

                        var response = await client.PostAsync(API_Request.API_Register, formContent);
                        response.EnsureSuccessStatusCode();
                        var Response_json = await response.Content.ReadAsStringAsync();
                        var data = JsonConvert.DeserializeObject<Dictionary<string, object>>(Response_json);
                        string ApiStatus = data["api_status"].ToString();

                        if (ApiStatus == "200")
                        {
                            var successtype = data["success_type"].ToString();

                            if (successtype.Contains("registered"))
                            {
                                UserDialogs.Instance.HideLoading();

                                try
                                {
                                    API_Request.User_id = data["user_id"].ToString();
                                    API_Request.Session = data["session_id"].ToString();

                                    //Insert user data to database
                                    DataTables.LoginTB user = new DataTables.LoginTB();
                                    user.UserID = API_Request.User_id;
                                    user.Session = API_Request.Session;
                                    user.Username = Txt_Username.Text;
                                    user.Password = Txt_password.Text;
                                    user.Status = "Active";

                                    Classes.DataUserLoginList.Add(user);
                                    SQL_Commander.Insert_Login_Credentials(user);

                                    //Messeges is login user 
                                    Hamburg_Page.IsLogin = true;

                                    //Check User Status and Get data
                                    SQL_Commander.Get_data_Login_Credentials();
                                    var s = Classes.SeconderyListPage.FirstOrDefault(a => a.Name_page == AppResources.Label_Login);
                                    if (s != null)
                                    {
                                        s.Name_page = AppResources.Label_Logout;
                                        s.Icon_page = IoniciconsFont.LogOut;
                                    }

                                    UserDialogs.Instance.HideLoading();
                                    await Navigation.PopModalAsync(true);
                                }
                                catch (Exception exception)
                                {
                                    exception.ToString();
                                    await Navigation.PopAsync();
                                }
                            }
                            else if (successtype.Contains("verification"))
                            {
                                successtype = data["message"].ToString();

                                //await DisplayAlert("Error registered", successtype, "OK");
                                UserDialogs.Instance.ShowError(successtype);
                            }
                        }
                        else if (ApiStatus == "400")
                        {
                            JObject errors = JObject.FromObject(data["errors"]);
                            var error_id = errors["error_id"].ToString();
                            var error_text = errors["error_text"].ToString();

                            UserDialogs.Instance.HideLoading();
                            await DisplayAlert("Error", error_text, "OK");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
                await DisplayAlert(AppResources.Label_Error, AppResources.Label_Session_Dead, AppResources.Label_OK);
                UserDialogs.Instance.HideLoading();
            }
        }

        //Click Button Login
        private async void Btn_Login_OnClicked(object sender, EventArgs e)
        {
            var loginPage = new Login_Page();
            try
            {
                await Navigation.PushAsync(loginPage);
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
                await Navigation.PushModalAsync(loginPage);
            }
        }

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
    }
}