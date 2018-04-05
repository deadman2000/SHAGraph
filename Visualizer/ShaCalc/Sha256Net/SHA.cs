using System;
using System.Collections.Generic;
using System.Text;
using ShaCalc.Model;

namespace ShaCalc.Sha256Net
{
    class SHA : BitGroup
    {
        private Block256 state;
        private List<BitGroup> _groups = new List<BitGroup>();

        public SHA(byte[] input)
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
            _groups.Add(state);

            ulong bitlen = (ulong)input.Length * 8;

            int len = input.Length;
            int d = input.Length % 64;
            if (d < 56)
            {
                Array.Resize(ref input, input.Length + 64 - d);
                input[len] = 0x80;
            }
            else
            {
                Array.Resize(ref input, input.Length + 64 - d + 64);
                input[len] = 0x80;
            }

            for (int i = input.Length - 1; i > input.Length - 8; i--)
            {
                input[i] = (byte)bitlen;
                bitlen = bitlen >> 8;
            }

            IntValue[] data = new IntValue[16];
            for (int i = 0; i < input.Length / 64; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    int offset = i * 64 + j * 4;
                    data[j] = new IntValue(input[offset + 3] | (input[offset + 2] << 8) | (input[offset + 1] << 16) | (input[offset] << 24));
                }
                AddRound(data);
            }
        }

        private void AddRound(IntValue[] data)
        {
            state = new SHARound(state, data);
            _groups.Add(state);
        }

        public byte[] Result()
        {
            byte[] value = new byte[32];
            for (int i = 0; i < 8; i++)
            {
                uint v = state[i].Get();
                value[i * 4] = (byte)(v);
                value[i * 4 + 1] = (byte)(v >> 8);
                value[i * 4 + 2] = (byte)(v >> 16);
                value[i * 4 + 3] = (byte)(v >> 24);
            }
            return value;
        }

        public string ResultStr()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 8; i++)
                sb.Append(state[i].Get().ToString("x8"));
            return sb.ToString();
        }

        public override BitValue[] GetBits()
        {
            return null;
        }

        public override BitGroup[] GetSubgroups()
        {
            return _groups.ToArray();
        }
    }
}
