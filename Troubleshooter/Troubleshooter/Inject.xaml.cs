/*
Troubleshooter for Leaguesharp
Copyright (C) 2015  HyunMi

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using System.Diagnostics;
using System.Windows;

namespace Troubleshooter
{
	public partial class Inject
	{
		public Inject()
		{
			InitializeComponent();
		}

		private void button2_Click(object sender, RoutedEventArgs e)
		{
			Process.Start("https://www.joduska.me/forum/store/");
		}

		private void button1_Click(object sender, RoutedEventArgs e)
		{
			Process.Start("https://www.joduska.me/forum/index.php?app=core&module=usercp&tab=leaguesharp");
		}

		private void button_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}
	}
}
