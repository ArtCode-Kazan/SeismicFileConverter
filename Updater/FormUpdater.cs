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
            ServerInfo serverInfo = new ServerInfo(Constants.ServerUrl);
            labelversion.Text = serverInfo.AppVersion();
        }

        public string programmFolderPath
        {
            get
            {
                string pathToFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                return pathToFolder;
            }
        }

        public dynamic GetZipHashSum(string path)
        {
            string hashsum = "";
            using (FileStream fstream = new FileStream(path, FileMode.Open))
            {
                var md5 = MD5.Create();
                byte[] hashValue = md5.ComputeHash(inputStream: fstream);
                hashsum = BitConverter.ToString(hashValue).Replace(oldValue: "-", newValue: string.Empty).ToLower();
            }
            return hashsum;
        }

        public void DownloadZip()
        {
            ServerInfo serverInfo = new ServerInfo(Constants.ServerUrl);

            using (WebClient wclient = new WebClient())
            {
                wclient.DownloadFile(
                    address: serverInfo.ArchiveUri,
                    fileName: Path.Combine(programmFolderPath, Constants.ZipName));
            }
        }

        public bool IsZipBroken()
        {
            ServerInfo serverInfo = new ServerInfo(Constants.ServerUrl);
            string serverHashsum = serverInfo.Hashsum();

            if (File.Exists(path: Path.Combine(programmFolderPath, Constants.ZipName)))
            {
                if (GetZipHashSum(path: Path.Combine(programmFolderPath, Constants.ZipName)) == serverHashsum)
                {
                    return false;
                }
            }
            MessageBox.Show(text: "Archive file is broken. Try again", "Archive error");
            return true;
        }

        public void RunConverter()
        {
            ProcessStartInfo info = new ProcessStartInfo(fileName: Path.Combine(programmFolderPath, Constants.ConverterAppName));
            info.WorkingDirectory = Path.GetDirectoryName(path: Assembly.GetExecutingAssembly().Location);
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
                if (!Constants.FriendlyFileNames.Contains(Path.GetFileName(path)))
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
                ZipFile.ExtractToDirectory(
                    sourceArchiveFileName: Path.Combine(programmFolderPath, Constants.ZipName),
                    destinationDirectoryName: Environment.CurrentDirectory);
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
        public const string ServerUrl = "https://sigma-geophys.com/Distr/";
        public const string ConverterAppName = "SeisJsonConverter.exe";
        public const string ZipName = "ConverterLatestVersion.zip";

        public static List<string> FriendlyFileNames = new List<string>()
        {
            ZipName,
            AppDomain.CurrentDomain.FriendlyName,
            "SeisJsonConveterUpdater.pdb"
        };
    }

    public class ServerInfo
    {
        public const string ArchiveName = "SeisJsonConveter.zip";
        public const string DescriptionName = "version.txt";        
        public const string VersionFieldName = "version:";
        public const string HashsumMD5FieldName = "MD5:";

        public Uri uriServer;

        public ServerInfo(string serverUrlString)
        {
            this.uriServer = new Uri(serverUrlString);
        }

        public Uri DescriptionUri
        {
            get
            {
                Uri uriToDescription = new Uri(
                    baseUri: this.uriServer, 
                    relativeUri: ServerInfo.DescriptionName);
                return uriToDescription;
            }
        }

        public Uri ArchiveUri
        {
            get
            {
                Uri archiveUri = new Uri(
                    baseUri: this.uriServer, 
                    relativeUri: ServerInfo.ArchiveName);
                return archiveUri;
            }
        }

        public string AppVersion()
        {
            string line;
            string serverVersion = "";
            using (WebClient client = new WebClient())
            {
                using (Stream stream = client.OpenRead(DescriptionUri))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        while ((line = reader.ReadLine()) != null)
                        {
                            if (line.Contains(ServerInfo.VersionFieldName))
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
                using (Stream stream = client.OpenRead(DescriptionUri))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        while ((line = reader.ReadLine()) != null)
                        {
                            if (line.Contains(ServerInfo.HashsumMD5FieldName))
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
