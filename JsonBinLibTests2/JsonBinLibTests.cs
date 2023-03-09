using Microsoft.VisualStudio.TestTools.UnitTesting;
using JsonBinLib;
using System;
using Moq;

namespace JsonBinLibTests
{
    public class HelperFunctions
    {
        public static DateTime AppendSecondsToDateTimeBaikal7(ulong secondsamount)
        {
            DateTime Baikal7 = new DateTime(1980, 1, 1, 0, 0, 0).AddSeconds(secondsamount);
            return Baikal7;
        }

        public static int[] RandomArray(int length)
        {
            int[] arr = new int[length];
            Random rand = new Random();
            arr[0] = -32768;
            arr[1] = 32768;

            for (int i = 2; i < arr.Length; i++)
            {
                arr[i] = rand.Next(-32768, 32768);
            }
            return arr;
        }

        public static float[] FormatArray(int[] array, float coefficient = 35000)
        {
            float[] formatArr = new float[array.Length];

            for (int i = 0; i < formatArr.Length; i++)
            {
                formatArr[i] = (float)array[i] / coefficient;
            }
            return formatArr;
        }

        public static int[] MoveArray(int[] array, int coef = 2354)
        {
            int[] arr = new int[array.Length];

            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = array[i] - coef;
            }
            return arr;
        }
    }

    [TestClass]
    public class JsonBinLibTest
    {
        [TestMethod]
        public void Baikal7HeaderMemorySize()
        {
            SeisBinaryFile testclass = new SeisBinaryFile(new JsonDataContainer(), "");
            int actualSeconds = testclass.Baikal7HeaderMemorySize;
            Assert.AreEqual(336, actualSeconds);
        }

        [TestMethod]
        [DataRow((UInt64)0, (UInt64)0)]
        [DataRow((UInt64)10, (UInt64)2560000000)]
        [DataRow((UInt64)198651, (UInt64)50854656000000)]
        [DataRow((UInt64)16516186181, (UInt64)4228143662336000000)]
        [DataRow((UInt64)11911, (UInt64)3049216000000)]
        public void TestGetBaikal7SecondsForWriting(ulong realSeconds, ulong expectedSeconds)
        {
            SeisBinaryFile testclass = new SeisBinaryFile(new JsonDataContainer(), "");
            ulong actualSeconds = testclass.GetBaikal7SecondsForWriting(HelperFunctions.AppendSecondsToDateTimeBaikal7(realSeconds));
            Assert.AreEqual(expectedSeconds, actualSeconds);
        }

        [TestMethod]
        public void TestNormalizeSignal()
        {
            int[] expectedArray = HelperFunctions.RandomArray(1000);
            float[] inputArray = HelperFunctions.FormatArray(HelperFunctions.MoveArray(expectedArray));
            var mock = new Mock<SeisBinaryFile>(new JsonDataContainer(), "") { CallBase = true };
            int[] actualArray = mock.Object.NormalizeSignal(inputArray);

            for (int j = 0; j < actualArray.Length; j++)
            {
                Assert.AreEqual(expectedArray[j], actualArray[j]);
            }
        }

        [TestMethod]
        public void TestNormalizeSignalException()
        {
            float[] inputArray = new float[] { -9999, -9999, -9999, -9999, -9999 };
            var mock = new Mock<SeisBinaryFile>(new JsonDataContainer(), "") { CallBase = true };
            bool actualException;

            try
            {
                int[] outputArrayProgram = mock.Object.NormalizeSignal(inputArray);
                actualException = false;
            }
            catch
            {
                actualException = true;
            }

            Assert.AreEqual(true, actualException);
        }
    }
}
