using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ViewModel;

namespace ComputerComplectorAdministrativeTool
{
	/// <summary>
	/// Логика взаимодействия для LoginWindow.xaml
	/// </summary>
	public partial class LoginWindow : Window
	{
		public LoginWindow()
		{
			InitializeComponent();

			DataContext = new LoginViewModel(Properties.Settings.Default.AuthentificationConnectionString);

			if (DataContext is LoginViewModel model)
			{
				password.PasswordChanged += (sender, args) =>
				{
					if (sender is PasswordBox pass)
					{
						model.Password = pass.Password;
					}
				};

				model.PropertyChanged += (sender, property) =>
				{
					if (property.PropertyName == "IsAuthorized" && model.IsAuthorized)
					{
						IsSuccess = true;

						MainWindow window = new MainWindow();

						if (model.User.Role.ToUpper() == "ADMIN" ||
							model.User.Role.ToUpper() == "MODER")
						{
							window.User = model.User;
							Application.Current.MainWindow = window;
							Close();
						}
						else
						{
							model.Error = "Access denied. You shall not pass [user]!";

							var row = body.RowDefinitions[2];

							if (model.Error == "")
							{
								row.Height = new GridLength(0);
							}
							else
							{
								row.Height = new GridLength(1, GridUnitType.Star);
							}
						}
					}
					else if (property.PropertyName == "Error")
					{
						var row = body.RowDefinitions[2];

						if (model.Error == "")
						{
							row.Height = new GridLength(0);
						}
						else
						{
							row.Height = new GridLength(1, GridUnitType.Star);
						}
					}
				};
			}
		}

		public bool IsSuccess { get; private set; } = false;

		private void CloseBtn_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void MinimizeBtn_Click(object sender, RoutedEventArgs e)
		{
			WindowState = WindowState.Minimized;
		}

		protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
		{
			base.OnMouseLeftButtonDown(e);

			// Begin dragging the window
			this.DragMove();
		}
	}
}
