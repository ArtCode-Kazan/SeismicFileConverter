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
    }
}
