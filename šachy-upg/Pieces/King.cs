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

        private static readonly Smer[] dirs = new Smer[]
        {
            Smer.North,
            Smer.South,
            Smer.West,
            Smer.East,
            Smer.NorthWest,
            Smer.SouthEast,
            Smer.NorthEast,
            Smer.SouthWest
        };

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

        private IEnumerable<Pozice> MovePositions(Pozice from, Deska deska)
        {
            foreach (Smer dir in dirs)
            {
                Pozice to = from + dir;

                if (!Deska.IsInside(to))
                {
                    continue;
                }

                if (deska.IsEmpty(to) || deska[to].Color != Color)
                {
                    yield return to;
                }


            }
        }

        public override IEnumerable<Move> GetMoves(Pozice from, Deska deska)
        {
            foreach (Pozice to in MovePositions(from, deska))
            {
                yield return new NormalMove(from, to);
            }
        }

    }
}
