using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using JsonBinLib;
using Newtonsoft.Json;

namespace BinaryToJSONConverterApp
{
    public partial class MainConverterWindow : Form
    {
        public List<string> pathsToJsonFiles = new List<string>();
        public string pathToSaveBinaryFileFolder = "";
        public string helpFileName = "JsonСonverter.chm";

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
                foreach (String file in openFileDialog.FileNames)
                {
                    this.pathsToJsonFiles.Add(file);
                    textBoxLoadFromFolder.Text += openFileDialog.FileName + ";";
                }                
            }
        }

        internal void buttonBrowseSaveFolder_Click(object sender, EventArgs e)
        {            
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxSaveFolder.Text = folderBrowserDialog.SelectedPath;
                this.pathToSaveBinaryFileFolder = folderBrowserDialog.SelectedPath;
            }
        }

        internal void buttonConvert_Click(object sender, EventArgs e)
        {
            int i = 1;
            foreach (string path in this.pathsToJsonFiles)            
            {
                SeisFileConverter converter = new SeisFileConverter(path, this.pathToSaveBinaryFileFolder);
                converter.ConvertAndSave();     
                toolStripStatusLabel.Text = "Processing...(" + i + "/" + Convert.ToString(this.pathsToJsonFiles.Count) + ")";                
                statusStrip.Refresh();
                i++;
            }
            toolStripStatusLabel.Text = "Success";
        }
        
        private void buttonHelp_Click(object sender, EventArgs e)
        {
            string exeDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);

            if (File.Exists(exeDirectory + "\\" + helpFileName) == true)
            {
                Help.ShowHelp(this, exeDirectory + "\\" + helpFileName);
            }

            else
            {
                MessageBox.Show("Help file not found.");
            }
        }
    }       
}
