using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace šachy_upg
{
    public class Queen : Piece
    {
        public override PieceType Type => PieceType.Queen;
        public override hrac Color { get; }

        private static readonly Smer[] dirs = new Smer[]
            { 
                Smer.North,
                Smer.South,
                Smer.West,
                Smer.East,
                Smer.NorthEast,
                Smer.NorthWest,
                Smer.SouthEast,
                Smer.SouthWest
            };

        public Queen(hrac color)
        {
            Color = color;
        }

        public override Piece Copy()
        {
            Queen copy = new Queen(Color);
            copy.HasMowed = HasMowed;
            return copy;
        }

        public override IEnumerable<Move> GetMoves(Pozice from, Deska deska)
        {
            return MovePositionsInDirs(from, deska, dirs).Select(to => new NormalMove(from, to));
        }
    }
}
