using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace šachy_upg
{
    public class Pawn : Piece
    {
        public override PieceType Type => PieceType.Pawn;
        public override hrac Color { get; }
        public Pawn (hrac color)
        {
            Color = color;
        }

        public override Piece Copy()
        {
            Pawn copy = new Pawn(Color);
            copy.HasMowed = HasMowed;
            return copy;
        }
    }
}
