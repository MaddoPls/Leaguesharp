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
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace Troubleshooter
{
    public class Error
    {
        private TextBlock _debugOut;
	    private WebClient _webClient;
	    private Window _activeWindow;

        public Error(IReadOnlyList<object> objArray)
        {
            _debugOut = objArray[0] as TextBlock;
        }

        public void Solve()
        {
            string problemToSolve = ErrorSelectionWindow.CurrentSelected;
            App.ErrorSelectionWindow.Close();
	        _webClient?.Dispose();
	        _webClient = new WebClient();
            switch (problemToSolve)
            {
                case "cbInstallLinks":
                    InstallLink();
                    break;
                case "cbMenu":
                    MenuNotShowing();
                    break;
                case "cbOrbwalkStutter":
                    OrbwalkerStuttering();
                    break;
                case "cbAuth":
                    AuthFailed();
                    break;
                case "cbBugSplat":
                    BugSplat();
                    break;
                case "cbInject":
                    NotInjecting();
                    break;
                case "cbStartError":
                    ErrorStart();
                    break;
                case "cbDependenciesReinstall":
                    ReinstallDependencies();
                    break;
                case "cbAppdata":
                    ClearAppdata();
                    break;
                case "cbReinstallKeepSettings":
                    ReinstallLeaguesharpKeepSettings();
                    break;
                case "cbReinstall":
                    ReinstallLeaguesharp();
                    break;
                case "cbResetSettings":
                    ResetSettings();
                    break;
            }
        }

        private void InstallLink()
        {
            DebugWriteLine("Problem: InstallLink");
            MessageBox.Show("Please nagivate to your Leaguesharp folder and select your loader.exe");
            OpenFileDialog loaderdialog = new OpenFileDialog
            {
                Multiselect = false,
                AddExtension = true,
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = ".exe",
                Title = "Please select your Loader.exe"
            };
            loaderdialog.ShowDialog();
            string loaderPath = loaderdialog.FileName;
            DebugWriteLine("Selected loader path " + loaderPath);

			DebugWriteLine("Deleting old reg key");
	        string[] subKeys = Registry.ClassesRoot.GetSubKeyNames();
	        if (subKeys.Contains("ls"))
	        {
		        Registry.ClassesRoot.DeleteSubKeyTree("ls");
	        }
			DebugWriteLine("Adding level 0 ls reg key");
			//level 0 ls
	        RegistryKey baseKey = Registry.ClassesRoot.CreateSubKey("ls");
	        baseKey?.SetValue(null, "URL:Custom Protocol", RegistryValueKind.String);
	        baseKey?.SetValue("URL Protocol", "");
			DebugWriteLine("Adding level 1 stdIcon reg key");
			//level 1 stdIcon
			RegistryKey stdIcon = baseKey?.CreateSubKey("DefaultIcon");
			stdIcon?.SetValue(null, loaderPath + ",0", RegistryValueKind.String);
			DebugWriteLine("Adding level 1 shell reg key");
			//level 1 shell
			RegistryKey shell = baseKey?.CreateSubKey("shell");
			DebugWriteLine("Adding level 2 open reg key");
			//level 2 open
			RegistryKey open = shell?.CreateSubKey("open");
			open?.SetValue(null, "\"" + loaderPath + "\" \",%1\"", RegistryValueKind.String);
			DebugWriteLine("Adding level 3 command reg key");
			//level 3 command
			RegistryKey command = open?.CreateSubKey("command");
			command?.SetValue(null, "\"" + loaderPath + "\" \"%1\"", RegistryValueKind.String);

            DebugWriteLine("InstallLink fix done");
            DebugWriteLine("Exitcode 0");
        }

		private void MenuNotShowing()
		{
			DebugWriteLine("Problem: Menu not showing");
			_webClient.DownloadFile("https://bitbucket.org/HyunMi/leaguesharp/downloads/dxwebsetup.exe", "dxwebsetup.exe");
			DebugWriteLine("Directx downloaded");
			Process p = Process.Start("dxwebsetup.exe");
			DebugWriteLine("Directx installing");
			p?.WaitForExit();
			DebugWriteLine("Download .NET Repair kit");
			_webClient.DownloadFile("https://bitbucket.org/HyunMi/leaguesharp/downloads/NetFxRepairTool.exe", "NetFixRepairTool.exe");
			DebugWriteLine("Repair kit downloaded");
			Process pp = Process.Start("NetFixRepairTool.exe");
			DebugWriteLine(".NET Framework repairing");
			pp?.WaitForExit();
			DebugWriteLine(".NET Framework repaired");
			DebugWriteLine("Exitcode 0");
		}

		private void OrbwalkerStuttering()
        {
			DebugWriteLine("Problem: Orbwalker stuttering");
			WindupWindow solution = new WindupWindow();
			solution.Show();
			solution.Closed += ChildwindowClose;
		}

        private void AuthFailed()
        {
			DebugWriteLine("Problem: Auth failed");
			ChangePassWindow solution = new ChangePassWindow();
			solution.Show();
			solution.Closed += ChildwindowClose;
		}

        private void BugSplat()
        {
			DebugWriteLine("Problem: Menu not showing");
			BugsplatWindow solution = new BugsplatWindow();
			solution.Show();
			solution.Closed += ChildwindowClose;
        }

		private void NotInjecting()
        {
			Inject solution = new Inject();
			solution.Show();
			DebugWriteLine("Problem: Not injecting");
			_webClient.DownloadFile("https://bitbucket.org/HyunMi/leaguesharp/downloads/dxwebsetup.exe", "dxwebsetup.exe");
			DebugWriteLine("Directx download complete");
			Process p = Process.Start("dxwebsetup.exe");
	        p?.WaitForExit();
			DebugWriteLine("Directx install done");
			DebugWriteLine("Exitcode 0");
			if (solution.IsLoaded)
			{
				solution.Closed += ChildwindowClose;
			}
        }

		private void ErrorStart()
        {
			DebugWriteLine("Problem Leaguesharp starting with an error");
			ErrorStartWindow solution = new ErrorStartWindow();
			solution.Show();
			_activeWindow = solution;
			solution.Closed += ChildwindowClose;
        }

	    private void ReinstallDependencies()
        {
			DebugWriteLine("Problem: reinstall dependencies");
			if (IntPtr.Size == 8)
	        {
				DebugWriteLine("System is 64-bit");
		        _webClient.DownloadFile("https://bitbucket.org/HyunMi/leaguesharp/downloads/vcredist_x64.exe", "vcredist_x64.exe");
				DebugWriteLine("vcredist 64-bit download complete");
				Process p = Process.Start("vcredist_x64.exe");
				DebugWriteLine("vcredist install started");
		        p?.WaitForExit();
				DebugWriteLine("vcredist install done");
			}
	        else
	        {
				DebugWriteLine("System is 32-bit");
		        _webClient.DownloadFile("https://bitbucket.org/HyunMi/leaguesharp/downloads/vcredist_x86.exe", "vcredist_x86.exe");
				DebugWriteLine("vcredist 32-bit download complete");
		        Process p = Process.Start("vcredist_x86.exe");
				DebugWriteLine("vcredist install started");
		        p?.WaitForExit();
				DebugWriteLine("vcredist install done");
			}

			DebugWriteLine("Checking .NET framework version, version: " + get_netReleasekey());
	        if (get_netReleasekey() < 393273)
	        {
				DebugWriteLine(".NET framework version not supported");
		        _webClient.DownloadFile("https://bitbucket.org/HyunMi/leaguesharp/downloads/NDP46-KB3045557-x86-x64-AllOS-ENU.exe", "NDP46-KB3045557-x86-x64-AllOS-ENU.exe");
				DebugWriteLine(".NET framework installer download done");
		        Process p = Process.Start("NDP46-KB3045557-x86-x64-AllOS-ENU.exe");
				DebugWriteLine(".NET framework install started");
		        p?.WaitForExit();
				DebugWriteLine(".NET framework install done");
	        }
			DebugWriteLine("Exitcode 0");
        }

        private void ClearAppdata()
        {
			DebugWriteLine("Problem: clear appdata");
			string appdataRoaming = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
			DebugWriteLine("Appdata path: " + appdataRoaming);
	        string[] folders = Directory.GetDirectories(appdataRoaming);
	        for (int i = 0; i < folders.Length; i++)
	        {
				folders[i] = folders[i].Replace(appdataRoaming, "");
	        }
	        const string pattern = "LS\\w{8}";
	        string leaguesharpAppdata = folders.Where((t, i) => System.Text.RegularExpressions.Regex.IsMatch(folders[i], pattern)).FirstOrDefault();
			DebugWriteLine("Leaguesharp appdata name: " + leaguesharpAppdata);
	        leaguesharpAppdata = appdataRoaming + leaguesharpAppdata;
			DebugWriteLine("Leaguesharp appdata path: " + leaguesharpAppdata);
	        if (string.IsNullOrWhiteSpace(leaguesharpAppdata).Equals(false))
	        {
		        DebugWriteLine("Path ok, starting delete");
		        DirectoryInfo dirInfo = new DirectoryInfo(leaguesharpAppdata);
		        SetAttributesNormal(dirInfo);
		        Directory.Delete(leaguesharpAppdata, true);
	        }
	        else DebugWriteLine("Path not ok exiting");
	        DebugWriteLine("Exitcode 0");
		}

        private void ReinstallLeaguesharpKeepSettings()
        {
			DebugWriteLine("Problem: reinstall leaguesharp but keeping your settings");
			MessageBox.Show("Please nagivate to your Leaguesharp folder and select your loader.exe");
			OpenFileDialog loaderdialog = new OpenFileDialog
			{
				Multiselect = false,
				AddExtension = true,
				CheckFileExists = true,
				CheckPathExists = true,
				DefaultExt = ".exe",
				Title = "Please select your Loader.exe"
			};
			loaderdialog.ShowDialog();
			string loaderPath = loaderdialog.FileName;
			DebugWriteLine("Loaderpath: " + loaderPath);
	        loaderPath = loaderPath.Replace(@"\loader.exe", "");
			DebugWriteLine("Copied config.xml");
			File.Copy(loaderPath + @"\config.xml", AppDomain.CurrentDomain.BaseDirectory + "config.xml");
			DebugWriteLine("Setting attributes normal");
			DirectoryInfo dirInfo = new DirectoryInfo(loaderPath);
			SetAttributesNormal(dirInfo);
			DebugWriteLine("Deleting directory");
			Directory.Delete(loaderPath, true);
	        Directory.CreateDirectory(loaderPath);
			DebugWriteLine("Downloading leaguesharp installer");
			_webClient.DownloadFile("https://bitbucket.org/HyunMi/leaguesharp/downloads/LeagueSharp-update.exe", "LeagueSharp-update.exe");
	        MessageBox.Show("Please install leaguesharp in the original location");
	        Process p = Process.Start("LeagueSharp-update");
			DebugWriteLine("Leaguesharp install started");
	        p?.WaitForExit();
			DebugWriteLine("Leaguesharp install finished");
			DebugWriteLine("Deleting config.xml");
			File.Delete(loaderPath + @"\config.xml");
			DebugWriteLine("Replacing old config.xml");
			File.Copy(AppDomain.CurrentDomain.BaseDirectory + "config.xml", loaderPath + @"\config.xml");
			DebugWriteLine("Exitcode 0");
        }

		private void ReinstallLeaguesharp()
        {
			DebugWrite("Problem: Reinstall leaguesharp");
			MessageBox.Show("Please nagivate to your Leaguesharp folder and select your loader.exe");
			OpenFileDialog loaderdialog = new OpenFileDialog
			{
				Multiselect = false,
				AddExtension = true,
				CheckFileExists = true,
				CheckPathExists = true,
				DefaultExt = ".exe",
				Title = "Please select your Loader.exe"
			};
			loaderdialog.ShowDialog();
			string loaderPath = loaderdialog.FileName;
			DebugWriteLine("Loaderpath " + loaderPath);
			loaderPath = loaderPath.Replace(@"\loader.exe", "");
			DebugWriteLine("Setting attributes normal");
			DirectoryInfo dirInfo = new DirectoryInfo(loaderPath);
			SetAttributesNormal(dirInfo);
			DebugWriteLine("Deleting directory");
			Directory.Delete(loaderPath, true);
			Directory.CreateDirectory(loaderPath);
			DebugWriteLine("Downloading leaguesharp");
			_webClient.DownloadFile("https://bitbucket.org/HyunMi/leaguesharp/downloads/LeagueSharp-update.exe", "LeagueSharp-update.exe");
			DebugWriteLine("Leaguesharp download complete");
			Process p = Process.Start("LeagueSharp-update");
			DebugWriteLine("Leaguesharp install started");
			p?.WaitForExit();
			DebugWriteLine("Leaguesharp install finished");
			DebugWriteLine("Exitcode 0");
		}

        private void ResetSettings()
        {
			DebugWriteLine("Problem: Reset settings");
			MessageBox.Show("Please nagivate to your Leaguesharp folder and select config.xml");
			OpenFileDialog configDialog = new OpenFileDialog
			{
				Multiselect = false,
				AddExtension = true,
				CheckFileExists = true,
				CheckPathExists = true,
				DefaultExt = ".xml",
				Title = "Please select config.xml"
			};
			configDialog.ShowDialog();
			string configPath = configDialog.FileName;
			DebugWriteLine("Config path: " + configPath);
			DebugWriteLine("Deleting config.xml");
			File.Delete(configPath);
			DebugWriteLine("Downloading standard config.xml");
			_webClient.DownloadFile("https://bitbucket.org/HyunMi/leaguesharp/downloads/config.xml", configPath);
	        DebugWriteLine("config.xml download complete");
			DebugWriteLine("Exitcode 0");
		}

		private void ChildwindowClose(object sender, EventArgs e)
		{
			DebugWriteLine("Exitcode 0");
		}

	    private static void SetAttributesNormal(DirectoryInfo dir)
		{
			foreach (DirectoryInfo subDirPath in dir.GetDirectories())
			{
				SetAttributesNormal(new DirectoryInfo(subDirPath.FullName));
			}
			foreach (FileInfo file in dir.GetFiles().Select(filePath => new FileInfo(filePath.FullName) {Attributes = FileAttributes.Normal})){}
		}

	    private static int get_netReleasekey()
	    {
			using (RegistryKey ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey("SOFTWARE\\Microsoft\\NET Framework Setup\\NDP\\v4\\Full\\"))
			{
				return Convert.ToInt32(ndpKey?.GetValue("Release"));
			}
		}


	    private void DebugWriteLine(string inpt)
        {
            string current = _debugOut.Text;
            current += inpt + "\n";
            _debugOut.Text = current;
        }

        private void DebugWrite(string inpt)
        {
            string current = _debugOut.Text;
            current += inpt;
            _debugOut.Text = current;
        }


        [DllImport("kernel32.dll", EntryPoint = "GetStdHandle", SetLastError = true, CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll", EntryPoint = "AllocConsole", SetLastError = true, CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        private static extern int AllocConsole();

        private const int StdOutputHandle = -11;
        private const int MyCodePage = 437;

        private static void SetConsole()
        {
            AllocConsole();
            IntPtr stdHandle = GetStdHandle(StdOutputHandle);
            SafeFileHandle safeFileHandle = new SafeFileHandle(stdHandle, true);
            FileStream fileStream = new FileStream(safeFileHandle, FileAccess.Write);
            Encoding encoding = Encoding.GetEncoding(MyCodePage);
            StreamWriter standardOutput = new StreamWriter(fileStream, encoding) {AutoFlush = true};
            Console.SetOut(standardOutput);  
        }
    }
}
