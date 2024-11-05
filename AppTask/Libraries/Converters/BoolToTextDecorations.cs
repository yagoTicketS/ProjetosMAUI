using System.Globalization;

namespace AppTask.Libraries.Converters
{
	public class BoolToTextDecorations : IValueConverter
	{
		public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
		{
			if (value is bool isCompleted)
			{
				return isCompleted ? TextDecorations.Strikethrough : TextDecorations.None;
			}

			return TextDecorations.None;
		}

		public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
