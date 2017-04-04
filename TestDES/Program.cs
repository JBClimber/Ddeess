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
            

            string msg = "596F7572206C6970732061726520736D6F6F74686572207468616E20766173656C696E650D0A0000";
            string key = "0E329232EA6D0D73";
           string dmsg = "C0999FDDE378D7ED727DA00BCA5A84EE47F269A4D6438190D9D52F78F5358499828AC9B453E0E653";
            // pé¢0ø5Õ6ôXp"÷
            string msg2 = "8787878787878787";
           string key2 = "0E329232EA6D0D73";
          string dmsg2 = "0000000000000000";

            string m = "c0999fdde378d7ed";
            //h¡?O ?? ÅU
            //h¡?O??ÅU
            //h¡?O??ÅU
            string k = "0E329232EA6D0D73";
            string o = "Your lips are smoother than vase";
            string dm = "";

            RunDES des = new RunDES(key, "68A187D49381C5D9F2B9CF74F82ABDE1");

            //des.RunEncrypt();
            des.RunDecrypt();
            /*Console.WriteLine("-----------------------");
                for (int c = 0; c <= 255; c++)
                {
                    Console.WriteLine((char)(c));
                }

                Console.WriteLine();
            Console.WriteLine("-------------------------");*/
            Console.ReadKey();
        }
    }
}
