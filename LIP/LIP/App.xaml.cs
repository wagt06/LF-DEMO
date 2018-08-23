using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace LIP
{
	public partial class App : Application
	{
        public App ()
		{
			InitializeComponent();

            NavigationPage Nav = new NavigationPage(new LoginPage());
            Nav.BarBackgroundColor = Color.FromRgb(53, 75, 96);
            Nav.Popped += (sender, e) => {
            };
            //MainPage = new NavigationPage(new MainPage());
            MainPage = Nav;
            Acr.UserDialogs.UserDialogs.Init(() => (Android.App.Activity)Forms.Context);
     
        }

		protected override void OnStart ()
		{
            // Handle when your app starts
            Localization.Current.CurrentCultureInfo = Localization.Current.GetCultureInfo("es-NI");

        }

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
