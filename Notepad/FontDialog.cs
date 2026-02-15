using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Notepad
{
	public class FontDialog
	{
		public FontFamily SelectedFontFamily { get; private set; }
		public double SelectedFontSize { get; private set; }
		public Window Owner { get; set; }

		public void SetCurrentFont(FontFamily fontFamily, double fontSize)
		{
			SelectedFontFamily = fontFamily;
			SelectedFontSize = fontSize;
		}

		public bool? ShowDialog()
		{
			// Окно для выбора шрифта
			Window fontWindow = new Window
			{
				Title = "Выбор шрифта",
				Width = 400,
				Height = 300,
				WindowStartupLocation = WindowStartupLocation.CenterOwner,
				Owner = Owner
			};

			var grid = new Grid();
			grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			grid.Margin = new Thickness(10);

			// Список шрифтов
			var fontLabel = new Label { Content = "Шрифт:", Margin = new Thickness(0, 5, 0, 0) };
			Grid.SetRow(fontLabel, 0);
			grid.Children.Add(fontLabel);

			var fontCombo = new ComboBox { Margin = new Thickness(0, 25, 0, 0) };
			foreach (var fontFamily in Fonts.SystemFontFamilies)
			{
				fontCombo.Items.Add(fontFamily.Source);
			}
			fontCombo.SelectedItem = SelectedFontFamily?.Source;
			Grid.SetRow(fontCombo, 0);
			grid.Children.Add(fontCombo);

			// Размер шрифта
			var sizeLabel = new Label { Content = "Размер:", Margin = new Thickness(0, 55, 0, 0) };
			Grid.SetRow(sizeLabel, 0);
			grid.Children.Add(sizeLabel);

			var sizeCombo = new ComboBox
			{
				Margin = new Thickness(0, 75, 0, 0),
				IsEditable = true,
				Text = SelectedFontSize.ToString()
			};
			int[] fontSizes = { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };
			foreach (var size in fontSizes)
			{
				sizeCombo.Items.Add(size);
			}
			Grid.SetRow(sizeCombo, 0);
			grid.Children.Add(sizeCombo);

			// Кнопки
			var buttonPanel = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Right, Margin = new Thickness(0, 150, 0, 0) };
			Grid.SetRow(buttonPanel, 2);

			var okButton = new Button { Content = "OK", Width = 75, Height = 25, Margin = new Thickness(0, 0, 10, 0) };
			okButton.Click += (s, e) =>
			{
				SelectedFontFamily = new FontFamily(fontCombo.SelectedItem?.ToString() ?? "Consolas");
				if (double.TryParse(sizeCombo.Text, out double size))
				{
					SelectedFontSize = size;
				}
				else
				{
					SelectedFontSize = 14;
				}
				fontWindow.DialogResult = true;
				fontWindow.Close();
			};

			var cancelButton = new Button { Content = "Отмена", Width = 75, Height = 25 };
			cancelButton.Click += (s, e) =>
			{
				fontWindow.DialogResult = false;
				fontWindow.Close();
			};

			buttonPanel.Children.Add(okButton);
			buttonPanel.Children.Add(cancelButton);
			grid.Children.Add(buttonPanel);

			fontWindow.Content = grid;
			return fontWindow.ShowDialog();
		}
	}
}