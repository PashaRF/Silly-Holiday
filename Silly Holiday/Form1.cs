using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;
using System.Media;

namespace Silly_Holiday
{
    public partial class Form1 : Form
    {
        #region Variables
        int startx = 50;
        int starty = 50;
        int length = 20;
        int sideNum = 5;
        double angle = 0;
        double avgRadians;
        int endx;
        int endy;

        int xGridMin = 30;
        int xGridMax = 60;
        int yGridMin = 30;
        int yGridMax = 60;

        double test;

        Random randGen = new Random();
        int randValue;
        double randValueD;
        #endregion
        #region Pens
        Graphics g;
        Pen blackPen = new Pen(Color.Black, 10);
        Pen grayPen = new Pen(Color.Gray, 10);
        Pen redPen = new Pen(Color.DarkOrange, 10);
        Pen oceanPen = new Pen(Color.Blue, 10);
        Pen landPen = new Pen(Color.DarkOliveGreen, 10);
        Pen defaultPen = new Pen(Color.FromArgb(30, 30, 30), 10);
        Pen smokePen = new Pen(Color.FromArgb(120, 120, 120), 10);
        Pen darkGrayPen = new Pen(Color.FromArgb(90, 90, 90), 10);

        SolidBrush grayBrush = new SolidBrush(Color.Gray);
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        SolidBrush redBrush = new SolidBrush(Color.DarkOrange);
        SolidBrush oceanBrush = new SolidBrush(Color.Blue);
        SolidBrush landBrush = new SolidBrush(Color.DarkOliveGreen);
        SolidBrush defaultBrush = new SolidBrush(Color.FromArgb(30, 30, 30));
        SolidBrush smokeBrush = new SolidBrush(Color.FromArgb(120, 120, 120));
        SolidBrush darkGrayBrush = new SolidBrush(Color.FromArgb(90, 90, 90));

