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

<<<<<<< HEAD
        public override bool Execute(Deska deska)
=======
        public override void Execute(Deska deska)
>>>>>>> ab1dce662257a5f4e468ef8d6ffdb0ee727dd4e9
        {
            new NormalMove(FromPos, ToPos).Execute(deska);
            deska[capturePos] = null;

<<<<<<< HEAD
            return true;
=======
>>>>>>> ab1dce662257a5f4e468ef8d6ffdb0ee727dd4e9
        }
    }
}
