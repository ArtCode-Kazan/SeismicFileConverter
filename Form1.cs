using System;
using System.Windows.Forms;
using BinReader;

namespace BinaryToJSONConverterApp
{   
    public partial class Form1 : Form
    {
        public string pathToOpenFile = "";
        public string pathToSaveFile = "";
        public Form1()
        {
            InitializeComponent();
        }

        public void buttonBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"D:\",
                Title = "Browse Binary Files",

                CheckFileExists = true,
                CheckPathExists = true,

                //DefaultExt = "txt",
                //Filter = "txt files (*.txt)|*.txt",
                FilterIndex = 1,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxLoadFromFolder.Text = folderBrowserDialog1.SelectedPath;
                pathToOpenFile = folderBrowserDialog1.SelectedPath;
            }
        }

        internal void buttonSaveBrowse_Click(object sender, EventArgs e)
        {
            SaveFileDialog save_file = new SaveFileDialog();
            save_file.Filter = "JSON File (*.json*)|*.json*";
            save_file.FilterIndex = 1;
            if (save_file.ShowDialog() == DialogResult.OK)
            {
                pathToSaveFile = save_file.FileName + ".json";
                textBoxSaveFolder.Text = save_file.FileName + ".json";
            }
        }

        internal void buttonGo_Click(object sender, EventArgs e)
        {
            BinarySeismicFile fileToConvert = new BinarySeismicFile(pathToOpenFile);
            JsonClass newJson = new JsonClass();
            FileHeader binaryHeader = fileToConvert.GetFileHeader;
            newJson.start_time = binaryHeader.datetimeStart;                        
            newJson.N_wgs84_latitude = Convert.ToString(binaryHeader.latitude);
            newJson.E_wgs84_longitude = Convert.ToString(binaryHeader.longitude);
            newJson.filename = Convert.ToString(binaryHeader.); ;
            newJson.signal = ;

        }
    }
    public class JsonClass
    {
        public DateTime start_time { get; set; }
        public string N_wgs84_latitude { get; set; }
        public string E_wgs84_longitude { get; set; }
        public string filename { get; set; }
        public float[] signal { get; set; }
    }
}
