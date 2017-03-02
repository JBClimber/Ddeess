using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTestDES.DESCode
{
    class KeyGenerators
    {
        private BitArray bitKey64;
        private BitArray bitKey56;
        private bool[][] cdKeys;
        private bool[][] kKeys;

        public KeyGenerators(string key)
        {
            this.bitKey64 = ByteKeyToBitKey (HexStringToByteArray(key) );
            this.bitKey56 = BitKey64ToBitKey56(this.bitKey64);
            this.cdKeys = CreateCDKeys(this.bitKey56);
            this.kKeys = CreateKKeys(this.cdKeys);
        }

        //public string ToHexString(string s)
        //{   // converts text to HEX string
        //    Console.WriteLine("");
        //    char[] chars = s.ToCharArray();
        //    StringBuilder sb = new StringBuilder();
        //    foreach (char c in chars)
        //    {
        //        sb.Append(((Int16)c).ToString("x"));
        //    }
        //    Console.WriteLine("key in HEX: "+sb.ToString());
        //    return sb.ToString();
        //}

        public byte[] HexStringToByteArray(string hex)
        {
            Console.WriteLine("Converting to byte array ...");
            return Enumerable.Range(0, hex.Length).Where(x => x % 2 == 0).Select(x => Convert.ToByte(hex.Substring(x, 2), 16)).ToArray();
        }

        public BitArray ByteKeyToBitKey(byte[] key)
        {
            int ki = 0;
            BitArray toBitKey = new BitArray(key.Length * 8);
            for (int i = 0; i < key.Length; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    toBitKey.Set(ki, ((key[i] & (1 << 7 - j)) != 0));
                    ki++;
                }
            }
            Console.WriteLine("\n64-bit key:\n" + Helper.PrintBitArray(toBitKey, 8));
            return toBitKey;
        }

        public BitArray BitKey64ToBitKey56(BitArray bitKey)
        {   // converts 64 bit key to 56 bits using Boxes.pc1
            BitArray bit56 = new BitArray(56);

            for (int i = 0; i < 56; i++)
            {
                bool temp = bitKey[i];
                bit56.Set(i, bitKey.Get(Boxes.PC1[i] - 1)); // pc1 index 1 through 64 hence PC1 - 1
            }
            Console.WriteLine("\n56 bit Key:\n" + Helper.PrintBitArray(bit56, 7));
            return bit56;
        }

        public bool[][] CreateCDKeys(BitArray key56)
        {
            bool[][] cd = new bool[17][];
            bool[] row = new bool[56];
            for (int i=0; i<28; i++)    // sets the first half and second half of 56 to cdKeys 
            {
                row[i] = key56.Get(i);         // the c keys on the left side 0-27
                row[i+28] = key56.Get(i+28);  // the d keys on the right side 28-55
            }
            cd[0] = row;

            bool oneBitC;    // bit for shift by 1 for Ckeys
            bool twoBitC;    // bit for shift by 2 for CKeys

            bool oneBitD;    // bit for shift by 1 for Dkeys
            bool twoBitD;    // bit for shift by 2 for Dkeys

            for (int i=1; i<17; i++)    // create a new key from the previous row (key)
            {
                row = new bool[56];
                if (i == 1 || i == 2 || i == 9 || i == 16)  // shifts bit left by 1
                {
                    oneBitC = cd[i - 1][0];
                    oneBitD = cd[i - 1][28];
                    for (int j = 1; j < 28; j++)
                    {
                        row[j - 1] = cd[i - 1][j];
                        row[j - 1 + 28] = cd[i-1][j+28];
                    }
                    row[27] = oneBitC;
                    row[55] = oneBitD;
                    cd[i] = row;
                }
                else                    // shift bit left by 2
                {
                    oneBitC = cd[i - 1][0];
                    twoBitC = cd[i - 1][1];
                    oneBitD = cd[i - 1][28];
                    twoBitD = cd[i - 1][29];

                    for (int j = 2; j < 28; j++)
                    {
                        row[j - 2] = cd[i - 1][j];
                        row[j - 2 + 28] = cd[i - 1][j + 28];
                    }
                    row[26] = oneBitC;
                    row[27] = twoBitC;
                    row[54] = oneBitD;
                    row[55] = twoBitD;
                    cd[i] = row;
                }
            }
            Console.WriteLine("\nC D keys: \n"+Helper.PrintCorDKeys(cd, 28));
            return cd;
        }

        public bool[][] CreateKKeys(bool[][] cd)
        {   // creates 16 K keys from the CDkeys
            bool[][] k = new bool[17][]; // 0 row is not used
            bool[] temp = new bool[48];
            k[0] = temp;

            for(int i=1; i<17; i++)
            {
                temp = new bool[48];
                for (int j = 0; j < 48; j++)
                {
                    temp[j] = cd[i][Boxes.PC2[j] - 1];  // PC2 index 1 - 48 hence PC2 -1
                }
                k[i] = temp;
            }
            Console.WriteLine("\nK keys:\n" + Helper.PrintCorDKeys(k, 6));
            return k;
        }

        //==================================================

        public BitArray getBitKey64()
        {
            return this.bitKey64;
        }

        public BitArray getBitKey56()
        {
            return this.bitKey56;
        }

        public bool[] GetKKeyNumber(int i)
        {
            return this.kKeys[i];
        }

    }
}
