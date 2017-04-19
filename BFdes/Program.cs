using ConsoleTestDES.DESCode;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFdes
{
    class Program
    {
        /*       program not complete
         * 
         * 
         * 
         * 
         */


        static void Main(string[] args)
        {
            Console.WriteLine(" - - - - - BFdes started - - - - -\r\n");

            Console.Write("enter cryb: ");
            string cryb = Console.ReadLine();

            Console.Write(" enter msg: ");
            string msg = Console.ReadLine();


            Console.WriteLine("\r\nenter number of keys to test");
            Console.Write("from: ");
            ulong start = Convert.ToUInt64(Console.ReadLine());

            Console.Write("  to: ");
            ulong end = Convert.ToUInt64(Console.ReadLine());

            Stopwatch t = new Stopwatch();
            t.Start();

            for (; start <= end; start += 2)
            {

                string key = CreateKeyFromULong(start);
                Console.Write(key + " | " + msg + " | ");
                RunDES rd = new RunDES();
                string output = rd.RunEncrypt(key, msg);
                Console.WriteLine(output);
            }

            t.Stop();
            TimeSpan ts = t.Elapsed;

            Console.WriteLine("\r\n - - - - - TEST COMPLETE - - - - -");
            PrintElapsedTime(ts);

            Console.ReadKey();
        }

        public static string CreateKeyFromULong(ulong k)
        {   // created a key from ULong number to 16 digit HEX number

            string hexKey = string.Format( "{0:X}" ,k);
            while (hexKey.Length < 16)
            {
                hexKey =  "0" + hexKey;
            }
            return hexKey;
        }

        public static void PrintElapsedTime(TimeSpan s)
        {
            Console.WriteLine("timed at: "+s.Days+":"+s.Hours+":"+s.Minutes+":"+s.Seconds+"."+s.Milliseconds);
        }



    }
}
