using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using JsonBinLib;
using Newtonsoft.Json;

namespace BinaryToJSONConverterApp
{
    public partial class Form1 : Form
    {
        public string openedJson;
        public string savedBinary = "";

        public Form1()
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

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxLoadFromFolder.Text = openFileDialog1.FileName;
                openedJson = openFileDialog1.FileName;
            }
        }

        internal void buttonSaveBrowse_Click(object sender, EventArgs e)
        {
            SaveFileDialog save_file = new SaveFileDialog();
            save_file.Filter = "Baikal7|*.00|Baikal8|*.xx|Sigma|*.bin";

            if (save_file.ShowDialog() == DialogResult.OK)
            {
                textBoxSaveFolder.Text = save_file.FileName;
                savedBinary = save_file.FileName;
            }
        }

        internal void buttonGo_Click(object sender, EventArgs e)
        {
            JsonClass seisFile = ReadClassFromJson(openedJson);
                        
            Int32[] signalInInt32 = NormalizeNConvertSignal(seisFile.signal);            
            double latitude = Convert.ToDouble(seisFile.NWGS84Latitude.PadRight(8, '0').Substring(0, 8).Replace('.', ','));
            double longitude = Convert.ToDouble(seisFile.EWGS84Longitude.PadRight(8, '0').Substring(0, 8).Replace('.', ','));
            DateTime dateTimeStart = seisFile.startTime;

            JsonFileBinary jsonya = new JsonFileBinary(savedBinary, signalInInt32, latitude, longitude, dateTimeStart);
            jsonya.WriteBinaryFile(savedBinary);

            MessageBox.Show("Готова");
        }

        public Int32[] NormalizeNConvertSignal(float[] originSignal)
        {
            Int32[] signalInInt32 = new Int32[originSignal.Length];
            double maximumOfSignal = originSignal.Max();
            double minimumOfSignal = originSignal.Min();
            double maximumOfAmplitude = Math.Max(Math.Abs(maximumOfSignal), Math.Abs(minimumOfSignal));
            double coefNorm = Convert.ToDouble(1024) / maximumOfAmplitude;
            
            for (int i = 0; i < originSignal.Length; i++)
            {
                signalInInt32[i] = Convert.ToInt32(originSignal[i] * coefNorm);
            }

            return signalInInt32;
        }

        public JsonClass ReadClassFromJson(string filePath)
        {
            JsonClass seisFile = new JsonClass();
            using (StreamReader reader = new StreamReader(filePath))
            {
                string jsonInput = reader.ReadToEnd();
                seisFile = JsonConvert.DeserializeObject<JsonClass>(jsonInput);
            }

            return seisFile;
        }
    }    
    public class JsonClass
    {
        public DateTime startTime { get; set; }
        public string NWGS84Latitude { get; set; }
        public string EWGS84Longitude { get; set; }
        public string fileName { get; set; }
        public float[] signal { get; set; }
    }
}
