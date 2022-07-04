using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Практика_Город
{
    public partial class Form1 : Form
    {
        public bool pressA;
        public bool pressD;
        public bool pressS;
        public bool pressW;
        public int kadr;
        public Bitmap screen;
        public Graphics screenG;
        public Car PlayerCar , tree, roof, leftline,green,red,yellow,whi;
        double[,] leftlines = new double[1000, 2];
        public double penalty = 0,vertic=0,horizon=0;
        public bool flag = false;
        public int startx, starty;
       
       
        public Form1()
        {
            InitializeComponent();
            pressA = false;
            pressD = false;
            pressS = false;
            pressW = false;
            kadr = 0;
            screen = new Bitmap(10000, 10000);
            screenG = Graphics.FromImage(screen);
           
            PlayerCar = new Car(250, 400, Pics.textureCar1);
            tree = new Car(100, 50, Pics.Tree);
            roof = new Car(100, 90, Pics.Roof);
            leftline = new Car(175, 70, Pics.LeftLine);
            green=new Car(190,50,Pics.Green);
            yellow = new Car(190, 50, Pics.Yellow);
            red = new Car(190, 50, Pics.Red);
            whi = new Car(0, 0, Pics.Whi);

            pictureBox1.Image = screen;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {                     
            this.Text = kadr.ToString();
            kadr++;
            pictureBox1.Invalidate();
            
            screenG.DrawImage(PlayerCar.pic, PlayerCar.x, PlayerCar.y);
            screenG.DrawImage(tree.pic, tree.x, tree.y);
            screenG.DrawImage(roof.pic, roof.x, roof.y);
            screenG.DrawImage(leftline.pic, leftline.x, leftline.y);
            screenG.DrawImage(green.pic, green.x, green.y);
            screenG.DrawImage(yellow.pic, yellow.x, yellow.y);
            screenG.DrawImage(red.pic, red.x, red.y);

            
            if (pressA) 
            {               
                PlayerCar.x -= 1;
                for (int i = 0; i <vertic ; i++)
                {      
                    for (int j = 0; j < 31; j++)
                    {
                        if (leftlines[i, 0] == PlayerCar.x && leftlines[i, 1] == PlayerCar.y+j)
                        {
                            penalty++;
                            MessageBox.Show("Штраф 100 рублей за пересечении сплошной полосы");                           
                        }
                    }
                }
                if (flag == false && PlayerCar.x == 175 && PlayerCar.y >= 70 && PlayerCar.y <= 100)
                {
                    penalty++;
                    MessageBox.Show("Штраф 100 рублей за пересечении сплошной полосы");
                }
            }          
            if (pressD) PlayerCar.x += 1;
            if (pressW) PlayerCar.y -= 1;
            if (pressS) PlayerCar.y += 1;        
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
           
            if (e.KeyCode == Keys.A) pressA = true;
            if (e.KeyCode == Keys.D) pressD = true;
            if (e.KeyCode == Keys.S) pressS = true;
            if (e.KeyCode == Keys.W) pressW = true;
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A) pressA = false;
            if (e.KeyCode == Keys.D) pressD = false;
            if (e.KeyCode == Keys.S) pressS = false;
            if (e.KeyCode == Keys.W) pressW = false;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
           long  xx = 0, yy = 0;
            if(radioButton1.Checked)
            {
                tree = new Car(Cursor.Position.X, Cursor.Position.Y, Pics.Tree);
            }
            if (radioButton2.Checked)
            {
                roof = new Car(Cursor.Position.X, Cursor.Position.Y, Pics.Roof);
            }
            if (radioButton3.Checked)
            {
                
                leftline = new Car(Cursor.Position.X, Cursor.Position.Y, Pics.LeftLine);
                leftlines[xx, 0] = Cursor.Position.X;
                leftlines[xx, 1] = Cursor.Position.Y;
                vertic++;
                xx++;
            }
            if (radioButton4.Checked)
            {
                PlayerCar = new Car(Cursor.Position.X, Cursor.Position.Y, Pics.textureCar1);
            }

            if (radioButton5.Checked)
            {
                red = new Car(Cursor.Position.X, Cursor.Position.Y, Pics.Red);
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            radioButton1.Dispose();
            radioButton2.Dispose();
            radioButton3.Dispose();
            radioButton4.Dispose();
            radioButton5.Dispose();
            button1.Dispose();
            button2.Dispose();
            startx=PlayerCar.x;
            starty = PlayerCar.y;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            screenG.DrawImage(whi.pic, whi.x, whi.y);
            flag = true;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
           
        }

        private void Form1_DoubleClick(object sender, EventArgs e)
        {
           
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
        
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
        }

        private void radioButton5_Enter(object sender, EventArgs e)
        {
        
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show("Общая сумма штрафов составляет" + penalty*100);
            MessageBox.Show("Начальное местоположение машины x=" + PlayerCar.x + "y=" + PlayerCar.y);
            MessageBox.Show("Последнее местоположение машины x=" + PlayerCar.x + "y=" + PlayerCar.y);
        }

        private void pictureBox1_MouseCaptureChanged(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_Move(object sender, EventArgs e)
        {
            
        }

        public class Car
        {
            public int x;
            public int y;
            public Bitmap pic;
            public Car(int x, int y, Bitmap pic)
            {
                this.x = x;
                this.y = y;
                this.pic = pic;                  
            }
        }
        public static class Pics
        {
            public static Bitmap textureCar1 = (Bitmap)Image.FromFile(Directory.GetCurrentDirectory() + "//car1.png");
            public static Bitmap Tree = (Bitmap)Image.FromFile(Directory.GetCurrentDirectory() + "//tree.png");
            public static Bitmap Roof = (Bitmap)Image.FromFile(Directory.GetCurrentDirectory() + "//roof.png");
            public static Bitmap LeftLine = (Bitmap)Image.FromFile(Directory.GetCurrentDirectory() + "//leftline.jpg");
            public static Bitmap Green= (Bitmap)Image.FromFile(Directory.GetCurrentDirectory() + "//green.png");
            public static Bitmap Yellow = (Bitmap)Image.FromFile(Directory.GetCurrentDirectory() + "//yellow.png");
            public static Bitmap Red = (Bitmap)Image.FromFile(Directory.GetCurrentDirectory() + "//red.png");
            public static Bitmap Whi = (Bitmap)Image.FromFile(Directory.GetCurrentDirectory() + "//whi.jpg");

        }
        public static class Rand
        {
            public static Random rand = new Random();
            public static int GetInT(int min, int max)
            {
                return rand.Next(min, max);
            }
        }
    }
}
