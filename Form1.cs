using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        bool b;
        public Form1()
        {
            InitializeComponent();
        }
        public bool rec(string s,int q)
        {
            int r=0;
            bool kl = true;
            if (s.IndexOf("+", r)!=-1)
            {
                r = s.IndexOf("+", r)+1;
                if (s.IndexOf("(") < r - 1 && s.IndexOf(")") > r - 1)
                {
                    kl=false;
                    while (s.IndexOf("+", r) != -1)
                    {
                        kl = true;
                        r = s.IndexOf("+", r) + 1;
                        if (s.IndexOf("(") != -1 && s.IndexOf("(") < r-1 && s.IndexOf(")") > r-1)
                            kl = false;
                        else
                            break;
                    }
                }
                if (kl || s.IndexOf("+", r) != -1)
                {
                    string st = "+ - операция";
                    for (int i = 0; i < q; i++)
                        st = "_" + st;
                    b = rec(s.Substring(0, r-1), q + 1);
                    textBox2.AppendText(st + Environment.NewLine);
                    b = b && rec(s.Substring(r, s.Length - r), q + 1);
                    return b;
                }
            }
            r = 0;
            if (s.IndexOf("*", r) != -1)
            {
                kl = true;
                r = s.IndexOf("*", r)+1;
                if (s.IndexOf("(") < r - 1 && s.IndexOf(")") > r - 1)
                {
                    kl = false;
                    while (s.IndexOf("*", r) != -1)
                    {
                        kl = true;
                        r = s.IndexOf("*", r) + 1;
                        if (s.IndexOf("(") != -1 && s.IndexOf("(") < r-1 && s.IndexOf(")") >r-1)
                            kl = false;
                        else
                            break;
                    }
                }
                if (kl || s.IndexOf("*", r) != -1)
                {
                    string st = "* - операция";
                    for (int i = 0; i < q; i++)
                        st = "_" + st;
                    b = rec(s.Substring(0, r - 1), q + 1);
                    textBox2.AppendText(st + Environment.NewLine);
                    b = b && rec(s.Substring(r, s.Length - r), q + 1);
                    return b;
                }
            }
            r = 0;
            if (s.IndexOf("/", r) != -1)
            {
                kl = true;
                r = s.IndexOf("/", r) + 1;
                if (s.IndexOf("(") < r - 1 && s.IndexOf(")") > r - 1)
                {
                    kl = false;
                    while (s.IndexOf("/", r) != -1)
                    {
                        kl = true;
                        r = s.IndexOf("/", r) + 1;
                        if (s.IndexOf("(") != -1 && s.IndexOf("(") < r - 1 && s.IndexOf(")") > r - 1)
                            kl = false;
                        else
                            break;
                    }
                }
                if (kl || s.IndexOf("/", r) != -1)
                {
                    string st = "/ - операция";
                    for (int i = 0; i < q; i++)
                        st = "_" + st;
                    b = rec(s.Substring(0, r - 1), q + 1);
                    textBox2.AppendText(st + Environment.NewLine);
                    b = b && rec(s.Substring(r, s.Length - r), q + 1);
                    return b;
                }
            }
            r = 0;
            if (new Regex(@"^\-").Match(s).Success)
            {
                for (int i = 0; i < q; i++)
                    textBox2.AppendText("_");
                textBox2.AppendText(" - отрицание:" + Environment.NewLine);
                return (rec(s.Substring(1, s.Length-1), q+1));
            }
            r = 0;
            if (s.IndexOf("(", r) != -1)
            {
                int c = 1;
                while (r <= s.Length)
                    if (s.IndexOf("(", r + 1) != -1 && s.IndexOf("(", r + 1) < s.IndexOf(")"))
                    {
                        r = s.IndexOf("(", r + 1);
                        c++;
                    }
                    else
                        break;
                r = 0;
                int w = c;
                for (int i = 0; i < c && r < s.Length; i++)
                {
                    if (s.IndexOf(")", r + 1) != -1)
                        r = s.IndexOf(")", r + 1);
                    else
                        break;
                    w--;
                }
                if (w == 0)
                {
                    return (rec(s.Substring(s.IndexOf("(", 0) + 1, r - 1), q));
                }
                else
                    return false;
            }
            if (new Regex(@"^-?[0-9A-Fa-f]+").Match(s).Success && (new Regex(@"^-?[0-9A-Fa-f]+").Match(s).ToString().Length==s.Length))
            {
                for (int i = 0; i < q; i++)
                    textBox2.AppendText("_");
                textBox2.AppendText(new Regex(@"^-?[0-9A-F]+").Match(s).ToString() + " - число" + Environment.NewLine);
                return true;
            }
            if (new Regex(@"^-?[A-Za-z_][A-Za-z_0-9]*").Match(s).Success && new Regex(@"^-?[A-Za-z_][A-Za-z_0-9]*").Match(s).ToString().Length==s.Length)
            {
                for (int i = 0; i < q; i++)
                    textBox2.AppendText("_");
                textBox2.AppendText(new Regex(@"^-?([A-Za-z_][A-Za-z_0-9]*)").Match(s).ToString() + " - идентификатор" + Environment.NewLine);
                return true;
            }
            return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            if (rec(textBox1.Text, 0))
                textBox2.AppendText("успешное завершение" + Environment.NewLine);
            else
                textBox2.AppendText("ошибки" + Environment.NewLine);
   

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string s;
                System.IO.StreamReader str = new System.IO.StreamReader(openFileDialog1.FileName);
                while ((s = str.ReadLine()) != null)
                    textBox1.AppendText(s + Environment.NewLine);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
        }
    }
}
