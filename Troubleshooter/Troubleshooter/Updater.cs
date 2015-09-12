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
using System.Net;
using System.Reflection;
using System.Windows;

namespace Troubleshooter
{
	public static class Updater
	{
		private static readonly WebClient WebClient = new WebClient();
		private static Version _version = Assembly.GetExecutingAssembly().GetName().Version;
		private static Version _externalVersion;

		private static void Get_ExternalVersion()
		{
			string strExternalVersion = _version.ToString();
            try
            {
	            strExternalVersion = WebClient.DownloadString(
		            "http://bitbucket.org/HyunMi/leaguesharp/downloads/version.txt");
            }
            catch (Exception)
            {
	            // ignored
            }
			_externalVersion = new Version(strExternalVersion);
		}

		private static bool NewerVersionAvaible()
		{
			return _version.CompareTo(_externalVersion) < 0;
		}

		public static void Run()
		{
			Get_ExternalVersion();
			if (NewerVersionAvaible().Equals(false)) return;
			MessageBox.Show("New version avaible.\nTroubleshooter will update now.");
			Process.Start("Updater.exe");
			Environment.Exit(0);
		}
	}
}
