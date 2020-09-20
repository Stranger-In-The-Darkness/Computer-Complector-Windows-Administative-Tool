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
using ViewModel.Interfaces;

namespace ComputerComplectorAdministrativeTool
{
    /// <summary>
    /// Логика взаимодействия для AddEditElementWindow.xaml
    /// </summary>
    public partial class AddEditRuleWindow : Window
    {
		private IModelEditor _viewModel;

		private string[] _relations;

		private Tuple<string, string>[] _firstComponentDescription;

		private Tuple<string, string>[] _secondComponentDescription;

		private ElementWindowAction _action;

		public ElementWindowAction Action { get => _action; }

		public string ModelType { get; private set; }

        public AddEditRuleWindow()
        {
            InitializeComponent();
        }

		public AddEditRuleWindow(ElementWindowAction action) : this()
		{
			_action = action;
		}

		public AddEditRuleWindow(ElementWindowAction action, IModelEditor viewModel) : this(action)
		{
			DataContext = viewModel;
			_viewModel = viewModel;
			_viewModel.OnUnsavedChangesClose += OnViewModelUnsavedChangesCloseAttempt;
			_viewModel.OnModelEditClosed += OnViewModelEditorClosed;
		}

		public AddEditRuleWindow(ElementWindowAction action, IModelEditor viewModel, string[] relations, 
			Tuple<string, string>[] firstComponentDescription, Tuple<string, string>[] secondComponentDescription) : this(action, viewModel)
		{
			_relations = relations;
			_firstComponentDescription = firstComponentDescription;
			_secondComponentDescription = secondComponentDescription;

			FirstComponentProperties.ItemsSource = _firstComponentDescription;
			SecondComponentProperties.ItemsSource = _secondComponentDescription;
			Relations.ItemsSource = _relations;
		}

		private void OnViewModelEditorClosed()
		{
			Close();
		}

		private void OnViewModelUnsavedChangesCloseAttempt()
		{
			var res = ShowSaveChangesDialog();
			switch (res)
			{
				case MessageBoxResult.Yes:
				{
					_viewModel.ApplyChanges.Execute(null);
					_viewModel.CloseModelEdit.Execute(null);
					break;
				}
				case MessageBoxResult.No:
				{
					_viewModel.DeclineChanges.Execute(null);
					_viewModel.CloseModelEdit.Execute(null);
					break;
				}
				case MessageBoxResult.Cancel:
				{
					return;
				}
			}
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

		private MessageBoxResult ShowSaveChangesDialog()
		{
			MessageBoxButton buttons = MessageBoxButton.YesNoCancel;
			string message = "You have unsaved changhes. Save them?";
			string caption = "Unsaved changes!";

			MessageBoxResult res = MessageBox.Show(message, caption, buttons);
			return res;
		}
	}
}
