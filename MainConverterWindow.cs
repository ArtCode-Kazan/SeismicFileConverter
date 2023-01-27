using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Windows.Forms;
using JsonBinLib;
using ServerConnectorLib;

namespace BinaryToJSONConverterApp
{
    public partial class MainConverterWindow : Form
    {
        ServerConnector server = new ServerConnector(Constants.serverUrlString, Constants.archiveName);

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

        public string ProgramFolderPath
        {
            get
            {
                string pathToFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                return pathToFolder;
            }
        }

        public void RunUpdater()
        {
            ProcessStartInfo info = new ProcessStartInfo(Path.Combine(ProgramFolderPath, Constants.UpdaterAppName));
            info.WorkingDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            Process process = Process.Start(info);
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
                    this.pathsJsons.Add(path);
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
                JsonParser jsonParser = new JsonParser(path);
                string binaryFileName = jsonParser.jsonDeserialized.fileName + ".00";
                string pathSaveBinary = Path.Combine(this.pathFolderBinarySave, binaryFileName);
                SeisBinaryFile binaryFile = new SeisBinaryFile(jsonDeserialized: jsonParser.jsonDeserialized, savePath: pathSaveBinary);
                binaryFile.SaveToBaykal7Format();
                toolStripStatusLabel.Text = "Processing...(" + (i + 1) + "/" + Convert.ToString(this.pathsJsons.Count) + ")";
                statusStrip.Refresh();
            }
            toolStripStatusLabel.Text = "Success";
        }

        private void OpenHelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string exeDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string helpFilePath = Path.Combine(exeDirectory, Constants.HelpFileName);

            if (File.Exists(helpFilePath))
            {
                Help.ShowHelp(parent: this, url: helpFilePath);
            }
            else
            {
                MessageBox.Show("Help file not found.");
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
                Process.Start(mailtoUrl);
            }
            catch (System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
            catch (Exception other)
            {
                MessageBox.Show(other.Message);
            }
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (server.IsVersionLatest(OriginAssemblyVersion))
                {
                    MessageBox.Show(text: "The latest version is installed", caption: "Update");
                }
                else
                {
                    UpdateProgramm();
                }
            }
            catch (WebException)
            {
                MessageBox.Show(text: "No server connection", caption: "Exception Caught!");
            }
        }
    }

    public class Constants
    {
        public const string SupportMailtoUrl = "mailto:ArtCode-Kazan@yandex.ru?subject=Support.JsonConverter";
        public const string UpdaterAppName = "SeisJsonConverterUpdater.exe";
        public const string HelpFileName = "ConverterHelpFile.chm";
        public const string serverUrlString = "https://sigma-geophys.com/Distr/";
        public const string archiveName = "SeisJsonConveter.zip";
    }
}
