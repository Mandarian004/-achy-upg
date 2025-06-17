using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace šachy_upg
{
    class Castle : Move
    {
        public override MoveType Type { get; }
        public override Pozice FromPos { get; }
        public override Pozice ToPos { get; }

        private readonly Smer kingMoveDir;
        private readonly Pozice rookFromPos;
        private readonly Pozice rookToPos;

        public Castle(MoveType type, Pozice kingPos)
        {
            Type = type;
            FromPos = kingPos;

            if (type == MoveType.CastleKS)
            {
                kingMoveDir = Smer.East;
                ToPos = new Pozice(kingPos.Row, 6);
                rookFromPos = new Pozice(kingPos.Row, 7);
                rookToPos = new Pozice(kingPos.Row, 5);
            }
            else if (type == MoveType.CastleQS) 
            {
                kingMoveDir = Smer.West;
                ToPos = new Pozice(kingPos.Row, 2);
                rookFromPos = new Pozice(kingPos.Row, 0);
                rookFromPos = new Pozice(kingPos.Row,3);
            }
        }

<<<<<<< HEAD
        public override bool Execute(Deska deska)
=======
        public override void Execute(Deska deska)
>>>>>>> ab1dce662257a5f4e468ef8d6ffdb0ee727dd4e9
        {
            new NormalMove(FromPos, ToPos).Execute(deska);
            new NormalMove(rookFromPos, rookToPos).Execute(deska);

<<<<<<< HEAD
            return false;
=======
>>>>>>> ab1dce662257a5f4e468ef8d6ffdb0ee727dd4e9
        }

        public override bool IsLegal(Deska deska)
        {
            hrac hrac = deska[FromPos].Color;

            if (deska.IsIncheck(hrac))
            {
                return false;
            }

            Deska copy = deska.Copy();
            Pozice kingPosInCopy = FromPos;

            for (int i = 0; i < 2; i++)
            {
                new NormalMove(kingPosInCopy, kingPosInCopy + kingMoveDir).Execute(copy);
                kingPosInCopy += kingMoveDir;

                if (copy.IsIncheck(hrac) )
                {
                    return false;
                }
            }

            return true;
        }
    }
}
