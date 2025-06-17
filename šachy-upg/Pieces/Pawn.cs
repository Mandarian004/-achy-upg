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
            { 
                return false;
            }
            return deska[pos].Color != Color;
        }

        private static IEnumerable<Move> PromotionMoves(Pozice from, Pozice to)
        {
            yield return new Pawn_Zmena(from, to, PieceType.Knight);
            yield return new Pawn_Zmena(from, to, PieceType.Bishop);
            yield return new Pawn_Zmena(from, to, PieceType.Rook);
            yield return new Pawn_Zmena(from, to, PieceType.Queen);
        }

        private IEnumerable<Move> ForwardMoves(Pozice from, Deska deska)
        {
            Pozice oneMovePos = from + forward;

            if (CanMoveTo(oneMovePos, deska)) 
            {
                if (oneMovePos.Row == 0 || oneMovePos.Row == 7)
                {
                    foreach (Move promMove in PromotionMoves(from, oneMovePos))
                    {
                        yield return promMove;
                    }
                }
                else
                {
                    yield return new NormalMove(from, oneMovePos);
                }

                Pozice twoMovesPos = oneMovePos + forward;

                if (!HasMowed && CanMoveTo(twoMovesPos, deska))
                {
                    yield return new DoublePawn(from, twoMovesPos);
                }
            }
        }

        private IEnumerable<Move> DiagonalMoves(Pozice from, Deska deska)
        {
            foreach (Smer dir in new Smer[] { Smer.West, Smer.East })
            {
                Pozice to = from + forward + dir;

                if(to == deska.GetPawnSkipPosition(Color.protihrac()))
                {
                    yield return new EnPassant(from, to);
                }

                else if (CanCaptureAt(to, deska))
                {
                    if (to.Row == 0 || to.Row == 7)
                    {
                        foreach (Move promMove in PromotionMoves(from, to))
                        {
                            yield return promMove;
                        }
                    }
                    else
                    {
                        yield return new NormalMove(from, to);
                    }
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
