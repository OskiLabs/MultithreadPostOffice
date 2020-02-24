using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        //static String info = "Zero";

        int i;
        int j;
        private int x;
        private int predkosc;
        private int liczbaOkienek;
        private bool typOkna;
        private int szybkoscLos = 0;
        int szyb = 0;
        Okienko[] zbiorOkienek1 = new Okienko[8];
        Okienko[] zbiorOkienek2 = new Okienko[8];
        Okienko okien = new Okienko();
        public int liczbaKlientow;
        public int liczbaOkienek1;
        public int liczbaOkienek2;

        private Semaphore zieloni = new Semaphore(1, 1);
        private Semaphore niebiescy = new Semaphore(1, 1);
        private Semaphore zolci = new Semaphore(1, 1);

        public const int wejsciex = 440;
        public const int wejsciey = 230;

        static int[] kolejka = new int[8];


        public Form1()
        {

            this.Hide();
            Form2 f2 = new Form2(this);
            f2.Owner = this;
            f2.ShowDialog();

            InitializeComponent();
            predkosc = 5;
            label1.Text = predkosc.ToString();
            x = 10;
            liczbaOkienek = 2;
            liczbaKlientow = 0;
            okien.typOkienka = false;

            for (i = 0; i < liczbaOkienek1; ++i)
            {
                zbiorOkienek1[i] = new Okienko();
                zbiorOkienek1[i].liczbaOsob = 0;
                zbiorOkienek1[i].polozenie = ((60 * i) + 60 * (i + 1));
                //zbiorOkienek1[i].liczba = i;
                //kolejka[i] = 0;
            }

            for (i = 0; i < liczbaOkienek2; ++i)
            {
                zbiorOkienek2[i] = new Okienko();
                zbiorOkienek2[i].liczbaOsob = 0;
                zbiorOkienek2[i].polozenie = ((60 * (i+liczbaOkienek1)) + 60 * ((i+liczbaOkienek1) + 1));
                //zbiorOkienek2[i].liczba = i;
                //kolejka[i + liczbaOkienek1] = 0;
            }

            i = 5;
            //zbiorOkienek[0].typOkienka = false;
           // zbiorOkienek[1].typOkienka = false; 
            //label4.Text = liczbaOkienek1.ToString();
            typOkna = false;
        }

        public void Klient(int wyb)
        {
            Random rndTyp = new Random();
            int czas = 200;
            int minus = 0;
            int typ;
            Okienko cel = new Okienko();
            cel.liczbaOsob = 100;
            
            Brush Kolor;

            switch (wyb)
            {
                case 0:
                    typ = rndTyp.Next(1, 4);
                    break;
                case 1:
                    typ = 1;
                    break;
                case 2:
                    typ = 2;
                    break;
                default:
                    typ = 3;
                    break;
            }

            switch (typ)
            {
                case 1: //Przesyłka Listowa
                    Kolor = Brushes.Yellow;
                    for (i = 0; i < liczbaOkienek1; ++i)
                    {
                        if (zbiorOkienek1[i].liczbaOsob <= cel.liczbaOsob)
                        {
                            cel = zbiorOkienek1[i];
                        }
                    }
                    for (i = 0; i < liczbaOkienek2; ++i)
                    {
                        if (zbiorOkienek2[i].liczbaOsob <= cel.liczbaOsob)
                        {
                            cel = zbiorOkienek2[i];
                        }
                    }
                    break;
                case 2: //Paczka
                    Kolor = Brushes.Green;
                    for (i = 0; i < liczbaOkienek1; ++i)
                    {
                        if (zbiorOkienek1[i].liczbaOsob <= cel.liczbaOsob)
                        {
                            cel = zbiorOkienek1[i];
                        }
                    }
                    break;
                default: //Pieniądze
                    Kolor = Brushes.Blue;
                    for (i = 0; i < liczbaOkienek2; ++i)
                    {
                        if (zbiorOkienek2[i].liczbaOsob <= cel.liczbaOsob)
                        {
                            cel = zbiorOkienek2[i];
                        }
                    }
                    break;
            }


            ++cel.liczbaOsob;
            //MessageBox.Show(cel.liczbaOsob.ToString());
            switch (typ)
            {
                case 1:
                    bool sprawdz = false;
                    bool sprawdzZ = false;
                    bool sprawdzN = false;
                    zolci.WaitOne();
                    //niebiescy.WaitOne();
                    //zieloni.WaitOne();
                    for (i=0;i<liczbaOkienek1;++i)
                    {
                        if (zbiorOkienek1[i].liczbaOsob < 8)
                        {
                            sprawdzZ = true;
                        }
                    }
                    if (sprawdzZ == false)
                    {
                        zieloni.WaitOne();
                    }
                    for (i = 0; i < liczbaOkienek2; ++i)
                    {
                        if (zbiorOkienek2[i].liczbaOsob < 8)
                        {
                            sprawdzN = true;
                        }
                    }
                    if (sprawdzN == false)
                    {
                        niebiescy.WaitOne();
                    }

                    for (i = 0; i < liczbaOkienek1; ++i)
                    {
                        if (zbiorOkienek1[i].liczbaOsob < 8)
                        {
                            sprawdz = true;
                            //sprawdzZ = true;
                        }
                    }
                    for (i = 0; i < liczbaOkienek2; ++i)
                    {
                        if (zbiorOkienek2[i].liczbaOsob < 8)
                        {
                            sprawdz = true;
                            //sprawdzN = true;
                        }
                    }
                    if (sprawdz == true)
                    {
                        zolci.Release();
                    }
                    break; 
                case 2:
                    bool sprawdzZiel = false;
                    zieloni.WaitOne();
                    for (i = 0; i < liczbaOkienek1; ++i)
                    {
                        if (zbiorOkienek1[i].liczbaOsob < 8)
                        {
                            sprawdzZiel = true;
                        }
                    }
                    if (sprawdzZiel == true)
                    {
                        zieloni.Release();
                    }
                    
                    break;
                case 3:
                     bool sprawdzNieb = false;
                     niebiescy.WaitOne();
                     for (i = 0; i < liczbaOkienek2; ++i)
                     {
                         if (zbiorOkienek2[i].liczbaOsob < 8)
                         {
                             sprawdzNieb = true;
                         }
                     }
                     if (sprawdzNieb == true)
                     {
                         niebiescy.Release();
                     }
                    break;
            }

            minus = x;
            while ((x - minus) < 40)
            {
                this.CreateGraphics().FillEllipse(Kolor, wejsciex, (270 - (x - minus)), 20, 20);
            }

            minus = x;
            if (cel.polozenie < wejsciex)
            {
                while ((wejsciex - (x - minus)) > cel.polozenie)
                {
                    this.CreateGraphics().FillEllipse(Kolor, wejsciex - (x - minus), wejsciey, 20, 20);
                }
            }
            else
            {
                while ((wejsciex + (x - minus)) < cel.polozenie)
                {
                    this.CreateGraphics().FillEllipse(Kolor, wejsciex + (x - minus), wejsciey, 20, 20);
                }
            }
            //////////////////////////////////////////////////////////////////////////////////

            minus = x;
            while ((wejsciey - (x - minus)) > 180)
            {
                this.CreateGraphics().FillEllipse(Kolor, cel.polozenie, (wejsciey - (x - minus)), 20, 20);
            }
            cel.pierwszySem.WaitOne();
            cel.flaga1 = 0;

            while ((wejsciey - (x - minus)) > 160)
            {
                this.CreateGraphics().FillEllipse(Kolor, cel.polozenie, (wejsciey - (x - minus)), 20, 20);
            }
            while (cel.flaga2 == 0)
            {
                this.CreateGraphics().FillEllipse(Kolor, cel.polozenie, 160, 20, 20);
            }
            cel.drugiSem.WaitOne();
            cel.flaga2 = 0;
            cel.pierwszySem.Release();
            cel.flaga1 = 1;

            while ((wejsciey - (x - minus)) > 140)
            {
                this.CreateGraphics().FillEllipse(Kolor, cel.polozenie, (wejsciey - (x - minus)), 20, 20);
            }
            while (cel.flaga3 == 0)
            {
                this.CreateGraphics().FillEllipse(Kolor, cel.polozenie, 140, 20, 20);
            }
            cel.trzeciSem.WaitOne();
            cel.flaga3 = 0;
            cel.drugiSem.Release();
            cel.flaga2 = 1;

            while ((wejsciey - (x - minus)) > 120)
            {
                this.CreateGraphics().FillEllipse(Kolor, cel.polozenie, (wejsciey - (x - minus)), 20, 20);
            }
            while (cel.flaga4 == 0)
            {
                this.CreateGraphics().FillEllipse(Kolor, cel.polozenie, 120, 20, 20);
            }
            cel.czwartySem.WaitOne();
            cel.flaga4 = 0;
            cel.trzeciSem.Release();
            cel.flaga3 = 1;

            while ((wejsciey - (x - minus)) > 100)
            {
                this.CreateGraphics().FillEllipse(Kolor, cel.polozenie, (wejsciey - (x - minus)), 20, 20);
            }
            while (cel.flaga5 == 0)
            {
                this.CreateGraphics().FillEllipse(Kolor, cel.polozenie, 100, 20, 20);
            }
            cel.piatySem.WaitOne();
            cel.flaga5 = 0;
            cel.czwartySem.Release();
            cel.flaga4 = 1;

            while ((wejsciey - (x - minus)) > 80)
            {
                this.CreateGraphics().FillEllipse(Kolor, cel.polozenie, (wejsciey - (x - minus)), 20, 20);
            }
            while (cel.flaga6 == 0)
            {
                this.CreateGraphics().FillEllipse(Kolor, cel.polozenie, 80, 20, 20);
            }
            cel.szostySem.WaitOne();
            cel.flaga6 = 0;
            cel.piatySem.Release();
            cel.flaga5 = 1;

            while ((wejsciey - (x - minus)) > 60)
            {
                this.CreateGraphics().FillEllipse(Kolor, cel.polozenie, (wejsciey - (x - minus)), 20, 20);
            }
            while (cel.flaga7 == 0)
            {
                this.CreateGraphics().FillEllipse(Kolor, cel.polozenie, 60, 20, 20);
            }
            cel.siodmySem.WaitOne();
            cel.flaga7 = 0;
            cel.szostySem.Release();
            cel.flaga6 = 1;

            while ((wejsciey - (x - minus)) > 40)
            {
                this.CreateGraphics().FillEllipse(Kolor, cel.polozenie, (wejsciey - (x - minus)), 20, 20);
            }

            while (cel.flaga8 == 0)
            {
                this.CreateGraphics().FillEllipse(Kolor, cel.polozenie, 40, 20, 20);
            }
            cel.osmySem.WaitOne();
            cel.flaga8 = 0;
            cel.siodmySem.Release();
            cel.flaga7 = 1;

            while ((wejsciey - (x - minus)) > 20)
            {
                this.CreateGraphics().FillEllipse(Kolor, cel.polozenie, (wejsciey - (x - minus)), 20, 20);
            }

            minus = x;
            czas = rndTyp.Next(50, 1000);
            while ((x - minus) < czas)
            {
                this.CreateGraphics().FillEllipse(Kolor, cel.polozenie, 20, 20, 20);
            }
            
            cel.osmySem.Release();
            cel.flaga8 = 1;

            ////////////////////////////////////////////////////////////////////////////////////
            minus = x;
            while ((x - minus) < 20)
            {
                this.CreateGraphics().FillEllipse(Kolor, cel.polozenie + ( x - minus), 20, 20, 20);
            }

            switch (typ)
            {
                case 1:
                    {
                        {
                            bool sprawdz = false;
                            bool sprawdzZ = false;
                            bool sprawdzN = false;
                            for (i = 0; i < liczbaOkienek1; ++i)
                            {

                                if (zbiorOkienek1[i].liczbaOsob < 8)
                                {
                                    sprawdz = true;
                                    sprawdzZ = true;
                                }
                            }
                            if (sprawdzZ == false)
                            {
                                zieloni.Release();
                            }
                            for (i = 0; i < liczbaOkienek2; ++i)
                            {

                                if (zbiorOkienek2[i].liczbaOsob < 8)
                                {
                                    sprawdz = true;
                                    sprawdzN = true;
                                }
                            }
                            if (sprawdzN == false)
                            {
                                niebiescy.Release();
                            }
                            if (sprawdz == false)
                            {
                                zolci.Release();
                            }
                        }
                    }
                    break;
                case 2:
                    bool sprawdzZiel = false;
                    for (i = 0; i < liczbaOkienek1; ++i)
                    {
                        if (zbiorOkienek1[i].liczbaOsob < 8)
                        {
                            sprawdzZiel = true;
                        }

                        if (sprawdzZiel == false)
                        {
                            zieloni.Release();
                        }
                    }
                    break;
                case 3:
                    bool sprawdzNieb = false;
                    for (i = 0; i < liczbaOkienek1; ++i)
                    {
                        
                        if (zbiorOkienek2[i].liczbaOsob < 8)
                        {
                            sprawdzNieb = true;
                        }

                        if (sprawdzNieb == false)
                        {
                            niebiescy.Release();
                        }
                    }
                    break;
            }
            --cel.liczbaOsob;

            minus = x;
            while ( (20 + (x - minus)) < (wejsciey-20))
            {
                this.CreateGraphics().FillEllipse(Kolor, cel.polozenie + 20, (20 + (x - minus)), 20, 20);
            }
            
            minus = x;
            if (cel.polozenie < wejsciex)
            {
                while ((cel.polozenie + 20 + (x - minus)) < wejsciex)
                {
                    this.CreateGraphics().FillEllipse(Kolor, cel.polozenie + 20 + (x - minus), (wejsciey - 20), 20, 20);
                }
            }
            else
            {
                while ((cel.polozenie + 20 - (x - minus)) > wejsciex)
                {
                    this.CreateGraphics().FillEllipse(Kolor, cel.polozenie + 20 - (x - minus), (wejsciey - 20), 20, 20);
                }
            }

            minus = x;
            while ((x-minus) < 60)
            {
                this.CreateGraphics().FillEllipse(Kolor, wejsciex, (wejsciey - 20) + (x - minus), 20, 20);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (predkosc > 0)
            {
                --predkosc;
                label1.Text = predkosc.ToString();
            }        
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (predkosc < 10)
            {
                ++predkosc;
                label1.Text = predkosc.ToString();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            szybkoscLos = 3;
            szyb = 0;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            szybkoscLos = 2;
            szyb = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Thread thr = new Thread(() => Klient(2));
            thr.IsBackground = true;
            thr.Start();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Thread thr = new Thread(() => Klient(1));
            thr.IsBackground = true;
            thr.Start();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        } 
        
        private void button6_Click(object sender, EventArgs e)
        {
            Thread thr = new Thread(() => Klient(3));
            thr.IsBackground = true;
            thr.Start();
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            szybkoscLos = 1;
            szyb = 0;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            szybkoscLos = 0;
        }

       private void button5_Click(object sender, EventArgs e)
        {
             Thread thr = new Thread(() => Klient(0));
             thr.IsBackground = true;
             thr.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            x+=predkosc;
            Invalidate();
            ++szyb;
            if (szyb == 20)
            {
                if (szybkoscLos == 1)
                {
                    Thread thr = new Thread(() => Klient(0));
                    thr.IsBackground = true;
                    thr.Start();
                    szyb = 0;
                }
            }
            if (szyb == 70) 
            {
                if (szybkoscLos == 2)
                {
                    Thread thr = new Thread(() => Klient(0));
                    thr.IsBackground = true;
                    thr.Start();
                    szyb = 0;
                }
            }
            if (szyb == 120)
            {
                if (szybkoscLos == 3)
                {
                    Thread thr = new Thread(() => Klient(0));
                    thr.IsBackground = true;
                    thr.Start();
                    szyb = 0;
                }
                else
                {
                    szyb = 0;
                }
            }
        } 
        
 

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.Black, 20, 0, 940,20 );
            for (i = 0; i < liczbaOkienek1; ++i)
            {
                    e.Graphics.FillRectangle(Brushes.Green, (100 * i) + 20 * (i + 1), 0, 100, 20);
                    e.Graphics.FillRectangle(Brushes.Red, (80 * i) + 40 * (i + 1), 180, 60, 3);
                    //e.Graphics.FillRectangle(Brushes.Green, (60 * i) + 60 * (i + 1), 20, 20, 20);
                   // e.Graphics.FillRectangle(Brushes.Green, zbiorOkienek1[i].polozenie, 20, 20, 20);
             }
            for (i = liczbaOkienek1; i < (liczbaOkienek1 + liczbaOkienek2); ++i)
              {
                  e.Graphics.FillRectangle(Brushes.Blue, (100 * i) + 20 * (i + 1), 0, 100, 20);
                  e.Graphics.FillRectangle(Brushes.Red, (80 * i) + 40 * (i + 1), 180, 60, 3);
                  //e.Graphics.FillRectangle(Brushes.Blue, (60 * i) + 60 * (i + 1), 20, 20, 20);
                //  e.Graphics.FillRectangle(Brushes.Blue, zbiorOkienek2[i-liczbaOkienek1].polozenie, 20, 20, 20);
              }

            e.Graphics.FillRectangle(Brushes.Black, 0, 0, 20, 270);
            e.Graphics.FillRectangle(Brushes.Black, 940, 0, 20, 270);
            e.Graphics.FillRectangle(Brushes.Black, 20, 250, 400, 20);
            e.Graphics.FillRectangle(Brushes.Black, 500, 250, 440, 20);
        }

     
       
      
    }
}
