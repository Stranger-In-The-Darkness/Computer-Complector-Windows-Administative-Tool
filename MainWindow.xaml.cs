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
using Model.Models.Data;

using ViewModel;
using ViewModel.Events.Arguments;

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
				Properties.Settings.Default.APIURIString,
				Properties.Settings.Default.ComponentsRequestFormat,
				Properties.Settings.Default.StatisticsRequestFormat,
				Properties.Settings.Default.Culture);

			DataContext = _viewModel;

			_viewModel.OnBeginAddingEvent += BeginAddingElementEventHandler;
			_viewModel.OnBeginEditingEvent += BeginEditingElementEventHandler;
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

		private void BeginAddingElementEventHandler(BeginAddingModelArgs args)
		{
			switch (args.ModelType)
			{
				case "body":
				{
					OpenModelEditWindow(ElementWindowAction.Edit, args.ModelType, new Body());
					break;
				}
				case "charger":
				{
					OpenModelEditWindow(ElementWindowAction.Edit, args.ModelType, new Charger());
					break;
				}
				case "cooler":
				{
					OpenModelEditWindow(ElementWindowAction.Edit, args.ModelType, new Cooler());
					break;
				}
				case "cpu":
				{
					OpenModelEditWindow(ElementWindowAction.Edit, args.ModelType, new CPU());
					break;
				}
				case "hdd":
				{
					OpenModelEditWindow(ElementWindowAction.Edit, args.ModelType, new HDD());
					break;
				}
				case "motherboard":
				{
					OpenModelEditWindow(ElementWindowAction.Edit, args.ModelType, new Motherboard());
					break;
				}
				case "ram":
				{
					OpenModelEditWindow(ElementWindowAction.Edit, args.ModelType, new RAM());
					break;
				}
				case "ssd":
				{
					OpenModelEditWindow(ElementWindowAction.Edit, args.ModelType, new SSD());
					break;
				}
				case "videocard":
				{
					OpenModelEditWindow(ElementWindowAction.Edit, args.ModelType, new Videocard());
					break;
				}
			}
		}

		private void BeginEditingElementEventHandler(BeginEditingModelArgs args)
		{
			switch (args.ModelType)
			{
				case "body":
				{
					OpenModelEditWindow(ElementWindowAction.Edit, args.ModelType, (Body)args.Model);
					break;
				}
				case "charger":
				{
					OpenModelEditWindow(ElementWindowAction.Edit, args.ModelType, (Charger)args.Model);
					break;
				}
				case "cooler":
				{
					OpenModelEditWindow(ElementWindowAction.Edit, args.ModelType, (Cooler)args.Model);
					break;
				}
				case "cpu":
				{
					OpenModelEditWindow(ElementWindowAction.Edit, args.ModelType, (CPU)args.Model);
					break;
				}
				case "hdd":
				{
					OpenModelEditWindow(ElementWindowAction.Edit, args.ModelType, (HDD)args.Model);
					break;
				}
				case "motherboard":
				{
					OpenModelEditWindow(ElementWindowAction.Edit, args.ModelType, (Motherboard)args.Model);
					break;
				}
				case "ram":
				{
					OpenModelEditWindow(ElementWindowAction.Edit, args.ModelType, (RAM)args.Model);
					break;
				}
				case "ssd":
				{
					OpenModelEditWindow(ElementWindowAction.Edit, args.ModelType, (SSD)args.Model);
					break;
				}
				case "videocard":
				{
					OpenModelEditWindow(ElementWindowAction.Edit, args.ModelType, (Videocard)args.Model);
					break;
				}
				case "option":
				{
					break;
				}
			}
		}

		private void ModelEditWindowClosingEventHandler(object sender, System.ComponentModel.CancelEventArgs e)
		{
			var wind = (AddEditElementWindow)sender;
			switch (wind.ModelType)
			{
				case "body":
				{
					var viewModel = (ChangeElementViewModel<Body>) wind.DataContext;

					var model = viewModel.Element;

					ModelEditAction(wind.Action, wind.ModelType, model, model.ID);
					break;
				}
				case "charger":
				{
					var viewModel = (ChangeElementViewModel<Charger>)wind.DataContext;

					var model = viewModel.Element;
					ModelEditAction(wind.Action, wind.ModelType, model, model.ID);
					break;
				}
				case "cooler":
				{
					var viewModel = (ChangeElementViewModel<Cooler>)wind.DataContext;

					var model = viewModel.Element;
					ModelEditAction(wind.Action, wind.ModelType, model, model.ID);
					break;
				}
				case "cpu":
				{
					var viewModel = (ChangeElementViewModel<CPU>)wind.DataContext;

					var model = viewModel.Element;
					ModelEditAction(wind.Action, wind.ModelType, model, model.ID);
					break;
				}
				case "hdd":
				{
					var viewModel = (ChangeElementViewModel<HDD>)wind.DataContext;

					var model = viewModel.Element;
					ModelEditAction(wind.Action, wind.ModelType, model, model.ID);
					break;
				}
				case "motherboard":
				{
					var viewModel = (ChangeElementViewModel<Motherboard>)wind.DataContext;

					var model = viewModel.Element;
					ModelEditAction(wind.Action, wind.ModelType, model, model.ID);
					break;
				}
				case "ram":
				{
					var viewModel = (ChangeElementViewModel<RAM>)wind.DataContext;

					var model = viewModel.Element;
					ModelEditAction(wind.Action, wind.ModelType, model, model.ID);
					break;
				}
				case "ssd":
				{
					var viewModel = (ChangeElementViewModel<SSD>)wind.DataContext;

					var model = viewModel.Element;
					ModelEditAction(wind.Action, wind.ModelType, model, model.ID);
					break;
				}
				case "videocard":
				{
					var viewModel = (ChangeElementViewModel<Videocard>)wind.DataContext;

					var model = viewModel.Element;
					ModelEditAction(wind.Action, wind.ModelType, model, model.ID);
					break;
				}
			}
		}

		private void ModelEditAction(ElementWindowAction action, string modelType, object model, int id)
		{
			switch (action)
			{
				case ElementWindowAction.Add:
				{
					_viewModel.AddElement.Execute(new Tuple<string, object>(modelType, model));
					break;
				}
				case ElementWindowAction.Edit:
				{
					_viewModel.ReplaceElement.Execute(new Tuple<string, int, object>(modelType, id, model));
					break;
				}
			}
		}

		private void OpenModelEditWindow<T>(ElementWindowAction action, string modelType, T model) where T : ICloneable
		{
			ChangeElementViewModel<T> _viewModel = new ChangeElementViewModel<T>(model);
			AddEditElementWindow window = new AddEditElementWindow(ElementWindowAction.Edit, modelType, _viewModel);
			window.Closing += ModelEditWindowClosingEventHandler;

			window.ShowDialog();
		}

		private void EditPropertyButtonClick(object sender, RoutedEventArgs e)
		{
			if (sender is Button button)
			{
				if (button.Tag is KeyValuePair<string, Option> property)
				{
					ChangeElementViewModel<Option> _viewModel = new ChangeElementViewModel<Option>(property.Value);
					EditPropertyWindow window = new EditPropertyWindow(_viewModel);
					window.Closing += PropertyEditWindowClosingEventHandler;

					window.ShowDialog();
				}
			}
		}

		private void PropertyEditWindowClosingEventHandler(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (sender is EditPropertyWindow window)
			{
				if (window.DataContext is ChangeElementViewModel<Option> viewModel)
				{
					_viewModel.ReplaceProperty.Execute(viewModel.Element);
				}
			}
		}

		private void EditRuleButtonClick(object sender, RoutedEventArgs e)
		{

		}
	}
}
