using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        Form1 form1;
        int liczba1 = 1;
        int liczba2 = 1;
        public Form2(Form1 form1)
        {
            this.form1 = form1;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (liczba1 > 0)
            {
                --liczba1;
                label4.Text = liczba1.ToString();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if ((liczba1 + liczba2) < 8)
            {
                ++liczba2;
                label5.Text = liczba2.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if ((liczba1 + liczba2) < 8)
            {
                ++liczba1;
                label4.Text = liczba1.ToString();
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (liczba2 > 0)
            {
                --liczba2;
                label5.Text = liczba2.ToString();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            form1.liczbaOkienek1 = liczba1;
            form1.liczbaOkienek2 = liczba2;
            if ((liczba1 + liczba2) > 1)
            {
                this.Close();
            }
        }
    }
}
