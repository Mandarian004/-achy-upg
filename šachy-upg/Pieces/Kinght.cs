using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace šachy_upg
{
    public class Knight : Piece
    {
        public override PieceType Type => PieceType.Knight;
        public override hrac Color { get; }
        public Knight(hrac color)
        {
            Color = color;
        }

        public override Piece Copy()
        {
            Knight copy = new Knight(Color);
            copy.HasMowed = HasMowed;
            return copy;
        }
    }
}
