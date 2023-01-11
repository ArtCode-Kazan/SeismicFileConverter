using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;

namespace JsonBinLib
{
    public class SeisBinaryFile
    {
        public const string componentOrder = "ZXY";

        private string savePath;
        private float[] signal;
        private string latitude;
        private string longitude;
        private DateTime dateTimeStart;
        private int channelCount;
        private int frequency;

        public SeisBinaryFile(
            string pathToSaveFile,
            float[] signal,
            string latitude = "",
            string longitude = "",
            DateTime dateTimeStart = new DateTime(),
            int frequency = 1000
            )
        {
            this.savePath = pathToSaveFile;
            this.signal = signal;
            this.channelCount = componentOrder.Length;
            this.frequency = frequency;
            this.latitude = latitude;
            this.longitude = longitude;
            this.dateTimeStart = dateTimeStart;
        }
        public int channelsCount
            { 
            get { return channelCount; } 
        }
        public Int32[] NormalizeNConvertSignal(float[] originSignal)
        {
            Int32[] signalInInt32 = new Int32[originSignal.Length];
            double maximumOfSignal = originSignal.Max();
            double minimumOfSignal = originSignal.Min();
            double maximumOfAmplitude = Math.Max(Math.Abs(maximumOfSignal), Math.Abs(minimumOfSignal));
            double coeffNorm = Convert.ToDouble(1024) / maximumOfAmplitude;

            for (int i = 0; i < originSignal.Length; i++)
            {
                signalInInt32[i] = Convert.ToInt32(originSignal[i] * coeffNorm);
            }

            return signalInInt32;
        }
        public void Save()
        {                        
            Int32[] normalSignal = NormalizeNConvertSignal(this.signal);                                                

            using (FileStream filestream = new FileStream(this.savePath, FileMode.Create))
            {
                using (BinaryWriter binaryWriter = new BinaryWriter(filestream))
                {
                    binaryWriter.Seek(0, SeekOrigin.Begin);
                    binaryWriter.Write(BitConverter.GetBytes(Convert.ToUInt16(this.channelCount)));
                    binaryWriter.Seek(22, SeekOrigin.Begin);
                    binaryWriter.Write(BitConverter.GetBytes(Convert.ToUInt16(this.frequency)));
                    binaryWriter.Seek(80, SeekOrigin.Begin);
                    binaryWriter.Write(BitConverter.GetBytes(Convert.ToDouble(this.longitude.PadRight(8, '0').Substring(0, 8).Replace('.', ','))));
                    binaryWriter.Seek(72, SeekOrigin.Begin);
                    binaryWriter.Write(BitConverter.GetBytes(Convert.ToDouble(this.latitude.PadRight(8, '0').Substring(0, 8).Replace('.', ','))));
                    
                    binaryWriter.Seek(104, SeekOrigin.Begin);
                    DateTime constDatetime = new DateTime(1980, 1, 1);
                    double secondsDuraion = (this.dateTimeStart - constDatetime).TotalSeconds;
                    ulong secondsForWriting = Convert.ToUInt64(secondsDuraion) * 256000000;
                    binaryWriter.Write(BitConverter.GetBytes(secondsForWriting));

                    int headerMemorySize = 120 + 72 * this.channelCount;
                    int columnIndex = 1 * sizeof(int);
                    int stridesSize = sizeof(int) * this.channelCount;
                    binaryWriter.Seek(headerMemorySize + columnIndex, 0);

                    for (int i = 0; i < signal.Length; i++)
                    {
                        binaryWriter.Write(BitConverter.GetBytes(normalSignal[i]));
                        binaryWriter.Seek(stridesSize - sizeof(int), SeekOrigin.Current);
                    }
                }
            }
        }                
    }
    public class SeisFileConverter
    {
        public string pathToJsonFile;
        public string pathToSaveFolder;

        public SeisFileConverter(string pathToJsonFile, string pathToSaveFolder)
        {
            this.pathToJsonFile = pathToJsonFile;
            this.pathToSaveFolder = pathToSaveFolder;
        }
        private JsonClass ParseJson(string pathToJsonFile)
        {
            JsonClass seisFile;
            using (StreamReader reader = new StreamReader(pathToJsonFile))
            {
                seisFile = JsonConvert.DeserializeObject<JsonClass>(reader.ReadToEnd());
            }

            return seisFile;
        }
        public void SaveBinary()
        {
            JsonClass jsonBinary = ParseJson(this.pathToJsonFile);
            string saveBinaryPath = this.pathToSaveFolder + "/" + jsonBinary.filename + ".00";
            SeisBinaryFile binaryFile = new SeisBinaryFile(
                saveBinaryPath,
                jsonBinary.signal,
                jsonBinary.N_wgs84_latitude,
                jsonBinary.E_wgs84_longitude,
                jsonBinary.start_time
                );
            binaryFile.Save();            
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
