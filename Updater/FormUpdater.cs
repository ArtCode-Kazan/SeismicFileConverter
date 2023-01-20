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
            ServerInfo serverInfo = new ServerInfo(url: Constants.TxtUrl);
            labelversion.Text = serverInfo.AppVersion();
        }

        public string programmFolderPath
        {
            get
            {
                string pathToFolder = Path.GetDirectoryName(path: Assembly.GetExecutingAssembly().Location);
                return pathToFolder;
            }
        }        

        public dynamic GetZipHashSum(string path)
        {
            string hashsum = "";
            using (FileStream fstream = new FileStream(path: path, mode: FileMode.Open))
            {
                var md5 = MD5.Create();
                byte[] hashValue = md5.ComputeHash(inputStream: fstream);
                hashsum = BitConverter.ToString(value: hashValue)
                    .Replace(oldValue: "-", newValue: string.Empty)
                    .ToLower();
            }
            return hashsum;
        }

        public void DownloadZip()
        {
            using (WebClient wclient = new WebClient())
            {
                wclient.DownloadFile(
                    address: Constants.ZipUrl, 
                    fileName: Path.Combine(
                        path1: programmFolderPath, 
                        path2: Constants.ZipName));
            }
        }

        public bool IsZipBroken()
        {
            ServerInfo serverInfo = new ServerInfo(url: Constants.TxtUrl);
            string serverHashsum = serverInfo.Hashsum();

            if (File.Exists(path: Path.Combine(path1: programmFolderPath, path2: Constants.ZipName)))
            {
                if (GetZipHashSum(path: Path.Combine(path1: programmFolderPath, path2: Constants.ZipName)) == serverHashsum)
                {
                    return false;
                }
            }
            MessageBox.Show(text: "Archive file is broken. Try again", "Archive error");
            return true;
        }

        public void RunConverter()
        {
            ProcessStartInfo info = new ProcessStartInfo(fileName: Path.Combine(path1: programmFolderPath, path2: Constants.ConverterAppName));
            info.WorkingDirectory = Path.GetDirectoryName(path: Assembly.GetExecutingAssembly().Location);
            info.CreateNoWindow = true;
            Process process = Process.Start(startInfo: info);
            Close();
        }

        public void DeleteFiles()
        {
            string dir = Environment.CurrentDirectory;
            string[] paths = Directory.GetFiles(path: dir);

            foreach (string path in paths)
            {
                if (!Constants.FriendlyFileNames.Contains(item: Path.GetFileName(path: path)))
                {
                    File.Delete(path: path);
                }
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            DownloadZip();
            if (!IsZipBroken())
            {
                DeleteFiles();
                ZipFile.ExtractToDirectory(
                    sourceArchiveFileName: Path.Combine(
                        path1: programmFolderPath, 
                        path2: Constants.ZipName), 
                    destinationDirectoryName: Environment.CurrentDirectory);
                File.Delete(
                    path: Path.Combine(
                        path1: programmFolderPath, 
                        path2: Constants.ZipName));
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

        public static List<string> FriendlyFileNames = new List<string>()
        {
            ZipName,
            AppDomain.CurrentDomain.FriendlyName,
            "SeisJsonConveterUpdater.pdb"
        };
    }

    public class ServerInfo
    {
        public string url;

        public ServerInfo(string url)
        {
            this.url = url;

        }
        public string AppVersion()
        {
            string line;
            string serverVersion = "";
            using (WebClient client = new WebClient())
            {
                using (Stream stream = client.OpenRead(address: url))
                {
                    using (StreamReader reader = new StreamReader(stream: stream))
                    {
                        while ((line = reader.ReadLine()) != null)
                        {
                            if (line.Contains(value: Constants.VersionFieldName))
                            {
                                serverVersion = line.Split(':')[1].Split(' ')[1];
                            }
                        }
                    }
                }
            }
            return serverVersion;
        }

        public string Hashsum()
        {
            string line;
            string serverHashsum = "";
            using (WebClient client = new WebClient())
            {
                using (Stream stream = client.OpenRead(address: Constants.TxtUrl))
                {
                    using (StreamReader reader = new StreamReader(stream: stream))
                    {
                        while ((line = reader.ReadLine()) != null)
                        {
                            if (line.Contains(value: Constants.HashsumMD5FieldName))
                            {
                                serverHashsum = line.Split(':')[1].Split(' ')[1];
                            }
                        }
                    }
                }
            }
            return serverHashsum;
        }
    }
}
