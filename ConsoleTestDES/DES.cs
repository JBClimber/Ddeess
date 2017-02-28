using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsoleTestDES
{
    public partial class DES : Form
    {
        private bool triple;    // singel of triple pass DES

        public DES()
        {
            InitializeComponent();
            FullResetDES();
        }

        private void menuTypeTripleSingle_Click(object sender, EventArgs e)
        {   // clicked menu "Type"

            if(menuTypeTripleSingle.Text == "Triple")   // selected Triple DES
            {
                this.triple = true;
                this.Text = "triple DES";
                menuTypeTripleSingle.Text = "Single";
            }
            else if (menuTypeTripleSingle.Text == "Single")  // Selected Single DES
            {
                this.triple = false;
                this.Text = "DES";
                menuTypeTripleSingle.Text = "Triple";
            }
        }

        private void ResetDES()
        {
            this.textKey.Text = "";
            this.textMsg.Text = "";
            this.textEncDec.Text = "";
            errorKey.Clear();
            errorMsg.Clear();
        }

        private void FullResetDES()
        {
            this.triple = false;
            menuTypeTripleSingle.Text = "Triple";
            this.textKey.Text = "";
            this.textMsg.Text = "";
            this.textEncDec.Text = "";
            errorKey.Clear();
            errorMsg.Clear();
        }

        private void menuFileReset_Click(object sender, EventArgs e)
        {
            ResetDES();
        }

        private void menuFileClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btEncDec_Click(object sender, EventArgs e)
        {
            if (isKeyBoxValidated(this.textKey) & isMsgBoxValidated(this.textMsg))
            {
                string dec = RunDES(this.textKey.Text, this.textMsg.Text, this.triple);
                this.textEncDec.Text = dec;
            }
        }

        public string RunDES(string key, string msg, bool triple)
        {
            /*string text = "Your";     // string to hex string
            char[] chars = text.ToCharArray();
            StringBuilder stringBuilder = new StringBuilder();
            foreach (char c in chars)
            {
                stringBuilder.Append(((Int16)c).ToString("x"));
            }
            String textAsHex = stringBuilder.ToString();
            Console.WriteLine(textAsHex);
            return textAsHex;*/

            string encMsg = "";

            DESCode.RunDES des = new DESCode.RunDES(key, msg);

            //encMsg += des.GetStringKey() + "\n";
            //encMsg += des.GetStringMsg();

            ///Console.WriteLine("KEY: " + des.GetBinaryStringKey());
            //Console.WriteLine("MSG: " + des.GetBinaryStringMsg());
            Console.Read();

            return encMsg;
        }

        private bool isKeyBoxValidated(TextBox key)
        {   // validates textBox key for length of 16 characters

            if (key.Text.Length != 16)  // length must be 16
            {
                errorKey.SetError(key,"must have 16\ncharacters !!!");
                return false;
            }
            else
            {
                key.BackColor = Color.Green;
                errorKey.Clear();
                return true;
            }
        }

        private bool isMsgBoxValidated(TextBox msg)
        {   // textMsg box must have someting to encrypt else returns false

            if (msg.Text.Length == 0)   // must have something to encrypt
            {
                errorMsg.SetError(msg, "must have something to encrypt !!!");
                return false;
            }
            else
            {
                errorMsg.Clear();
                return true;
            }
        }

        private void textKey_Enter(object sender, EventArgs e)
        {
            errorKey.Clear();
        }

        private void textKey_Leave(object sender, EventArgs e)
        {
            isKeyBoxValidated(this.textKey);
        }

        private void textMsg_Enter(object sender, EventArgs e)
        {
            errorMsg.Clear();
        }

        private void textMsg_Leave(object sender, EventArgs e)
        {
            isMsgBoxValidated(this.textMsg);
        }
    }
}
