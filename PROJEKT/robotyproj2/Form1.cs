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
    public partial class Form1 : Form
    {
        private PictureBox pictureBox1;
        private Label label1;
        private Button button1;
        Pen blackPen = new Pen(Color.Black, 3);
        Pen bluePen = new Pen(Color.Blue, 6);
        Pen redPen = new Pen(Color.Red, 3);
        Color czerwony = Color.Red; // ustalenie czerwonego koloru
        Color czarny = Color.Black; // ustalenie czarnego koloru
        Color zielony = Color.Green; // ustalenie czarnego koloru
        private Button button2;


        // Bitmap image1;
        Bitmap image1 = new Bitmap(@"C:\Users\Marcin\Desktop\robotyproj2\worldmap.png", true);

        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
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
            this.button2.Location = new System.Drawing.Point(22, 110);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(106, 37);
            this.button2.TabIndex = 3;
            this.button2.Text = "Znajdż drogę";
            this.button2.UseVisualStyleBackColor = true;

            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(985, 452);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button1);
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

            /*int[,] mapa = new int[image1.Width, image1.Height];

            for (x = 0; x < image1.Width; x++)
            {
                for (y = 0; y < image1.Height; y++)
                {
                    Color pixelColor = image1.GetPixel(x, y);
                    if(pixelColor.R == 0)
                    {
                        mapa[x, y] = 1;
                    }
                    else
                    {
                        mapa[x, y] = 0;
                    }
 
                }
            }*/

            Graphics g = Graphics.FromImage(image1);

            int MAX = 1000; //ile punktów losujemy     
            int[,] bufor = new int[2, MAX];
            int i = 1;
            ///Losowanie 5000 dowolnych punktów
            /// 
            Random rnd = new Random();

            while (i < ((MAX - 2)))
            {
                x = rnd.Next(1, 3592);
                y = rnd.Next(1, 2416);

                Color pixelColor = image1.GetPixel(x, y); //Pobranie koloru piksela     

                if (pixelColor.R != 0) //Wybieranie tych, które znajdują się na wodzie (nie są w kolorze czarnym)
                {
                    bufor[0, i] = x;
                    bufor[1, i] = y;

                    SolidBrush myBrush = new SolidBrush(Color.Green); //rysowanie czarnych punktów dookoła wybranych pikseli
                    g.FillEllipse(myBrush, new Rectangle(x, y, 10, 10)); //rysowanie linii
                }
                i++;
            }
            bufor[0, MAX - 1] = 3049; //Tokyo
            bufor[1, MAX - 1] = 775;
            bufor[0, MAX - 2] = 1775; //Londyn
            bufor[1, MAX - 2] = 561;

            //Narysowanie punktu startowego i końcowego (Tokio-Londyn)   
            SolidBrush myBrush1 = new SolidBrush(Color.Red);
            g.FillEllipse(myBrush1, new Rectangle(3049, 775, 20, 20));

            SolidBrush myBrush2 = new SolidBrush(Color.Red);
            g.FillEllipse(myBrush2, new Rectangle(1775, 561, 20, 20));

            ///Rysowanie ścieżek
            /// 
            /*
            for (i = 0; i < MAX; i++)
            {
                int odl_min = 20; ///min. odległość od wierzchołka sąsiedniego
                int odl_max = 150; //max. odległość od wierzchołka sąsiedniego
                int sasiad = i;

                for (int z = 0; z < MAX; z++)
                {

                    if (dlugosc(bufor[0, z], bufor[1, z], bufor[0, sasiad], bufor[1, sasiad]) > odl_min)
                    {

                        if (dlugosc(bufor[0, z], bufor[1, z], bufor[0, sasiad], bufor[1, sasiad]) < odl_max)
                        {
                            
                            using (var graphics = Graphics.FromImage(image1))
                            {
                                graphics.DrawLine(bluePen, bufor[0, z], bufor[1, z], bufor[0, sasiad], bufor[1, sasiad]);
                            }
                            

                        }
                    }

                }

            }
            */

            //Algorytm PRM
            int odl_min = 0; ///min. odległość od wierzchołka sąsiedniego
            int odl_max = 150; //max. odległość od wierzchołka sąsiedniego
                               //int sasiad = z;
            int sprawdzenie1 = 0;
            //int i = 0;
            //int sprawdzenie2 = 0;
            int z = 0;
            int c = 0;
            int d = 0;

            for (z = 0; z < MAX; z++)
            {
                /*
                for (c = bufor[0, z] - odl_max; c < bufor[0, z] + odl_max; c++)
                {
                    for (d = bufor[1, z] - odl_max; d < bufor[1, z] + odl_max; d++)
                    {
                        if (bufor[0, z] - odl_max > 0 && bufor[1, z] - odl_max > 0 && bufor[0, z] + odl_max < MAX - 1 && bufor[1, z] + odl_max < MAX - 1)
                        {
                            Color pixelColor = image1.GetPixel(bufor[0, c], bufor[1, d]);
                            if (pixelColor.G == 255)
                            {
                                using (var graphics = Graphics.FromImage(image1))
                                {
                                    graphics.DrawLine(redPen, bufor[0, c], bufor[1, c], bufor[0, d], bufor[1, d]);
                                    pictureBox1.Image = image1;
                                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                                }
                            }
                        }
                    }
                    */
                                        for (i = 0; i < MAX; i++)
                                        {
                                            //i = i;
                                            if (dlugosc(bufor[0, z], bufor[1, z], bufor[0, i], bufor[1, i]) > odl_min)
                                            {
                                                if (dlugosc(bufor[0, z], bufor[1, z], bufor[0, i], bufor[1, i]) < odl_max)
                                                {
                                                    //prm w gore
                                                    sprawdzenie1 = 0;
                                                    if (bufor[0, i] > bufor[0, z] && bufor[1, i] > bufor[1, z])
                                                    {
                                                        for (x = bufor[0, z]; x < bufor[0, i]; x++)
                                                        {
                                                            for (y = bufor[1, z]; y < bufor[1, i]; y++)
                                                            {
                                                                if (x > 0 && y > 0)
                                                                {
                                                                    Color pixelColor = image1.GetPixel(x, y);
                                                                    if (pixelColor.R == 0)
                                                                    {
                                                                        sprawdzenie1 = 1;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                    if (bufor[0, i] < bufor[0, z] && bufor[1, i] < bufor[1, z])
                                                    {
                                                        for (x = bufor[0, z]; x > bufor[0, i]; x--)
                                                        {
                                                            for (y = bufor[1, z]; y > bufor[1, i]; y--)
                                                            {
                                                                if (x > 0 && y > 0)
                                                                {
                                                                    Color pixelColor = image1.GetPixel(x, y);
                                                                    if (pixelColor.R == 0)
                                                                    {
                                                                        sprawdzenie1 = 1;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                    if (bufor[0, i] < bufor[0, z] && bufor[1, i] > bufor[1, z])
                                                    {
                                                        for (x = bufor[0, z]; x > bufor[0, i]; x--)
                                                        {
                                                            for (y = bufor[1, z]; y < bufor[1, i]; y++)
                                                            {
                                                                if (x > 0 && y > 0)
                                                                {
                                                                    Color pixelColor = image1.GetPixel(x, y);
                                                                    if (pixelColor.R == 0)
                                                                    {
                                                                        sprawdzenie1 = 1;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                    if (bufor[0, i] > bufor[0, z] && bufor[1, i] < bufor[1, z])
                                                    {
                                                        for (x = bufor[0, z]; x < bufor[0, i]; x++)
                                                        {
                                                            for (y = bufor[1, z]; y > bufor[1, i]; y--)
                                                            {
                                                                if (x > 0 && y > 0)
                                                                {
                                                                    Color pixelColor = image1.GetPixel(x, y);
                                                                    if (pixelColor.R == 0)
                                                                    {
                                                                        sprawdzenie1 = 1;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                    if (sprawdzenie1 == 0)
                                                    {
                                                        using (var graphics = Graphics.FromImage(image1))
                                                        {
                                                            //if (bufor[0, i] > bufor[0, z] && bufor[1, i] > bufor[1, z])
                                                            graphics.DrawLine(bluePen, bufor[0, z], bufor[1, z], bufor[0, i], bufor[1, i]);

                                                            //if (bufor[0, i] < bufor[0, z] && bufor[1, i] < bufor[1, z])
                                                            //graphics.DrawLine(bluePen, bufor[0, i], bufor[1, i], bufor[0, z], bufor[1, z]);

                                                            //graphics.DrawLine(bluePen, bufor[0, i], bufor[1, z], bufor[0, i], bufor[1, z]);
                                                            //graphics.DrawLine(bluePen, bufor[0, i], bufor[1, i], bufor[0, z], bufor[1, z]);
                                                            pictureBox1.Image = image1;
                                                            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                                                        }
                                                    }
                                                }
                                            }

                                        }

                                    }

                }
            }

                // }











                ////Poszukiwanie drogi i rysowanie jej
                //int k = 0;

                //szukanie(bufor[0, MAX], bufor[1, MAX]);
                // Display in Label1.
                label1.Text = "Zakończono rysowanie";
                //pictureBox1.Image = image1;
                //pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

}
        

        public int dlugosc(int x_pocz, int y_pocz,int x_koniec,int y_koniec) // 
        {
            
            float odl_x = Math.Abs(x_pocz - x_koniec);
            float odl_y = Math.Abs(y_pocz - y_koniec);
            int odl = (int)Math.Sqrt(odl_x * odl_x + odl_y * odl_y);
            return odl;
        }


        /* szukanie(int x_pocz, int y_pocz) // 
        {
            int punkt_x = 0, punkt_y = 0;

            
        }*/

    }  
}
