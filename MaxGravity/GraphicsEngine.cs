using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing;

namespace MaxGravity
{
    class GraphicsEngine
    {
        private Graphics handleWindows;
        private Font defaultFont = new Font("Candara", 7, FontStyle.Regular);
        SpaceObjects spaceObjects;

        private Thread thread;
        private bool startThread = false;
        private bool startAnimation = false;

        public int width
        {
            get;
            private set;
        }

        public int height
        {
            get;
            private set;
        }

        public GraphicsEngine(System.Windows.Forms.Panel handleBlackPanel)
        {
            spaceObjects = new SpaceObjects();
            thread = new Thread(DrawingAnimation);
            handleWindows = handleBlackPanel.CreateGraphics();
            width = handleBlackPanel.Width;
            height = handleBlackPanel.Height;
        }

        ~GraphicsEngine()
        {
            if (thread.IsAlive)
            {
                thread.Abort();
            }
        }

        public void RefreshSize(System.Windows.Forms.Panel handleBlackPanel)
        {
            width = handleBlackPanel.Width;
            height = handleBlackPanel.Height;
            handleWindows = handleBlackPanel.CreateGraphics();
        }

        public void DrawPoint(Color color, float x, float y)
        {
            handleWindows.DrawLine(new Pen(color), x, y, x + 0.5f, y + 0.5f);
        }

        public void DrawLine(Color color, float x, float y, float x2, float y2)
        {
            handleWindows.DrawLine(new Pen(color), x, y, x2, y2);
        }

        public void DrawCircle(Color color, float x, float y, float r)
        {
            handleWindows.FillEllipse(new SolidBrush(color), x - r, y - r, r * 2, r * 2);
        }

        public void DrawText(Color color, string text, float x, float y)
        {
            handleWindows.DrawString(text, defaultFont, new SolidBrush(color), x, y);
        }

        public void DrawFrame()
        {
            foreach(SpaceObject iter in spaceObjects.GetList())
            {
                DrawCircle(iter.c, iter.p.x, iter.p.y, iter.r);

                //drawing velocity vector (zoom*6)
                DrawLine(iter.c, iter.p.x, iter.p.y, iter.p.x + 6 * iter.v.vectorPoint.x, iter.p.y + 6 * iter.v.vectorPoint.y);

                //drawing name object
                DrawText(iter.c, iter.name, iter.p.x + 3, iter.p.y - iter.r - 8);
            }
        }

        public void Clear()
        {
            handleWindows.Clear(Color.Black);
        }

        public void DrawingAnimation()
        {
            for (; ; )
            {
                if (startAnimation)
                {
                    // ------------------------  my animation loop -----------------------
                    DrawFrame();
                }
                Thread.Sleep(10);
            }
        }

        public void AnimationStart()
        {
            if (!startThread)
            {
                thread.Start();
                startThread = true;
            }
            startAnimation = true;
        }

        public void AnimationStop()
        {
            startAnimation = false;
            Thread.Sleep(200);
        }

        public void Destroy()
        {
            if(thread.IsAlive)
            {
                thread.Abort();
            }
        }

        public void AddSpaceObject(SpaceObject o)
        {
            spaceObjects.AddObject(o);
        }

        public List<SpaceObject> GetListSpaceObjects()
        {
            return spaceObjects.GetList();
        }
    }
}
