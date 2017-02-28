using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTestDES.DESCode
{
    class Helper
    {

        public static string ToHexString(string s)
        {   // converts text to HEX string
            Console.WriteLine("");
            char[] chars = s.ToCharArray();
            StringBuilder sb = new StringBuilder();
            foreach (char c in chars)
            {
                sb.Append(((Int16)c).ToString("x"));
            }
            Console.WriteLine("key in HEX: " + sb.ToString());
            return sb.ToString();
        }

        public static byte[] HexStringToByteArray(string hex)
        {
            Console.WriteLine("Converting to byte array ...");
            return Enumerable.Range(0, hex.Length).Where(x => x % 2 == 0).Select(x => Convert.ToByte(hex.Substring(x, 2), 16)).ToArray();
        }

        public static BitArray ByteArrayToBitArray(byte[] key)
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
            //Console.WriteLine("\n64-bit:\n" + Helper.PrintBitArray(toBitKey, 8));
            return toBitKey;
        }

        public static string PrintBitArray(BitArray b, int l)
        {   // string with values in the bitarray with spaces every l bits
            string output = "";
            for (int i = 0; i < b.Length; i++)
            {
                output += ((b.Get(i)) ? "1" : "0");
                if ((i + 1) % l == 0)
                {
                    output += " ";
                }
            }
            return output;
        }

        public static string PrintCorDKeys(bool[][] cd, int l)
        {
            string output = "";

            for(int i=0; i<cd.Length; i++)
            {
                bool[] temp = cd[i];
                for (int j = 0; j < temp.Length; j++)
                {
                    output += (temp[j]) ? "1" : "0";
                    if ((j + 1) % l == 0)
                        output += " ";
                }
                output += "\n";
            }

            return output;
        }

        public static string printBoolArray(bool[] arr, int l)
        {
            string output = "";

            for (int i=0; i<arr.Length; i++)
            {
                output += (arr[i]) ? "1" : "0";

                if ((i + 1) % l == 0)
                {
                    output += " ";
                }
            }

            return output;
        }
    }
}
