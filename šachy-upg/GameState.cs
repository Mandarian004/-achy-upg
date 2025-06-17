using šachy_lastPart;
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

        private int noCaptureOrPawnMoves = 0;
        private string stateString;

        private readonly Dictionary<string, int> stateHistory = new Dictionary<string, int>();

        public GameState(hrac hrac, Deska deska)
        {
            CurrentHrac = hrac;
            Deska = deska;

            stateString = new StateString(CurrentHrac, deska).ToString();
            stateHistory[stateString] = 1;
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
            bool CaptureOrPwan = move.Execute(Deska);

            if (CaptureOrPwan)
            {
                noCaptureOrPawnMoves = 0;
                stateHistory.Clear();
            }
            else
            { 
                noCaptureOrPawnMoves++; 
            }
            CurrentHrac = CurrentHrac.protihrac();
            UpdateStateString();
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
            else if (Deska.InsufficientMaterial(CurrentHrac))
            {
                Result = Result.draw(EndReason.InsufficientMaterial);
            }
            else if (FiftyMoveRule())
            {
                Result = Result.draw(EndReason.FiftyMoveRule);
            }
            else if (ThreefoldRepetition())
            {
                Result = Result.draw(EndReason.ThreefoldRepetition);
            }
        }

        public bool IsGameOver()
        {
            return Result != null;
        }

        private bool FiftyMoveRule()
        {
            int fullMoves = noCaptureOrPawnMoves / 2;
            return fullMoves == 50;
        }
        private void UpdateStateString()
        {
            stateString = new StateString(CurrentHrac, Deska).ToString();

            if (!stateHistory.ContainsKey(stateString))
            {
                stateHistory[stateString] = 1;
            }
            else
            {
                stateHistory[stateString]++;
            }
        }
        private bool ThreefoldRepetition()
        {
            return stateHistory[stateString] == 3;
        }
    }
}
