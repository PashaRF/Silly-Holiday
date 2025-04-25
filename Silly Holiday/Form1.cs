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

namespace Silly_Holiday
{
    public partial class Form1 : Form
    {
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

        int xRocket = 52;
        int yRocket = 330;
        int hRocket = 30;
        int wRocket = 30;

        int wRocketBody = 60;
        int hRocketBody = 120;
        int SideRocketBody = 42;

        Graphics g;
        Pen blackPen = new Pen(Color.Black, 10);
        Pen grayPen = new Pen(Color.Gray, 10);
        Pen redPen = new Pen(Color.DarkOrange, 10);
        Pen defaultPen = new Pen(Color.FromArgb(30, 30, 30), 10);
        Pen darkGrayPen = new Pen(Color.FromArgb(90, 90, 90), 10);

        Random randGen = new Random();
        int randValue;
        double randValueD;

        SolidBrush grayBrush = new SolidBrush(Color.Gray);
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        SolidBrush redBrush = new SolidBrush(Color.DarkOrange);
        SolidBrush defaultBrush = new SolidBrush(Color.FromArgb(30, 30, 30));
        SolidBrush darkGrayBrush = new SolidBrush(Color.FromArgb(90, 90, 90));
        public Form1()
        {
            InitializeComponent();
            avgRadians = (Math.PI * (sideNum - 2)) / (sideNum);
            g = this.CreateGraphics();
        }
        private void Form1_Shown(object sender, EventArgs e)
        {
           // this.Size = new System.Drawing.Size(500, 600);
           
          //while (angle != 20)
          for (int i = 0; i < 1; i++) 
           {
                Stars();
                g.DrawRectangle(darkGrayPen, xRocket - 15, yRocket - 80, wRocketBody, hRocketBody);
                g.FillRectangle(darkGrayBrush, xRocket - 15, yRocket - 80, wRocketBody, hRocketBody);
                g.DrawEllipse(darkGrayPen, xRocket - 15, yRocket - (wRocketBody / 2 ), wRocketBody, wRocketBody);
                g.FillEllipse(darkGrayBrush, xRocket - 15, yRocket - (wRocketBody / 2 ), wRocketBody, wRocketBody);
                g.DrawEllipse(redPen, xRocket, yRocket, wRocket, hRocket);
                g.FillEllipse(redBrush, xRocket, yRocket, wRocket, hRocket);
                Thread.Sleep(200);
                Rocket();
                this.Close();
            }
        }
        void Stars()
        {
           // g.DrawRectangle(grayPen, 0, 0, 800, 700);
           // g.FillRectangle(grayBrush, 0, 0, 800, 700);
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
            for (xRocket = 52; xRocket < 250; xRocket= xRocket + 2)
            {
                g.TranslateTransform(xRocket - 15, yRocket -80);
                g.DrawEllipse(darkGrayPen, xRocket - 15, yRocket - 80,wRocketBody, wRocketBody);
                g.FillEllipse(darkGrayBrush, xRocket - 15, yRocket - 80, wRocketBody, wRocketBody);
                g.DrawRectangle(darkGrayPen, xRocket - 15, yRocket - 80, wRocketBody, hRocketBody);
                g.FillRectangle(darkGrayBrush, xRocket - 15, yRocket - 80, wRocketBody, hRocketBody);
                g.DrawEllipse(redPen, xRocket, yRocket, wRocket, hRocket);
                g.FillEllipse(redBrush, xRocket, yRocket, wRocket, hRocket);
                Thread.Sleep(20);
                g.DrawRectangle(defaultPen, xRocket - 15, yRocket - 80, wRocketBody, hRocketBody);
                g.FillRectangle(defaultBrush, xRocket - 15, yRocket - 80, wRocketBody, hRocketBody);

                test =  Math.Pow((xRocket - 120),2)* .014 + 150;
                yRocket = Convert.ToInt32(test);
            }
            g.DrawRectangle(darkGrayPen, xRocket - 15, yRocket - 80, wRocketBody, hRocketBody);
            g.FillRectangle(darkGrayBrush, xRocket - 15, yRocket - 80, wRocketBody, hRocketBody);
            g.DrawEllipse(redPen, xRocket, yRocket, wRocket, hRocket);
            g.FillEllipse(redBrush, xRocket, yRocket, wRocket, hRocket);
            Thread.Sleep(3000);
        }
    }
}
