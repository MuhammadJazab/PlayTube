using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using PlayTube.Controls;
using Plugin.Connectivity;
using Refractored.XamForms.PullToRefresh;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PlayTube.Pages.Default
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Comment_Page : ContentPage
    {

        #region Event Refresh

        public class RefreshMVVM : INotifyPropertyChanged
        {
            public ObservableCollection<string> Items { get; set; }
            Comment_Page pageChosen;
            public RefreshMVVM(Comment_Page page)
            {
                this.pageChosen = page;
                Items = new ObservableCollection<string>();
            }

            bool isBusy;

            public bool IsBusy
            {
                get { return isBusy; }
                set
                {
                    if (isBusy == value)
                        return;

                    isBusy = value;
                    OnPropertyChanged("IsBusy");
                }
            }

            ICommand refreshCommand;

            public ICommand RefreshCommand
            {
                get { return refreshCommand ?? (refreshCommand = new Command(async () => await ExecuteRefreshCommand())); }
            }

            async Task ExecuteRefreshCommand()
            {
                if (IsBusy)
                {
                    return;
                }

                IsBusy = true;

                //Run code
                pageChosen.Add_Comment(pageChosen.Video_id);

                IsBusy = false;
            }

            #region INotifyPropertyChanged implementation

            public event PropertyChangedEventHandler PropertyChanged;

            #endregion

            public void OnPropertyChanged(string propertyName)
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        private string Video_id;
        public Comment_Page(string Videoid )
        {
            try
            {
                InitializeComponent();

                BindingContext = new RefreshMVVM(this);
                PullToRefreshLayoutView.SetBinding<RefreshMVVM>(PullToRefreshLayout.IsRefreshingProperty, vm => vm.IsBusy, BindingMode.OneWay);
                PullToRefreshLayoutView.SetBinding<RefreshMVVM>(PullToRefreshLayout.RefreshCommandProperty, vm => vm.RefreshCommand);

                Video_id = Videoid;

                if (Settings.Show_ADMOB_On_Timeline)
                {
                    if (Device.OS == TargetPlatform.iOS)
                    {
                        AdmobBanner.IsVisible = false;
                        GridHieght.RowDefinitions[2].Height = 0;
                    }
                    else
                    {
                        AdmobBanner.IsVisible = true;
                    }
                }
                else
                {
                    AdmobBanner.IsVisible = false;
                    GridHieght.RowDefinitions[2].Height = 0;
                }

                Add_Comment(Video_id);
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }


        public async void Add_Comment(string video_id)
        {
            try
            {
                if (!String.IsNullOrEmpty(API_Request.Cookie))
                {
                    if (!CrossConnectivity.Current.IsConnected)
                    {
                        // await DisplayAlert(AppResources.Label_Error, AppResources.Label_Check_Your_Internet, AppResources.Label_OK);

                        CommentWebLoader.IsVisible = false;

                        OfflinePage.IsVisible = true;
                        AdmobBanner.IsVisible = false;
                        GridHieght.RowDefinitions.Clear();
                        RowDefinition ff = new RowDefinition();
                        ff.Height = 600;
                        GridHieght.RowDefinitions.Add(ff);
                    }
                    else
                    {
                        PullToRefreshLayoutView.IsRefreshing = true;
                        OfflinePage.IsVisible = false;
                        CommentWebLoader.IsVisible = true;

                        CommentWebLoader.Source = Settings.WebsiteUrl + "/get-video-comments?video_id="+ video_id + "&cookie=" + API_Request.Cookie;
                    }
                }
                else
                {
                    CommentWebLoader.IsVisible = false;

                    OfflinePage.IsVisible = true;
                    AdmobBanner.IsVisible = false;
                    GridHieght.RowDefinitions.Clear();
                    RowDefinition ff = new RowDefinition();
                    ff.Height = 600;
                    GridHieght.RowDefinitions.Add(ff);
                }
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }


        private void CommentWebLoader_OnOnContentLoaded(object sender, System.EventArgs e)
        {
            try
            {
                PullToRefreshLayoutView.IsRefreshing = false;
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }

        private void Btn_Try_Again_OnClicked(object sender, System.EventArgs e)
        {
            try
            {
                Add_Comment(Video_id);
            }
            catch (Exception ex)
            {
                var exception = ex.ToString();
            }
        }
    }
}