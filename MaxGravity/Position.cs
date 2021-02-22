using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxGravity
{
    class Position
    {
        public Position (float x, float y)
        {
            this.x = x;
            this.y = y;
        }
        public Position(ref Position p)
        {
            this.x = p.x;
            this.y = p.y;
        }
        public float x
        {
            get;
            set;
        }
        public float y
        {
            get;
            set;
        }
    }
}
