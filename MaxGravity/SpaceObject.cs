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
        /// Single object in the universe has name, x, y, mass, velocity, radius, color identifier and direction/velocity of flight, setting a lock
        /// </summary>
        public SpaceObject(string objectName, Position position, double mass, double radius, Color color, DirectionVector velocity, bool blocked)
        {
            this.name = objectName;
            this.p = position;
            this.m = mass;
            this.r = radius;
            this.c = color;
            this.v = velocity;
            this.blocked = blocked;
            this.forceOfGravity = new DirectionVector();
        }

        /// <summary>
        /// Single object in the universe has name, x, y, mass, velocity, radius, color identifier and direction/velocity of flight, NO blockade!
        /// </summary>
        public SpaceObject(string objectName, Position position, double mass, double radius, Color color, DirectionVector velocity)
        {
            this.name = objectName;
            this.p = position;
            this.m = mass;
            this.r = radius;
            this.c = color;
            this.v = velocity;
            this.blocked = false;
            this.forceOfGravity = new DirectionVector();
        }

        /// <summary>
        /// name object.
        /// </summary>
        public string name
        {
            get;
            private set;
        }

        /// <summary>
        /// if an object is locked, it doesn't move. The "sun" should be such a blocked object.
        /// </summary>
        public bool blocked
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

        /// <summary>
        /// force of gravity, calculated from all objects
        /// </summary>
        public DirectionVector forceOfGravity
        {
            get;
            set;
        }
    }
}
