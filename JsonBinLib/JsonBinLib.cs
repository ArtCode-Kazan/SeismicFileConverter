using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonBinLib
{
    public class JsonFileBinary
    {
        public const string Sigma = ".bin";
        public const string Baikal7 = ".00";
        public const string Baikal8 = ".xx";
        string order = "ZXY";

        public string path;
        public Int32[] signal;
        public int channelCount;
        public int frequency;
        public double latitude;
        public double longitude;
        public DateTime dateTimeStart;


        public JsonFileBinary(
            string pathToSave,
            int[] signal,
            double latitude = 0,
            double longitude = 0,
            DateTime dateTimeStart = new DateTime(),
            int channelCount = 3,
            int frequency = 1000
            )
        {
            this.path = pathToSave;
            this.signal = signal;
            this.channelCount = channelCount;
            this.frequency = frequency;
            this.latitude = latitude;
            this.longitude = longitude;
            this.dateTimeStart = dateTimeStart;
        }

        public void WriteBinaryFile(string path)
        {
            FileStream filestream = new FileStream(path, FileMode.Create);
            //File binfile = File.Open(fileName, FileMode.Create)
            BinaryWriter binaryWriter = new BinaryWriter(filestream);

            if (FileExtension == ".bin")
            {
                binaryWriter.Seek(12, SeekOrigin.Begin);
                binaryWriter.Write(BitConverter.GetBytes(Convert.ToUInt16(this.channelCount)));
                binaryWriter.Seek(24, SeekOrigin.Begin);
                binaryWriter.Write(BitConverter.GetBytes(Convert.ToUInt16(this.frequency)));
                binaryWriter.Seek(40, SeekOrigin.Begin);
                string latitudeString = Convert.ToString(this.latitude);
                binaryWriter.Write(latitudeString); //string 8 digits
                binaryWriter.Seek(48, SeekOrigin.Begin);
                string longitudeString = Convert.ToString(this.latitude);
                binaryWriter.Write(longitudeString); //string 8 digits
                binaryWriter.Seek(60, SeekOrigin.Begin);
                string date = Convert.ToString(this.dateTimeStart.Year).Substring(2, 2)
                    + Convert.ToString(this.dateTimeStart.Month).PadLeft(2, '0')
                + Convert.ToString(this.dateTimeStart.Day).PadLeft(2, '0');
                string time = Convert.ToString(this.dateTimeStart.Hour).PadLeft(2, '0')
                    + Convert.ToString(this.dateTimeStart.Minute).PadLeft(2, '0')
                + Convert.ToString(this.dateTimeStart.Second).PadLeft(2, '0');
                binaryWriter.Write(BitConverter.GetBytes(Convert.ToUInt32(date)));
                binaryWriter.Seek(64, SeekOrigin.Begin);
                binaryWriter.Write(BitConverter.GetBytes(Convert.ToUInt32(time)));
            }

            else if (FileExtension == ".00") // main
            {
                binaryWriter.Seek(0, SeekOrigin.Begin);
                binaryWriter.Write(BitConverter.GetBytes(Convert.ToUInt16(this.channelCount)));
                binaryWriter.Seek(22, SeekOrigin.Begin);
                binaryWriter.Write(BitConverter.GetBytes(Convert.ToUInt16(this.frequency)));
                binaryWriter.Seek(80, SeekOrigin.Begin);
                binaryWriter.Write(BitConverter.GetBytes(Convert.ToDouble(this.longitude)));
                binaryWriter.Seek(72, SeekOrigin.Begin);
                binaryWriter.Write(BitConverter.GetBytes(Convert.ToDouble(this.latitude)));
                binaryWriter.Seek(104, SeekOrigin.Begin);

                DateTime constDatetime = new DateTime(1980, 1, 1);
                double secondsDuraion = (this.dateTimeStart - constDatetime).TotalSeconds;
                ulong secondsForWriting = Convert.ToUInt64(secondsDuraion) * 256000000;

                binaryWriter.Write(BitConverter.GetBytes(secondsForWriting));
            }

            else if (FileExtension == ".xx")
            {
                binaryWriter.Seek(0, SeekOrigin.Begin);
                binaryWriter.Write(BitConverter.GetBytes(Convert.ToUInt16(this.channelCount)));
                binaryWriter.Seek(6, SeekOrigin.Begin);
                UInt16 day = Convert.ToUInt16(this.dateTimeStart.Day);
                binaryWriter.Write(BitConverter.GetBytes(day));
                binaryWriter.Seek(8, SeekOrigin.Begin);
                UInt16 month = Convert.ToUInt16(this.dateTimeStart.Month);
                binaryWriter.Write(BitConverter.GetBytes(month));
                binaryWriter.Seek(10, SeekOrigin.Begin);
                UInt16 year = Convert.ToUInt16(this.dateTimeStart.Year);
                binaryWriter.Write(BitConverter.GetBytes(year));
                binaryWriter.Seek(48, SeekOrigin.Begin);
                binaryWriter.Write(BitConverter.GetBytes(Convert.ToDouble(1) / this.frequency));
                binaryWriter.Seek(56, SeekOrigin.Begin);
                double second = Convert.ToDouble(this.dateTimeStart.Second);
                binaryWriter.Write(BitConverter.GetBytes(second));
                binaryWriter.Seek(72, SeekOrigin.Begin);
                binaryWriter.Write(BitConverter.GetBytes(Convert.ToDouble(this.latitude)));
                binaryWriter.Seek(80, SeekOrigin.Begin);
                binaryWriter.Write(BitConverter.GetBytes(Convert.ToDouble(this.longitude)));
            }

            int headerMemorySize = 120 + 72 * channelCount;
            int columnIndex = 1 * 4;
            binaryWriter.Seek(headerMemorySize + columnIndex, 0);

            for (int i = 0; i < signal.Length; i++)
            {
                binaryWriter.Write(BitConverter.GetBytes(Convert.ToInt32(signal[i])));
                binaryWriter.Seek(4 * 2, SeekOrigin.Current);
            }
            binaryWriter.Close();
            filestream.Close();
        }
        public string FileExtension
        {   
            get
            {
                return System.IO.Path.GetExtension(this.path);
            }
        }
    }
}
