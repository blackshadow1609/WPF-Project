using Microsoft.Win32;
using WinForms = System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Design;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dialogs
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void btnMessageBox_Click(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("This is message box", "Caption", MessageBoxButton.OK, MessageBoxImage.Information);

		}

		private void btnOpenFile_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog dialog = new OpenFileDialog();
			dialog.ShowDialog();
			if (!string.IsNullOrWhiteSpace (dialog.FileName))
			{
				MessageBox.Show(dialog.FileName, "File name", MessageBoxButton.OK, MessageBoxImage.Information);
			}
		}

		private void btnOpenFolder_Click(object sender, RoutedEventArgs e)
		{
			WinForms.FolderBrowserDialog dialog = new WinForms.FolderBrowserDialog();
			dialog.ShowDialog();
			MessageBox.Show(dialog.SelectedPath, "Folder", MessageBoxButton.OK, MessageBoxImage.Information);
		}
	}
}