        Font drawFont = new Font("Arial", 35, FontStyle.Bold);
        SolidBrush drawBrush = new SolidBrush(Color.DarkOrange);
        #endregion
        public Form1()
        {
            InitializeComponent();
            avgRadians = (Math.PI * (sideNum - 2)) / (sideNum);
            g = this.CreateGraphics();
        }
        private void Form1_Shown(object sender, EventArgs e)
        {
            SoundPlayer spaceMusic = new SoundPlayer(Properties.Resources.spaceMusic);
            spaceMusic.Play();

            // earth drawn 
            g.DrawEllipse(oceanPen, 70, 30, 220, 220);
            g.FillEllipse(oceanBrush, 70, 30, 220, 220);

            g.DrawEllipse(landPen, 100, 70, 30, 30);
            g.FillEllipse(landBrush, 100, 70, 30, 30);
            g.DrawEllipse(landPen, 100, 70, 30, 30);
            g.FillEllipse(landBrush, 200, 140, 70, 70);
            g.DrawEllipse(landPen, 120, 80, 70, 30);
            g.FillEllipse(landBrush, 120, 80, 70, 30);
            g.DrawEllipse(landPen, 120, 90, 50, 70);
            g.FillEllipse(landBrush, 120, 90, 50, 70);
            g.DrawEllipse(landPen, 120, 90, 50, 70);
            g.FillEllipse(landBrush, 120, 170, 50, 70);
            g.DrawEllipse(landPen, 170, 60, 50, 40);
            g.FillEllipse(landBrush, 170, 60, 50, 40);
            g.DrawEllipse(landPen, 150, 80, 50, 50);
            g.FillEllipse(landBrush, 150, 80, 50, 50);

            g.DrawEllipse(landPen, 220, 90, 20, 20);
            g.FillEllipse(landBrush, 220, 90, 20, 20);
            g.DrawEllipse(landPen, 80, 120, 20, 20);
            g.FillEllipse(landBrush, 80, 120, 20, 20);
            g.DrawEllipse(landPen, 200, 220, 20, 20);
            g.FillEllipse(landBrush, 200, 220, 20, 20);
        }
        void Stars()
        {
            // stars are drawn randomly according to grid 
            for (int i = 1; i <= 5; i++)
            {
                for (int k = 1; k <= 4; k++)
                {
                    randValue = randGen.Next(xGridMin, xGridMax);
                    startx = randValue;
                    randValue = randGen.Next(yGridMin, yGridMin);
                    starty = randValue;
                    randValue = randGen.Next(10, 32);
                    length = randValue;
                    randValue = randGen.Next(0, (int)(Math.PI * 2));
                    randGen.NextDouble();
                    randValueD = randGen.NextDouble() * Math.PI;
                    angle = randValue;
                    avgRadians = (Math.PI * (sideNum - 2)) / (sideNum);

                    Pen whitePen = new Pen(Color.White, 10);

                    PointF[] points = new PointF[sideNum];

                    for (int j = 1; j <= sideNum; j++)
                    {

                        endx = Convert.ToInt32(startx + length * (Math.Cos(angle * 2)));
                        endy = Convert.ToInt32(starty + length * (Math.Sin(angle * 2)));
                        points[j - 1] = new PointF(endx, endy);

                        startx = endx;
                        starty = endy;
                        angle = angle + avgRadians;
                    }
                    g.DrawPolygon(whitePen, points);
                    g.FillPolygon(whiteBrush, points);
                    xGridMax += 80;
                    xGridMin += 80;
                }
                yGridMin += 95;
                yGridMax += 95;
                xGridMax = 60;
                xGridMin = 30;
            }
            yGridMin = 30;
            yGridMax = 60;
            xGridMax = 60;
            xGridMin = 30;
            g.DrawEllipse(grayPen, 80, 300, 880, 880);
            g.FillEllipse(grayBrush, 80, 300, 880, 880);
        }
        void Rocket()
        {
            //rocket blasts across sky in parabola
            int xRocket = 52;
            int yRocket = 330;
            int hRocket = 30;
            int wRocket = 30;

            int wRocketBody = 60;
            int hRocketBody = 120;
            int xRocketBody = xRocket - 15;
            int yRocketBody = yRocket - 80;

            int yRoof = yRocketBody - (wRocketBody / 2);
            int xRoof = xRocket -16;
            int lFlame = 40;
            int wFlame = wRocketBody - 20;
            int xFlame = xRocket - 5;

            SoundPlayer launchSound = new SoundPlayer(Properties.Resources.rocket_launch_306441);
            launchSound.Play();

            g.DrawRectangle(darkGrayPen, xRocketBody, yRocketBody, wRocketBody, hRocketBody);
            g.FillRectangle(darkGrayBrush, xRocketBody, yRocketBody, wRocketBody, hRocketBody);

            g.DrawEllipse(darkGrayPen, xRoof, yRoof, wRocketBody, wRocketBody);
            g.FillEllipse(darkGrayBrush, xRoof, yRoof, wRocketBody, wRocketBody);

            g.DrawEllipse(redPen, xRocket, yRocket, wRocket, hRocket);
            g.FillEllipse(redBrush, xRocket, yRocket, wRocket, hRocket);
            Thread.Sleep(200);

            for (xRocket = 52; xRocket < 250; xRocket = xRocket + 2)
            {
              if (lFlame < 120)
                 {
                    lFlame = lFlame + 20;
                 }
                 else
                  {
                    lFlame = 40;
                  }
                    g.DrawEllipse(redPen, xFlame, yRocket, wFlame, lFlame);
                    g.FillEllipse(redBrush, xFlame, yRocket, wFlame, lFlame);

                    g.DrawEllipse(darkGrayPen, xRoof, yRoof, wRocketBody, wRocketBody);
                    g.FillEllipse(darkGrayBrush, xRoof, yRoof, wRocketBody, wRocketBody);

                    g.DrawEllipse(darkGrayPen, xRocketBody, yRocketBody, wRocketBody, wRocketBody);
                    g.DrawEllipse(darkGrayPen, xRocketBody, yRocketBody, wRocketBody, wRocketBody);
                    g.FillEllipse(darkGrayBrush, xRocketBody, yRocketBody, wRocketBody, wRocketBody);
                    g.DrawRectangle(darkGrayPen, xRocketBody, yRocketBody, wRocketBody, hRocketBody);
                    g.FillRectangle(darkGrayBrush, xRocketBody, yRocketBody, wRocketBody, hRocketBody);
                    g.DrawEllipse(redPen, xRocket, yRocket, wRocket, hRocket);
                    g.FillEllipse(redBrush, xRocket, yRocket, wRocket, hRocket);

                    Thread.Sleep(20);
                    g.DrawRectangle(defaultPen, xRocketBody, yRocketBody, wRocketBody, hRocketBody);
                    g.FillRectangle(defaultBrush, xRocketBody, yRocketBody, wRocketBody, hRocketBody);
                    g.DrawEllipse(defaultPen, xRoof, yRoof, wRocketBody, wRocketBody);
                    g.FillEllipse(defaultBrush, xRoof, yRoof, wRocketBody, wRocketBody);
                    g.DrawEllipse(defaultPen, xFlame, yRocket, wFlame, lFlame);
                    g.FillEllipse(defaultBrush, xFlame, yRocket, wFlame, lFlame);

                g.DrawEllipse(grayPen, 80, 300, 880, 880);
                g.FillEllipse(grayBrush, 80, 300, 880, 880);

                test = Math.Pow((xRocket - 120), 2) * .014 + 150;
                    yRocket = Convert.ToInt32(test);

                xRocketBody = xRocket - 15;
                yRocketBody = yRocket - 80;
                yRoof = yRocketBody - (wRocketBody / 2);
                xRoof = xRocket - 16;
                xFlame = xRocket - 5;
            }
            g.DrawEllipse(grayPen, 80, 300, 880, 880);
            g.FillEllipse(grayBrush, 80, 300, 880, 880);
            g.DrawRectangle(darkGrayPen, xRocketBody, yRocketBody, wRocketBody, hRocketBody);
            g.FillRectangle(darkGrayBrush, xRocketBody, yRocketBody, wRocketBody, hRocketBody);
            g.DrawEllipse(darkGrayPen, xRoof, yRoof, wRocketBody, wRocketBody);
            g.FillEllipse(darkGrayBrush, xRoof, yRoof, wRocketBody, wRocketBody);
            g.DrawEllipse(redPen, xRocket, yRocket, wRocket, hRocket);
            g.FillEllipse(redBrush, xRocket, yRocket, wRocket, hRocket);
            g.DrawString("You're out \n of this \n world!", drawFont, drawBrush, 50, 60);
        }

        private void cardButton_Click(object sender, EventArgs e)
        {
            // the earth + button disappears
            g.DrawEllipse(defaultPen, 70, 30, 220, 220);
            g.FillEllipse(defaultBrush, 70, 30, 220, 220);
            cardButton.Hide();
            for (int i = 0; i < 1; i++)
            {
                Stars();
                Rocket();
                SoundPlayer successMusic = new SoundPlayer(Properties.Resources.successMusic);
                successMusic.Play();
            }
        }
    }
}