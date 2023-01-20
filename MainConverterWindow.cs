﻿using System;
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
                string pathToFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                return pathToFolder;
            }
        }

        public string GetServerAssemblyVersion()
        {
            string line;
            string serverVersion = "";
            using (WebClient wclient = new WebClient())
            {
                using (Stream stream = wclient.OpenRead(Constants.TxtUrl))
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

        public bool IsVersionLatest()
        {
            string[] originVersion = OriginAssemblyVersion.Split('.');
            string[] serverVersion = GetServerAssemblyVersion().Split('.');

            for (int i = 0; i < 4; i++)
            {
                if (originVersion[i] != serverVersion[i])
                {
                    return false;
                }
            }
            return true;
        }

        public void RunUpdater()
        {
            ProcessStartInfo info = new ProcessStartInfo(Path.Combine(ProgrammFolderPath, Constants.UpdaterAppName));
            info.WorkingDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            Process process = Process.Start(info);
            Close();
        }

        public void UpdateProgramm()
        {
            DialogResult result = MessageBox.Show(
            "New version available.\nUpdate?",
            "Update",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Information,
            MessageBoxDefaultButton.Button1,
            MessageBoxOptions.DefaultDesktopOnly);

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
                SeisBinaryFile binaryFile = new SeisBinaryFile(jsonParser.jsonDeserialized, pathSaveBinary);
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
                Help.ShowHelp(this, helpFilePath);
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
            string target = Constants.SupportMailToUrl;

            try
            {
                Process.Start(fileName: target);
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
            if (IsVersionLatest())
            {
                MessageBox.Show("The latest version is installed", "Update");
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
        public const string SupportMailToUrl = "mailto:ArtCode-Kazan@yandex.ru?subject=Support.JsonConverter";
    }
}
