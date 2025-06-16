namespace šachy_upg
{
    public abstract class Move
    {
        public abstract MoveType Type { get; }
        public abstract Pozice FromPos { get; }
        public abstract Pozice ToPos { get; }
        public abstract void Execute(Deska deska);

        public virtual bool IsLegal(Deska deska)
        {
            hrac hrac = deska[FromPos].Color;
            Deska deskacopy = deska.Copy();
            Execute(deskacopy);
            return !deskacopy.IsIncheck(hrac);
        }
    }
}
