using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using LIP.Droid;
using LIP;
using Android.Content;
using LIP.CustomRenders;

[assembly: ExportRenderer(typeof(MyButton), typeof(MyButtonRenderer))]
namespace LIP.Droid
{
   public class MyButtonRenderer : ButtonRenderer
   {
        public MyButtonRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                //ControlTemplate(global::Android.Graphics.Color.Transparent);
                Control.SetLines(0);
                Control.SetMaxLines(1);

            }
        }
    }
}