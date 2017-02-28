using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTestDES.DESCode
{
    class MsgGenerator
    {
        private BitArray bitMsg;
        private bool[] msg;
        private bool[] left;
        private bool[] right;

        public MsgGenerator(string msg)
        {
            Console.WriteLine("\nOriginal msg:\n"+msg);

            string hex = "0123456789ABCDEF";    // Helper.ToHexString(msg);
            Console.WriteLine("\nmsg in HEX:\n" + hex);

            byte[] hexByte = Helper.HexStringToByteArray(hex);

            this.bitMsg = Helper.ByteArrayToBitArray(hexByte);
            Console.WriteLine("\n64-bit msg:\n" + Helper.PrintBitArray(this.bitMsg, 4));

            this.msg = InitialPermutation(this.bitMsg);

            SplitMsgLeftRight(this.msg);
            Console.WriteLine("\nL0:\n"+Helper.printBoolArray(this.left, 4));
            Console.WriteLine("\nR0:\n"+Helper.printBoolArray(this.right, 4));
        }

        public bool[] InitialPermutation(BitArray msg)
        {   // swaps each bit according to the IP table and returns as a bool array

            bool[] m = new bool[msg.Length];

            for (int i=0; i<msg.Length; i++) 
            {
                m[i] = msg.Get(Boxes.IP[i] - 1);        // IP numbers 1 - 64 hence PI -1 for array 0-63
            }
            Console.WriteLine("\nIP:\n"+Helper.printBoolArray(m, 4));
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

        public bool[] GetLeft()
        {
            return this.left;
        }

        public bool[] GetRight()
        {
            return this.right;
        }

    }
}
