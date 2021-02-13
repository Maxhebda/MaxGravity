using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
namespace MaxGravity
{
    public partial class Form1 : Form
    {
        Graphics graphics;
        Thread th;
        bool StartSimulation = false;
        int mainFrame = 4;
        int mainWidth = 500;
        int mainHight = 500;
        List<objectData> listOfObjects;

        private struct speedVector
        {
            public speedVector(float x, float y)
            {
                this.x = x;
                this.y = y;
            }
            public float x;
            public float y;
        }
        private struct objectData
        {
            public objectData(string name, float x, float y, speedVector v, bool blocked, float r, float m, Color color)
            {
                this.name = name;
                this.x = x;
                this.y = y;
                this.v = v;
                this.blocked = blocked;
                this.r = r;
                this.m = m;
                this.color = color;
            }
            public string name;
            public float x;
            public float y;
            public speedVector v;
            public bool blocked;
            public float r;
            public float m;
            public Color color;
        }

        public Form1()
        {
            InitializeComponent();
            th = new Thread(CalculatePosition);
            th.IsBackground = true;
            th.Start();

            listOfObjects = new List<objectData>();
            graphics = CreateGraphics();
            this.SetClientSizeCore(mainWidth + button1.Width + mainFrame * 2 + 5, mainHight + mainFrame * 2);


            button1.Left = mainWidth + mainFrame + 5;
            button1.Top = mainFrame + 2;
            button2.Left = mainWidth + mainFrame + 5;
            button2.Top = mainFrame + 30;
        }
        private void AddObject(string name, float x, float y, speedVector v, bool blocked, float r, float m, Color color)
        {
            objectData temp = new objectData();
            temp.name = name;
            temp.x = x;
            temp.y = y;
            temp.v = v;
            temp.blocked = blocked;
            temp.r = r;
            temp.m = m;
            temp.color = color;
            listOfObjects.Add(temp);
        }
        private void ClearBlack()
        {
            graphics.FillRectangle(Brushes.Black, mainFrame + 1, mainFrame + 1, mainWidth, mainHight);
        }
        private void ShowObjects()
        {
            ClearBlack();
            foreach (objectData i in listOfObjects)
            {
                graphics.FillEllipse(new SolidBrush(i.color), i.x - i.r, i.y - i.r, 2 * i.r, 2 * i.r);
                Console.WriteLine("Rysowanie - " + i.name);
            }
        }
        private void CalculatePosition()
        {
            for (; ; )
            {
                if (StartSimulation)
                {
                    for (var i = 0; i < listOfObjects.Count; i++)
                    {
                        listOfObjects[i] = new objectData(
                            listOfObjects[i].name,
                            listOfObjects[i].x + listOfObjects[i].v.x,
                            listOfObjects[i].y + listOfObjects[i].v.y,
                            listOfObjects[i].v,
                            listOfObjects[i].blocked,
                            listOfObjects[i].r,
                            listOfObjects[i].m,
                            listOfObjects[i].color);
                    }

                    //------------- show -------------
                    ShowObjects();
                }
                Thread.Sleep(20);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
             StartSimulation = true;
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            bool restart = false;
            if (StartSimulation)
            {
                StartSimulation = false;
                restart = true;
            }
            graphics.DrawRectangle(Pens.Gray, mainFrame, mainFrame, mainWidth + 1, mainHight + 1);
            ClearBlack();
            ShowObjects();
            if (restart)
            {
                StartSimulation = true;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (th.IsAlive)
            {
                th.Abort();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //-----------------------------------
            AddObject("Słońce", 300, 170, new speedVector(0, 0), true, 8, 3000, Color.Yellow);
            AddObject("Ziemia", 250, 150, new speedVector(2, 0), false, 3, 3, Color.Blue);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StartSimulation = false;
        }
    }
}
