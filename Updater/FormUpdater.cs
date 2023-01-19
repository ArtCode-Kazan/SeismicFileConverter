﻿using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Windows.Forms;

namespace Updater
{    
    public partial class FormUpdater : Form
    {
        public const string PathToZip = @"C:\Users\user\Desktop\Новая папка\programm.zip";
        public const string PathToTxt = "C:/Users/user/Desktop/Новая папка/descripton.txt";

        public FormUpdater()
        {
            InitializeComponent();
            labelversion.Text = GetServerAssemblyVersion(PathToTxt);
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

        public string PathToFolder
        {
            get
            {
                string pathToFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                return pathToFolder;
            }
        }

        public void RunConverter()
        {
            ProcessStartInfo info = new ProcessStartInfo(@"D:\Codingapps\BinaryToJSONConverterApp\bin\Debug\SeisJsonConverter.exe");
            info.WorkingDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            info.CreateNoWindow = true;
            Process process = Process.Start(info);
            Close();
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
            ZipFile.ExtractToDirectory(PathToZip, Environment.CurrentDirectory);
            RunConverter();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
