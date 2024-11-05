using AppTask.Views;
using Microsoft.Maui.Platform;

namespace AppTask
{
	public partial class App : Application
	{
		public App()
		{
			CustomHandler();

			InitializeComponent();

			MainPage = new NavigationPage(root: new StartPage());
		}

		private void CustomHandler()
		{
			EntryNoBorder();
			DatePickerNoBorder();
		}

		private static void EntryNoBorder()
		{
			Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("NoBorder", (handler, view) =>
			{
#if ANDROID
				//ANDROID
				handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Colors.Transparent.ToPlatform());
#endif

#if IOS || MACCATALYST
				//IOS || MAC
				handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
#endif
#if WINDOWS
				//WINDOWS
				handler.PlatformView.BorderThickness = new Thickness(0).ToPlatform();
#endif
			});
		}

		private static void DatePickerNoBorder()
		{
			Microsoft.Maui.Handlers.DatePickerHandler.Mapper.AppendToMapping("NoBorder", (handler, view) =>
			{
#if ANDROID
				//ANDROID
				handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Colors.Transparent.ToPlatform());
#endif

#if IOS || MACCATALYST
				//IOS || MAC
				if (handler.PlatformView.Subviews.FirstOrDefault(x => x is UIKit.UITextField) is UIKit.UITextField textField)
				{
					textField.BorderStyle = UIKit.UITextBorderStyle.None;
				}
#endif
#if WINDOWS
				//WINDOWS
				handler.PlatformView.BorderThickness = new Thickness(0).ToPlatform();
#endif
			});
		}
	}
}
