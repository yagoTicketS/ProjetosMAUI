﻿using AppMAUIGallery.Views;

namespace AppMAUIGallery
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			MainPage = new AppFlyout();
		}
	}
}
