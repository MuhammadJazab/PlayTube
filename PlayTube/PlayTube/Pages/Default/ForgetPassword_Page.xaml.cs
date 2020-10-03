using System;
using System.Collections.Generic;
using System.Net.Http;
using Acr.UserDialogs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PlayTube.Controls;
using PlayTube.Languish;
using Plugin.Connectivity;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PlayTube.Pages.Default
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForgetPassword_Page : ContentPage
    {
        public ForgetPassword_Page()
        {
            try
            {
                InitializeComponent();

                NavigationPage.SetHasNavigationBar(this, false);

            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        private async void Btn_Send_OnClicked(object sender, EventArgs e)
        {
            try
            {
                if (!CrossConnectivity.Current.IsConnected)
                {
                    await DisplayAlert(AppResources.Label_Error, AppResources.Label_Check_Your_Internet, AppResources.Label_OK);
                }
                else
                {
                    try
                    {
                        using (var client = new HttpClient())
                        {
                            if (!Txt_Email.Text.Contains("@"))
                            {
                                await DisplayAlert(AppResources.Label_Error, AppResources.Label_Please_Write_your_full_email, AppResources.Label_OK);
                                Txt_Email.Focus();
                            }
                            var formContent = new FormUrlEncodedContent(new[]
                            {
                                new KeyValuePair<string, string>("email", Txt_Email.Text),
                            });

                            var response = await client.PostAsync(Settings.WebsiteUrl + API_Request.API_Reset_password, formContent).ConfigureAwait(false);
                            response.EnsureSuccessStatusCode();
                            string json = await response.Content.ReadAsStringAsync();
                            var data = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
                            string apiStatus = data["api_status"].ToString();
                            if (apiStatus == "200")
                            {
                                UserDialogs.Instance.Toast(AppResources.Label_Email_Has_Been_Send);
                            }
                            else
                            {
                                JObject errors = JObject.FromObject(data["errors"]);
                                var errortext = errors["error_text"].ToString();
                                await DisplayAlert(AppResources.Label_Security, errortext, AppResources.Label_Retry);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        var exception = ex.ToString();
                        UserDialogs.Instance.HideLoading();

                        await DisplayAlert(AppResources.Label_Error, exception, AppResources.Label_OK);
                    }

                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }
    
        //Click Icon this close
        private void OnCloseButtonClicked(object sender, EventArgs e)
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