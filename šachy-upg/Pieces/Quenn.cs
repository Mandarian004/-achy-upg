using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace šachy_upg
{
    public class Quenn : Piece
    {
        public override PieceType Type => PieceType.Quenn;
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

        public Quenn(hrac color)
        {
            Color = color;
        }

        public override Piece Copy()
        {
            Quenn copy = new Quenn(Color);
            copy.HasMowed = HasMowed;
            return copy;
        }

        public override IEnumerable<Move> GetMoves(Pozice from, Deska deska)
        {
            return MovePositionsInDirs(from, deska, dirs).Select(to => new NormalMove(from, to));
        }
    }
}
