using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace šachy_upg
{
    public class Rook : Piece
    {
        public override PieceType Type => PieceType.Rook;
        public override hrac Color { get; }
        public Rook(hrac color)
        {
            Color = color;
        }

        public override Piece Copy()
        {
            Rook copy = new Rook(Color);
            copy.HasMowed = HasMowed;
            return copy;
        }
    }
}
