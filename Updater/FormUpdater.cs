using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Windows.Forms;
using ServerConnectorLib;

namespace Updater
{
    public partial class FormUpdater : Form
    {
        ServerConnector server = new ServerConnector(Constants.serverUrlString, Constants.archiveName);

        public FormUpdater()
        {
            InitializeComponent();
            try
            {
                labelversion.Text = this.server.GetDescription().version;
            }
            catch (WebException)
            {
                MessageBox.Show(text: "No server connection", caption: "Exception Caught!");
            }
        }

        public string programFolderPath
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

        public bool IsZipBroken()
        {
            string pathToZip = Path.Combine(programFolderPath, Constants.ZipName);

            if (File.Exists(pathToZip))
            {
                try
                {
                    string actualHashsum = GetZipHashSum(Path.Combine(programFolderPath, Constants.ZipName));

                    if (this.server.IsHashsumEqual(actualHashsum))
                    {
                        return false;
                    }
                }
                catch (WebException)
                {
                    MessageBox.Show(text: "No server connection", caption: "Exception Caught!");
                }
            }
            MessageBox.Show("Archive file is broken. Try again", "Archive error");
            return true;
        }

        public void RunConverter()
        {
            ProcessStartInfo info = new ProcessStartInfo(Path.Combine(programFolderPath, Constants.ConverterAppName));
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
            try
            {
                server.DownloadFile(Path.Combine(programFolderPath, Constants.ZipName), server.ArchiveUri);
                if (!IsZipBroken())
                {
                    DeleteFiles();
                    ZipFile.ExtractToDirectory(
                        sourceArchiveFileName: Path.Combine(programFolderPath, Constants.ZipName),
                        destinationDirectoryName: Environment.CurrentDirectory);
                    File.Delete(Path.Combine(programFolderPath, Constants.ZipName));
                }
                RunConverter();
            }
            catch (WebException)
            {
                MessageBox.Show(text: "No server connection", caption: "Exception Caught!");
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }

    public class Constants
    {
        public const string ConverterAppName = "SeisJsonConverter.exe";
        public const string ZipName = "ConverterLatestVersion.zip";
        public const string UpdaterLibDll = "UpdaterLib.dll";
        public const string UpdaterLibPbd = "UpdaterLib.pdb";
        public const string serverUrlString = "https://sigma-geophys.com/Distr/";
        public const string archiveName = "SeisJsonConveter.zip";

        public static List<string> FriendlyFileNames = new List<string>()
        {
            Constants.ZipName,
            AppDomain.CurrentDomain.FriendlyName,
            "SeisJsonConveterUpdater.pdb",
            Constants.UpdaterLibDll,
            Constants.UpdaterLibPbd
        };
    }
}
