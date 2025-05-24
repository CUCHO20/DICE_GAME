using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dice
{
    public class Dice
    {
        public string Name { get; }
        public IReadOnlyList<int> Faces { get; }

        public Dice(int[] faces)
        {
            Faces = new List<int>(faces).AsReadOnly();
            Name = $"[{string.Join(",", Faces)}]";
        }

        public int Roll(int faceInx)
        {
            return Faces[faceInx];
        }
    }
}
