namespace šachy_upg
{
    public abstract class Move
    {
        public abstract MoveType Type { get; }
        public abstract Pozice FromPos { get; }
        public abstract Pozice ToPos { get; }
<<<<<<< HEAD
        public abstract bool Execute(Deska deska);
=======
        public abstract void Execute(Deska deska);
>>>>>>> ab1dce662257a5f4e468ef8d6ffdb0ee727dd4e9

        public virtual bool IsLegal(Deska deska)
        {
            hrac hrac = deska[FromPos].Color;
            Deska deskacopy = deska.Copy();
            Execute(deskacopy);
            return !deskacopy.IsIncheck(hrac);
        }
    }
}
