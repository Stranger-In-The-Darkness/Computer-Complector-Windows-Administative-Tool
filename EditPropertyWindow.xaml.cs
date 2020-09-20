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
	/// Логика взаимодействия для EditPropertyWindow.xaml
	/// </summary>
	public partial class EditPropertyWindow : Window
	{
		public string ElementType { get; set; }

		private IModelEditor _modelEdit;

		public EditPropertyWindow()
		{
			InitializeComponent();
		}

		public EditPropertyWindow(IModelEditor modelEdit) : this()
		{
			DataContext = modelEdit;
			_modelEdit = modelEdit;
			_modelEdit.OnUnsavedChangesClose += OnViewModelUnsavedChangesCloseAttempt;
			_modelEdit.OnModelEditClosed += OnViewModelEditorClosed;
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
					_modelEdit.ApplyChanges.Execute(null);
					_modelEdit.CloseModelEdit.Execute(null);
					break;
				}
				case MessageBoxResult.No:
				{
					_modelEdit.DeclineChanges.Execute(null);
					_modelEdit.CloseModelEdit.Execute(null);
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
