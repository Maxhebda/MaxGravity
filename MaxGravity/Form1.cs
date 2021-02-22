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

            engine.AddSpaceObject(new SpaceObject("Sun",   new Position(150, 150), 1000, 15, Color.Yellow, new DirectionVector(), true));
            engine.AddSpaceObject(new SpaceObject("Earth", new Position(150, 100),   20,  5, Color.Blue  , new DirectionVector(4,0)));
            engine.AddSpaceObject(new SpaceObject("Mars",  new Position(230, 150),   10,  4, Color.Red   , new DirectionVector(1, 5)));
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
