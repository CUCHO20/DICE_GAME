using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame
{
    public class DiceParser
    {
        public List<Dice>? Parse(string[] args)
        {
            try {
                var result = new List<Dice>();
                foreach(var arg in args)
                {
                    var faces = Array.ConvertAll(arg.Split(','), int.Parse);
                    result.Add(new Dice(faces));
                }
                return result;
            } catch {
                return null;
            }
        }
    }
}
