using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace robotyproj2
{
    public struct Punkt
    {
        public int x;
        public int y;
        public List<int> sasiedzi;
        public List<int> odleglosci;
        public Punkt(int px, int py)
        {
            x = px;
            y = py;
            sasiedzi = new List<int>();
            odleglosci = new List<int>();
        }
    }
    public struct Punkty
    {
        public Punkt[] punkty;
        public int rozmiar;
        public Punkty(int max)
        {
            punkty = new Punkt[max+2];
            rozmiar = 0;
            //punkty[rozmiar++] = new Punkt (3060,775); //Tokyo
            //punkty[rozmiar++] = new Punkt (1795, 541); //London
        }
        public void dodaj(int x, int y)
        {
            punkty[rozmiar++] = new Punkt(x, y);
        }
        public void dodaj_poczatek(int x, int y)
        {
            punkty[0] = new Punkt(x, y);
        }
        public void dodaj_koniec(int x, int y)
        {
            punkty[1] = new Punkt(x, y);
        }
    }

    public partial class Form1 : Form
    {
        bool jest_droga;
        private Punkty punkty;
        private List<int> dobrePunkty = new List<int>();
        public int odleglosc(Punkt a, Punkt b)
        {
            float odl_x = Math.Abs(a.x - b.x);
            float odl_y = Math.Abs(a.y - b.y);
            int odleg = (int)Math.Sqrt(odl_x * odl_x + odl_y * odl_y);
            return odleg;
        }
        public void line(Punkt a, Punkt b)
        {
            int w = b.x - a.x;
            int h = b.y - a.y;
            int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;
            if (w < 0) dx1 = -1; else if (w > 0) dx1 = 1;
            if (h < 0) dy1 = -1; else if (h > 0) dy1 = 1;
            if (w < 0) dx2 = -1; else if (w > 0) dx2 = 1;
            int longest = Math.Abs(w);
            int shortest = Math.Abs(h);
            if (!(longest > shortest))
            {
                longest = Math.Abs(h);
                shortest = Math.Abs(w);
                if (h < 0) dy2 = -1; else if (h > 0) dy2 = 1;
                dx2 = 0;
            }
            int numerator = longest >> 1;
            for (int i = 0; i <= longest; i++)
            {
                //putpixel(x, y, color);
                if (image1.GetPixel(a.x, a.y).B == 0)
                {
                    jest_droga = false;
                    break;
                }
                numerator += shortest;
                if (!(numerator < longest))
                {
                    numerator -= longest;
                    a.x += dx1;
                    a.y += dy1;
                }
                else
                {
                    a.x += dx2;
                    a.y += dy2;
                }
            }
        }
        private PictureBox pictureBox1;
        private Label label1;

        private Button button1;
        Pen blackPen = new Pen(Color.Black, 3);
        Pen bluePen = new Pen(Color.FromArgb(0,0,255), 6);
        Pen redPen = new Pen(Color.Red, 6);
        Pen greenPen = new Pen(Color.Green, 12);
        Color czerwony = Color.Red; // ustalenie czerwonego koloru
        Color czarny = Color.Black; // ustalenie czarnego koloru
        Color zielony = Color.Green; // ustalenie czarnego koloru
        private Button button2;
        private Button button3;

        // Bitmap image1;
        Bitmap image1 = new Bitmap(@" C:\Users\Marek\Desktop\RM\PROJEKT\worldmap.png", true);

        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(22, 24);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(106, 35);
            this.button1.TabIndex = 0;
            this.button1.Text = "Rysuj";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(158, 8);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(820, 436);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(22, 160);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(106, 37);
            this.button2.TabIndex = 3;
            this.button2.Text = "A*";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(22, 117);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(106, 37);
            this.button3.TabIndex = 4;
            this.button3.Text = "Dijkstra";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Dijkstra);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(985, 452);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button3);
            this.Name = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public Form1()
        {
            InitializeComponent();
            //Set the PictureBox to display the image.
            pictureBox1.Image = image1;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e) //Znalezienie losowych puntków na wodzie, rysowanie
        {

            int x, y;

            Graphics g = Graphics.FromImage(image1);

            int MAX = 1000; //ile punktów losujemy     
            int ilosc_punktow = 0;
            this.punkty = new Punkty(MAX + 1);

            Random rnd = new Random();
            for(int i=0;i< MAX;++i)
            {
                x = rnd.Next(1, 3592);
                y = rnd.Next(1, 2416);

                if (image1.GetPixel(x, y).R != 0) //Wybieranie tych, które znajdują się na wodzie (nie są w kolorze czarnym)
                {                  
                    this.punkty.dodaj(x, y);
                    SolidBrush myBrush;        
                        myBrush = new SolidBrush(Color.FromArgb(0, 0, 255)); //rysowanie czarnych punktów dookoła wybranych pikseli
                    g.FillEllipse(myBrush, new Rectangle(x - 10, y - 10, 20, 20)); //rysowanie linii
                }
            }
            this.punkty.dodaj_poczatek(3060,775); //Tokyo
            this.punkty.dodaj_koniec(1795, 541); //London

            //Narysowanie punktu startowego i końcowego (Tokio-Londyn)   
            SolidBrush myBrush1 = new SolidBrush(Color.FromArgb(255, 0, 0));
            g.FillEllipse(myBrush1, new Rectangle(3060-10, 765-10, 20, 20));

            SolidBrush myBrush2 = new SolidBrush(Color.FromArgb(0, 255, 0));
            g.FillEllipse(myBrush2, new Rectangle(1795, 541, 20, 20));

            //Algorytm PRM
            int odl_max = 190; //max. odległość od wierzchołka sąsiedniego
            for (int i = 0; i < this.punkty.rozmiar - 1; ++i)
            {
                for (int j = i + 1; j < this.punkty.rozmiar; ++j)
                {
                    Punkt a = this.punkty.punkty[i];
                    Punkt b = this.punkty.punkty[j];
                    jest_droga = false;
                    int odl = odleglosc(a, b);
                    if (odl != 0 && odl < odl_max)
                    {

                        jest_droga = true;
                        line(a, b);
                        if (jest_droga)
                        {
                            dobrePunkty.Add(i);
                            a.sasiedzi.Add(j);
                            b.sasiedzi.Add(i);
                            a.odleglosci.Add(odl);
                            b.odleglosci.Add(odl);
                            a = this.punkty.punkty[i];
                            using (var graphics = Graphics.FromImage(image1))
                            {
                                graphics.DrawLine(bluePen, a.x, a.y, b.x, b.y);
                                pictureBox1.Image = image1;
                                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                            }

                        }
                    }
                }
            }     
            label1.Text = "Zakończono rysowanie";
        }
    private void Dijkstra(Object sender,EventArgs e)
        {
            Graphics g = Graphics.FromImage(image1);

            List<int> sprawdzone = new List<int>();
            List<int> doSprawdzenia = new List<int>();
            for (int i = 0; i < this.punkty.rozmiar; i++)
            {
                    doSprawdzenia.Add(i);        
            }

            List<int> odleglosci = new List<int>();
            List<int> poprzednik = new List<int>();
            for (int i = 0; i < this.punkty.rozmiar; i++)
            {
                odleglosci.Add(1000000);
                poprzednik.Add(-1);
            }
            odleglosci[0] = 0;
            int ileDoSprawdzenia = doSprawdzenia.Count;

            while(ileDoSprawdzenia > 0)
            {
                int minCost = 1000000;
                List<int> minimum = new List<int>();

                for(int i = 0; i < this.punkty.rozmiar; i++)
                {
                    if(doSprawdzenia.Contains(i) && odleglosci[i] < minCost)
                    {
                        minimum = new List<int>();
                        minimum.Add(i);
                        minCost = odleglosci[i];
                    }
                    else if(doSprawdzenia.Contains(i) && odleglosci[i] == minCost)
                    {
                        minimum.Add(i);
                    }
                }

                for(int i = 0; i < minimum.Count; i++)
                {
                    sprawdzone.Add(minimum[i]);
                    doSprawdzenia.Remove(minimum[i]);
                    ileDoSprawdzenia = doSprawdzenia.Count;

                    for(int k = 0; k < this.punkty.punkty[minimum[i]].sasiedzi.Count; k++)
                    {
                        if(doSprawdzenia.Contains(this.punkty.punkty[minimum[i]].sasiedzi[k]))
                        {
                            if (odleglosci[this.punkty.punkty[minimum[i]].sasiedzi[k]] > odleglosci[minimum[i]] + this.punkty.punkty[minimum[i]].odleglosci[k])
                            {
                                odleglosci[this.punkty.punkty[minimum[i]].sasiedzi[k]] = odleglosci[minimum[i]] + this.punkty.punkty[minimum[i]].odleglosci[k];
                                poprzednik[this.punkty.punkty[minimum[i]].sasiedzi[k]] = minimum[i];
                            }
                        }
                    }
                }
            }

            List<int> droga = new List<int>();
            droga.Add(poprzednik[1]);

            int startowy = poprzednik[1];
            int licznik = 0;

            while (startowy != 0)
            {
                try
                {
                    droga.Add(poprzednik[droga[licznik]]);
                    startowy = droga[licznik];
                    licznik++;
                }
                catch (ArgumentOutOfRangeException outOfRange)
                {
                    Console.WriteLine("Nie ma drogi", outOfRange.Message);
                }
            }
            
          
            for (int i = 0; i < this.punkty.rozmiar; i++)
            {
                if (droga.Contains(i))
                {
                    using (var graphics = Graphics.FromImage(image1))
                    {

                        SolidBrush myBrush1 = new SolidBrush(Color.FromArgb(255, 0, 0));
                        g.FillEllipse(myBrush1, new Rectangle(this.punkty.punkty[i].x - 10, this.punkty.punkty[i].y - 10, 20, 20));
                        pictureBox1.Image = image1;
                        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                    
                }
            }
            
        }
    }
}

