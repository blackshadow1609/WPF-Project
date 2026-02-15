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

namespace ListBox
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

		private void btnAdd_Click(object sender, RoutedEventArgs e)
		{
			if(!listBox.Items.Contains(txtInput.Text))
			listBox.Items.Add(txtInput.Text);
		}

		private void btnClear_Click(object sender, RoutedEventArgs e)
		{
			listBox.Items.Clear();
		}

		private void btnDelete_Click(object sender, RoutedEventArgs e)
		{
			listBox.Items.Remove(listBox.SelectedItem);
		}
	}
}
