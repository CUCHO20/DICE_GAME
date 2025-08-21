using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame
{
    internal class ProbabilityCalculator
    {
        public static double[,] CalculatorProbabilites(List<Dice> diceList)
        {
            int n = diceList.Count;
            double[,] matrix = new double[n, n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i == j)
                        matrix[i, j] = -1;
                    else
                        matrix[i, j] = GetWinProbability(diceList[i], diceList[j]);
                }
            }

            return matrix;
        }

        public static double GetWinProbability(Dice d1, Dice d2)
        {
            int count = 0;
            int total = d1.Faces.Count * d2.Faces.Count;

            foreach (int a in d1.Faces)
            {
                foreach (int b in d2.Faces)
                {
                    if (a > b) count++;
                }
            }

            return (double)count / total;
        }
    }
}
