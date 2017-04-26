using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleTestDES.DESCode
{
    class Helper
    {

        public static string ToHexString(string s)
        {   // converts text to HEX string
            
            char[] chars = s.ToCharArray();
            StringBuilder sb = new StringBuilder();
            foreach (char c in chars)
            {
                string hexoutput = String.Format("{0:X2}", Convert.ToInt16(c));
                sb.Append(hexoutput);
            }

            return sb.ToString();
        }

        public static byte[] HexStringToByteArray(string hex)
        {
            //Console.WriteLine("Converting hexString to byte array ...");

            byte[] hexBytes = new byte[hex.Length / 2];
            for (int i = 0; i < hex.Length; i += 2)
            {
                string p = hex.Substring(i, 2);
                hexBytes[i / 2] = Convert.ToByte(p, 16);
            }
            return hexBytes;
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

                if (l != 0 && (i + 1) % l == 0)
                {
                    output += " ";
                }
            }

            return output;
        }

        public static bool[] IntToFourBit(int n)
        {   // takes int between 0 and 15 (inclusive) and 
            // returns a 4 bit representaion in bool array form

            bool[] b = new bool[4];

            switch (n)
            {
                case 0:
                    return b = new bool[] { false, false, false, false};
                case 1:
                    return b = new bool[] { false, false, false, true };
                case 2:
                    return b = new bool[] { false, false, true, false };
                case 3:
                    return b = new bool[] { false, false, true, true };
                case 4:
                    return b = new bool[] { false, true, false, false };
                case 5:
                    return b = new bool[] { false, true, false, true };
                case 6:
                    return b = new bool[] { false, true, true, false };
                case 7:
                    return b = new bool[] { false, true, true, true };
                case 8:
                    return b = new bool[] { true, false, false, false };
                case 9:
                    return b = new bool[] { true, false, false, true };
                case 10:
                    return b = new bool[] { true, false, true, false };
                case 11:
                    return b = new bool[] { true, false, true, true };
                case 12:
                    return b = new bool[] { true, true, false, false };
                case 13:
                    return b = new bool[] { true, true, false, true };
                case 14:
                    return b = new bool[] { true, true, true, false };
                case 15:
                    return b = new bool[] { true, true, true, true };
            }

            return b;
        }

        public static string AddPaddingToMsg(string m, int padLength, string pl)
        {   // pads the msg with space(s) so it is multiple of 16 characters
            while (m.Length % padLength != 0)
            {
                m = m + pl;
            }

            //Console.WriteLine("\nPadded msg:\n|"+m+"|");
            return m;
        }

        public static List<string> SplitMsgBySize(string m, int size)
        {
            List<string> l = new List<string>();

            for (int i=0; i<m.Length; i = i + size)
            {
                l.Add(m.Substring(i, size));
            }

            return l;
        }

        public static Boolean IsHexString(string h)
        {
            return Regex.IsMatch(h, @"\A\b[0-9a-fA-F]+\b\Z");
        }
    }
}
