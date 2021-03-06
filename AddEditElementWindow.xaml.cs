﻿using System;
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
	public enum ElementWindowAction
	{
		Add,
		Edit,
		None
	}

    /// <summary>
    /// Логика взаимодействия для AddEditElementWindow.xaml
    /// </summary>
    public partial class AddEditElementWindow : Window
    {
		private IModelEditor _viewModel;

		private ElementWindowAction _action;

		public ElementWindowAction Action { get => _action; }

		public string ModelType { get; private set; }

        public AddEditElementWindow()
        {
            InitializeComponent();
        }

		public AddEditElementWindow(ElementWindowAction action) : this()
		{
			_action = action;
		}

		public AddEditElementWindow(ElementWindowAction action, string modelType) : this(action)
		{
			ModelType = modelType;

			foreach (var column in ModelDisplayGrid.ColumnDefinitions)
			{
				column.Width = new GridLength(0, GridUnitType.Star);
			}

			switch (modelType)
			{
				case "body":
				{
					ModelDisplayGrid.ColumnDefinitions[0].Width = new GridLength(1, GridUnitType.Star);
					break;
				}
				case "charger":
				{
					ModelDisplayGrid.ColumnDefinitions[1].Width = new GridLength(1, GridUnitType.Star);
					break;
				}
				case "cooler":
				{
					ModelDisplayGrid.ColumnDefinitions[2].Width = new GridLength(1, GridUnitType.Star);
					break;
				}
				case "cpu":
				{
					ModelDisplayGrid.ColumnDefinitions[3].Width = new GridLength(1, GridUnitType.Star);
					break;
				}
				case "hdd":
				{
					ModelDisplayGrid.ColumnDefinitions[4].Width = new GridLength(1, GridUnitType.Star);
					break;
				}
				case "motherboard":
				{
					ModelDisplayGrid.ColumnDefinitions[5].Width = new GridLength(1, GridUnitType.Star);
					break;
				}
				case "ram":
				{
					ModelDisplayGrid.ColumnDefinitions[6].Width = new GridLength(1, GridUnitType.Star);
					break;
				}
				case "ssd":
				{
					ModelDisplayGrid.ColumnDefinitions[7].Width = new GridLength(1, GridUnitType.Star);
					break;
				}
				case "videocard":
				{
					ModelDisplayGrid.ColumnDefinitions[8].Width = new GridLength(1, GridUnitType.Star);
					break;
				}
			}
		}

		public AddEditElementWindow(ElementWindowAction action, string modelType, IModelEditor viewModel) : this(action, modelType)
		{
			DataContext = viewModel;
			_viewModel = viewModel;
			_viewModel.OnUnsavedChangesClose += OnViewModelUnsavedChangesCloseAttempt;
			_viewModel.OnModelEditClosed += OnViewModelEditorClosed;
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
