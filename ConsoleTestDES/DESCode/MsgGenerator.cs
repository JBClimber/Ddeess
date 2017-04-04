using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleTestDES.DESCode
{
    class MsgGenerator
    {
        private BitArray bitMsg;
        private bool[] msg;     // size 64
        private bool[] left;    // size 32
        private bool[] right;   // size 32
        private bool isHex;

        public MsgGenerator(string m)
        {
            //Console.WriteLine("\nOriginal msg:\n"+m);

            string hex = "";

            if (!Regex.IsMatch(m, @"\A\b[0-9a-fA-F]+\b\Z"))    // if not a hex string convert to HEX
            {
                hex = Helper.ToHexString(m);
                isHex = false;
            }
            else
            {
                hex = m.ToUpper();
                isHex = true;
            }
           
            //Console.WriteLine("\nmsg in HEX:\n" + hex);

            byte[] hexByte = Helper.HexStringToByteArray(hex);

            this.bitMsg = Helper.ByteArrayToBitArray(hexByte);
            //Console.WriteLine("\n64-bit msg:\n" + Helper.PrintBitArray(this.bitMsg, 4));

            this.msg = InitialPermutation(this.bitMsg);

            SplitMsgLeftRight(this.msg);
            //Console.WriteLine("\nL0:\n"+Helper.printBoolArray(this.left, 4));
            //Console.WriteLine("\nR0:\n"+Helper.printBoolArray(this.right, 4));
        }

        public bool[] InitialPermutation(BitArray msg)
        {   // swaps each bit according to the IP table and returns as a bool array

            bool[] m = new bool[msg.Length];

            for (int i=0; i<msg.Length; i++) 
            {
                m[i] = msg.Get(Boxes.IP[i] - 1);        // IP numbers 1 - 64 hence PI -1 for array 0-63
            }

            //Console.WriteLine("\nIP:\n"+Helper.printBoolArray(m, 4));
            return m;
        }

        public void SplitMsgLeftRight(bool[] m)
        {   // splits single bool[] into left bool[] and right bool[]

            this.left = new bool[32];   // m.length/2 = 32
            this.right = new bool[32];

            for (int i=0; i<32; i++)
            {
                this.left[i] = m[i];
                this.right[i] = m[i+32];
            }
        }

        public bool[] L_XOR_f(bool[] f)
        {   // XOR function for left side msg and result of the f function

            bool[] xorResult = new bool[32];

            for (int i=0; i<xorResult.Length; i++)
            {
                xorResult[i] = left[i] ^ f[i];
            }

            //Console.WriteLine("\nL_XOR_f function:\n"+Helper.printBoolArray(xorResult, 4));
            return xorResult;
        }

        public bool[] RconcatL()
        {   // concatenates Right  with Left

            bool[] c = new bool[64];

            for (int i=0; i<32; i++)
            {
                c[i] = right[i];
                c[i + 32] = left[i];
            }

            //Console.WriteLine("\nR concatenate L:\n"+Helper.printBoolArray(c, 8));
            return c;
        }

        public bool[] IPinvPermutation(bool[] m)
        {   // permutation on the IP inverse box

            bool[] p = new bool[64];
            for (int i=0; i<m.Length; i++)
            {
                p[i] = m[Boxes.IPinv[i] - 1];       // IP inverse numbers 1 - 64, p numbers 0->63 hence Boxes.IPinv[i] - 1
            }

            this.msg = p;

            //Console.WriteLine("\nIPinvPermutation:\n"+Helper.printBoolArray(p, 8));
            return p;
        }

        public string BoolToBinaryString(bool[] b)
        {   // converts bool[] to a hex string

            return Helper.printBoolArray(b, 0);
        }

        public string BinaryStringToHexString(string b)
        {   // converts binary string to hex string

            StringBuilder result = new StringBuilder(b.Length / 8 + 1);

            for (int i = 0; i < b.Length; i += 8)
            {
                string eightBits = b.Substring(i, 8);
                result.AppendFormat("{0:X2}", Convert.ToByte(eightBits, 2));
            }

            //Console.WriteLine("\nBinaryStringToHexString:\n"+ result.ToString());
            return result.ToString();
        }

        public bool[] GetLeft()
        {   // gets left side of the message
            return this.left;
        }

        public bool[] GetRight()
        {   // gets right side of the message
            return this.right;
        }

        public void SetLeft(bool[] left)
        {
            this.left = left;
        }

        public void SetRight(bool[] right)
        {
            this.right = right;
        }

        public string GetMsgAsHexString()
        {
            return BinaryStringToHexString(BoolToBinaryString(this.msg));
        }

        public string GetMsgAsHexOrText()
        {
            if (isHex)
            {
                return GetMsgAsHexString();
            }
            else
            {
                return GetMsgAsText();
            }
        }

        public string HexToText(string h)
        {   // converts hex string to text (ascii)string

            /*string s = "";

            for (int i=0; i<h.Length; i = i+2)
            {
                s += Convert.ToChar(Convert.ToUInt32(h.Substring(i, 2), 16));
            }

            return s;


            /*byte[] tmp;
            int j = 0;
            tmp = new byte[(h.Length) / 2];
            for (int i = 0; i <= h.Length- 2; i += 2)
            {
                tmp[j] = (byte)Convert.ToChar(Int32.Parse(h.Substring(i, 2), System.Globalization.NumberStyles.HexNumber));

                j++;
            }
            return Encoding.GetEncoding(1252).GetString(tmp);*/

            /*string res = String.Empty;

            for (int a = 0; a < h.Length; a = a + 2)

            {

                string Char2Convert = h.Substring(a, 2);

                int n = Convert.ToInt32(Char2Convert, 16);

                char c = (char)n;

                //Console.WriteLine("int:"+n+"  -->  "+c);

                res += c.ToString();

            }

            return res;*/

            try
            {
                string ascii = "";

                for (int i = 0; i < h.Length; i += 2)
                {
                    /*String hs = string.Empty;
                    
                    hs = h.Substring(i, 2);
                    uint decval = System.Convert.ToUInt16(hs, 16);
                    char character = System.Convert.ToChar(decval);*/

                    char character = (char)int.Parse(h.Substring(i, 2), NumberStyles.HexNumber);

                    ascii += character;
                }

                return ascii;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }

            return string.Empty;
        }

        public string GetMsgAsText()
        {
            return HexToText(BinaryStringToHexString(BoolToBinaryString(this.msg)));
        }

    }
}
