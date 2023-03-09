using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JsonBinLib
{
    /// <summary>
    /// Class Constants.
    /// </summary>
    internal class Constants
    {
        public const string ComponentsOrder = "ZXY";
        public const int NormalizationMaximum = 32768;
        public const Int32 nullValue = -9999;

        /// <summary>
        /// Gets the channels count.
        /// </summary>
        /// <value>The channels count.</value>
        public static UInt16 channelsCount
        {
            get
            {
                return Convert.ToUInt16(Constants.ComponentsOrder.Length);
            }
        }

        /// <summary>
        /// Gets the baikal7 base date time.
        /// </summary>
        /// <value>The baikal7 base date time.</value>
        public static DateTime Baikal7BaseDateTime
        {
            get
            {
                return new DateTime(1980, 1, 1);
            }
        }

        /// <summary>
        /// Gets the index of the components.
        /// </summary>
        /// <value>String with the index of the components.</value>
        public static Dictionary<string, int> ComponentsIndex
        {
            get
            {
                var componentsIndexes = new Dictionary<string, int>();
                for (int i = 0; i < ComponentsOrder.Length; i++)
                {
                    componentsIndexes.Add(key: ComponentsOrder[i].ToString(), value: i);
                }
                return componentsIndexes;
            }
        }
    }

    /// <summary>
    /// Class JsonDataContainer, contains information from json.
    /// </summary>
    public class JsonDataContainer
    {
        public DateTime startTime { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string fileName { get; set; }
        public float[] signal { get; set; }
        public UInt16 frequency { get; set; }
        public string componentName { get; set; }
    }

    /// <summary>
    /// Class JsonParser, for deserialize json.
    /// </summary>
    public class JsonParser
    {
        public string pathToJsonFile;
        public JsonDataContainer jsonDeserialized;

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonParser"/> class.
        /// </summary>
        /// <param name="pathToJsonFile">The path to json file.</param>
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

    /// <summary>
    /// Class SeisBinaryFile.
    /// </summary>
    public class SeisBinaryFile
    {
        public JsonDataContainer jsonInfo;
        public string savePath;

        /// <summary>
        /// Initializes a new instance of the <see cref="SeisBinaryFile"/> class.
        /// </summary>
        /// <param name="jsonDeserialized">Class JsonDataContainer with information</param>
        /// <param name="savePath">The path to save binary file.</param>
        public SeisBinaryFile(JsonDataContainer jsonDeserialized, string savePath)
        {
            this.jsonInfo = jsonDeserialized;
            this.savePath = savePath;
        }

        /// <summary>
        /// Gets the byte size of the baikal7 header in memory.
        /// </summary>
        /// <value>Amount of bytes.</value>
        public int Baikal7HeaderMemorySize
        {
            get
            {
                int headerMemorySize = 120 + 72 * Constants.channelsCount;
                return headerMemorySize;
            }
        }

        /// <summary>
        /// Gets the baikal7 seconds for writing.
        /// </summary>
        /// <param name="startTime">The start time.</param>
        /// <returns>Second amount.</returns>
        public ulong GetBaikal7SecondsForWriting(DateTime startTime)
        {
            double secondsDuration = (startTime - Constants.Baikal7BaseDateTime).TotalSeconds;
            ulong secondsForWriting = Convert.ToUInt64(secondsDuration) * 256000000;
            return secondsForWriting;
        }

        /// <summary>
        /// Normalizes the signal, move wave in normalization range.
        /// </summary>
        /// <param name="originSignal">The origin signal.</param>
        /// <returns>Normalized signal</returns>
        /// <exception cref="System.DivideByZeroException"></exception>
        public Int32[] NormalizeSignal(float[] originSignal)
        {

            Int32[] normalizedSignal = new Int32[originSignal.Length];

            float minimumOrigin = originSignal.Min();
            float maximumOrigin = originSignal.Max();
            float height = maximumOrigin - minimumOrigin;
            double coeffNorm;

            if (height == 0)
                throw new DivideByZeroException();

            coeffNorm = Constants.NormalizationMaximum * 2 / height;
            double amplitudeOffset = minimumOrigin * coeffNorm + Constants.NormalizationMaximum;

            for (int i = 0; i < originSignal.Length; i++)
            {
                normalizedSignal[i] = Convert.ToInt32(originSignal[i] * coeffNorm - amplitudeOffset);
            }

            return normalizedSignal;
        }

        /// <summary>
        /// Saves binary seismic file in Baykal7 format.
        /// </summary>
        public void SaveToBaykal7Format()
        {
            Int32[] normalSignal = NormalizeSignal(this.jsonInfo.signal);

            using (FileStream filestream = new FileStream(this.savePath, FileMode.Create))
            {
                using (BinaryWriter binaryWriter = new BinaryWriter(filestream))
                {
                    Int32 value;

                    binaryWriter.Seek(0, SeekOrigin.Begin);
                    binaryWriter.Write(BitConverter.GetBytes(Constants.channelsCount));
                    binaryWriter.Seek(22, SeekOrigin.Begin);
                    binaryWriter.Write(BitConverter.GetBytes(this.jsonInfo.frequency));
                    binaryWriter.Seek(80, SeekOrigin.Begin);
                    binaryWriter.Write(BitConverter.GetBytes(this.jsonInfo.longitude));
                    binaryWriter.Seek(72, SeekOrigin.Begin);
                    binaryWriter.Write(BitConverter.GetBytes(this.jsonInfo.latitude));
                    binaryWriter.Seek(104, SeekOrigin.Begin);
                    binaryWriter.Write(BitConverter.GetBytes(GetBaikal7SecondsForWriting(this.jsonInfo.startTime)));

                    binaryWriter.Seek(Baikal7HeaderMemorySize, SeekOrigin.Begin);
                    Constants.ComponentsIndex.TryGetValue(this.jsonInfo.componentName, out int columnIndex);

                    for (int i = 0; i < normalSignal.Length; i++)
                    {
                        for (int j = 0; j < Constants.channelsCount; j++)
                        {
                            if (j == columnIndex)
                            {
                                value = normalSignal[i];
                            }
                            else
                            {
                                value = Constants.nullValue;
                            }

                            binaryWriter.Write(BitConverter.GetBytes(value));
                        }
                    }
                }
            }
        }
    }
}