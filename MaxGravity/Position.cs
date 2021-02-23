using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxGravity
{
    class Position
    {
        public Position (double x, double y)
        {
            this.x = x;
            this.y = y;
        }
        public Position(ref Position p)
        {
            this.x = p.x;
            this.y = p.y;
        }
        public double x
        {
            get;
            set;
        }
        public double y
        {
            get;
            set;
        }
    }
}
