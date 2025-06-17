namespace šachy_upg
{
    public class Pawn_Zmena: Move
    {
        public override MoveType Type => MoveType.PawnPromotion;
        public override Pozice FromPos{ get; }
        public override Pozice ToPos { get; }

        private readonly PieceType newType;

        public Pawn_Zmena(Pozice from, Pozice to, PieceType newType)
        {
            FromPos = from;
            ToPos = to;
            this.newType = newType;
        }

        private Piece CreatePromotionPiece(hrac color)
        {
            return newType switch
            {
                PieceType.Knight => new Knight(color),
                PieceType.Bishop => new Bishop(color),
                PieceType.Rook => new Rook(color),
                _ => new Queen(color),
            };
        }


        public override bool Execute(Deska deska)
        {
            Piece pawn = deska[FromPos];
            deska[FromPos] = null;

            Piece promotionPiece = CreatePromotionPiece(pawn.Color);
            promotionPiece.HasMowed = true;
            deska[ToPos] = promotionPiece;

            return true;
        }
    }
}
