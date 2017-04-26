using ConsoleTestDES.DESCode;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESkeyTest
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int j=1; j<16; j++)    // looks for 1 - 15 distinct keys 
            {
                Console.WriteLine("working on "+j+" RIGHT ...");
                StreamWriter write = new StreamWriter(@"d:\ET\BFKeyValidityDES\des_"+j+"_diff_K_keysRIGHT.txt");

                Stopwatch t = new Stopwatch();
                t.Start();

                string binString = "";

                // rigth side of CD keys
                for (int i = 0; i <= 268435455; i++) {

                    binString = IntToBinaryStringRightSide(i);
                    bool[] keys = BinaryStringToBoolArray(binString);
                    //Console.WriteLine(keys[i].Length);
                    bool[][] key = CreateCDKeys(keys);

                    bool[][] keyWork = CreateKKeys(key);
                    bool[][] keyCopy = CreateKKeys(key);

                    // check for number of same keys in k
                    if (FindSameKKeysRight(keyWork, j))
                    {
                        //PrintBoolArray(keys[i]);
                        //Console.WriteLine(Convert.ToInt64(binString[i], 2));
                        bool[] recovered = RecoverKey(keys);

                        string k = ToStringBoolArray(recovered, 8);

                        Console.WriteLine("64bit key: " + k);
                        write.WriteLine("64bit key: " + k);

                        Console.WriteLine("56bit right side: " + binString);
                        write.WriteLine("56bit right side: " + binString);

                        Console.WriteLine("key: " + k + "\r\nno: " + i);
                        write.WriteLine("key: " + k);

                        k = ToStringBoolArrayOfArrays(key, 7);
                        Console.WriteLine("CD keys: \n" + k);
                        write.WriteLine("CD keys: \n" + k);

                        k = ToStringBoolArrayOfArrays(keyCopy, 6);
                        Console.WriteLine("K keys: \n" + k);
                        write.WriteLine("K keys: \n" + k);

                        write.Flush();

                    }
                }
                t.Stop();
                TimeSpan ts = t.Elapsed;
                Console.WriteLine(ToStringElapsedTime(ts));
                write.WriteLine(ToStringElapsedTime(ts));
                write.Flush();
                write.Close();


                write = new StreamWriter(@"d:\ET\BFKeyValidityDES\des_"+j+"_diff_K_keysLEFT.txt");

                t = new Stopwatch();
                t.Start();

                Console.WriteLine("working on "+j+" LEFT ...");
                // left side of the CD Keys

                for (int i = 0; i <= 268435455; i++)
                {
                    binString = InToBinaryStringLeftSide(i);
                    bool[] keys = BinaryStringToBoolArray(binString);

                    bool[][] key = CreateCDKeys(keys);

                    bool[][] keyWork = CreateKKeys(key);
                    bool[][] keyCopy = CreateKKeys(key);

                    if (FindSameKKeysLeft(keyWork, j))
                    {
                        //PrintBoolArray(keys[i]);
                        //Console.WriteLine(Convert.ToInt64(binString[i], 2));
                        bool[] recovered = RecoverKey(keys);

                        string k = ToStringBoolArray(recovered, 8);

                        Console.WriteLine("64bit key: " + k);
                        write.WriteLine("64bit key: " + k);

                        Console.WriteLine("56bit right side: " + binString);
                        write.WriteLine("56bit right side: " + binString);

                        Console.WriteLine("key: " + k + "\r\nno: " + i);
                        write.WriteLine("key: " + k);

                        k = ToStringBoolArrayOfArrays(key, 7);
                        Console.WriteLine("CD keys: \n" + k);
                        write.WriteLine("CD keys: \n" + k);

                        k = ToStringBoolArrayOfArrays(keyCopy, 6);
                        Console.WriteLine("K keys: \n" + k);
                        write.WriteLine("K keys: \n" + k);

                        write.Flush();

                    }
                }


                t.Stop();
                ts = t.Elapsed;
                Console.WriteLine(ToStringElapsedTime(ts));
                write.WriteLine(ToStringElapsedTime(ts));
                write.Flush();
                write.Close();

            }

            //CountFrequency();

            Console.WriteLine(" - - - TEST COMPLETE - - -");
            CompleteBeep();
            Console.ReadKey();
        }

        public static string IntToBinaryStringRightSide(int n)
        {   // converts integer to 56 bit binary string rigth side
                string num = Convert.ToString(n, 2);

                while (num.Length < 56)
                {
                    num = "0" + num;
                }

            return num;
        }

        public static void CountFrequency()
        {   // records frequency of LEFT and RIGHT similarities

            Stopwatch t = new Stopwatch();
            t.Start();

            string binStringLeft = "";
            string binStringRight = "";

            int[,] freq = new int[17,2];

            for (int j = 0; j < 268435455; j++)
            {
                Console.WriteLine("key: "+j);
                binStringLeft = InToBinaryStringLeftSide(j);
                binStringRight = IntToBinaryStringRightSide(j);

                bool[] keysL = BinaryStringToBoolArray(binStringLeft);
                bool[] keysR = BinaryStringToBoolArray(binStringRight);

                bool[][] keyL = CreateCDKeys(keysL);
                bool[][] keyR = CreateCDKeys(keysR);

                for (int i = 1; i < freq.Length; i++)
                {
                    bool[][] keyLeft = CreateKKeys(keyL);
                    bool[][] keyRight = CreateKKeys(keyR);

                    if (FindSameKKeysLeft(keyLeft, i))
                    {
                        freq[i, 0]++;
                    }

                    if (FindSameKKeysRight(keyRight, i))
                    {
                        freq[i, 1]++;
                    }
                }
            }

            StreamWriter write = new StreamWriter(@"d:\ET\BFKeyValidityDES\des_KeyFreqLeftRight.txt");

            for (int i=1; i<freq.Length; i++)
            {
                write.WriteLine(i+":"+freq[i,0]+":"+freq[i,1]);
                write.Flush();
            }

            t.Stop();
            TimeSpan ts = t.Elapsed;
            Console.WriteLine(ToStringElapsedTime(ts));
            write.WriteLine(ToStringElapsedTime(ts));
            write.Flush();
            write.Close();

        }

        public static string InToBinaryStringLeftSide(int n)
        {   // converts integer to 56 bit binary string left side
            string num = Convert.ToString(n, 2);

            while (num.Length < 28)
            {
                num = "0" + num;
            }

            while (num.Length < 56)
            {
                num += "0";
            }

            return num;
        }

        public static bool[] BinaryStringToBoolArray(string binString)
        {   // returns an array of arrays of boolean keys

            bool[] keys56bit = binString.Select(c => c == '1').ToArray();

            return keys56bit;
        }

        public static bool[][] CreateCDKeys(bool[] key)
        {   // creates CD keys. from DES
            bool[][] cdKeys = new bool[17][];
            cdKeys[0] = key;
            //PrintBoolArray(key);

            for (int i = 1; i < 17; i++)        // 16 rounds
            {
                bool[] keys = new bool[56];     // reusing
                if (i == 1 || i == 2 || i == 9 || i == 16)  // shifts bit left by 1
                {
                    bool oneBitC = cdKeys[i - 1][0];
                    bool oneBitD = cdKeys[i - 1][28];
                    for (int j = 1; j < 28; j++)
                    {
                        keys[j - 1] = cdKeys[i - 1][j];
                        keys[j - 1 + 28] = cdKeys[i - 1][j + 28];
                    }
                    keys[27] = oneBitC;
                    keys[55] = oneBitD;
                    cdKeys[i] = keys;
                }
                else                    // shift bit left by 2
                {
                    bool oneBitC = cdKeys[i - 1][0];
                    bool twoBitC = cdKeys[i - 1][1];
                    bool oneBitD = cdKeys[i - 1][28];
                    bool twoBitD = cdKeys[i - 1][29];

                    for (int j = 2; j < 28; j++)
                    {
                        keys[j - 2] = cdKeys[i - 1][j];
                        keys[j - 2 + 28] = cdKeys[i - 1][j + 28];
                    }
                    keys[26] = oneBitC;
                    keys[27] = twoBitC;
                    keys[54] = oneBitD;
                    keys[55] = twoBitD;
                    cdKeys[i] = keys;
                }
            }
            //Console.WriteLine("CD keys:");
            //PrintBoolArrayOfArrays(cdKeys, 7);
            return cdKeys;
        }

        public static bool[][] CreateKKeys(bool[][] cd)
        {   // creates K keys . from DES
            bool[][] k = new bool[17][]; // 0 row is not used
            bool[] temp = new bool[48];
            k[0] = temp;

            for (int i = 1; i < 17; i++)
            {
                temp = new bool[48];
                for (int j = 0; j < 48; j++)
                {
                    temp[j] = cd[i][Boxes.PC2[j] - 1];  // PC2 index 1 - 48 hence PC2 -1
                }
                k[i] = temp;
            }
            //Console.WriteLine("K keys:");
            //PrintBoolArrayOfArrays(k, 6);
            return k;
        }

        public static bool FindSameKKeysRight(bool[][] kKeys, int distKeys)
        {   // finds number of distinct keys in the K keys (RIGHT side)

            string result = "";
            int distinctKeys = 16;

            for (int i=1; i<kKeys.Length; i++)
            {
                int same = 0;
                if (kKeys[i] != null)
                {
                    for (int j=i+1; j<kKeys.Length; j++)
                    {
                        if (kKeys[j] != null)
                        {
                            int count = 0;
                            for (int k=24; k<kKeys[j].Length; k++)
                            {
                                if (kKeys[i][k] ^ kKeys[j][k] == false)
                                {
                                    count++;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            if (count == 24)
                            {
                                result += i+","+j+"  ";
                                kKeys[j] = null;
                                same++;
                            }
                        }
                    }
                }
                distinctKeys = distinctKeys - same;
                
                kKeys[i] = null;
            }

            if (distinctKeys != distKeys)
            {
                return false;
            }
            //Console.WriteLine(result);
            return true;
        }

        public static bool FindSameKKeysLeft(bool[][] kKeys, int distKeys)
        {   // finds number of distinct keys in the K keys (LEFT side)
            string result = "";
            int distinctKeys = 16;

            for (int i = 1; i < kKeys.Length; i++)
            {
                int same = 0;
                if (kKeys[i] != null)
                {
                    for (int j = i + 1; j < kKeys.Length; j++)
                    {
                        if (kKeys[j] != null)
                        {
                            int count = 0;
                            for (int k = 0; k < 24; k++)
                            {
                                if (kKeys[i][k] ^ kKeys[j][k] == false)
                                {
                                    count++;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            if (count == 24)
                            {
                                result += i + "," + j + "  ";
                                kKeys[j] = null;
                                same++;
                            }
                        }
                    }
                }
                distinctKeys = distinctKeys - same;

                kKeys[i] = null;
            }

            if (distinctKeys != distKeys)
            {
                return false;
            }
            //Console.WriteLine(result);
            return true;
        }

        public static bool[] RecoverKey(bool[] k)
        {   // recovers 64 bit key from 54 bit , using reverse of the first permutation Box PC1
            // 8th bits are left as "0" = false
            bool[] rec = new bool[64];

            for (int i=0; i<k.Length; i++)
            {
                rec[Boxes.PC1[i]-1] = k[i];
            }

            return rec;
        }

        public static string BoolArrayToBinaryString(bool[] k)
        {   // converts bool array to binary string
            string result = "";

            for (int i=0; i<k.Length; i++)
            {
                if (k[i])
                {
                    result = result + "1";
                }
                else
                {
                    result = result + "0";
                }
                if ((i+1)%8==0)
                {
                    result = result + " ";
                }
            }

            return result;
        }

        public static string BinaryStringToHexString(string b)
        {   // converts binary string to HEX
            string hex = Convert.ToInt64(b, 2).ToString("X");
            while (hex.Length < 16)
            {
                hex = "0" + hex;
            }
            return hex;
        }

        public static string ToStringBoolArray(bool[] p, int group)
        {   // prints bool array

            string temp = "";
            for (int j = 0; j < p.Length; j++)
            {
                if (p[j] == true)
                {
                    temp += "1";
                }
                else
                {
                    temp += "0";
                }
                if ((j+1)%group == 0)
                {
                    temp = temp + " ";
                }
            }
            return temp;
        }

        public static string ToStringBoolArrayOfArrays(bool[][] p, int group)
        {   // to string bool array of arrays

            string output = "";

            for (int i=0; i<p.Length; i++)
            {
                int count = 1;
                for (int j=0; j<p[i].Length; j++)
                {
                    if (p[i][j] == true)
                    {
                        output = output + "1";
                    }
                    else
                    {
                        output = output + "0";
                    }
                    if(count % group == 0)
                    {
                        output = output + " ";
                    }
                    count++;
                }
                output = output + "\n"; ;
            }
            return output;
        }

        public static void CompleteBeep()
        {
            Console.Beep(5000, 250);
            Console.Beep(2000, 250);
            Console.Beep(5000, 250);
            Console.Beep(2000, 250);
            Console.Beep(500, 500);
        }

        public static string ToStringElapsedTime(TimeSpan s)
        {   // toString of elapsed time
            return ("timed at: " + s.Days + ":" + s.Hours + ":" + s.Minutes + ":" + s.Seconds + "." + s.Milliseconds);
        }
    }
}
