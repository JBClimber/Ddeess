using ConsoleTestDES.DESCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDES
{
    class Program
    {
        static void Main(string[] args)
        {
            

            string msg = "0123456789ABCDEF";
            string key = "133457799BBCDFF1";
           string dmsg = "85E813540F0AB405";

           string msg2 = "8787878787878787";
           string key2 = "0E329232EA6D0D73";
          string dmsg2 = "0000000000000000";

            RunDES des = new RunDES(key, dmsg);    // give hex only for now


            Console.ReadKey();
        }
    }
}
