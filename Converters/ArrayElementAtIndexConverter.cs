using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ComputerComplectorAdministrativeTool
{
	public class ArrayElementAtIndexConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (parameter is int index)
			{
				switch (targetType.Name)
				{
					case "String":
					{
						if (value is string[] list)
						{
							try
							{
								return list[index];
							}
							catch (IndexOutOfRangeException)
							{
								return null;
							}
						}
						return null;
					}
					default:
					{
						return null;
					}
				}
			}
			return null;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
