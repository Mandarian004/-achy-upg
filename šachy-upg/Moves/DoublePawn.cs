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

        public override void Execute(Deska deska)
        {
            hrac hrac = deska[FromPos].Color;
            deska.SetPawnSkipPosition(hrac, skippedPos);
            new NormalMove(FromPos, ToPos).Execute(deska);
        }
    }
}
