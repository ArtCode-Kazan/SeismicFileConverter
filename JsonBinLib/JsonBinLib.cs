﻿using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;

namespace JsonBinLib
{
    internal class Constants
    {
        public const string ComponentsOrder = "ZXY";
        public const int NormalizationMaximum = 32768;
        public const int componentOffset = 1;
    }
    public class JsonDataContainer
    {
        public DateTime start_time { get; set; }
        public double N_wgs84_latitude { get; set; }
        public double E_wgs84_longitude { get; set; }
        public string filename { get; set; }
        public float[] signal { get; set; }
        public UInt16 frequency { get; set; }
        public string componentName { get; set; }
    }
    public class JsonParser
    {
        public string pathToJsonFile;
        public JsonDataContainer jsonDeserialized;

        public JsonParser(string pathToJsonFile)
        {
            this.pathToJsonFile = pathToJsonFile;

            using (StreamReader reader = new StreamReader(this.pathToJsonFile))
            {
                string jsonString = reader.ReadToEnd();
                this.jsonDeserialized = JsonConvert.DeserializeObject<JsonDataContainer>(jsonString);
            }
        }
    }
    public class SeisBinaryFile
    {
        public JsonDataContainer jsonInfo;
        public string savePath;

        public SeisBinaryFile(JsonDataContainer jsonDeserialized, string savePath)
        {
            this.jsonInfo = jsonDeserialized;
            this.savePath = savePath;
        }
        public UInt16 channelsCount
        {
            get
            {
                return Convert.ToUInt16(Constants.ComponentsOrder.Length);
            }
        }
        public Int32[] NormalizeSignal(float[] originSignal)
        {
            float minimumOrigin = originSignal.Min();
            float[] positiveSignal = originSignal;

            for (int i = 0; i < positiveSignal.Length; i++)
            {
                positiveSignal[i] -= minimumOrigin;
            }

            float maximumPositive = positiveSignal.Max();
            double coeffNorm = (Constants.NormalizationMaximum * 2) / maximumPositive;
            Int32[] normalizedSignal = new Int32[positiveSignal.Length];

            for (int i = 0; i < originSignal.Length; i++)
            {
                normalizedSignal[i] = Convert.ToInt32(positiveSignal[i] * coeffNorm - Constants.NormalizationMaximum);
            }

            return normalizedSignal;
        }
        public void SaveToBaykal7Format()
        {
            Int32[] normalSignal = NormalizeSignal(this.jsonInfo.signal);

            using (FileStream filestream = new FileStream(this.savePath, FileMode.Create))
            {
                using (BinaryWriter binaryWriter = new BinaryWriter(filestream))
                {
                    binaryWriter.Seek(0, SeekOrigin.Begin);
                    binaryWriter.Write(BitConverter.GetBytes(channelsCount));
                    binaryWriter.Seek(22, SeekOrigin.Begin);
                    binaryWriter.Write(BitConverter.GetBytes(this.jsonInfo.frequency));
                    binaryWriter.Seek(80, SeekOrigin.Begin);
                    binaryWriter.Write(BitConverter.GetBytes(this.jsonInfo.E_wgs84_longitude));
                    binaryWriter.Seek(72, SeekOrigin.Begin);
                    binaryWriter.Write(BitConverter.GetBytes(this.jsonInfo.N_wgs84_latitude));

                    binaryWriter.Seek(104, SeekOrigin.Begin);
                    DateTime constDatetime = new DateTime(1980, 1, 1);
                    double secondsDuraion = (this.jsonInfo.start_time - constDatetime).TotalSeconds;
                    ulong secondsForWriting = Convert.ToUInt64(secondsDuraion) * 256000000;
                    binaryWriter.Write(BitConverter.GetBytes(secondsForWriting));

                    int headerMemorySize = 120 + 72 * channelsCount;
                    int columnIndex = Constants.componentOffset * sizeof(int);
                    int stridesSize = sizeof(int) * channelsCount;
                    binaryWriter.Seek(headerMemorySize + columnIndex, SeekOrigin.Begin);

                    for (int i = 0; i < normalSignal.Length; i++)
                    {
                        binaryWriter.Write(BitConverter.GetBytes(normalSignal[i]));
                        binaryWriter.Seek(stridesSize - sizeof(int), SeekOrigin.Current);
                    }
                }
            }
        }
    }
}
