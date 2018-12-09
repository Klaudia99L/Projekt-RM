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
        Pen bluePen = new Pen(Color.Blue, 10);
        Color czerwony = Color.Red; // ustalenie czerwonego koloru
        Color czarny = Color.Black; // ustalenie czarnego koloru
        Color zielony = Color.Green; // ustalenie czarnego koloru
        private Button button2;

        // Bitmap image1;
        Bitmap image1 = new Bitmap(@"C:\WORLDMAP\worldmap.png", true);


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
            this.button2.Click += new System.EventHandler(this.button2_Click);
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
  

            int i=1;
            int[,] bufor = new int[2,5000];
            ///Losowanie 5000 dowolnych punktów
            /// 
             Random rnd = new Random();

             while (i<=((bufor.Length/2)-2))
             {
                 x = rnd.Next(1, 3592);
                 y = rnd.Next(1, 2416);

                Color pixelColor = image1.GetPixel(x, y); //Pobranie koloru piksela     
               
                if (pixelColor.R!=0) //Wybieranie tych, które znajdują się na wodzie (nie są w kolorze czarnym)
                 {
                     bufor[0, i] = x;
                     bufor[1, i] = y;
                    
                    SolidBrush myBrush = new SolidBrush(Color.Black); //rysowanie czarnych punktów dookoła wybranych pikseli
                    g.FillEllipse(myBrush, new Rectangle(x, y, 10, 10));
                }
                i++;
             }

            //Narysowanie punktu startowego i końcowego (Tokio-Londyn)   
            SolidBrush myBrush1 = new SolidBrush(Color.Green); 
            g.FillEllipse(myBrush1, new Rectangle(3049, 775, 20, 20));

            SolidBrush myBrush2 = new SolidBrush(Color.Red);
            g.FillEllipse(myBrush2, new Rectangle(1775, 561, 20, 20));     

            //Set the PictureBox to display the image.
            pictureBox1.Image = image1;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            // Display in Label1.
            label1.Text = "Zakończono rysowanie";
         
        }

        private void button2_Click(object sender, EventArgs e) ///Wykonanie algorytmu szukającego drogi
        {











        }
    }  
}
