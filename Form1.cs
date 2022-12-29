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
        public string openedJson = "";
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
                CheckFileExists = true,
                CheckPathExists = true,
                FilterIndex = 1,
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

            save_file.FilterIndex = 1;
            if (save_file.ShowDialog() == DialogResult.OK)
            {                
                textBoxSaveFolder.Text = save_file.FileName;
                savedBinary = save_file.FileName;
            }
        }

        internal void buttonGo_Click(object sender, EventArgs e)
        {
            JsonClass seisFile = new JsonClass();

            using (StreamReader r = new StreamReader(openedJson))
            {
                string jsonInput = r.ReadToEnd();
                seisFile = JsonConvert.DeserializeObject<JsonClass>(jsonInput);
            }

            float[] signal = seisFile.signal;
            Int32[] signalInInt32 = new Int32[signal.Length];

            double maximumOfSignal = signal.Max();
            double minimumOfSignal = signal.Min();
            double maximumOfAmplitude = Math.Max(Math.Abs(maximumOfSignal), Math.Abs(minimumOfSignal));

            double coefNorm = Convert.ToDouble(1024) / maximumOfAmplitude;
            for (int i = 0; i < signal.Length; i++)
            {
                signalInInt32[i] = Convert.ToInt32(signal[i] * coefNorm);
            }

            double latitude = Convert.ToDouble(seisFile.N_wgs84_latitude.PadRight(8, '0').Substring(0,8).Replace('.',','));
            double longitude = Convert.ToDouble(seisFile.E_wgs84_longitude.PadRight(8, '0').Substring(0, 8).Replace('.', ','));

            DateTime dateTimeStart = seisFile.start_time;

            

            JsonFileBinary jsonya = new JsonFileBinary(savedBinary, signalInInt32, latitude, longitude, dateTimeStart);
            jsonya.WriteBinaryFile(savedBinary);
            MessageBox.Show("Гатова");
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
