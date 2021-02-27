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
        private Font defaultFont = new Font("Candara", 8, FontStyle.Regular);
        private bool clearBackgroundOnce = false;

        private double constantOfGravity = 0.0000000016f;

        SpaceObjects spaceObjects;

        private Thread thread;
        private bool startThread = false;
        private bool startAnimation = false;

        private bool _clearBackground = true;
        /// <summary>
        /// Cleaning before drawing
        /// true  = The animations (orbits) will be cleared.
        /// false = Objects will move without clearing the old position.
        /// </summary>
        public bool ClearBackground
        {
            get => _clearBackground;
            set
            {
                if (!value)     //if it doesn't clear the screen then it doesn't display the names of the objects
                {
                    _showObjectNames = false;
                }
                _clearBackground = value;
            }
        }

        private bool _showObjectNames = false;
        /// <summary>
        /// Show object names
        /// true  = Show names and disable clearing the old position
        /// false = Not show names
        /// </summary>
        public bool ShowObjectNames
        {
            get => _showObjectNames;
            set
            {
                if (value)
                {
                    _clearBackground = true;
                }
                _showObjectNames = value;
            }
        }

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
            thread.Priority = ThreadPriority.Highest;
            handleWindows = handleBlackPanel.CreateGraphics();
            width = handleBlackPanel.Width;
            height = handleBlackPanel.Height;
            _clearBackground = true;
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

        public void DrawPoint(Color color, double x, double y)
        {
            float xx = (float)x;
            float yy = (float)y;
            handleWindows.DrawLine(new Pen(color), xx, yy, xx + 0.5f, yy + 0.5f);
        }

        public void DrawLine(Color color, double x, double y, double x2, double y2)
        {
            handleWindows.DrawLine(new Pen(color), (float)x, (float)y, (float)x2, (float)y2);
        }

        public void DrawCircle(Color color, double x, double y, double r)
        {
            float rr = (float)(r);
            float xx = (float)x - rr;
            float yy = (float)y - rr;
            float r2 = (float)(r * 2);
            handleWindows.FillEllipse(new SolidBrush(color), xx, yy, r2, r2);
        }

        public void DrawText(Color color, string text, double x, double y)
        {
            float xx = (float)x;
            float yy = (float)y;
            handleWindows.DrawString(text, defaultFont, new SolidBrush(color), xx, yy);
        }

        public void DrawFrame()
        {
            for (int iter = 0; iter < spaceObjects.count; iter++)
            {
                DrawCircle(spaceObjects.GetList()[iter].c, spaceObjects.GetList()[iter].p.x, spaceObjects.GetList()[iter].p.y, spaceObjects.GetList()[iter].r);

                //drawing velocity vector (zoom*6)
                //DrawLine(spaceObjects.GetList()[iter].c, spaceObjects.GetList()[iter].p.x, spaceObjects.GetList()[iter].p.y, spaceObjects.GetList()[iter].p.x + 6 * spaceObjects.GetList()[iter].v.vectorPoint.x, spaceObjects.GetList()[iter].p.y + 6 * spaceObjects.GetList()[iter].v.vectorPoint.y);

                //drawing name object
                if (_showObjectNames)
                {
                    DrawText(spaceObjects.GetList()[iter].c, spaceObjects.GetList()[iter].name, spaceObjects.GetList()[iter].p.x + 13, spaceObjects.GetList()[iter].p.y - spaceObjects.GetList()[iter].r - 8);
                }

                //drawing force vector (zoom*6)
                //DrawLine(spaceObjects.GetList()[iter].c, spaceObjects.GetList()[iter].p.x, spaceObjects.GetList()[iter].p.y, spaceObjects.GetList()[iter].p.x + 6 * spaceObjects.GetList()[iter].forceOfGravity.vectorPoint.x, spaceObjects.GetList()[iter].p.y + 6 * spaceObjects.GetList()[iter].forceOfGravity.vectorPoint.y);

                //drawing ID object
                //DrawText(spaceObjects.GetList()[iter].c, iter.ToString(), spaceObjects.GetList()[iter].p.x + 3, spaceObjects.GetList()[iter].p.y - spaceObjects.GetList()[iter].r - 8);

                //drawing force gravity vector (zoom*6)
                //DrawLine(spaceObjects.GetList()[iter].c, spaceObjects.GetList()[iter].p.x, spaceObjects.GetList()[iter].p.y, spaceObjects.GetList()[iter].p.x + 6 * spaceObjects.GetList()[iter].forceOfGravity.vectorPoint.x, spaceObjects.GetList()[iter].p.y + 6 * spaceObjects.GetList()[iter].forceOfGravity.vectorPoint.y);

            }
        }
        public void CalculateNewPosition()
        {
            for (int iter = 0; iter < spaceObjects.count; iter++)
            {
                if (!spaceObjects.GetList()[iter].blocked)
                {
                    spaceObjects.GetList()[iter].v.vectorPoint.x += spaceObjects.GetList()[iter].forceOfGravity.vectorPoint.x;
                    spaceObjects.GetList()[iter].v.vectorPoint.y += spaceObjects.GetList()[iter].forceOfGravity.vectorPoint.y;

                    spaceObjects.GetList()[iter].p.x += spaceObjects.GetList()[iter].v.vectorPoint.x;
                    spaceObjects.GetList()[iter].p.y += spaceObjects.GetList()[iter].v.vectorPoint.y;
                }
            }
        }
        public void CalculateForceOfGravity()
        {
            DirectionVector tempforceGravity;
            double force;
            double distanceX;
            double distanceY;
            double distance;
            double ratio;
            double acceleration;
            double mass1;
            double mass2;

            //everyone with everyone only once
            for (int iter1 = 0; iter1 < spaceObjects.count; iter1++)
            {
                tempforceGravity = new DirectionVector();
                mass1 = spaceObjects.GetList()[iter1].m;
                for (int iter2 = 0; iter2 < spaceObjects.count; iter2++)
                {
                    mass2 = spaceObjects.GetList()[iter2].m;
                    if (iter1 != iter2)
                    {
                        distanceX = spaceObjects.GetList()[iter2].p.x - spaceObjects.GetList()[iter1].p.x;
                        distanceY = spaceObjects.GetList()[iter2].p.y - spaceObjects.GetList()[iter1].p.y;
                        distance = Math.Sqrt((distanceX) * (distanceX) + (distanceY) * (distanceY));
                        force = constantOfGravity * (mass1 * mass2) / (distance * distance);
                        acceleration = force * 3000 / mass1;
                        ratio = acceleration / distance;
                        tempforceGravity.vectorPoint.x += ratio * distanceX;
                        tempforceGravity.vectorPoint.y += ratio * distanceY;
                    }
                }
                spaceObjects.setForceOfGravity(iter1, tempforceGravity);
            }
        }

        public void ClearListObject()
        {
            spaceObjects.Clear();
        }

        /// <summary>
        /// External method.allows you to clean the window
        /// </summary>
        public void Clear()
        {
            clearBackgroundOnce = true;
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

                CalculateForceOfGravity();
                CalculateNewPosition();
                Thread.Sleep(1);
                if (_clearBackground || clearBackgroundOnce)
                {
                    handleWindows.Clear(Color.Black);
                    if (clearBackgroundOnce)
                    {
                        clearBackgroundOnce = false;
                    }
                }
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
    }
}
