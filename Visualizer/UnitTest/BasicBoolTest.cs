using System;
using System.Security.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShaCalc.Model;
using ShaCalc.Sha256Net;

namespace UnitTest
{
    [TestClass]
    public class BasicBoolTest
    {
        uint[] table = { 368779801, 3340276706, 451503747, 453347691, 1827579633, 546540889, 3764776411, 324309599, 1449763902, 3349866931 };

        private static uint RotRight(uint x, byte n)
        {
            return (x >> n) | (x << (32 - n));
        }

        [TestMethod]
        public void TestBitOperators()
        {
            for (int i = 0; i < 2; i++)
            {
                bool a = i > 0;

                Assert.AreEqual(new NOT(new ConstantBit(a)).Get(), !a);

                for (int j = 0; j < 2; j++)
                {
                    bool b = j > 0;
                    Assert.AreEqual(new AND(new ConstantBit(a), new ConstantBit(b)).Get(), a & b);
                    Assert.AreEqual(new OR(new ConstantBit(a), new ConstantBit(b)).Get(), a || b);
                    Assert.AreEqual(new XOR(new ConstantBit(a), new ConstantBit(b)).Get(), a ^ b);
                }
            }
        }

        [TestMethod]
        public void TestConstants()
        {
            Assert.IsFalse(new ConstantBit(false).Get().Value);
            Assert.IsTrue(new ConstantBit(true).Get().Value);
            Assert.IsNull(new VariableBit().Get());

            for (int i = 0; i < table.Length; i++)
                Assert.AreEqual(new IntValue(table[i]).Get(), table[i]);

            Assert.AreEqual(new ByteValue(0x12).Get(), 0x12);
            Assert.AreEqual(new ByteValue(0xFA).Get(), 0xFA);
            Assert.AreEqual(new ByteValue(0xfa4556).Get(), 0x56);

            Assert.AreEqual(new IntValue(new ByteValue(0x3f), new ByteValue(0x4d), new ByteValue(0x8a), new ByteValue(0x01)).Get(), (uint)0x018a4d3f);
        }

        [TestMethod]
        public void TestShift()
        {
            for (int i = 0; i < table.Length; i++)
            {
                uint a = table[i];
                byte n = (byte)(i % 32);
                Assert.AreEqual(new IntValue(a).ShiftCirc(n).Get(), RotRight(a, n), "Shift error for {0} >> {1}", a, n);
                Assert.AreEqual(new IntValue(a).Shift(n).Get(), a >> n, "Shift error for {0} >> {1}", a, n);
            }
        }

        [TestMethod]
        public void TestIntOperators()
        {
            for (int i = 1; i < table.Length; i++)
            {
                uint a = table[i - 1];
                uint b = table[i];

                Assert.AreEqual(new NOT32(new IntValue(a)).Get(), ~a);
                Assert.AreEqual(new XOR32(new IntValue(a), new IntValue(b)).Get(), a ^ b);
                Assert.AreEqual(new AND32(new IntValue(a), new IntValue(b)).Get(), a & b);
            }
        }

        [TestMethod]
        public void TestArithmetic()
        {
            for (int i = 1; i < table.Length; i++)
            {
                uint a = table[i - 1];
                uint b = table[i];

                Assert.AreEqual(new IntValue(a).Add(new IntValue(b)).Get(), a + b);
            }
        }

        [TestMethod]
        public void TestSHAParts()
        {
            for (int i = 0; i < table.Length - 3; i++)
            {
                uint x = table[i];
                uint y = table[i + 1];
                uint z = table[i + 2];

                var X = new IntValue(x);
                var Y = new IntValue(y);
                var Z = new IntValue(z);

                Assert.AreEqual(new Ch(X, Y, Z).Get(), (x & y) ^ (~x & z));
                Assert.AreEqual(new EP0(X).Get(), RotRight(x, 2) ^ RotRight(x, 13) ^ RotRight(x, 22));
                Assert.AreEqual(new EP1(X).Get(), RotRight(x, 6) ^ RotRight(x, 11) ^ RotRight(x, 25));
                Assert.AreEqual(new Maj(X, Y, Z).Get(), (x & y) ^ (x & z) ^ (y & z));
                Assert.AreEqual(new SIG0(X).Get(), RotRight(x, 7) ^ RotRight(x, 18) ^ (x >> 3));
                Assert.AreEqual(new SIG1(X).Get(), RotRight(x, 17) ^ RotRight(x, 19) ^ (x >> 10));
            }
        }

        [TestMethod]
        public void TestSHA()
        {
            Assert.AreEqual(new SHA(new byte[0]).ResultStr(), "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855");
        }
        
        [TestMethod]
        public void TestRandomSHA()
        {
            CalcRandomSHA(0);
            CalcRandomSHA(63);
            CalcRandomSHA(64);
            CalcRandomSHA(65);
            CalcRandomSHA(812);
        }

        [TestMethod]
        public void TestDoubleSHA()
        {
            CalcRandomDoubleSHA(0);
            CalcRandomDoubleSHA(63);
            CalcRandomDoubleSHA(64);
            CalcRandomDoubleSHA(65);
            CalcRandomDoubleSHA(812);
        }

        private void CalcRandomSHA(int size)
        {
            Random r = new Random();
            byte[] data = new byte[size];
            r.NextBytes(data);

            SHA256 sha = SHA256.Create();
            var shaOut = sha.ComputeHash(data);

            SHA mySHA = new SHA(data);
            var myHash = mySHA.Result();
            Assert.AreEqual(BitConverter.ToString(shaOut), BitConverter.ToString(myHash));
        }

        private void CalcRandomDoubleSHA(int size)
        {
            Random r = new Random();
            byte[] data = new byte[size];
            r.NextBytes(data);


            SHA256 sha = SHA256.Create();
            var shaOut = sha.ComputeHash(data);

            SHA256 sha2 = SHA256.Create();
            var hash = BitConverter.ToString(sha2.ComputeHash(shaOut)).Replace("-", String.Empty).ToLower();

            var s1 = new SHA(data).OutBits();
            //var s1 = new SHA(data).Result();
            //Assert.AreEqual(BitConverter.ToString(shaOut), BitConverter.ToString(s1));
            
            SHA sha2Bits = new SHA(s1);
            string myHash = sha2Bits.ResultStr();
            Assert.AreEqual(hash, myHash);
        }
    }
}
