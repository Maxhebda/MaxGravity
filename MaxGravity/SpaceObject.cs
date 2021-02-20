using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MaxGravity
{
    /// <summary>
    /// Single object in the universe has name, x, y, mass, velocity, radius, color identifier and direction of flight
    /// </summary>
    class SpaceObject
    {

        /// <summary>
        /// name object.
        /// </summary>
        public string name
        {
            get;
            private set;
        }

        /// <summary>
        /// x,y position. Zero = left edge of window
        /// </summary>
        public Position p
        {
            get;
            private set;
        }

        /// <summary>
        /// object mass
        /// </summary>
        public double m
        {
            get;
            private set;
        }

        /// <summary>
        /// object radius
        /// </summary>
        public double r
        {
            get;
            private set;
        }

        /// <summary>
        /// object color
        /// </summary>
        public Color c
        {
            get;
            private set;
        }

        /// <summary>
        /// object velocity, velocity vector
        /// </summary>
        public DirectionVector v
        {
            get;
            private set;
        }
    }
}
