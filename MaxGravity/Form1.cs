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

            InitialValues();

            engine.CalculateForceOfGravity();
            label2.Text = panel2.Width + "/" + panel2.Height;
        }

        private void InitialValues()
        {
            engine.ClearListObject();
            engine.AddSpaceObject(new SpaceObject("Sun", new Position(350, 250), 20000000, 15, Color.YellowGreen, new DirectionVector(), true));

            engine.AddSpaceObject(new SpaceObject("Mercury", new Position(350, 270),      900,    1, Color.White,           new DirectionVector(2.3f, 0)));

            engine.AddSpaceObject(new SpaceObject("Venus",   new Position(350, 340),  70000, 4, Color.White, new DirectionVector(1.1f, 0)));
            engine.AddSpaceObject(new SpaceObject("VenMoon", new Position(350, 330),   1000, 1, Color.LawnGreen, new DirectionVector(1.315f, 0.0f)));

            engine.AddSpaceObject(new SpaceObject("Earth", new Position(350, 450), 70000, 5, Color.Blue, new DirectionVector(0.7f, 0)));
            engine.AddSpaceObject(new SpaceObject("Moon",  new Position(350, 440),  1000, 1, Color.White, new DirectionVector(0.9f, 0)));

            //engine.AddSpaceObject(new SpaceObject("X1",     new Position(350, 540), 80000, 5, Color.White, new DirectionVector(0.585f, 0)));

            engine.AddSpaceObject(new SpaceObject("Jupiter",   new Position(350, 650), 700000, 9, Color.ForestGreen, new DirectionVector(0.5f, 0)));
            engine.AddSpaceObject(new SpaceObject("Io",        new Position(350, 638),   1000, 1, Color.WhiteSmoke,  new DirectionVector(1.02f, 0)));
            engine.AddSpaceObject(new SpaceObject("Europa",     new Position(350, 619),    100, 2, Color.FloralWhite, new DirectionVector(0.845f, 0)));
            engine.AddSpaceObject(new SpaceObject("Ganimedes",  new Position(350, 609),     50, 1, Color.FloralWhite, new DirectionVector(0.8f, 0)));
            engine.AddSpaceObject(new SpaceObject("Kallisto",   new Position(350, 602),      1, 1, Color.FloralWhite, new DirectionVector(0.785f, 0)));

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

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitialValues();
            engine.Clear();
        }

        private void doNotCleanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            doNotCleanToolStripMenuItem.Checked = !doNotCleanToolStripMenuItem.Checked;
            engine.ClearBackground = !doNotCleanToolStripMenuItem.Checked;
            engine.Clear();
            if (!engine.ClearBackground)
            {
                showObjectNamesToolStripMenuItem.Checked = false;
            }
        }

        private void showObjectNamesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            engine.ShowObjectNames = !engine.ShowObjectNames;
            showObjectNamesToolStripMenuItem.Checked = engine.ShowObjectNames;
            if (engine.ShowObjectNames)
            {
                doNotCleanToolStripMenuItem.Checked = false;
            }
        }
    }
}
