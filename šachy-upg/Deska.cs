using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace šachy_upg
{
    public class Deska
    {
        private readonly Piece[,] pieces = new Piece[8,8];

        public Piece this[int row, int col]
        {
            get { return pieces[ row, col]; }
            set { pieces[row, col] = value; }
        }

        public Piece this[Pozice pos]
        {
            get { return this[pos.Row, pos.Column]; }
            set { this[pos.Row, pos.Column] = value; }
        }

        public static Deska Initial()
        {
            Deska deska = new Deska();
            deska.AddStartPieces();
            return deska;
        }

        private void AddStartPieces()//rasistická válka
        {
            //černoši
            this[0, 0] = new Rook(hrac.Black);
            this[0, 1] = new Knight(hrac.Black);
            this[0, 2] = new Bishop(hrac.Black);
            this[0, 3] = new Quenn(hrac.Black);
            this[0, 4] = new King(hrac.Black);
            this[0, 5] = new Bishop(hrac.Black);
            this[0, 6] = new Knight(hrac.Black);
            this[0, 7] = new Rook(hrac.Black);

            //běloši
            this[7, 0] = new Rook(hrac.White);
            this[7, 1] = new Knight(hrac.White);
            this[7, 2] = new Bishop(hrac.White);
            this[7, 3] = new Quenn(hrac.White);
            this[7, 4] = new King(hrac.White);
            this[7, 5] = new Bishop(hrac.White);
            this[7, 6] = new Knight(hrac.White);
            this[7, 7] = new Rook(hrac.White);

            //černý + bílý pěšáci
            for (int c = 0; c<8;c++)
            {
                this[1, c] = new Pawn(hrac.Black);
                this[6, c] = new Pawn(hrac.White);
            }
        }

        public static bool IsInside(Pozice pos)
        {
            return pos.Row >= 0 && pos.Row < 8 && pos.Column >= 0 && pos.Column < 8;
        }

        public bool IsEmpty(Pozice pos)
        {
            return this[pos] == null;
        }
    }
}
