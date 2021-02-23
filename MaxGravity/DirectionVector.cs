using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxGravity
{
    /// <summary>
    /// Class can be used to determine the speed of an object and calculate the force of gravity
    /// </summary>
    class DirectionVector
    {
        /// <summary>
        /// vector x,y direction
        /// </summary>
        public DirectionVector(double xVector, double yVector)
        {
            vectorPoint = new Position(xVector, yVector);
        }
        /// <summary>
        /// vector x = 0, y = 0
        /// </summary>
        public DirectionVector()
        {
            vectorPoint = new Position(0, 0);
        }

        /// <summary>
        /// vector x,y direction
        /// </summary>
        public Position vectorPoint
        {
            get;
            set;
        }
    }
}
