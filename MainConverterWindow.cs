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
        public string pathToJsonFile;
        public string pathToSaveBinaryFile = "";

        public MainConverterWindow()
        {
            InitializeComponent();
        }

        public void buttonBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"D:\",
                Title = "Browse JSON Files",
                RestoreDirectory = true,
                ReadOnlyChecked = true,
                ShowReadOnly = true
            };
            openFileDialog1.Filter = "JSON files (*.json)|*.json";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxLoadFromFolder.Text = openFileDialog1.FileName;
                pathToJsonFile = openFileDialog1.FileName;
            }
        }

        internal void buttonSaveBrowse_Click(object sender, EventArgs e)
        {
            SaveFileDialog save_file = new SaveFileDialog();
            save_file.Filter = "Baikal7|*.00|Baikal8|*.xx|Sigma|*.bin";

            if (save_file.ShowDialog() == DialogResult.OK)
            {
                textBoxSaveFolder.Text = save_file.FileName;
                pathToSaveBinaryFile = save_file.FileName;
            }
        }

        internal void buttonGo_Click(object sender, EventArgs e)
        {            
            JsonFileBinary jsonya = new JsonFileBinary(pathToJsonFile, pathToSaveBinaryFile);
            jsonya.WriteBinaryFile(pathToSaveBinaryFile);

            MessageBox.Show("Готова");
        }
    }       
}
