using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using LIP.Droid;
using LIP;
using Android.Content;
using LIP.CustomRenders;

[assembly: ExportRenderer(typeof(MyEntry), typeof(MyEntryRenderer))]
namespace LIP.Droid
{
    class MyEntryRenderer : EntryRenderer
    {
        public MyEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.SetBackgroundColor(global::Android.Graphics.Color.Transparent);
                
            }
        }
    }
}

