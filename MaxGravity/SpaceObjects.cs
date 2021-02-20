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
        private List<SpaceObject> data;
        public SpaceObjects()
        {
            data = new List<SpaceObject>();
        }
        public List<SpaceObject> GetList()
        {
            return data;
        }
    }
}
