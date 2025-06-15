namespace šachy_upg
{
    public class Smer
    {
        public readonly static Smer North = new Smer(-1, 0);
        public readonly static Smer South = new Smer(1, 0);
        public readonly static Smer East = new Smer(0, 1);
        public readonly static Smer West = new Smer(0, -1);
        public readonly static Smer NorthEast = North + East;
        public readonly static Smer NorthWest = North + West;
        public readonly static Smer SouthEast = South + East;
        public readonly static Smer SouthWest = South + West;

        public int RowDelta { get; }
        public int ColumnDelta { get; }

        public Smer(int rowDelta, int columnDelta)
        {
            RowDelta = rowDelta;
            ColumnDelta = columnDelta;
        }

        public static Smer operator +(Smer dir1, Smer dir2)
        {
            return new Smer(dir1.RowDelta + dir2.RowDelta, dir1.ColumnDelta + dir2.ColumnDelta);
        }

        public static Smer operator *(int scalar, Smer dir)
        {
            return new Smer(scalar * dir.RowDelta, scalar * dir.ColumnDelta);
        }
    }
}
