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
        public FormUpdater()
        {
            InitializeComponent();
            labelversion.Text = GetServerAssemblyVersion(Constants.TxtUrl);
        }

        public string programmFolderPath
        {
            get
            {
                string pathToFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                return pathToFolder;
            }
        }

        public string GetServerAssemblyVersion(string url)
        {
            string line;
            string serverVersion = "";
            using (WebClient client = new WebClient())
            {
                using (Stream stream = client.OpenRead(url))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        while ((line = reader.ReadLine()) != null)
                        {
                            if (line.Contains(Constants.VersionFieldName))
                            {
                                serverVersion = line.Split(':')[1].Split(' ')[1];
                            }
                        }
                    }
                }
            }
            return serverVersion;
        }

        public string GetServerHashSum()
        {
            string line;
            string serverHashsum = "";
            using (WebClient client = new WebClient())
            {
                using (Stream stream = client.OpenRead(Constants.TxtUrl))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        while ((line = reader.ReadLine()) != null)
                        {
                            if (line.Contains(Constants.HashsumMD5FieldName))
                            {
                                serverHashsum = line.Split(':')[1].Split(' ')[1];
                            }
                        }
                    }
                }
            }
            return serverHashsum;
        }

        public dynamic GetZipHashSum(string path)
        {
            string hashsum = "";
            using (var fs = new FileStream(path, FileMode.Open))
            {
                var md5 = MD5.Create();
                byte[] hashValue = md5.ComputeHash(fs);
                hashsum = BitConverter.ToString(hashValue)
                    .Replace("-", string.Empty)
                    .ToLower();
            }
            return hashsum;
        }

        public void DownloadZip()
        {
            using (var client = new WebClient())
            {
                client.DownloadFile(Constants.ZipUrl, Path.Combine(programmFolderPath, Constants.ZipName));
            }
        }

        public bool IsZipBroken()
        {
            if (File.Exists(Path.Combine(programmFolderPath, Constants.ZipName)))
            {
                if (GetZipHashSum(Path.Combine(programmFolderPath, Constants.ZipName)) == GetServerHashSum())
                {
                    return false;
                }
            }
            MessageBox.Show("Archive file is broken. Try again", "Archive error");
            return true;
        }

        public void RunConverter()
        {
            ProcessStartInfo info = new ProcessStartInfo(Path.Combine(programmFolderPath, Constants.ConverterAppName));
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
                if (!Constants.MainFileNames.Contains(Path.GetFileName(path)))
                {
                    File.Delete(path);
                }
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            DownloadZip();
            if (!IsZipBroken())
            {
                DeleteFiles();
                ZipFile.ExtractToDirectory(Path.Combine(programmFolderPath, Constants.ZipName), Environment.CurrentDirectory);
                File.Delete(Path.Combine(programmFolderPath, Constants.ZipName));
            }
            RunConverter();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }

    public class Constants
    {
        public const string TxtUrl = "https://sigma-geophys.com/Distr/version.txt";
        public const string ZipUrl = "https://sigma-geophys.com/Distr/SeisJsonConveter.zip";
        public const string ZipName = "ConverterLatestVersion.zip";
        public const string ConverterAppName = "SeisJsonConverter.exe";
        public const string VersionFieldName = "version:";
        public const string HashsumMD5FieldName = "MD5:";

        public static List<string> MainFileNames = new List<string>()
        {
            ZipName,
            AppDomain.CurrentDomain.FriendlyName,
            "SeisJsonConveterUpdater.pdb"
        };
    }
}
