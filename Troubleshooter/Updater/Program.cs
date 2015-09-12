/*
Troubleshooter updater for Leaguesharp
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
using System.IO;
using System.Net;
using System.Threading;

namespace Updater
{
	public class Program
	{
		static void Main(string[] args)
		{
			Thread.Sleep(TimeSpan.FromSeconds(5));
			if (File.Exists("Troubleshooter.exe"))
			{
				try
				{
					File.Delete("Troubleshooter.exe");
				}
				catch (Exception ex)
				{
					Console.Write(ex.Message + "\n" + ex.Data + "\n" + ex.Source + "\n" + ex.StackTrace);
					Console.ReadKey();
				}
			}
			WebClient webClient = new WebClient();
			webClient.DownloadFile("https://bitbucket.org/HyunMi/leaguesharp/downloads/Troubleshooter.exe", "Troubleshooter.exe");
			Process.Start("Troubleshooter.exe");
			Environment.Exit(0);
		}
	}
}
