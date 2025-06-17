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

        private static bool IsUnmovedRook(Pozice pos, Deska deska)
        {
            if (deska.IsEmpty(pos))
            {
                return false;
            }

            Piece piece = deska[pos];
            return piece.Type == PieceType.Rook && !piece.HasMowed;
        }

        private static bool AllEmpty(IEnumerable<Pozice> pozices, Deska deska) 
        {
            return pozices.All(pos => deska.IsEmpty(pos));
        }

        private bool CanCastleKingSide(Pozice from, Deska deska)
        {
            if (HasMowed)
            {
                return false;
            }

            Pozice rookPos = new Pozice(from.Row, 7);
            Pozice[] betweenPositions = new Pozice[] { new(from.Row, 5), new(from.Row, 6) };

            return IsUnmovedRook(rookPos, deska) && AllEmpty(betweenPositions, deska);
        }

        private bool CanCastleQueenSide(Pozice from, Deska deska)
        {
            if (HasMowed)
            {
                return false;
            }

            Pozice rookPos = new Pozice(from.Row, 0);
            Pozice[] betweenPositions = new Pozice[] { new(from.Row, 1), new(from.Row, 2), new(from.Row, 3) };

            return IsUnmovedRook(rookPos, deska) && AllEmpty(betweenPositions, deska);
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

            if (CanCastleKingSide(from, deska))
            {
                yield return new Castle(MoveType.CastleKS, from);
            }

            if (CanCastleQueenSide(from, deska))
            {
                yield return new Castle(MoveType.CastleQS, from);
            }
        }

        public override bool CanCaptureOpponentKing(Pozice from, Deska deska)
        {
            return MovePositions(from, deska).Any(to =>
            {
                Piece piece = deska[to];
                return piece != null && piece.Type == PieceType.King;
            });
        }
    }
}
