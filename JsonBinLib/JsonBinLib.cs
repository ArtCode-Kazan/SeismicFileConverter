using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JsonBinLib
{
    public class JsonToBinary
    {
        public const string SigmaExt = "bin";
        public const string Baikal7Ext = "00";
        public const string Baikal8Ext = "xx";
        string order = "ZXY";

        public string pathToJsonFile;
        public string pathToSaveFolder;

        public JsonToBinary(
            string pathToJsonFile,
            string pathToSaveFolder
            )
        {
            this.pathToJsonFile = pathToJsonFile;
            this.pathToSaveFolder = pathToSaveFolder;
        }

        public void WriteBinaryFiles()
        {
            JsonClass jsonya = returnJsonClass(pathToJsonFile);
            string pathToSave = this.pathToSaveFolder + "/" + jsonya.filename + ".00";
            Int32[] signal = NormalizeNConvertSignal(jsonya.signal);
            int channelCount = order.Length;
            int frequency = 1000;
            string latitude = jsonya.N_wgs84_latitude;
            string longitude = jsonya.E_wgs84_longitude;
            DateTime dateTimeStart = jsonya.start_time;

            using (FileStream filestream = new FileStream(pathToSave, FileMode.Create))
            {
                using (BinaryWriter binaryWriter = new BinaryWriter(filestream))
                {
                    binaryWriter.Seek(0, SeekOrigin.Begin);
                    binaryWriter.Write(BitConverter.GetBytes(Convert.ToUInt16(channelCount)));
                    binaryWriter.Seek(22, SeekOrigin.Begin);
                    binaryWriter.Write(BitConverter.GetBytes(Convert.ToUInt16(frequency)));
                    binaryWriter.Seek(80, SeekOrigin.Begin);
                    binaryWriter.Write(BitConverter.GetBytes(Convert.ToDouble(longitude.PadRight(8, '0').Substring(0, 8).Replace('.', ','))));
                    binaryWriter.Seek(72, SeekOrigin.Begin);
                    binaryWriter.Write(BitConverter.GetBytes(Convert.ToDouble(latitude.PadRight(8, '0').Substring(0, 8).Replace('.', ','))));
                    binaryWriter.Seek(104, SeekOrigin.Begin);

                    DateTime constDatetime = new DateTime(1980, 1, 1);
                    double secondsDuraion = (dateTimeStart - constDatetime).TotalSeconds;
                    ulong secondsForWriting = Convert.ToUInt64(secondsDuraion) * 256000000;

                    binaryWriter.Write(BitConverter.GetBytes(secondsForWriting));

                    int headerMemorySize = 120 + 72 * channelCount;
                    int columnIndex = 1 * sizeof(int);
                    int stridesSize = sizeof(int) * channelCount;
                    binaryWriter.Seek(headerMemorySize + columnIndex, 0);

                    for (int i = 0; i < signal.Length; i++)
                    {
                        binaryWriter.Write(BitConverter.GetBytes(Convert.ToInt32(signal[i])));
                        binaryWriter.Seek(stridesSize - sizeof(int), SeekOrigin.Current);
                    }
                }
            }
        }

        public JsonClass returnJsonClass(string pathToJson)
        {
            JsonClass seisFile;
            using (StreamReader reader = new StreamReader(pathToJson))
            {
                seisFile = JsonConvert.DeserializeObject<JsonClass>(reader.ReadToEnd());
            }

            return seisFile;
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
