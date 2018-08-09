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

			//MainPage = new NavigationPage(new MainPage());
            MainPage = new NavigationPage(new LoginPage());
            Acr.UserDialogs.UserDialogs.Init(() => (Android.App.Activity)Forms.Context);


        }

		protected override void OnStart ()
		{
			// Handle when your app starts
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
