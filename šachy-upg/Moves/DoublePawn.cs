namespace šachy_upg
{
    class DoublePawn : Move
    {
        public override MoveType Type => MoveType.DoublePawn;
        public override Pozice FromPos {  get; }
        public override Pozice ToPos { get; }

        private readonly Pozice skippedPos;

        public DoublePawn(Pozice from, Pozice to)
        {
            FromPos = from;
            ToPos = to;
            skippedPos = new Pozice((from.Row + to.Row) / 2, from.Column);
        }

<<<<<<< HEAD
        public override bool Execute(Deska deska)
=======
        public override void Execute(Deska deska)
>>>>>>> ab1dce662257a5f4e468ef8d6ffdb0ee727dd4e9
        {
            hrac hrac = deska[FromPos].Color;
            deska.SetPawnSkipPosition(hrac, skippedPos);
            new NormalMove(FromPos, ToPos).Execute(deska);
<<<<<<< HEAD

            return true;
=======
>>>>>>> ab1dce662257a5f4e468ef8d6ffdb0ee727dd4e9
        }
    }
}
