using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;

namespace JsonBinLib
{
    public class JsonFileBinary
    {
        public const string SigmaExt = "bin";
        public const string Baikal7Ext = "00";
        public const string Baikal8Ext = "xx";
        string order = "ZXY";

        public string path;
        public Int32[] signal;
        public int channelCount;
        public int frequency;
        public string latitude;
        public string longitude;
        public DateTime dateTimeStart;


        public JsonFileBinary(
            string pathToSave,
            int[] signalNormalize,
            string latitude = "",
            string longitude = "",
            DateTime dateTimeStart = new DateTime(),
            int channelCount = 3,
            int frequency = 1000
            )
        {
            this.path = pathToSave;
            this.signal = signalNormalize;
            this.channelCount = channelCount;
            this.frequency = frequency;
            this.latitude = latitude;
            this.longitude = longitude;
            this.dateTimeStart = dateTimeStart;
        }

        public JsonFileBinary(
            string pathToJson,
            string pathToSave
            )
        {
            JsonClass jsonya = returnJsonClass(pathToJson);
            this.path = pathToSave;
            this.signal = NormalizeNConvertSignal(jsonya.signal);
            this.channelCount = order.Length;
            this.frequency = 1000;
            this.latitude = jsonya.N_wgs84_latitude;
            this.longitude = jsonya.E_wgs84_longitude;
            this.dateTimeStart = jsonya.start_time;

        }

        public void WriteBinaryFile(string path)
        {
            using (FileStream filestream = new FileStream(path, FileMode.Create))
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