/* Próba zaimplementowania algorytmu A*
 * 
         public class OpenL
    {
        public int Current { get; set; }
        public int Parent { get; set; }
        public float F { get; set; }
        public float G { get; set; }
    }
   
   
   
    //Algorytm A*
            Punkt poczatek = punkty.punkty[0];
            Punkt koniec = punkty.punkty[1];
            Punkt sasiad = punkty.punkty[0];
            int index = 0;
            int koszt = 1000;
            int koszt_save = 1000;
            bool bylo = false;
            int index_save = 0;
 
            //Heuristic H             odleglosc(Node, End);
            //Movement cost G         odleglosc(currentNode, neighbour);
            //Total cost F            F = H(neighbourToEnd) + G;
 
            List<OpenL> OpenList = new List<OpenL>(); // a priority queue of nodes to be transversed (Parent, cost F, cumulative G).
            List<OpenL> OpenList2 = new List<OpenL>(); // a priority queue of nodes to be transversed (Parent, cost F, cumulative G).
            List<int> ClosedList = new List<int>(); // a list of nodes already transversed.
 
            OpenList.Add(new OpenL { Current = 0, Parent = 0, F = odleglosc(poczatek, koniec) + 0, G = 0 });
 
            do
            {
                foreach (var item in OpenList)
                {
                    for (int i = 0; i < punkty.rozmiar - 1; ++i)
                    {     
                        jest_droga = false;
                        bylo = false;
                        index = item.Current;
                        Punkt currentNode = punkty.punkty[index];
                        Punkt neighbour = punkty.punkty[i];
                        int odl = odleglosc(currentNode, neighbour);
                        if (odl != 0 && odl < odl_max)
                        {
                            jest_droga = true;
                            line(currentNode, neighbour);
                            if (jest_droga)
                            {
                                foreach (var item2 in OpenList2)
                                {
                                    if (item2.Current == i) bylo = true;
                                }
                                if (!bylo)
                                {
                                    OpenList.Add(new OpenL { Current = i, Parent = index, F = odleglosc(neighbour, koniec) + odl, G = odl });
                                }
                                koszt = odleglosc(neighbour, koniec) + odl;
                                if (koszt < koszt_save)
                                {
                                    index_save = index;
                                    koszt_save = koszt;
                                }
                            }
                        }
                        sasiad = punkty.punkty[index_save];
                        using (var graphics = Graphics.FromImage(image1))
                        {
                            graphics.DrawLine(bluePen, currentNode.x, currentNode.y, sasiad.x, sasiad.y);
                            pictureBox1.Image = image1;
                            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                        }
                    }
                }
                OpenList.Remove();
            }
            while ((sasiad.x != koniec.x) && (sasiad.y != koniec.y));
     * */
