
using Android.Content;
using Android.Views;
using Xamarin.Forms.Platform.Android;
using LIP.CustomRenders;
using Xamarin.Forms;
using Android.Graphics.Drawables;
using LIP.Droid;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(CustomViewCell), typeof(ExtendedViewCellRenderer))]
namespace LIP.Droid
{
    class ExtendedViewCellRenderer:ViewCellRenderer
    {

        private Android.Views.View _cellCore;
        private Drawable _unselectedBackground;
        private bool _selected;

        protected override Android.Views.View GetCellCore(Cell item,
                                                          Android.Views.View convertView,
                                                          ViewGroup parent,
                                                          Context context)
        {

            _cellCore = base.GetCellCore(item, convertView, parent, context);

            _selected = false;
            //_unselectedBackground = _cellCore.Background;

            return _cellCore;
        }

        protected override void OnCellPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            base.OnCellPropertyChanged(sender, args);

            if (args.PropertyName == "IsSelected")
            {
                _selected = !_selected;

                if (_selected)
                {
                    var extendedViewCell = sender as CustomViewCell;
                    _cellCore.SetBackgroundColor(extendedViewCell.SelectedBackgroundColor.ToAndroid());
                }
                else
                {
                    //_cellCore.SetBackground(_unselectedBackground);
                    var extendedViewCell = sender as CustomViewCell;
                    _cellCore.SetBackgroundColor(extendedViewCell.BackgroundColor.ToAndroid());

                }
            }
        }
    }
}