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


        public bool[] Expander(bool[] r)
        {   // expansio function based on Box E 
            bool[] exp = new bool[48];

            for (int i=0; i<48; i++)
            {
                exp[i] = r[Boxes.E[i] -1 ];     // E has 1- 48 array is 0-47 hence E-1
            }
            Console.WriteLine("\nE(r):\n"+Helper.printBoolArray(exp, 6));
            return exp;
        }

        public bool[] ERi_XOR_Ki(bool[] eri, bool[] k)
        {   // Xor function on right side msg and key
            bool[] xor = new bool[48];

            for (int i=0; i<48; i++)
            {
                xor[i] = eri[i] ^ k[i];
            }
            Console.WriteLine("\nFunction E(R) XOR Ki:\n" + Helper.printBoolArray(xor, 6));
            return xor;
        }
    }
}
