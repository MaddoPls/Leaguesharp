using System.Diagnostics;
using System.Windows;

namespace Troubleshooter
{
	public partial class ErrorStartWindow
	{
		public ErrorStartWindow()
		{
			InitializeComponent();
		}

		private void button_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void button1_Click(object sender, RoutedEventArgs e)
		{
			Process.Start("https://bitbucket.org/HyunMi/leaguesharp/issues");
		}
	}
}
