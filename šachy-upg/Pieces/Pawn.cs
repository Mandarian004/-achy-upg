using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;

namespace šachy_upg
{
    public class Pawn : Piece
    {
        public override PieceType Type => PieceType.Pawn;
        public override hrac Color { get; }

        private readonly Smer forward;
        public Pawn (hrac color)
        {
            Color = color;


            if (color == hrac.White)
            {
                forward = Smer.North;
            }
            else if (color == hrac.Black) {
                forward = Smer.South;
            }
        }

        public override Piece Copy()
        {
            Pawn copy = new Pawn(Color);
            copy.HasMowed = HasMowed;
            return copy;
        }

        private static bool CanMoveTo(Pozice pos, Deska deska)
        {
            return Deska.IsInside(pos) && deska.IsEmpty(pos);

        }

        private bool CanCaptureAt(Pozice pos, Deska deska)
        {
            if (!Deska.IsInside(pos) || deska.IsEmpty(pos))
            { return false;
            }
            return deska[pos].Color != Color;
        }
        private IEnumerable<Move> ForwardMoves(Pozice from, Deska deska)
        {
            Pozice oneMovePos = from + forward;

            if (CanMoveTo(oneMovePos, deska)) {
            yield return new NormalMove(from, oneMovePos);

                Pozice twoMovesPos = oneMovePos + forward;

                if (!HasMowed && CanMoveTo(twoMovesPos, deska))
                {
                    yield return new NormalMove(from, twoMovesPos);
                }
            }
        }

        private IEnumerable<Move> DiagonalMoves(Pozice from, Deska deska)
        {
            foreach (Smer dir in new Smer[] { Smer.West, Smer.South })
            {
                Pozice to = from + forward + dir;

                if (CanMoveTo(to, deska)) 
                {
                    yield return new NormalMove(from, to); 
                }

            }
        }

        public override IEnumerable<Move> GetMoves(Pozice from, Deska deska)
        {
            return ForwardMoves(from, deska).Concat(DiagonalMoves(from, deska));
        }

        public override bool CanCaptureOpponentKing(Pozice from, Deska deska)
        {
            return DiagonalMoves(from, deska).Any(move =>
            {
                Piece piece = deska[move.ToPos];
                return piece != null && piece.Type == PieceType.King;

            });
        }
    }
}
