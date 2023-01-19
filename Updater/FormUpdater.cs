using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace Updater
{
    public partial class FormUpdater : Form
    {
        public const string TxtUrl = "https://sigma-geophys.com/Distr/version.txt";
        public const string ZipUrl = "https://sigma-geophys.com/Distr/SeisJsonConveter.zip";
        public const string ZipName = "ConverterLatestVersion.zip";

        public List<string> MainFilesName = new List<string>() 
        { 
            ZipName, 
            AppDomain.CurrentDomain.FriendlyName,
            "SeisJsonConveterUpdater.pdb"
        };

        public FormUpdater()
        {
            InitializeComponent();
            labelversion.Text = GetServerAssemblyVersion(TxtUrl);
        }

        public string programmFolder
        {
            get
            {
                string pathToFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                return pathToFolder;
            }
        }

        public string GetServerAssemblyVersion(string url)
        {
            string s;
            string result = "";
            using (WebClient client = new WebClient())
            {
                using (Stream stream = client.OpenRead(url))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        while ((s = reader.ReadLine()) != null)
                        {
                            if (s.Contains("version:"))
                            {
                                result = s.Split(':')[1].Split(' ')[1];
                            }
                        }
                    }              
                }
            }                                
            return result;
        }

        public string GetServerHashSum()
        {
            string s;
            string result = "";
            using (WebClient client = new WebClient())
            {
                using (Stream stream = client.OpenRead(TxtUrl))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        while ((s = reader.ReadLine()) != null)
                        {
                            if (s.Contains("MD5:"))
                            {
                                result = s.Split(':')[1].Split(' ')[1];
                            }
                        }
                    }
                }
            }
            return result;
        }

        public dynamic GetZipHashSum(string path)
        {
            string hash = "";
            using (var fs = new FileStream(path, FileMode.Open))
            {
                var md5 = MD5.Create();
                byte[] hashValue = md5.ComputeHash(fs);
                hash = BitConverter.ToString(hashValue)
                    .Replace("-", string.Empty)
                    .ToLower();
            }
            return hash;
        }

        public void DownloadZip()
        {
            using (var client = new WebClient())
            {
                client.DownloadFile(ZipUrl, Path.Combine(programmFolder, ZipName));
            }
        }

        public bool IsZipBroken()
        {
            if (File.Exists(Path.Combine(programmFolder, ZipName)))
            {
                if (GetZipHashSum(Path.Combine(programmFolder, ZipName)) == GetServerHashSum())
                {
                    return false;
                }                
            }
            MessageBox.Show("Файл архива поврежден. Повторите попытку","Ошибка загрузки");
            return true;
        }

        public void RunConverter()
        {
            ProcessStartInfo info = new ProcessStartInfo(Path.Combine(programmFolder, "SeisJsonConverter.exe"));
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
                if (!MainFilesName.Contains(Path.GetFileName(path)))
                {
                    File.Delete(path);
                }                
            }            
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            DownloadZip();
            if (IsZipBroken() == false)
            {
                DeleteFiles();
                ZipFile.ExtractToDirectory(Path.Combine(programmFolder, ZipName), Environment.CurrentDirectory);
                File.Delete(Path.Combine(programmFolder, ZipName));
            }                        
            RunConverter();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {            
            Close();
        }
    }
}
