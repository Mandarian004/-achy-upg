using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace šachy_upg
{
    public class GameState
    {
        public Deska Deska { get; }
        public hrac CurrentHrac { get; private set; }
        public Result Result { get; private set; } = null;
<<<<<<< HEAD

        private int noCaptureOrPawnMoves = 0;
=======
>>>>>>> ab1dce662257a5f4e468ef8d6ffdb0ee727dd4e9
        public GameState(hrac hrac, Deska deska)
        {
            CurrentHrac = hrac;
            Deska = deska;
        }

        public IEnumerable<Move> LegalMOvesForPiece(Pozice pos)
        {
            if (Deska.IsEmpty(pos) || Deska[pos].Color != CurrentHrac)
            {
                return Enumerable.Empty<Move>();
            }
            Piece piece = Deska[pos];
           IEnumerable<Move> moveCandidates = piece.GetMoves(pos, Deska);
            return moveCandidates.Where(move => move.IsLegal(Deska));
        }

        public void MakeMove(Move move)
        {
            Deska.SetPawnSkipPosition(CurrentHrac, null);
<<<<<<< HEAD
            bool CaptureOrPwan = move.Execute(Deska);

            if (CaptureOrPwan)
            {
                noCaptureOrPawnMoves = 0;
            }
            else
                { noCaptureOrPawnMoves++; }
=======
            move.Execute(Deska);
>>>>>>> ab1dce662257a5f4e468ef8d6ffdb0ee727dd4e9
            CurrentHrac = CurrentHrac.protihrac();
            CheckForGameOver();
        }

        public IEnumerable<Move> ALLlegalMovesFor(hrac hrac)
        {
            IEnumerable<Move> moveCandidates = Deska.PiecePositionsFor(hrac).SelectMany(pos =>
            {
                Piece piece = Deska[pos];
                return piece.GetMoves(pos, Deska);
            });
            return moveCandidates.Where(move => move.IsLegal(Deska));
        }

        private void CheckForGameOver()
        {
            if (!ALLlegalMovesFor(CurrentHrac).Any())
            {
                if(Deska.IsIncheck(CurrentHrac))
                {
                    Result = Result.Win(CurrentHrac.protihrac());
                }
                else
                {
                    Result = Result.draw(EndReason.Stalemate);
                }
            }
<<<<<<< HEAD
            else if (FiftyMoveRule())
            {
                Result = Result.draw(EndReason.FiftyMoveRule);
            }
=======
>>>>>>> ab1dce662257a5f4e468ef8d6ffdb0ee727dd4e9
        }

        public bool IsGameOver()
        {
            return Result != null;
        }
<<<<<<< HEAD

        private bool FiftyMoveRule()
        {
            int fullMoves = noCaptureOrPawnMoves / 2;
            return fullMoves == 50;
        }
=======
>>>>>>> ab1dce662257a5f4e468ef8d6ffdb0ee727dd4e9
    }
}
