using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using JsonBinLib;

namespace BinaryToJSONConverterApp
{
    public partial class MainConverterWindow : Form
    {
        public List<string> pathsToJson = new List<string>();
        public string pathSaveBinaryFolder = "";
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
                foreach (String path in openFileDialog.FileNames)
                {
                    this.pathsToJson.Add(path);
                    textBoxLoadFromFolder.Text += openFileDialog.FileName + ";";
                }                
            }
        }

        internal void buttonBrowseSaveFolder_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxSaveFolder.Text = folderBrowserDialog.SelectedPath;
                this.pathSaveBinaryFolder = folderBrowserDialog.SelectedPath;
            }
        }

        internal void buttonConvert_Click(object sender, EventArgs e)
        {            
            for (int i = 0; i < this.pathsToJson.Count; i++)  
            {
                string path = this.pathsToJson[i];
                SeisJsonParser jsonParser = new SeisJsonParser(path);                
                string binaryFileName = jsonParser.jsonDeserialized.filename + ".00";
                string pathSaveBinary = Path.Combine(this.pathSaveBinaryFolder, binaryFileName);                
    
                SeisBinaryFile binaryFile = new SeisBinaryFile(jsonParser.jsonDeserialized, path);
                toolStripStatusLabel.Text = "Processing...(" + (i + 1) + "/" + Convert.ToString(this.pathsToJson.Count) + ")";                
                statusStrip.Refresh();             
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
