using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ComputerComplectorAdministrativeTool
{
	public class ListOfStringGenericToSymbolSeparatedStringConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null)
			{
				return "";
			}
			if(!(value is List<string>))
			{
				throw new ArgumentException("Value is not a list of strings!", "value");
			}
			if (parameter == null || !(parameter is string))
			{
				throw new ArgumentNullException("parameter", "Parameter cannot be null!");
			}

			List<string> list = value as List<string>;

			string res = string.Join(parameter as string, list);

			return res;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value", "Value cannot be null!");
			}
			if (!(value is string))
			{
				throw new ArgumentException("Value is not a string!", "value");
			}
			if (parameter == null)
			{
				throw new ArgumentNullException("parameter", "Parameter cannot be null!");
			}
			if (!(parameter is string))
			{
				throw new ArgumentException("Parameter is not a string!", "parameter");
			}
			List<string> res = (value as string).Split(new string[] { parameter as string }, StringSplitOptions.None).ToList();
			return res;
		}
	}
}
