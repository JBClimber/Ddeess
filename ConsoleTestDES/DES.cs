using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsoleTestDES
{
    public partial class DES : Form
    {
        private bool triple;    // singel of triple pass DES
        private bool decrypt;

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
            this.decrypt = false;
            menuTypeTripleSingle.Text = "Triple";
            textKey.Text = "";
            textMsg.Text = "";
            textEncDec.Text = "";
            Text = "DES encrypt";
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
                string dec = RunDES(this.textKey.Text, this.textMsg.Text);
                this.textEncDec.Text = dec;
            }
        }

        public string RunDES(string key, string msg)
        {
            string encMsg = "";
            DESCode.RunDES des;


            if (!this.decrypt && !this.triple)      // encrypts single
            {   
                des = new DESCode.RunDES(key.ToUpper(), msg);
                encMsg += des.RunEncrypt();
            }
            else if(this.decrypt && !this.triple)   // decrypts single
            {
                des = new DESCode.RunDES(key, msg);
                encMsg += des.RunDecrypt();
            }

            Console.Read();

            return encMsg;
        }

        private bool isKeyBoxValidated(TextBox key)
        {   // validates textBox key for length of 16 characters
            
            if ( !Regex.IsMatch(key.Text, "^[0-9A-Fa-f]{16}$"))  // length must be 16
            {
                errorKey.SetError(key,"must have 16\nHEX numbers !!!");
                key.BackColor = Color.Red;
                return false;
            }
            else
            {
                key.BackColor = Color.White;
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

        private void menuFileDecrypt_Click(object sender, EventArgs e)
        {
            if (this.menuFileDecrypt.Text == "Encrypt")
            {
                decrypt = false;
                menuFileDecrypt.Text = "Decrypt";
                btEncDec.Text = "Encrypt";
                labelEncDec.Text = "enc:";
                this.Text = "DES encrypt";

            }
            else if(this.menuFileDecrypt.Text == "Decrypt")
            {
                decrypt = true;
                menuFileDecrypt.Text = "Encrypt";
                btEncDec.Text = "Decrypt";
                labelEncDec.Text = "dec:";
                this.Text = "DES decrypt";
            }

        }
    }
}
