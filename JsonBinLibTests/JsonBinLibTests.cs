using Microsoft.VisualStudio.TestTools.UnitTesting;
using JsonBinLib;
using System;
using System.IO;

namespace JsonBinLibTests
{
    [TestClass]
    public class JsonBinLibTest
    {
        [TestMethod]
        [DataRow(@"C:\Users\user\Desktop\jsontests\input.txt", @"C:\Users\user\Desktop\jsontests\output.txt")]
        [DataRow(@"C:\Users\user\Desktop\jsontests\inputBig.txt", @"C:\Users\user\Desktop\jsontests\outputBig.txt")]
        public void TestNormalizeSignal(string inPath, string outPath)
        {
            float[] inputArray = new float[50];
            Int32[] outputArray = new Int32[50];
            Int32[] outputArrayOrigin = new Int32[50];

            string[] lines = File.ReadAllLines(inPath);
            int i = 0;

            foreach (string s in lines)
            {
                inputArray[i] = float.Parse(s);
                i++;
            }

            i = 0;
            lines = File.ReadAllLines(outPath);

            foreach (string s in lines)
            {
                outputArrayOrigin[i] = Int32.Parse(s);
                i++;
            }

            SeisBinaryFile testclass = new SeisBinaryFile(new JsonDataContainer(), "");
            outputArray = testclass.NormalizeSignal(inputArray);

            for (int j = 0; j < outputArray.Length; j++)
            {
                Assert.AreEqual(outputArrayOrigin[j], outputArray[j]);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException), "Invalid currency.(zero)")]
        [DataRow(@"C:\Users\user\Desktop\jsontests\inputZero.txt", @"C:\Users\user\Desktop\jsontests\outputZero.txt")]
        [DataRow(@"C:\Users\user\Desktop\jsontests\inputSingle.txt", @"C:\Users\user\Desktop\jsontests\outputSingle.txt")]
        public void TestNormalizeSignalZeroDivision(string inPath, string outPath)
        {
            float[] inputArray = new float[50];
            Int32[] outputArray = new Int32[50];
            Int32[] outputArrayOrigin = new Int32[50];

            string[] lines = File.ReadAllLines(inPath);
            int i = 0;

            foreach (string s in lines)
            {
                inputArray[i] = float.Parse(s);
                i++;
            }

            i = 0;
            lines = File.ReadAllLines(outPath);

            foreach (string s in lines)
            {
                outputArrayOrigin[i] = Int32.Parse(s);
                i++;
            }

            SeisBinaryFile testclass = new SeisBinaryFile(new JsonDataContainer(), "");
            outputArray = testclass.NormalizeSignal(inputArray);

            for (int j = 0; j < outputArray.Length; j++)
            {
                Assert.AreEqual(outputArrayOrigin[j], outputArray[j]);
            }
        }

        public DateTime AppendSecondsToDateTimeBaikal7(ulong secondsamount)
        { 
            DateTime Baikal7 = new DateTime(1980, 1, 1, 0, 0, 0).AddSeconds(secondsamount);
            return Baikal7;
        }

        [TestMethod]
        [DataRow((UInt64)0, (UInt64)0)]
        [DataRow((UInt64)10, (UInt64)2560000000)]
        [DataRow((UInt64)198651, (UInt64)50854656000000)]
        [DataRow((UInt64)16516186181, (UInt64)4228143662336000000)]
        [DataRow((UInt64)11911, (UInt64)3049216000000)]
        public void TestGetBaikal7SecondsForWriting(ulong realSeconds, ulong formatSeconds)
        {
            SeisBinaryFile testclass = new SeisBinaryFile(new JsonDataContainer(), "");
            ulong actualSeconds = testclass.GetBaikal7SecondsForWriting(AppendSecondsToDateTimeBaikal7(realSeconds));
            Assert.AreEqual(formatSeconds, actualSeconds);
        }

        [TestMethod]
        public void Baikal7HeaderMemorySize()
        {
            SeisBinaryFile testclass = new SeisBinaryFile(new JsonDataContainer(), "");
            int actualSeconds = testclass.Baikal7HeaderMemorySize;
            Assert.AreEqual(336, actualSeconds);
        }
        
        [TestMethod]
        [DataRow((UInt64)10, (UInt64)2560000000)]
        public void Baikal7HeaderMemorySize(int signalLenght, int byteLenght)
        {
            SeisBinaryFile testclass = new SeisBinaryFile(new JsonDataContainer(startTime), "");
            int actualSeconds = testclass.Baikal7HeaderMemorySize;
            Assert.AreEqual(336, actualSeconds);
        }
    }
}
