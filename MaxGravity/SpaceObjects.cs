using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxGravity
{
    /// <summary>
    /// List of objects in the universe. methods for calculating gravity, position and velocity.
    /// </summary>
    class SpaceObjects
    {
        private SpaceObject[] data;
        public int count
        {
            get;
            private set;
        }
        public SpaceObjects()
        {
            data = new SpaceObject[1000];
            count = 0;
        }
        public SpaceObject[] GetList()
        {
            return data;
        }
        public void AddObject(SpaceObject o)
        {
            data[count] = o;
            count++;
        }
        public void setForceOfGravity(int index, DirectionVector forceOfGravity)
        {
            data[index].forceOfGravity = forceOfGravity;
        }
    }
}
