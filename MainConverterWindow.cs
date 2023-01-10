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

        public MainConverterWindow()
        {
            InitializeComponent();
            toolStripStatusLabel1.Text = "Ready";
            
        }

        public void buttonBrowse_Click(object sender, EventArgs e)
        {                                    
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxLoadFromFolder.Text = "";
                foreach (String file in openFileDialog1.FileNames)
                {
                    pathsToJsonFiles.Add(file);
                    textBoxLoadFromFolder.Text += openFileDialog1.FileName + ";";
                }                
            }
        }

        internal void buttonSaveBrowse_Click(object sender, EventArgs e)
        {            
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxSaveFolder.Text = folderBrowserDialog1.SelectedPath;
                pathToSaveBinaryFileFolder = folderBrowserDialog1.SelectedPath;
            }
        }

        internal void buttonGo_Click(object sender, EventArgs e)
        {
            int i = 1;
            foreach (string path in pathsToJsonFiles)            
            {
                JsonToBinary jsonya = new JsonToBinary(path, pathToSaveBinaryFileFolder);
                jsonya.WriteBinaryFiles();
                toolStripStatusLabel1.Text = "Processing...(" + i + "/" + Convert.ToString(pathsToJsonFiles.Count) + ")";                
                statusStrip1.Refresh();
                i++;
            }
            toolStripStatusLabel1.Text = "Success";
        }

        private void buttonHelp_Click(object sender, EventArgs e)
        {
            string exeDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);           
            Help.ShowHelp(this, exeDirectory + "\\" + "JsonСonverter.chm");
        }
    }       
}
