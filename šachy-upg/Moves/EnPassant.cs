namespace šachy_upg
{
    public class EnPassant : Move
    {
        public override MoveType Type => MoveType.EnPassant;
        public override Pozice FromPos { get; }
        public override Pozice ToPos { get; }

        private readonly Pozice capturePos;

        public EnPassant(Pozice from, Pozice to)
        {
            FromPos = from;
            ToPos = to;
            capturePos = new Pozice(from.Row, to.Column);
        }

        public override bool Execute(Deska deska)
        {
            new NormalMove(FromPos, ToPos).Execute(deska);
            deska[capturePos] = null;

            return true;
        }
    }
}
