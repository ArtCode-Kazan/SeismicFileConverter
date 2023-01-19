using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Updater
{    
    public partial class FormUpdater : Form
    {
        public FormUpdater()
        {
            InitializeComponent();
            labelversion.Text = GetServerAssemblyVersion("C:/Users/user/Desktop/Новая папка/descripton.txt");
        }

        public class Constants
        {
            public const string PathToZip = @"C:\Users\user\Desktop\Новая папка\programm.zip";            
        }
        public string GetServerAssemblyVersion(string path)
        {
            string s;
            string result = "";

            using (var f = new StreamReader(path))
            {
                while ((s = f.ReadLine()) != null)
                {
                    if (s.Contains("version:"))
                    {
                        result = s.Split(':')[1];
                    }
                }
            }

            return result;
        }

        public string pathToFolder
        {
            get
            {
                string pathToFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                return pathToFolder;
            }
        }

        public void DeleteFiles()
        {
            string dir = Environment.CurrentDirectory;
            string[] paths = Directory.GetFiles(dir);
            
            foreach (string path in paths)
            {
                File.Delete(path);
            }            
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {            
            DeleteFiles();
            ZipFile.ExtractToDirectory(Constants.PathToZip, Environment.CurrentDirectory);
            ProcessStartInfo info = new ProcessStartInfo(@"D:\Codingapps\BinaryToJSONConverterApp\bin\Debug\SeisJsonConverter.exe");
            info.WorkingDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            info.CreateNoWindow = true;

            Process process = Process.Start(info);
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Process[] info = Process.GetProcessesByName("SeisJsonConveterUpdater");            
            MessageBox.Show(Environment.CurrentDirectory);            
            //Close();
        }
    }
}
