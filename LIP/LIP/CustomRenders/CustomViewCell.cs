using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace LIP.CustomRenders
{
   public class CustomViewCell:ViewCell
    {
        public static readonly BindableProperty SelectedBackgroundColorProperty =
            BindableProperty.Create("SelectedBackgroundColor",
                                    typeof(Color),
                                    typeof(CustomViewCell),
                                    Color.Default);
        public static readonly BindableProperty BackgroundColorProperty =
            BindableProperty.Create("BackgroundColor",
                                    typeof(Color),
                                    typeof(CustomViewCell),
                                    Color.Default);



        public Color SelectedBackgroundColor
        {
            get { return (Color)GetValue(SelectedBackgroundColorProperty); }
            set { SetValue(SelectedBackgroundColorProperty, value); }
        }
        public Color BackgroundColor
        {
            get { return (Color)GetValue(BackgroundColorProperty); }
            set { SetValue(BackgroundColorProperty, value); }
        }
    }
}



