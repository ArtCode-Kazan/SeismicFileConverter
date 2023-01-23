using Microsoft.VisualStudio.TestTools.UnitTesting;
using JsonBinLib;
using System;
using System.IO;
using System.Globalization;

namespace JsonBinLibTests
{
    [TestClass]
    public class JsonBinLibTest
    {
        [TestMethod]
        public void TestNormalizeSignal()
        {
            float[] inputArray = new float[50];
            Int32[] outputArray = new Int32[50];
            Int32[] outputArrayOrigin = new Int32[50];

            string[] lines = File.ReadAllLines(@"C:\Users\user\Desktop\jsontests\input.txt");
            int i = 0;

            foreach (string s in lines)
            {
                inputArray[i] = float.Parse(s.Replace('.','.'));
                i++;
            }
            
            i = 0;
            lines = File.ReadAllLines(@"C:\Users\user\Desktop\jsontests\output.txt");
            
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
