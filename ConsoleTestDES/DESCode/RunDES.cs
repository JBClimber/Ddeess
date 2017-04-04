using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsoleTestDES.DESCode
{
    public class RunDES
    {
        private KeyGenerators kg;
        private MsgGenerator mg;
        private Functions f;

        private List<string> msgPart;

        public RunDES()
        {
            this.kg = null;
            this.mg = null;
            this.f = null;
            msgPart = null;
        }


        public RunDES(string key, string msg)
        {   // constructor that encrypts or decrypts (single)

            this.kg = new KeyGenerators( key );
            this.f = new Functions();

            if (Helper.IsHexString(msg))    // if it is a hex string
            {
                if (msg.Length % 2 != 0)
                {
                    Console.WriteLine("WARNING !!! odd number of hex's");
                }
                
                msg = Helper.AddPaddingToMsg(msg, 16, "0");     // padding of 16 for hex block
                this.msgPart = Helper.SplitMsgBySize(msg, 16);
            }
            else                            // if not a hex string
            {
                msg = Helper.AddPaddingToMsg(msg, 8, "0");      // padding of 8 for character block
                this.msgPart = Helper.SplitMsgBySize(msg, 8);
            }
            
            //this.mg = new MsgGenerator(msg);        // initial permutation happens in the MsgGenerator constructor

        }

        public string RunEncrypt()
        {   // this encryption uses the MsgGenerator and KeyGenerators

            string hexoutput = "";
            string textoutput = "";

            for (int i=0; i<msgPart.Count; i++) {

                mg = new MsgGenerator(msgPart[i]);

                bool[] fcn;     // result for the f function
                bool[] lXORfcn; // temporary storage for the Lside and Rside swap

                for (int j = 0; j <= 15; j++)
                {
                    fcn = f.fFunction(mg.GetRight(), kg.GetKKeyNumber(j + 1));
                    lXORfcn = mg.L_XOR_f(fcn);
                    mg.SetLeft(mg.GetRight());
                    mg.SetRight(lXORfcn);
                }

                mg.IPinvPermutation(mg.RconcatL());

                //textoutput += mg.GetMsgAsText();
                textoutput += mg.GetMsgAsHexString();
                hexoutput += mg.GetMsgAsHexString()+" ";
            }

            Console.WriteLine("\nEncrypted Hex output is:\n"+hexoutput+"|");
            //MessageBox.Show("HEX output:\n"+hexoutput);
            //Console.WriteLine("\nEncrypted Text output:\n"+textoutput+"|");

            return textoutput;
        }

        public string RunEncrypt(string key, string msg)
        {   // use this encryption ONLY if the string is in HEX
            // else use the Constuctor and the RunEncrypt() methods above
            string output = "";

            kg = new KeyGenerators(key);
            f = new Functions();

            bool[] fcn;     // result for the f function
            bool[] lXORfcn; // temporary storage for the Lside and Rside swap

            msg = Helper.AddPaddingToMsg(msg, 16, "0");
            List<string> msgPart = Helper.SplitMsgBySize(msg, 16);

            for (int i = 0; i < msgPart.Count; i++)
            {
                mg = new MsgGenerator(msgPart[i]);

                for (int j = 0; j <= 15; j++)
                {
                    fcn = f.fFunction(mg.GetRight(), kg.GetKKeyNumber(j + 1));
                    lXORfcn = mg.L_XOR_f(fcn);
                    mg.SetLeft(mg.GetRight());
                    mg.SetRight(lXORfcn);
                }

                mg.IPinvPermutation(this.mg.RconcatL());
                output += mg.GetMsgAsHexString();
            }

                return output;
        }


        public string RunDecrypt()
        {
            string output = "";
            string hexoutput = "";

            for (int i=0; i < msgPart.Count; i ++) {

                mg = new MsgGenerator(msgPart[i]);

                bool[] fcn;     // result for the f function
                bool[] lXORfcn; // temporary storage for the Lside and Rside swap

                for (int j = 0; j <= 15; j++)
                {
                    fcn = f.fFunction(mg.GetRight(), kg.GetKKeyNumber(16 - j));
                    lXORfcn = mg.L_XOR_f(fcn);
                    mg.SetLeft(mg.GetRight());
                    mg.SetRight(lXORfcn);
                }

                mg.IPinvPermutation(mg.RconcatL());

                hexoutput += mg.GetMsgAsHexString() + " ";
                //output += mg.GetMsgAsText();
                output += mg.GetMsgAsHexString();
            }

            Console.WriteLine("\n\nDecrypted HEX output id:\n"+hexoutput);
            Console.WriteLine("\nDecrypted TEXT output is:\n" + output);

            return output;
        }

        //public string GetStringKey()
        //{
        //    return Encoding.ASCII.GetString(this.key);
        //}

        //public string GetStringMsg()
        //{
        //    return Encoding.ASCII.GetString(this.msg);
        //}

        //public string GetBinaryStringKey()
        //{   // converts key byte array to a binary string
        //    string toBinary = "";

        //    foreach (byte b in this.key ) {
        //        toBinary += (string) Int32.Parse(Convert.ToString(b, 2)).ToString("0000 0000") + " ";
        //    }

        //    return toBinary;
        //}

        //public string GetBinaryStringFromByteArray(byte[] byteArray)
        //{   // converts byte array to a binary string
        //    string toBinary = "";

        //    foreach (byte b in byteArray)
        //    {
        //        toBinary += (string)Int32.Parse(Convert.ToString(b, 2)).ToString("0000 0000") + " ";
        //    }

        //    return toBinary;
        //}

        //public string ToHexString(string s)
        //{   // converts text to HEX string

        //    char[] chars = s.ToCharArray();
        //    StringBuilder sb = new StringBuilder();
        //    foreach (char c in chars)
        //    {
        //        sb.Append(((Int16)c).ToString("x"));
        //    }
        //    return sb.ToString();
        //}

        //public byte[] HexStringToByteArray(string hex)
        //{
        //    return Enumerable.Range(0, hex.Length).Where(x => x % 2 == 0).Select(x => Convert.ToByte(hex.Substring(x, 2), 16)).ToArray();
        //}

        //public void SplitKeyToBitArrays()
        //{
        //    Console.WriteLine("keyarray length: "+key.Length);

        //    byte[] c = new byte[4];
        //    byte[] d = new byte[4];

        //    this.bitKeyC = new BitArray(32);
        //    int ci = 0;
        //    this.bitKeyD = new BitArray(32);
        //    int di = 0;

        //    for (int i=0; i < 4; i++)
        //    {
        //        c[i] = this.key[i];
        //        for (int cii=0; cii<8; cii++)
        //        {
        //            bitKeyC.Set(ci, ((c[i] & (1 << 7-cii)) != 0));
        //            ci++;
        //        }

        //        d[i] = this.key[i + 4];
        //        for (int dii=0; dii<8; dii++)
        //        {
        //            bitKeyD.Set(di, ((d[i] & (1 << 7 - dii)) != 0));
        //            di++;
        //        }
        //    }

        //    Console.WriteLine(GetBinaryStringFromByteArray(c));
        //    Console.WriteLine(GetBinaryStringFromByteArray(d));

        //    Console.WriteLine("C: "+PrintBitArray(this.bitKeyC, 8));
        //    Console.WriteLine("D: "+PrintBitArray(this.bitKeyD, 8));

        //}

        //public string PrintBitArray(BitArray b, int l)
        //{
        //    string output = "";
        //    for (int i=0; i < b.Length; i++)
        //    {
        //        output += ((b.Get(i)) ? "1" : "0");
        //        if ((i+1) % l == 0)
        //        {
        //            output += " ";
        //        }
        //    }
        //    return output;
        //}


    }
}
