using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Win32;

namespace Notepad
{
	public partial class MainWindow : Window
	{
		private string currentFilePath = string.Empty;
		private bool isTextChanged = false;
		private bool wordWrapEnabled = false;
		private bool statusBarVisible = true;

		public MainWindow()
		{
			InitializeComponent();
			MainTextBox.TextChanged += MainTextBox_TextChanged;
			MainTextBox.SelectionChanged += MainTextBox_SelectionChanged;
			UpdateCursorPosition();
			this.Closing += MainWindow_Closing;
		}

		private void MainTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			isTextChanged = true;
			UpdateTitle();
		}

		private void MainTextBox_SelectionChanged(object sender, RoutedEventArgs e)
		{
			UpdateCursorPosition();
		}

		private void UpdateCursorPosition()
		{
			if (MainTextBox != null && statusBarVisible)
			{
				int line = MainTextBox.GetLineIndexFromCharacterIndex(MainTextBox.CaretIndex) + 1;
				int column = MainTextBox.CaretIndex - MainTextBox.GetCharacterIndexFromLineIndex(line - 1) + 1;
				CursorPosition.Text = $"Стр {line}, Стлб {column}";
			}
		}

		private void UpdateTitle()
		{
			string title = "Notepad";
			if (!string.IsNullOrEmpty(currentFilePath))
			{
				title = System.IO.Path.GetFileName(currentFilePath) + (isTextChanged ? "*" : "") + " - Notepad";
			}
			else
			{
				title = "Безымянный" + (isTextChanged ? "*" : "") + " - Notepad";
			}
			this.Title = title;
		}

		// Файл
		private void NewFile_Click(object sender, RoutedEventArgs e)
		{
			if (CheckSaveChanges())
			{
				MainTextBox.Clear();
				currentFilePath = string.Empty;
				isTextChanged = false;
				UpdateTitle();
				StatusText.Text = "Новый файл создан";
			}
		}

		private void OpenFile_Click(object sender, RoutedEventArgs e)
		{
			if (CheckSaveChanges())
			{
				OpenFileDialog openDialog = new OpenFileDialog();
				openDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
				openDialog.FilterIndex = 1;

				if (openDialog.ShowDialog() == true)
				{
					try
					{
						MainTextBox.Text = File.ReadAllText(openDialog.FileName, Encoding.UTF8);
						currentFilePath = openDialog.FileName;
						isTextChanged = false;
						UpdateTitle();
						StatusText.Text = $"Открыт файл: {openDialog.FileName}";
					}
					catch (Exception ex)
					{
						MessageBox.Show($"Ошибка при открытии файла: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
					}
				}
			}
		}

		private void SaveFile_Click(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrEmpty(currentFilePath))
			{
				SaveAsFile_Click(sender, e);
			}
			else
			{
				SaveFile(currentFilePath);
			}
		}

		private void SaveAsFile_Click(object sender, RoutedEventArgs e)
		{
			SaveFileDialog saveDialog = new SaveFileDialog();
			saveDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
			saveDialog.FilterIndex = 1;
			saveDialog.FileName = string.IsNullOrEmpty(currentFilePath) ? "Безымянный.txt" : System.IO.Path.GetFileName(currentFilePath);

			if (saveDialog.ShowDialog() == true)
			{
				SaveFile(saveDialog.FileName);
			}
		}

		private void SaveFile(string filePath)
		{
			try
			{
				File.WriteAllText(filePath, MainTextBox.Text, Encoding.UTF8);
				currentFilePath = filePath;
				isTextChanged = false;
				UpdateTitle();
				StatusText.Text = $"Файл сохранен: {filePath}";
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Ошибка при сохранении файла: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private void Exit_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private bool CheckSaveChanges()
		{
			if (isTextChanged)
			{
				MessageBoxResult result = MessageBox.Show(
					"Сохранить изменения в файле?",
					"Блокнот",
					MessageBoxButton.YesNoCancel,
					MessageBoxImage.Question);

				if (result == MessageBoxResult.Yes)
				{
					SaveFile_Click(null, null);
					return true;
				}
				else if (result == MessageBoxResult.No)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			return true;
		}

		private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			e.Cancel = !CheckSaveChanges();
		}

		// Правка
		private void Undo_Click(object sender, RoutedEventArgs e)
		{
			if (MainTextBox.CanUndo)
			{
				MainTextBox.Undo();
			}
		}

		private void Cut_Click(object sender, RoutedEventArgs e)
		{
			MainTextBox.Cut();
		}

		private void Copy_Click(object sender, RoutedEventArgs e)
		{
			MainTextBox.Copy();
		}

		private void Paste_Click(object sender, RoutedEventArgs e)
		{
			MainTextBox.Paste();
		}

		private void Delete_Click(object sender, RoutedEventArgs e)
		{
			MainTextBox.SelectedText = "";
		}

		private void SelectAll_Click(object sender, RoutedEventArgs e)
		{
			MainTextBox.SelectAll();
		}

		private void TimeDate_Click(object sender, RoutedEventArgs e)
		{
			MainTextBox.SelectedText = DateTime.Now.ToString("HH:mm dd.MM.yyyy");
		}

		// Формат
		private void WordWrap_Click(object sender, RoutedEventArgs e)
		{
			wordWrapEnabled = !wordWrapEnabled;
			MainTextBox.TextWrapping = wordWrapEnabled ? TextWrapping.Wrap : TextWrapping.NoWrap;
			((MenuItem)sender).IsChecked = wordWrapEnabled;
		}

		private void Font_Click(object sender, RoutedEventArgs e)
		{
			
			var fontDialog = new FontDialog();
			fontDialog.Owner = this;

			
			fontDialog.SetCurrentFont(new FontFamily(MainTextBox.FontFamily.ToString()), MainTextBox.FontSize);

			if (fontDialog.ShowDialog() == true)
			{
				// Выбраный шрифт
				MainTextBox.FontFamily = fontDialog.SelectedFontFamily;
				MainTextBox.FontSize = fontDialog.SelectedFontSize;
			}
		}

		// Вид
		private void StatusBar_Click(object sender, RoutedEventArgs e)
		{
			statusBarVisible = !statusBarVisible;
			MainStatusBar.Visibility = statusBarVisible ? Visibility.Visible : Visibility.Collapsed;
			((MenuItem)sender).IsChecked = statusBarVisible;
		}

		// Справка
		private void Help_Click(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("Это простой текстовый редактор, созданный для демонстрации.\n\nГорячие клавиши:\nCtrl+N - Новый\nCtrl+O - Открыть\nCtrl+S - Сохранить\nF5 - Время и дата",
				"Справка", MessageBoxButton.OK, MessageBoxImage.Information);
		}

		private void About_Click(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("Блокнот\nВерсия 1.0\n\nПростой текстовый редактор на WPF",
				"О программе", MessageBoxButton.OK, MessageBoxImage.Information);
		}
	}
}