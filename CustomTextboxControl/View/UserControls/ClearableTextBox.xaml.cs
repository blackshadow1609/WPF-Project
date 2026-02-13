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

namespace CustomTextboxControl.View.UserControls
{
	/// <summary>
	/// Interaction logic for ClearableTextBox.xaml
	/// </summary>
	public partial class ClearableTextBox : UserControl
	{
		private string placeholder;
		public string Placeholder
		{
			get { return placeholder; }
			set { placeholder = tbPlaceholder.Text = value; }
		}

		public ClearableTextBox()
		{
			InitializeComponent();

			btnClear.Click += (s, e) =>
			{
				txtInput.Text = string.Empty;
				txtInput.Focus();
			};
		}

		private void txtInput_TextChanged(object sender, TextChangedEventArgs e)
		{
			tbPlaceholder.Visibility = txtInput.Text == "" ? Visibility.Visible : Visibility.Hidden;

			btnClear.Visibility = txtInput.Text == "" ? Visibility.Collapsed : Visibility.Visible;

			if (txtInput.Text.Length > 16)
			{
				txtInput.Text = txtInput.Text.Substring(0, 16);
				txtInput.CaretIndex = 16;
			}
		}

		private void txtInput_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				UIElement element = e.Source as UIElement;
				if (element != null)
				{
					element.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
				}
				e.Handled = true;
			}
		}
	}
}