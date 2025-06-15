namespace šachy_upg
{
    public class Pozice
    {
        public int Row { get; }
        public int Column { get; }

        public Pozice(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public hrac SquareColor()
        {
            if((Row + Column) % 2 == 0)
            {
                return hrac.White;
            }
            return hrac.Black;
        }

        public override bool Equals(object? obj)
        {
            return obj is Pozice pozice &&
                   Row == pozice.Row &&
                   Column == pozice.Column;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Row, Column);
        }

        public static bool operator ==(Pozice left, Pozice right)
        {
            return EqualityComparer<Pozice>.Default.Equals(left, right);
        }

        public static bool operator !=(Pozice? left, Pozice? right)
        {
            return !(left == right);
        }

        public static Smer operator +(Pozice pos, Smer dir)
        {
            return new Smer(pos.Row + dir.RowDelta, pos.Column + dir.ColumnDelta);
        }
    }
}
