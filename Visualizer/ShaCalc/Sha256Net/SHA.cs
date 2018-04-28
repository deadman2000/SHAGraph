using System;
using System.Collections.Generic;
using System.Text;
using ShaCalc.Model;

namespace ShaCalc.Sha256Net
{
    class SHA
    {
        private Block256 state;

        public SHA(byte[] input)
            : this(ToByteValue(input))
        {
        }

        public SHA(BitValue[] input)
            : this(ToByteValue(input))
        {
        }

        public SHA(ByteValue[] input)
        {
            state = new Block256(
                new IntValue(0x6a09e667),
                new IntValue(0xbb67ae85),
                new IntValue(0x3c6ef372),
                new IntValue(0xa54ff53a),
                new IntValue(0x510e527f),
                new IntValue(0x9b05688c),
                new IntValue(0x1f83d9ab),
                new IntValue(0x5be0cd19));

            ulong bitlen = (ulong)input.Length * 8;

            int len = input.Length;
            int d = input.Length % 64;
            if (d < 56)
                Array.Resize(ref input, input.Length + 64 - d);
            else
                Array.Resize(ref input, input.Length + 64 - d + 64);
            input[len] = new ByteValue(0x80);

            for (int i = input.Length - 1; i > input.Length - 8; i--)
            {
                input[i] = new ByteValue((byte)bitlen);
                bitlen = bitlen >> 8;
            }

            for (int i = len + 1; i < input.Length; i++)
            {
                if (input[i] == null)
                    input[i] = new ByteValue(0);
            }

            IntValue[] data = new IntValue[16];
            for (int i = 0; i < input.Length / 64; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    int offset = i * 64 + j * 4;
                    data[j] = new IntValue(input[offset + 3], input[offset + 2], input[offset + 1], input[offset]);
                }
                state = new SHARound(state, data);
            }
        }

        public BitValue[] OutBits()
        {
            return state.GetOutBits();
        }

        public byte[] Result()
        {
            return state.ToBytes();
        }

        public string ResultStr()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 8; i++)
                sb.Append(state[i].Get().ToString("x8"));
            return sb.ToString();
        }


        private static ByteValue[] ToByteValue(byte[] input)
        {
            ByteValue[] bytes = new ByteValue[input.Length];
            for (int i = 0; i < input.Length; i++)
                bytes[i] = new ByteValue(input[i]);
            return bytes;
        }

        private static ByteValue[] ToByteValue(BitValue[] bits)
        {
            if (bits.Length % 8 != 0) throw new Exception();

            ByteValue[] bytes = new ByteValue[bits.Length / 8];
            for (int i = 0; i < bytes.Length; i++)
            {
                ByteValue b = new ByteValue();
                for (int n = 0; n < 8; n++)
                {
                    b.Bits[n] = bits[i * 8 + n];
                }
                bytes[i] = b;
            }
            return bytes;
        }

    }
}
