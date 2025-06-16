using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace šachy_upg
{
    public partial class MainWindow : Window
    {
        private readonly Image[,] pieceImage = new Image[8, 8];

        private GameState gameState;

        public MainWindow()
        {
            InitializeComponent();
            InitializeBoard();

            gameState = new GameState(hrac.White, Deska.Initial());
            DrawBoard(gameState.Deska);
        }

        private void InitializeBoard()
        {
            for(int r=0;r<8;r++)
            {
                for (int c=0;c<8;c++)
                {
                    Image image = new Image();
                    pieceImage[r, c] = image;
                    PieceGrid.Children.Add(image);
                }
            }
        }

        private void DrawBoard(Deska deska)
        {
            for (int r=0; r<8;r++)
            {
                for (int c = 0;c<8;c++)
                {
                    Piece piece = deska[r, c];
                    pieceImage[r, c].Source = Obrazky.GetImage(piece);
                }
            }
        }
    }
}