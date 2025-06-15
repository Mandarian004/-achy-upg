using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace šachy_upg
{
    public class King : Piece
    {
        public override PieceType Type => PieceType.King;
        public override hrac Color { get; }
        public King(hrac color)
        {
            Color = color;
        }

        public override Piece Copy()
        {
            King copy = new King(Color);
            copy.HasMowed = HasMowed;
            return copy;
        }
    }
}
