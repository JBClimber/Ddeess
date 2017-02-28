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

            RunDES des = new RunDES(key, msg);



            /*Console.WriteLine("msg in Hex : "+msg);

            byte[] byteMsg = des.HexStringToByteArray(msg);
            byte[] byteKey = des.HexStringToByteArray(key);

            Console.WriteLine("msg binary:\n" +des.GetBinaryStringFromByteArray(des.msg));
            Console.WriteLine("key byteArray:\n" + des.GetBinaryStringFromByteArray(des.key));*/

            Console.ReadKey();
        }
    }
}
