using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace šachy_upg
{
    public class Deska
    {
        private readonly Piece[,] pieces = new Piece[8, 8];

        private readonly Dictionary<hrac, Pozice> pawnSkipPositions = new Dictionary<hrac, Pozice>
        {
            { hrac.White, null },
            { hrac.Black, null }
        };

        public Piece this[int row, int col]
        {
            get { return pieces[row, col]; }
            set { pieces[row, col] = value; }
        }

        public Piece this[Pozice pos]
        {
            get { return this[pos.Row, pos.Column]; }
            set { this[pos.Row, pos.Column] = value; }
        }

        public Pozice GetPawnSkipPosition(hrac hrac)
        {
            return pawnSkipPositions[hrac];
        }

        public void SetPawnSkipPosition(hrac hrac, Pozice pos)
        {
            pawnSkipPositions[hrac] = pos;
        }

        public static Deska Initial()
        {
            Deska deska = new Deska();
            deska.AddStartPieces();
            return deska;
        }

        private void AddStartPieces()//rasistická válka
        {
            //černoši
            this[0, 0] = new Rook(hrac.Black);
            this[0, 1] = new Knight(hrac.Black);
            this[0, 2] = new Bishop(hrac.Black);
            this[0, 3] = new Queen(hrac.Black);
            this[0, 4] = new King(hrac.Black);
            this[0, 5] = new Bishop(hrac.Black);
            this[0, 6] = new Knight(hrac.Black);
            this[0, 7] = new Rook(hrac.Black);

            //běloši
            this[7, 0] = new Rook(hrac.White);
            this[7, 1] = new Knight(hrac.White);
            this[7, 2] = new Bishop(hrac.White);
            this[7, 3] = new Queen(hrac.White);
            this[7, 4] = new King(hrac.White);
            this[7, 5] = new Bishop(hrac.White);
            this[7, 6] = new Knight(hrac.White);
            this[7, 7] = new Rook(hrac.White);

            //černý + bílý pěšáci
            for (int c = 0; c < 8; c++)
            {
                this[1, c] = new Pawn(hrac.Black);
                this[6, c] = new Pawn(hrac.White);
            }
        }

        public static bool IsInside(Pozice pos)
        {
            return pos.Row >= 0 && pos.Row < 8 && pos.Column >= 0 && pos.Column < 8;
        }

        public bool IsEmpty(Pozice pos)
        {
            return this[pos] == null;
        }

        public IEnumerable<Pozice> PiecePositions()
        {
            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    Pozice pos = new Pozice(r, c);

                    if (!IsEmpty(pos))
                    {
                        yield return pos;
                    }

                }
            }
        }

        public IEnumerable<Pozice> PiecePositionsFor(hrac hrac)
        {
            return PiecePositions().Where(pos => this[pos].Color == hrac);
        }

        public bool IsIncheck(hrac hrac)
        {
            return PiecePositionsFor(hrac.protihrac()).Any(pos =>
            {
                Piece piece = this[pos];
                return piece.CanCaptureOpponentKing(pos, this);
            });
        }

        public Deska Copy()
        {
            Deska copy = new Deska();

            foreach (Pozice pos in PiecePositions())
            {
                copy[pos] = this[pos].Copy();
            }

            return copy;
        }

        public Counting CountPieces()
        {
            Counting counting = new Counting();
            foreach (Pozice pos in PiecePositions())
            {
                Piece piece = this[pos];
                counting.Increment(piece.Color, piece.Type);
            }
            return counting;
        }


        public bool InsufficientMaterial(hrac hrac)
        {
            Counting counting = CountPieces();

            return IsKingVKing(counting) ||
                IsKingKnightVKing(counting) ||
                IsKingBishopVKing(counting) ||
                IsKingBishopVKingBishop(counting);
        }
        private bool IsKingBishopVKingBishop(Counting counting)
        {
            if (counting.TotalCount != 4)
            {
                return false;
            }
            if (counting.White(PieceType.Bishop) != 1 || counting.Black(PieceType.Bishop) != 1)
            {
                return false;
            }

            Pozice wBishoPos = FindPiece(hrac.White, PieceType.Bishop);
            Pozice bBishoPos = FindPiece(hrac.Black, PieceType.Bishop);

            return wBishoPos.SquareColor() == bBishoPos.SquareColor();
        }

        private Pozice FindPiece(hrac color, PieceType type)
        {
            return PiecePositionsFor(color).FirstOrDefault(pos => this[pos].Type == type);
        }

        private static bool IsKingBishopVKIng(Counting counting)
        {
            return counting.TotalCount == 3 &&(counting.White(PieceType.Bishop) == 1 || counting.Black(PieceType.Bishop) == 1);
        }
        private static bool IsKingBishopVKing(Counting counting)
        {
            return counting.TotalCount == 3 &&(counting.White(PieceType.Bishop) == 1 || counting.Black(PieceType.Bishop) == 1);
        }

        private static bool IsKingKnightVKing(Counting counting)
        {
            return counting.TotalCount == 3 &&(counting.White(PieceType.Knight) == 1 || counting.Black(PieceType.Knight) == 1);
        }

        private static bool IsKingVKing(Counting counting)
        {
            return counting.TotalCount == 2;
        }

        private bool IsUnmovedKingAndRook(Pozice kingPos, Pozice rookPos)
        {
            if (IsEmpty(kingPos) || IsEmpty(rookPos))
            {
                return false;
            }

            Piece king = this[kingPos];
            Piece rook = this[rookPos];

            return king.Type == PieceType.King && rook.Type == PieceType.Rook && !king.HasMowed;
        }

        public bool CastleRightKS(hrac hrac)
        {
            return hrac switch
            {
                hrac.White => IsUnmovedKingAndRook(new Pozice(7, 4), new Pozice(7, 7)),
                hrac.Black => IsUnmovedKingAndRook(new Pozice(0, 4), new Pozice(0, 7)),
                _ => false
            };
        }

        public bool CastleRightQS(hrac hrac)
        {
            return hrac switch
            {
                hrac.White => IsUnmovedKingAndRook(new Pozice(7, 4), new Pozice(7, 0)),
                hrac.Black => IsUnmovedKingAndRook(new Pozice(0, 4), new Pozice(0, 0)),
                _ => false
            };
        }

        private bool HasPawnInPosition(hrac hrac, Pozice[] pawnPositions, Pozice skipPos)
        {
            foreach (Pozice pos in pawnPositions.Where(IsInside))
            {
                Piece piece = this[pos];
                if (piece == null || piece.Color != hrac || piece.Type != PieceType.Pawn)
                {
                    continue;
                }

                EnPassant move = new EnPassant(pos, skipPos);
                if (move.IsLegal(this))
                {
                    return true;
                }
            }
            return false;
        }

        public bool CanCaptureEnPassant(hrac hrac)
        {
            Pozice skipPos = GetPawnSkipPosition(hrac.protihrac());

            if (skipPos == null)
            {
                return false;
            }

            Pozice[] pawnPositions = hrac switch
            {
                hrac.White => new Pozice[] { skipPos + Smer.SouthWest, skipPos + Smer.SouthEast },
                hrac.Black => new Pozice[] { skipPos + Smer.NorthWest, skipPos + Smer.NorthEast },
                _ => Array.Empty<Pozice>()
            };
            return HasPawnInPosition(hrac, pawnPositions, skipPos);
        }
    }
}
