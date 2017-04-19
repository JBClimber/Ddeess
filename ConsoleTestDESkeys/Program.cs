using ConsoleTestDES.DESCode;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTestDESkeys
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("  - - - - -  DES key test  - - - - - - \r\n");

            string readFile;
            do
            {
                Console.WriteLine("Enter path and file to read Keys from: ");
                readFile = Console.ReadLine();
            } while ( ! File.Exists(readFile));

            StreamReader file = new StreamReader(readFile);
            string key;
            while ((key = file.ReadLine()) != null)
            {
                Console.WriteLine(key);
                //ConvertToBoolArray(key);
                KeyGenerators kg = new KeyGenerators( new BitArray(ConvertToBoolArray(key))); ;
                Console.ReadKey();
                Console.WriteLine();

                //ConvertToBoolArrayComplement(key);
                kg = new KeyGenerators(new BitArray(ConvertToBoolArrayComplement(key)));
                Console.ReadKey();
                Console.WriteLine();
            }

            Console.WriteLine("  - - - - -  END test  - - - - - - - -");
            CompleteBeep();
            Console.ReadKey();
        }

        public static bool[] ConvertToBoolArray(string key)
        {   // converts binary string to bool array
            bool[] boolKey = new bool[key.Length];

            for (int i=0; i<boolKey.Length; i++)
            {
                if (key[i].Equals('1'))
                {
                    boolKey[i] = true;
                    Console.Write("t");
                }
                else
                {
                    boolKey[i] = false;
                    Console.Write("f");
                }
            }
            Console.WriteLine();
            return boolKey;
        }

        public static bool[] ConvertToBoolArrayComplement(string key)
        {   // returns binary string to complement bool array

            bool[] boolKey = new bool[key.Length];

            for (int i=0; i<boolKey.Length; i++)
            {
                if (key[i].Equals('1'))
                {
                    boolKey[i] = false;
                    Console.Write("f");
                }
                else
                {
                    boolKey[i] = true;
                    Console.Write("t");
                }
            }
            Console.WriteLine();
            return boolKey;
        }

        public static void CompleteBeep()
        {
            Console.Beep(5000, 250);
            Console.Beep(2000, 250);
            Console.Beep(5000, 250);
            Console.Beep(2000, 250);
            Console.Beep(500, 1500);
        }
    }
}
