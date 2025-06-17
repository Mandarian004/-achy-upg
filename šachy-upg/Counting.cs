using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace šachy_upg
{

    public class Counting
    {
        private readonly Dictionary<PieceType, int> whiteCount = new(); // Changed key type to PieceType
        private readonly Dictionary<PieceType, int> blackCount = new(); // Changed key type to PieceType

        public int TotalCount { get; private set; }

        public Counting()
        {
            foreach (PieceType type in Enum.GetValues(typeof(PieceType)))
            {
                whiteCount[type] = 0;
                blackCount[type] = 0;
            }
        }

        public void Increment(hrac color, PieceType type)
        {
            if (color == hrac.White)
            {
                whiteCount[type]++;
            }
            else if (color == hrac.Black)
            {
                blackCount[type]++;
            }
            TotalCount++;
        }

        public int White(PieceType type)
        {
            return whiteCount[type];
        }
        public int Black(PieceType type)
        {
            return blackCount[type];
        }
    }
}