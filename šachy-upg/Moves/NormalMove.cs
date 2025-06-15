using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace šachy_upg
{
    public class NormalMove : Move
    {
        public override MoveType Type => MoveType.Normal;
        public override Pozice FromPos { get; }
        public override Pozice ToPos { get; }

        public NormalMove(Pozice from, Pozice to)
        {
            FromPos = from;
            ToPos = to;
        }

        public override void Execute(Deska deska)
        {
            Piece piece = deska[FromPos];
            deska[ToPos] = piece;
            deska[FromPos] = null;
            piece.HasMowed = true;
        }
    }
}
