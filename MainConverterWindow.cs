using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using JsonBinLib;

namespace BinaryToJSONConverterApp
{
    public partial class MainConverterWindow : Form
    {
        public const string HelpFileName = "ConverterHelpFile.chm";

        public List<string> pathsJsons = new List<string>();
        public string pathFolderBinarySave = "";

        public MainConverterWindow()
        {
            InitializeComponent();
            toolStripStatusLabel.Text = "Ready";
        }

        public void buttonBrowseJsonFiles_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxLoadFromFolder.Text = "";
                foreach (String path in openFileDialog.FileNames)
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
            string exeDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            string helpFilePath = Path.Combine(exeDirectory, HelpFileName);

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
            string target = "mailto:ArtCode-Kazan@yandex.ru?subject=Support.JsonConverter";

            try
            {
                System.Diagnostics.Process.Start(target);
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
    }
}
