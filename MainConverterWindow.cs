using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Windows.Forms;
using JsonBinLib;

namespace BinaryToJSONConverterApp
{
    public partial class MainConverterWindow : Form
    {
        public List<string> pathsJsons = new List<string>();
        public string pathFolderBinarySave = "";

        public MainConverterWindow()
        {
            InitializeComponent();
            toolStripStatusLabel.Text = "Ready";
        }

        public string OriginAssemblyVersion
        {
            get
            {
                string appVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                return appVersion;
            }
        }

        public string ProgrammFolderPath
        {
            get
            {
                string pathToFolder = Path.GetDirectoryName(path: Assembly.GetExecutingAssembly().Location);
                return pathToFolder;
            }
        }              

        public void RunUpdater()
        {
            ProcessStartInfo info = new ProcessStartInfo(fileName: Path.Combine(path1: ProgrammFolderPath, path2: Constants.UpdaterAppName));
            info.WorkingDirectory = Path.GetDirectoryName(path: Assembly.GetExecutingAssembly().Location);
            Process process = Process.Start(startInfo: info);
            Close();
        }

        public void UpdateProgramm()
        {
            DialogResult result = MessageBox.Show(
            text: "New version available.\nUpdate?",
            caption: "Update",
            buttons: MessageBoxButtons.YesNo,
            icon: MessageBoxIcon.Information,
            defaultButton: MessageBoxDefaultButton.Button1,
            options: MessageBoxOptions.DefaultDesktopOnly);

            if (result == DialogResult.Yes)
            {
                RunUpdater();
            }

            this.TopMost = true;
        }

        public void buttonBrowseJsonFiles_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxLoadFromFolder.Text = "";
                foreach (string path in openFileDialog.FileNames)
                {
                    this.pathsJsons.Add(item: path);
                    textBoxLoadFromFolder.Text += openFileDialog.FileName + ";";
                }
            }
        }

        internal void buttonBrowseSaveFolder_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxSaveFolder.Text = folderBrowserDialog.SelectedPath;
                this.pathFolderBinarySave = folderBrowserDialog.SelectedPath;
            }
        }

        internal void buttonConvert_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.pathsJsons.Count; i++)
            {
                string path = this.pathsJsons[i];
                JsonParser jsonParser = new JsonParser(pathToJsonFile: path);
                string binaryFileName = jsonParser.jsonDeserialized.fileName + ".00";
                string pathSaveBinary = Path.Combine(path1: this.pathFolderBinarySave, path2: binaryFileName);
                SeisBinaryFile binaryFile = new SeisBinaryFile(jsonDeserialized: jsonParser.jsonDeserialized, savePath: pathSaveBinary);
                binaryFile.SaveToBaykal7Format();
                toolStripStatusLabel.Text = "Processing...(" + (i + 1) + "/" + Convert.ToString(value: this.pathsJsons.Count) + ")";
                statusStrip.Refresh();
            }

            toolStripStatusLabel.Text = "Success";
        }

        private void OpenHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string exeDirectory = Path.GetDirectoryName(path: Assembly.GetEntryAssembly().Location);
            string helpFilePath = Path.Combine(path1: exeDirectory, path2: Constants.HelpFileName);

            if (File.Exists(path: helpFilePath))
            {
                Help.ShowHelp(parent: this, url: helpFilePath);
            }
            else
            {
                MessageBox.Show(text: "Help file not found.");
            }
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form formHelp = new AboutProgramm();
            formHelp.ShowDialog();
        }

        private void ReportAProblemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string mailtoUrl = Constants.SupportMailtoUrl;

            try
            {
                Process.Start(fileName: mailtoUrl);
            }
            catch (System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(text: noBrowser.Message);
            }
            catch (Exception other)
            {
                MessageBox.Show(text: other.Message);
            }
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ServerInfo serverInfo = new ServerInfo(url: Constants.TxtUrl);

            if (serverInfo.IsVersionLatest(currentVersion: OriginAssemblyVersion))
            {
                MessageBox.Show(text: "The latest version is installed", caption: "Update");
            }
            else
            {
                UpdateProgramm();
            }
        }
    }

    public class Constants
    {
        public const string HelpFileName = "ConverterHelpFile.chm";
        public const string TxtUrl = "https://sigma-geophys.com/Distr/version.txt";
        public const string VersionFieldName = "version:";
        public const string UpdaterAppName = "SeisJsonConverterUpdater.exe";
        public const string SupportMailtoUrl = "mailto:ArtCode-Kazan@yandex.ru?subject=Support.JsonConverter";
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
            using (WebClient wclient = new WebClient())
            {
                using (Stream stream = wclient.OpenRead(address: url))
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

        public bool IsVersionLatest(string currentVersion)
        {            
            string[] originVersion = currentVersion.Split('.');
            string[] serverVersion = AppVersion().Split('.');

            for (int i = 0; i < 4; i++)
            {
                if (originVersion[i] != serverVersion[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
