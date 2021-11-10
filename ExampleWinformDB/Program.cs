using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExampleWinformDB
{
    static class Program
    {

        const long APPMODEL_ERROR_NO_PACKAGE = 15700L;

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        static extern int GetCurrentPackageFullName(ref int packageFullNameLength, StringBuilder packageFullName);



        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            CopyDatabase();

            ////-- set data directory
            string shortname = GetPackageShortName();
            var path = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\";
            var tpath = $"{path}Packages\\{shortname}\\LocalCache\\Local";
            AppDomain.CurrentDomain.SetData("DataDirectory", tpath);

            Application.Run(new Form1());
        }



        private static string GetPackageShortName()
        {
            int length = 0;
            StringBuilder sb = new StringBuilder(0);
            int result = GetCurrentPackageFullName(ref length, sb);

            sb = new StringBuilder(length);
            result = GetCurrentPackageFullName(ref length, sb);
            var name = sb.ToString();

            var begin = name.Substring(0, name.IndexOf("_"));
            string ending = name.Substring(name.LastIndexOf("_"));
            var fullshortname = begin + ending;
            return fullshortname;
        }

        private static void CopyDatabase()
        {
            string result = Assembly.GetExecutingAssembly().Location;
            int index = result.LastIndexOf("\\");
            string dbPath = $"{result.Substring(0, index)}\\Example.mdf";
            var path = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\";
            string dbName = "Example.mdf";
            var destinationPath = Path.Combine(path, dbName);
            //AppDomain.CurrentDomain.SetData("DataDirectory", path);

            if (!File.Exists(destinationPath))
            {
                Directory.CreateDirectory(path);
                File.Copy(dbPath, destinationPath, true);
            }
        }

    }
}
