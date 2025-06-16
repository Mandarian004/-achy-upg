namespace šachy_upg
{
    public abstract class Piece
    {
        public abstract PieceType Type { get; }
        public abstract hrac Color { get; }
        public bool HasMowed { get; set; } = false;

        public abstract Piece Copy();

        public abstract IEnumerable<Move> GetMoves(Pozice from, Deska deska);

        protected IEnumerable<Pozice> MovePositionsInDir(Pozice from, Deska deska, Smer dir)
        {
            for (Pozice pos = from + dir; Deska.IsInside(pos); pos += dir)
            {
                if (deska.IsEmpty(pos))
                {
                    yield return pos;
                    continue;
                }
                Piece piece = deska[pos];

                if (piece.Color != Color)
                {
                    yield return pos;
                }
                yield break;
            }
        }

        protected IEnumerable<Pozice> MovePositionsInDirs(Pozice from, Deska deska, Smer[] dirs)
        {
            return dirs.SelectMany(dir => MovePositionsInDir(from, deska, dir));
        }

        public virtual bool CanCaptureOpponentKing(Pozice from, Deska deska)
        {
            return GetMoves(from, deska).Any(move =>
            {
                Piece piece = deska[move.ToPos];
                return piece != null && piece.Type == PieceType.King;
            });
        }
    }
}
