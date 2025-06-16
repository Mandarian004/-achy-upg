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

        private static IEnumerable<Pozice> PotentialToPosition(Pozice from)
        {
            foreach (Smer vDir in new Smer[] { Smer.North, Smer.South})
            {
                foreach (Smer hDir in new Smer[] { Smer.West, Smer.East })
                {
                    yield return from + 2 * vDir + hDir;
                    yield return from + 2 * hDir + vDir;
                }
            }
        }

        private IEnumerable<Pozice> MovePosition(Pozice from, Deska deska)
        {
            return PotentialToPosition(from).Where(pos => Deska.IsInside(pos) && (deska.IsEmpty(pos) || deska[pos].Color != Color));
        }

        public override IEnumerable<Move> GetMoves(Pozice from, Deska deska)
        {
            return MovePosition(from, deska).Select(to => new NormalMove(from, to));
        }

    }
}
