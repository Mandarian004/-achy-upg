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

        private static readonly Smer[] dirs = new Smer[]
        {
            Smer.North,
            Smer.South,
            Smer.West,
            Smer.East
        };

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

        public override IEnumerable<Move> GetMoves(Pozice from, Deska deska)
        {
            return MovePositionsInDirs(from, deska, dirs).Select(to => new NormalMove(from, to));
        }
    }
}
