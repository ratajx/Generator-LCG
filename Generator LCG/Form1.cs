using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Numerics;
using System.Runtime;
using System.Diagnostics;
using System.Threading;
using System.IO;

namespace Generator_LCG
{
    public partial class Form1 : Form
    {
        public int lenght = 0;
        public bool[] keyTab;

        public Form1()
        {
            InitializeComponent();
            textBox8.Enabled = false;
            textBox9.Enabled = false;
        }

        public static byte[] BitArrayToByteArray(BitArray bits)
        {
            byte[] ret = new byte[(bits.Length - 1) / 8 + 1];
            bits.CopyTo(ret, 0);
            return ret;
        }

        bool[] getLin(BigInteger[] seriesTab, BigInteger m, BigInteger a, BigInteger b, TextBox textbox,ProgressBar progres)
        {
            int temp;
            textbox.AppendText(Convert.ToString(seriesTab[0] % 2));
            bool[] keyTab = new bool[seriesTab.Length];
            progres.Value = 0;
            progres.Maximum = seriesTab.Length;
            string s = "";
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i < seriesTab.Length; i++)
            {
               
                seriesTab[i] = (a * (seriesTab[i - 1]) + b) % m;
                if (seriesTab[i] < 0)
                    seriesTab[i] *= -1;
                temp = (int)(seriesTab[i] % 2);
                keyTab[i] = Convert.ToBoolean(temp);
                sb.Append(Convert.ToString(seriesTab[i] % 2));
                progres.Value += 1;

            }
            
            textbox.Text = sb.ToString();
            

            progres.Value = progres.Maximum;
            return keyTab;
        }

        bool[] getSqr(BigInteger[] seriesTab, BigInteger m, BigInteger a, BigInteger b, BigInteger c, TextBox textbox, ProgressBar progres)
        {
            int temp;
            textbox.AppendText(Convert.ToString(seriesTab[0] % 2) + " ");
            bool[] keyTab = new bool[seriesTab.Length];
            progres.Value = 0;
            progres.Maximum = seriesTab.Length;

            for (int i = 1; i < seriesTab.Length; i++)
            {
                seriesTab[i] = ((a * (seriesTab[i - 1]) * (seriesTab[i - 1])) + (b * (seriesTab[i - 1])) + c) % m;
                if (seriesTab[i] < 0)
                    seriesTab[i] *= -1;
                temp = (int)(seriesTab[i] % 2);
                keyTab[i] = Convert.ToBoolean(temp);
                textbox.AppendText(Convert.ToString(seriesTab[i] % 2) + " ");
                progres.Value += 1;
            }
            progres.Value = progres.Maximum;
            return keyTab;
        }

        bool[] getCub(BigInteger[] seriesTab, BigInteger m, BigInteger a, BigInteger b, BigInteger c, BigInteger d, TextBox textbox, ProgressBar progres)
        {
            int temp;
            textbox.AppendText(Convert.ToString(seriesTab[0] % 2) + " ");
            bool[] keyTab = new bool[seriesTab.Length];
            progres.Value = 0;
            progres.Maximum = seriesTab.Length;

            for (int i = 1; i < seriesTab.Length; i++)
            {
                seriesTab[i] = ((a * (seriesTab[i - 1] * seriesTab[i - 1] * seriesTab[i - 1]))
                    + (b * (seriesTab[i - 1] * seriesTab[i - 1]))
                    + (c * seriesTab[i - 1])
                    + d) % m;
                if (seriesTab[i] < 0)
                    seriesTab[i] *= -1;
                temp = (int)(seriesTab[i] % 2);
                keyTab[i] = Convert.ToBoolean(temp);
                textbox.AppendText(Convert.ToString(seriesTab[i] % 2) + " ");
                progres.Value += 1;
            }
            progres.Value = progres.Maximum;
            return keyTab;
        }

