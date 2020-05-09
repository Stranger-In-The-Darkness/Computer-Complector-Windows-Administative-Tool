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
using System.Windows.Navigation;
using System.Windows.Shapes;

using Model;
using ViewModel;

namespace ComputerComplectorAdministrativeTool
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private User _user;
		private AdminViewModel _viewModel;

		public MainWindow()
		{
			InitializeComponent();
			_viewModel = new AdminViewModel(new ViewModel.Models.DefaultDialogService(),
				Properties.Settings.Default.ServiceConnectionString,
				Properties.Settings.Default.Culture);

			DataContext = _viewModel;
		}

		public User User
		{
			get => _user;
			internal set
			{
				_user = value;
				_viewModel.User = _user;
			}
		}

		private void FullscreenBtn_Click(object sender, RoutedEventArgs e)
		{
			WindowState = WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
		}

		private void Expander_Expanded(object sender, RoutedEventArgs e)
		{
		}

		protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
		{
			base.OnMouseLeftButtonDown(e);

			// Begin dragging the window
			this.DragMove();
		}

		private void MinimizeBtn_Click(object sender, RoutedEventArgs e)
		{
			WindowState = WindowState.Minimized;
		}

		private void CloseBtn_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}
	}
}
