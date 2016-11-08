﻿using Xamarin.Forms;

namespace RadacodeTestForms
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			MainPage = new NavigationPage(new MainPageView());
		}
		public static Page GetMainPage()
		{
			return new NavigationPage(new MainPageView());
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
