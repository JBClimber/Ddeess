using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTestDES.DESCode
{
    class Functions
    {
        private bool[] e;       // expansion function


        public bool[] fFunction(bool[] ri, bool[] key)
        {   // complete f function
            // step 1: expander
            e = Expander(ri);

            // step 2: expanded msg XOR key
            e = ERi_XOR_Ki(e, key);

            // step 3: pass msg through S-Boxes
            e = SBoxesFunction(e);

            // step 4: permutaion of the msg
            e = PermutationSboxOutput(e);

            // f function complete
            return e;
        }

        public bool[] Expander(bool[] r)
        {   // expansion function based on Box E 
            bool[] exp = new bool[48];

            for (int i=0; i<48; i++)
            {
                exp[i] = r[Boxes.E[i] -1 ];     // E has 1- 48 array is 0-47 hence E-1
            }

            //Console.WriteLine("\nE(r):\n"+Helper.printBoolArray(exp, 6));
            return exp;
        }

        public bool[] ERi_XOR_Ki(bool[] eri, bool[] key)
        {   // Xor function on right side msg and key
            // eri = right side after the expansion function
            bool[] xor = new bool[48];

            for (int i=0; i<48; i++)
            {
                xor[i] = eri[i] ^ key[i];
            }

            //Console.WriteLine("\nFunction E(R) XOR Ki:\n" + Helper.printBoolArray(xor, 6));
            return xor;
        }

        public bool[] SBoxesFunction(bool[] eriK)
        {   // transforms 6bits to 4bits using the S Boxes
            bool[] c = new bool[32];
            int boxNum = 1;
            int cIndex = 0; // moves by 4
            for (int i=0; i<eriK.Length; i = i + 6)
            {
                int x = GetSBoxNumber(boxNum, eriK[i], eriK[i+1], eriK[i+2], eriK[i+3], eriK[i+4], eriK[i+5]);
                boxNum++;
                bool[] fbits = Helper.IntToFourBit(x);
                c[cIndex] = fbits[0];
                c[cIndex + 1] = fbits[1];
                c[cIndex + 2] = fbits[2];
                c[cIndex + 3] = fbits[3];
                cIndex += 4;
            }

            //Console.WriteLine("\nFunction SBoxes:\n"+Helper.printBoolArray(c, 4));
            return c;
        }

        public int GetSBoxNumber(int boxNum, bool f, bool e, bool d, bool c, bool b, bool a)
        {   // converts 6bits to int according to S box number
            // f and a create row number  0 -> 3
            // e, d, c, b create column number 0->15
            //  row and column get number form the SBox
            //Console.Write(boxNum);
            int row = 0;
            if (a)
            {
                row++;
            }
            if (f)
            {
                row += 2;
            }

            int col = 0;
            if (b)
            {
                col++;
            }
            if (c)
            {
                col += 2;
            }
            if (d)
            {
                col += 4;
            }
            if (e)
            {
                col += 8;
            }

            switch (boxNum)
            {
                case 1:
                    return Boxes.S1[row, col];
                case 2:
                    return Boxes.S2[row, col];
                case 3:
                    return Boxes.S3[row, col];
                case 4:
                    return Boxes.S4[row, col];
                case 5:
                    return Boxes.S5[row, col];
                case 6:
                    return Boxes.S6[row, col];
                case 7:
                    return Boxes.S7[row, col];
                default:
                    return Boxes.S8[row, col];
            }
        }

        public bool[] PermutationSboxOutput(bool[] sBoxOut)
        {   // permutation function based on the box P

            bool[] p = new bool[sBoxOut.Length];
            for (int i=0; i<sBoxOut.Length; i++)
            {
                p[i] = sBoxOut[Boxes.P[i] - 1];     // P index 1->32 sBoxOut 0->31 hence Boxes.P[i] - 1
            }

            //Console.WriteLine("\nFunction Permutation:\n"+Helper.printBoolArray(p, 4));
            return p;
        }
    }
}
