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

namespace CustomTextboxControl
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			try
			{
				this.Icon = new BitmapImage(new Uri("D:\\Users\\wwwbl\\source\\repos\\WPF\\ico\\text_box.ico"));
			}
			catch {}

			this.Loaded += (s, e) => box1.txtInput.Focus();
		}

		private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				UIElement elementWithFocus = Keyboard.FocusedElement as UIElement;
				if (elementWithFocus != null)
				{
					elementWithFocus.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
				}
				e.Handled = true;
			}
		}
	}
}