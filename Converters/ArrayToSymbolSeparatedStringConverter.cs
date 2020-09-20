using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ComputerComplectorAdministrativeTool
{
	public class ArrayToSymbolSeparatedStringConverter : IMultiValueConverter
	{
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			if (values == null || values.Length == 0)
			{
				throw new ArgumentException("Invalid argument", "values");
			}
			if (parameter == null || !(parameter is string) || (parameter as string).Length == 0)
			{
				throw new ArgumentException("Invalid argument", "parameter");
			}
			if (culture == null)
			{
				throw new ArgumentNullException("Invalid argument", "parameter");
			}
			string res = string.Join(parameter as string, values);
			return res;
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			if (value == null || !(value is string) || (value as string).Length == 0)
			{
				throw new ArgumentException("Invalid argument", "values");
			}
			if (parameter == null || !(parameter is string) || (parameter as string).Length == 0)
			{
				throw new ArgumentException("Invalid argument", "parameter");
			}
			if (culture == null)
			{
				throw new ArgumentNullException("Invalid argument", "parameter");
			}
			string[] res = (value as string).Split(new string[] { parameter as string }, StringSplitOptions.None);
			return res;
		}
	}
}