          //ANSI
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Text = Convert.ToString(Math.Pow(2, 32));
            textBox2.Text = Convert.ToString(1103515245);
            textBox3.Text = Convert.ToString(12356);
        }

        //MINSTD
        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Text = Convert.ToString(Math.Pow(2, 31) - 1);
            textBox2.Text = Convert.ToString(Math.Pow(7, 5));
            textBox3.Text = Convert.ToString(0);
        }

        //RANDU
        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Text = Convert.ToString(Math.Pow(2, 31));
            textBox2.Text = Convert.ToString(65539);
            textBox3.Text = Convert.ToString(0);
        }

        //Fortan
        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Text = Convert.ToString(Math.Pow(2, 32) - 1);
            textBox2.Text = Convert.ToString(630360016);
            textBox3.Text = Convert.ToString(0);
        }

        //NAG
        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Text = Convert.ToString(576460752303423488);
            textBox2.Text = Convert.ToString(Math.Pow(13, 13));
            textBox3.Text = Convert.ToString(0);
        }

        //APPLE
        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Text = Convert.ToString(Math.Pow(2, 35));
            textBox2.Text = Convert.ToString(19530937237);
            textBox3.Text = Convert.ToString(0);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            textBox4.Clear();
            BigInteger a, b, c, d, m;
            int seriesLenght = Convert.ToInt32(textBox10.Text);
            lenght = seriesLenght;
            BigInteger[] seriesTab = new BigInteger[seriesLenght];
            keyTab = new bool[seriesLenght];
            Random firstBit = new Random();

            if (checkBox1.Checked == true)
            {
                seriesTab[0] =Int64.Parse( textBox5.Text);
            }
            else
            {
                seriesTab[0] = (int)Math.Pow(firstBit.Next(2, 30), firstBit.Next(2, 15));
            }

            if (radioButton1.Checked == true)
            {
                if (radioButton5.Checked != true && radioButton7.Checked != true)
                    m = BigInteger.Parse(textBox1.Text) - 1;
                else m = BigInteger.Parse(textBox1.Text);
                a = BigInteger.Parse(textBox2.Text);
                b = BigInteger.Parse(textBox3.Text);


                keyTab = getLin(seriesTab, m, a, b, textBox4, progressBar1);
            }
            if (radioButton2.Checked == true)
            {
                if (radioButton5.Checked != true && radioButton7.Checked != true)
                    m = BigInteger.Parse(textBox1.Text) - 1;
                else m = BigInteger.Parse(textBox1.Text);
                a = BigInteger.Parse(textBox2.Text);
                b = BigInteger.Parse(textBox3.Text);
                c = BigInteger.Parse(textBox8.Text);


                keyTab = getSqr(seriesTab, m, a, b, c, textBox4,progressBar1);
            }
            if (radioButton3.Checked == true)
            {
                if (radioButton5.Checked != true && radioButton7.Checked != true)
                    m = BigInteger.Parse(textBox1.Text) - 1;
                else m = BigInteger.Parse(textBox1.Text);
                a = BigInteger.Parse(textBox2.Text);
                b = BigInteger.Parse(textBox3.Text);
                c = BigInteger.Parse(textBox8.Text);
                d = BigInteger.Parse(textBox9.Text);


                keyTab = getCub(seriesTab, m, a, b, c, d, textBox4, progressBar1);
            }
        }
        

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            textBox8.Enabled = false;
            textBox9.Enabled = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            
            textBox9.Enabled = false;
            textBox8.Enabled = true;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            textBox9.Enabled = true;
            textBox8.Enabled = true;
        }


        
        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog okienko = new OpenFileDialog();
            okienko.Filter = "Pliki textowe (txt)|*.txt";
            if (okienko.ShowDialog() == DialogResult.OK)
            {
                StringBuilder sb = new StringBuilder();
                string path = okienko.FileName;
                StreamReader sr = new StreamReader(path, Encoding.Default);
                string s;
                textBox4.Clear();
                do
                {
                    s = sr.ReadLine();
                    textBox4.Text = textBox4.Text + s;
                    sb.AppendLine(s);
                } while (s != null);
                sr.Close();
            }
            else MessageBox.Show("Nie wczytano  pliku.");

            String tekst;
            tekst =usunSpacje(textBox4.Text);
            char[] tab = tekst.ToCharArray();
            keyTab = new bool[tekst.Length];
            
            for (int i = 0; i < tekst.Length; i++)
            {
                if (tab[i] == '1')
                    keyTab[i] = true;
                else
                    keyTab[i] = false;
            }





        }

        private void button6_Click(object sender, EventArgs e)
        {
            bool[] key = keyTab;
            byte[] asciiBytes = Encoding.ASCII.GetBytes(textBox7.Text);
            BitArray taBitArray = new BitArray(asciiBytes);
            if (key.Length > textBox7.Text.Length * 8)
                Array.Resize(ref key, textBox7.Text.Length * 8);

            BitArray nowy = taBitArray.Xor(new BitArray(key));
            byte[] bytesBack = BitArrayToByteArray(nowy);
            string textBack = System.Text.Encoding.ASCII.GetString(bytesBack);

            StringBuilder sb = new StringBuilder();
            for (int j = 0; j < nowy.Length; j++)
            {
                if (nowy[j] == false)
                   sb.Append( "0");
                else
                    sb.Append("1");
            }

            textBox6.Text = sb.ToString();
        }

        string usunSpacje(string lancuch)
        {
            string bezSpacji;
            bezSpacji = lancuch.Replace(" ", "");
            return bezSpacji;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            bool[] key = keyTab;
            String szyfr = usunSpacje(textBox6.Text);
            char[] tab = szyfr.ToCharArray();
            BitArray ba=new BitArray(szyfr.Length);

            for (int i = 0; i < szyfr.Length; i++)
            {
                if (tab[i] == '1')
                    ba[i] = true;
                else
                    ba[i] = false;
            }
                
            if (key.Length > szyfr.Length)
                Array.Resize(ref key, szyfr.Length);

            BitArray nowy2 = ba.Xor(new BitArray(key));
            byte[] bytesBack2 = BitArrayToByteArray(nowy2);
            string textBack2 = System.Text.Encoding.ASCII.GetString(bytesBack2);
            textBox7.Text = textBack2;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveFileDialog okienko = new SaveFileDialog();
            okienko.Filter = "Pliki textowe (txt)|*.txt";
            if (okienko.ShowDialog() == DialogResult.OK)
            {
                string path = okienko.FileName;
                StreamWriter sw = new StreamWriter(path, true, Encoding.Default);
                sw.WriteLine(textBox4.Text);
                sw.Close();
            }
            else MessageBox.Show("Nie zapisano pliku");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SaveFileDialog okienko = new SaveFileDialog();
            okienko.Filter = "Pliki textowe (txt)|*.txt";
            if (okienko.ShowDialog() == DialogResult.OK)
            {
                string path = okienko.FileName;
                StreamWriter sw = new StreamWriter(path, true, Encoding.Default);
                sw.WriteLine(textBox7.Text);
                sw.Close();
            }
            else MessageBox.Show("Nie zapisano pliku");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            SaveFileDialog okienko = new SaveFileDialog();
            okienko.Filter = "Pliki textowe (txt)|*.txt";
            if (okienko.ShowDialog() == DialogResult.OK)
            {
                string path = okienko.FileName;
                StreamWriter sw = new StreamWriter(path, true, Encoding.Default);
                sw.WriteLine(textBox6.Text);
                sw.Close();
            }
            else MessageBox.Show("Nie zapisano pliku");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog okienko = new OpenFileDialog();
            okienko.Filter = "Pliki textowe (txt)|*.txt";
            if (okienko.ShowDialog() == DialogResult.OK)
            {
                StringBuilder sb = new StringBuilder();
                string path = okienko.FileName;
                StreamReader sr = new StreamReader(path, Encoding.Default);
                string s;
                textBox7.Clear();
                do
                {
                    s = sr.ReadLine();
                    textBox7.Text = textBox7.Text + s;
                    sb.AppendLine(s);
                } while (s != null);
                sr.Close();
            }
            else MessageBox.Show("Nie wczytano  pliku.");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            OpenFileDialog okienko = new OpenFileDialog();
            okienko.Filter = "Pliki textowe (txt)|*.txt";
            if (okienko.ShowDialog() == DialogResult.OK)
            {
                StringBuilder sb = new StringBuilder();
                string path = okienko.FileName;
                StreamReader sr = new StreamReader(path, Encoding.Default);
                string s;
                textBox6.Clear();
                do
                {
                    s = sr.ReadLine();
                    textBox6.Text = textBox6.Text + s;
                    sb.AppendLine(s);
                } while (s != null);
                sr.Close();
            }
            else MessageBox.Show("Nie wczytano  pliku.");
        }

    }
}
