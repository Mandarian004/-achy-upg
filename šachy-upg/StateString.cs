using šachy_upg;
using System.Text;

namespace šachy_lastPart
{
    public class StateString
    {
        private readonly StringBuilder sb = new StringBuilder();

        public StateString(hrac currentHrac, Deska deska)
        {
            AddPiecePlacement(deska);
            sb.Append(' ');

            AddCurrentHrac(currentHrac);
            sb.Append(' ');

            AddCastlingRights(deska);
            sb.Append(' ');

            AddEndPassant(deska, currentHrac);
        }

        public override string ToString()
        {
            return sb.ToString();
        }

        private static char PieceChar(Piece piece)
        {
            char c = piece.Type switch
            {
                PieceType.Pawn => 'p',
                PieceType.Knight => 'n',
                PieceType.Rook => 'r',
                PieceType.Bishop => 'b',
                PieceType.Queen => 'q',
                PieceType.King => 'k',
                _ => ' '
            };

            if (piece.Color == hrac.White)
            {
                return char.ToUpper(c);
            }
            return c;
        }

        private void AddRowData(Deska deska, int row)
        {
            int empty = 0;

            for (int c = 0; c < 8; c++)
            {
                if (deska[row, c] == null)
                {
                    empty++;
                    continue;
                }

                if (empty > 0)
                {
                    sb.Append(empty);
                    empty = 0;
                }

                sb.Append(PieceChar(deska[row, c]));
            }

            if (empty > 0)
            {
                sb.Append(empty);
            }
        }

        private void AddPiecePlacement(Deska deska)
        {
            for (int r = 0; r < 8; r++)
            {
                if (r != 0)
                {
                    sb.Append('/');
                }

                AddRowData(deska, r);
            }
        }

        private void AddCurrentHrac(hrac currentHrac)
        {
            if (currentHrac == hrac.White)
            {
                sb.Append('w');
            }
            else
            {
                sb.Append('b');
            }
        }

        private void AddCastlingRights(Deska deska)
        {
            bool castleWKS = deska.CastleRightKS(hrac.White);
            bool castleWQS = deska.CastleRightKS(hrac.White);
            bool castleBKS = deska.CastleRightKS(hrac.Black);
            bool castleBQS = deska.CastleRightKS(hrac.Black);

            if (!(castleWKS || castleWQS || castleBQS || castleBKS))
            {
                sb.Append('-');
                return;
            }

            if (castleWKS)
            {
                sb.Append('K');
            }
            if (castleWQS)
            {
                sb.Append('Q');
            }
            if (castleBKS)
            {
                sb.Append('k');
            }
            if (castleBQS)
            {
                sb.Append('q');
            }
        }

        private void AddEndPassant(Deska deska, hrac currentHrac)
        {
            if (!deska.CanCaptureEnPassant(currentHrac))
            {
                sb.Append('-');
                return;
            }

            Pozice pos = deska.GetPawnSkipPosition(currentHrac.protihrac());
            char file = (char)('a' + pos.Column);
            int rank = 8 - pos.Row;
            sb.Append(file);
            sb.Append(rank);
        }
    }
}
