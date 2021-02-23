using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MaxGravity
{
    public partial class Form1 : Form
    {
        GraphicsEngine engine;
        public Form1()
        {
            InitializeComponent();
            engine = new GraphicsEngine(panel2);

            engine.AddSpaceObject(new SpaceObject("Sun2", new Position(1350, 250),     2000000,   12, Color.YellowGreen,     new DirectionVector(), true));
            engine.AddSpaceObject(new SpaceObject("Sun",     new Position(350, 250),  20000000,   15, Color.YellowGreen,     new DirectionVector(), true));
            //engine.AddSpaceObject(new SpaceObject("Mercury", new Position(350, 280),     10000,    2, Color.White,           new DirectionVector(2.3f, 0)));
            engine.AddSpaceObject(new SpaceObject("Venus",   new Position(350, 340),     81000,    6, Color.White,           new DirectionVector(1.1f, 0)));
            engine.AddSpaceObject(new SpaceObject("VenMoon", new Position(350, 330),      1000,    1, Color.White,           new DirectionVector(1.33f, 0)));
            //engine.AddSpaceObject(new SpaceObject("VenMoon", new Position(350, 325),      2000,    2, Color.Yellow,          new DirectionVector(1.35f, 0.1f)));

            engine.AddSpaceObject(new SpaceObject("Earth",   new Position(350, 450),     70000,    5, Color.Blue   ,         new DirectionVector(0.7f, 0)));
            engine.AddSpaceObject(new SpaceObject("Moon",    new Position(350, 440),      1000,    1, Color.White  ,         new DirectionVector(0.9f, 0)));


            engine.CalculateForceOfGravity();
            label2.Text = panel2.Width + "/" + panel2.Height;
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
        }

        private void panel2_Resize(object sender, EventArgs e)
        {
            engine.RefreshSize(panel2);
            label2.Text = panel2.Width + "/" + panel2.Height;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            engine.Destroy();
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            engine.AnimationStart();
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            engine.AnimationStop();
        }
    }
}
