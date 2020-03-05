using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;

namespace DirectoryTransfer.UI
{
    internal static class MainExtensions
    {
        public static void Close()
        {
            foreach (Window window in Application.Current.Windows)
                window.Close();
        }

        public static void SetIsRunWhenStartValue(bool isRunWhenComputerStarts)
        {
            var rk = Registry.CurrentUser.OpenSubKey
                ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (rk == null)
                return;

            var appPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DirectoryTransfer.UI.exe");

            if (isRunWhenComputerStarts)
            {
             
                rk.SetValue("Directory Transfer", "\"" + appPath + "\"");
            }
            else
            {
                rk.DeleteValue("Directory Transfer",false);
            }
        }
    }
}