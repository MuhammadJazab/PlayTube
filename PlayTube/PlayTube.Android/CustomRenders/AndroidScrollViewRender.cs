using System;
using PlayTube.CustomRenders;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ScrollViewModifiedRender), typeof(PlayTube.Droid.CustomRenders.AndroidScrollViewRender))]
namespace PlayTube.Droid.CustomRenders
{
    public class AndroidScrollViewRender : ScrollViewRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            try
            {
                base.OnElementChanged(e);

                if (e.OldElement != null || this.Element == null)
                    return;

                if (e.OldElement != null)
                    e.OldElement.PropertyChanged -= OnElementPropertyChanged;

                e.NewElement.PropertyChanged += OnElementPropertyChanged;

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
               
            }
           
        }

        protected void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            try
            {
                if (ChildCount > 0)
                {
                    GetChildAt(0).HorizontalScrollBarEnabled = false;
                    GetChildAt(0).VerticalScrollBarEnabled = false;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                
            }
           
        }
    }
}