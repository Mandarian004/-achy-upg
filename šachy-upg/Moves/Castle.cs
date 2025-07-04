﻿namespace šachy_upg
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

        public override bool Execute(Deska deska)
        {
            new NormalMove(FromPos, ToPos).Execute(deska);
            new NormalMove(rookFromPos, rookToPos).Execute(deska);

            return false;
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
