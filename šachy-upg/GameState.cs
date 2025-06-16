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
            move.Execute(Deska);
            CurrentHrac = CurrentHrac.protihrac();
        }
    }
}
