using System;
using System.Collections.Generic;
using System.Text;
using Visualizer.Model;

namespace Visualizer.Sha256Net
{
    class SHA
    {
        List<SHARound> rounds = new List<SHARound>();
        Block256 state;
        int bitlen = 0;
        int datalen = 0;

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

            ByteValue[] data = new ByteValue[64];

            for (int i = 0; i < input.Length; i++)
            {
                data[datalen] = new ByteValue(input[i]);
                datalen++;
                if (datalen == 64)
                {
                    AddRound(data);
                    bitlen += 512;
                    datalen = 0;
                }
            }

            {
                int i = datalen;

                if (datalen < 56)
                {
                    data[i++] = new ByteValue(0x80);
                    while (i < 56)
                        data[i++] = new ByteValue(0x00);
                }
                else
                {
                    data[i++] = new ByteValue(0x80);
                    while (i < 64)
                        data[i++] = new ByteValue(0x00);

                    AddRound(data);

                    for (i = 0; i < 56; i++)
                        data[i] = new ByteValue(0);
                }

                bitlen += datalen * 8;

                data[63] = new ByteValue(bitlen);
                data[62] = new ByteValue(bitlen >> 8);
                data[61] = new ByteValue(bitlen >> 16);
                data[60] = new ByteValue(bitlen >> 24);
                data[59] = new ByteValue(bitlen >> 32);
                data[58] = new ByteValue(bitlen >> 40);
                data[57] = new ByteValue(bitlen >> 48);
                data[56] = new ByteValue(bitlen >> 56);
                AddRound(data);
            }
        }

        private void AddRound(ByteValue[] data)
        {
            SHARound r = new SHARound(state, data);
            rounds.Add(r);
            state = r.Out;
        }

        public string Result()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(state.a.Get().ToString("x8"));
            sb.Append(state.b.Get().ToString("x8"));
            sb.Append(state.c.Get().ToString("x8"));
            sb.Append(state.d.Get().ToString("x8"));
            sb.Append(state.e.Get().ToString("x8"));
            sb.Append(state.f.Get().ToString("x8"));
            sb.Append(state.g.Get().ToString("x8"));
            sb.Append(state.h.Get().ToString("x8"));
            return sb.ToString();
        }
    }
}
