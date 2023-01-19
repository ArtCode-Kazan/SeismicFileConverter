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
        }

        public class Constants
        {
            public const string PathToZip = @"C:\Users\user\Desktop\Новая папка\programm.zip";
            public const string PathToDir = @"D:\Codingapps\BinaryToJSONConverterApp\bin\Debug";
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
            string dir = @"D:\Codingapps\BinaryToJSONConverterApp\bin\Debug";
            string[] paths = Directory.GetFiles(dir);
            
            foreach (string path in paths)
            {
                File.Delete(path);
            }            
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {            
            DeleteFiles();
            ZipFile.ExtractToDirectory(Constants.PathToZip, Constants.PathToDir);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Process[] info = Process.GetProcessesByName("SeisJsonConveterUpdater");            
            MessageBox.Show(Process.GetCurrentProcess().StartInfo.WorkingDirectory);
            //Close();
        }
    }
}
