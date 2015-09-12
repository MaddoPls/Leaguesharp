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

using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Troubleshooter
{
    public partial class ErrorSelectionWindow
    {
		/// <summary>
		/// Constructor
		/// </summary>
        public ErrorSelectionWindow()
        {
			InitializeComponent();
			DelayedButtonDisable();
        }

		private async void DelayedButtonDisable()
		{
			Task delay = new Task(DelayTask);
			delay.Start();
			await delay;
			App.SolveAnOtherProblem.IsEnabled = false;
		}

	    private async void DelayTask()
	    {
			while (App.SolveAnOtherProblem == null)
			{
				await Task.Delay(TimeSpan.FromMilliseconds(100));
			}
		}

	    /// <summary>
		/// Currently selected option, null when nothing is selected
		/// </summary>
		public static string CurrentSelected;

		/// <summary>
		/// Cancel button click event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            App.ErrorSelectionWindow.Close();
        }

		#region checkboxes
		private void cbInstallLinks_Checked(object sender, RoutedEventArgs e)
        {
            DisableAllCheckBoxes();
            CheckBox callie = (CheckBox) sender;
            callie.IsEnabled = true;
            CurrentSelected = callie.Name;
        }

        private void cbInstallLinks_Unchecked(object sender, RoutedEventArgs e)
        {
            EnableAllCheckBoxes();
        }

        private void cbMenu_Checked(object sender, RoutedEventArgs e)
        {
            DisableAllCheckBoxes();
            CheckBox callie = (CheckBox)sender;
            callie.IsEnabled = true;
            CurrentSelected = callie.Name;
        }

        private void cbMenu_Unchecked(object sender, RoutedEventArgs e)
        {
            EnableAllCheckBoxes();
        }

        private void cbOrbwalkStutter_Checked(object sender, RoutedEventArgs e)
        {
            DisableAllCheckBoxes();
            CheckBox callie = (CheckBox)sender;
            callie.IsEnabled = true;
            CurrentSelected = callie.Name;
        }

        private void cbOrbwalkStutter_Unchecked(object sender, RoutedEventArgs e)
        {
            EnableAllCheckBoxes();
        }

        private void cbAuth_Checked(object sender, RoutedEventArgs e)
        {
            DisableAllCheckBoxes();
            CheckBox callie = (CheckBox)sender;
            callie.IsEnabled = true;
            CurrentSelected = callie.Name;
        }

        private void cbAuth_Unchecked(object sender, RoutedEventArgs e)
        {
            EnableAllCheckBoxes();
        }

        private void cbBugSplat_Checked(object sender, RoutedEventArgs e)
        {
            DisableAllCheckBoxes();
            CheckBox callie = (CheckBox)sender;
            callie.IsEnabled = true;
            CurrentSelected = callie.Name;
        }

        private void cbBugSplat_Unchecked(object sender, RoutedEventArgs e)
        {
            EnableAllCheckBoxes();
        }

        private void cbInject_Checked(object sender, RoutedEventArgs e)
        {
            DisableAllCheckBoxes();
            CheckBox callie = (CheckBox)sender;
            callie.IsEnabled = true;
            CurrentSelected = callie.Name;
        }

        private void cbInject_Unchecked(object sender, RoutedEventArgs e)
        {
            EnableAllCheckBoxes();
        }

        private void cbStartError_Checked(object sender, RoutedEventArgs e)
        {
            DisableAllCheckBoxes();
            CheckBox callie = (CheckBox)sender;
            callie.IsEnabled = true;
            CurrentSelected = callie.Name;
        }

        private void cbStartError_Unchecked(object sender, RoutedEventArgs e)
        {
            EnableAllCheckBoxes();
        }

        private void cbDependenciesReinstall_Checked(object sender, RoutedEventArgs e)
        {
            DisableAllCheckBoxes();
            CheckBox callie = (CheckBox)sender;
            callie.IsEnabled = true;
            CurrentSelected = callie.Name;
        }

        private void cbDependenciesReinstall_Unchecked(object sender, RoutedEventArgs e)
        {
            EnableAllCheckBoxes();
        }

        private void cbAppdata_Checked(object sender, RoutedEventArgs e)
        {
            DisableAllCheckBoxes();
            CheckBox callie = (CheckBox)sender;
            callie.IsEnabled = true;
            CurrentSelected = callie.Name;
        }

        private void cbAppdata_Unchecked(object sender, RoutedEventArgs e)
        {
            EnableAllCheckBoxes();
        }

        private void cbReinstallKeepSettings_Checked(object sender, RoutedEventArgs e)
        {
            DisableAllCheckBoxes();
            CheckBox callie = (CheckBox)sender;
            callie.IsEnabled = true;
            CurrentSelected = callie.Name;
        }

        private void cbReinstallKeepSettings_Unchecked(object sender, RoutedEventArgs e)
        {
            EnableAllCheckBoxes();
        }

        private void cbReinstall_Checked(object sender, RoutedEventArgs e)
        {
            DisableAllCheckBoxes();
            CheckBox callie = (CheckBox)sender;
            callie.IsEnabled = true;
            CurrentSelected = callie.Name;
        }

        private void cbReinstall_Unchecked(object sender, RoutedEventArgs e)
        {
            EnableAllCheckBoxes();
        }

        private void cbResetSettings_Checked(object sender, RoutedEventArgs e)
        {
            DisableAllCheckBoxes();
            CheckBox callie = (CheckBox)sender;
            callie.IsEnabled = true;
            CurrentSelected = callie.Name;
        }

        private void cbResetSettings_Unchecked(object sender, RoutedEventArgs e)
        {
            EnableAllCheckBoxes();
        }
		#endregion

		/// <summary>
		/// Disable all checkboxes + set selected option
		/// </summary>
		private void DisableAllCheckBoxes()
        {
            cbInstallLinks.IsEnabled = false;
            cbMenu.IsEnabled = false;
            cbOrbwalkStutter.IsEnabled = false;
            cbAuth.IsEnabled = false;
            cbBugSplat.IsEnabled = false;
            cbInject.IsEnabled = false;
            cbStartError.IsEnabled = false;
            cbDependenciesReinstall.IsEnabled = false;
            cbAppdata.IsEnabled = false;
            cbReinstallKeepSettings.IsEnabled = false;
            cbReinstall.IsEnabled = false;
            cbResetSettings.IsEnabled = false;
            GoButton.IsEnabled = true;
        }

		/// <summary>
		/// Enable all checkboxes again + clear preveiously selected option
		/// </summary>
        private void EnableAllCheckBoxes()
        {
            cbInstallLinks.IsEnabled = true;
            cbMenu.IsEnabled = true;
            cbOrbwalkStutter.IsEnabled = true;
            cbAuth.IsEnabled = true;
            cbBugSplat.IsEnabled = true;
            cbInject.IsEnabled = true;
            cbStartError.IsEnabled = true;
            cbDependenciesReinstall.IsEnabled = true;
            cbAppdata.IsEnabled = true;
            cbReinstallKeepSettings.IsEnabled = true;
            cbReinstall.IsEnabled = true;
            cbResetSettings.IsEnabled = true;
            CurrentSelected = null;
            GoButton.IsEnabled = false;
        }

		/// <summary>
		/// Do the work for me button click event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void GoButton_Click(object sender, RoutedEventArgs e)
        {
            App.Solver.Solve();
        }

		/// <summary>
		/// Problem not listed button click event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ProblemNotListed_Button_Click(object sender, RoutedEventArgs e)
		{
			Process.Start("https://bitbucket.org/HyunMi/leaguesharp/issues");
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			App.SolveAnOtherProblem.IsEnabled = true;
		}
	}
}
