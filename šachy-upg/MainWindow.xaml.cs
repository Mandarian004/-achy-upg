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
using System.Configuration;

namespace šachy_upg
{
    public partial class MainWindow : Window
    {
        private readonly Image[,] pieceImage = new Image[8, 8];
        private readonly Rectangle[,] highlights = new Rectangle[8, 8];
        private readonly Dictionary<Pozice, Move> moveCache = new Dictionary<Pozice, Move>();

        private GameState gameState;
        private Pozice selectedPos = null;

        public MainWindow()
        {
            InitializeComponent();
            InitializeBoard();

            gameState = new GameState(hrac.White, Deska.Initial());
            DrawBoard(gameState.Deska);
            SetCursor(gameState.CurrentHrac);
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

                    Rectangle highlight = new Rectangle();
                    highlights[r, c] = highlight;
                    HighlightGrid.Children.Add(highlight);
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

        private void BoardGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Point point = e.GetPosition(BoardGrid);
            Pozice pos = toSquarePosition(point);

            if (selectedPos == null)
            {
                OnFromPositionSelected(pos);
            }
            else
            {
                OnToPositionSelected(pos);
            }
        }

        private Pozice toSquarePosition(Point point)
        {
            double squareSize = BoardGrid.ActualWidth / 8;
            int row = (int)(point.Y / squareSize);
            int col = (int)(point.X / squareSize);
            return new Pozice(row, col);
        }

        private void OnFromPositionSelected(Pozice pos)
        {
            IEnumerable<Move> moves = gameState.LegalMOvesForPiece(pos);

            if(moves.Any())
            {
                selectedPos = pos;
                CacheMoves(moves);
                ShowHighLights();
            }
        }

        private void OnToPositionSelected(Pozice pos)
        {
            selectedPos = null;
            HideHighLights();

            if(moveCache.TryGetValue(pos, out Move move))
            {
                HandleMove(move);
            }
        }

        private void HandleMove(Move move)
        {
            gameState.MakeMove(move);
            DrawBoard(gameState.Deska);
        }

        private void CacheMoves(IEnumerable<Move> moves)
        {
            moveCache.Clear();

            foreach (Move move in moves)
            {
                moveCache[move.ToPos] = move;
            }

        }
        private void ShowHighLights()
        {
            Color color = Color.FromArgb(150, 125, 255, 125);

            foreach(Pozice to in moveCache.Keys)
            {
                highlights[to.Row, to.Column].Fill = new SolidColorBrush(color);
            }
        }

        private void HideHighLights()
        {
            foreach (Pozice to in moveCache.Keys)
            {
                highlights[to.Row, to.Column].Fill = Brushes.Transparent;

            }
        }

        private void SetCursor(hrac hrac)
        {
            if (hrac == hrac.White)
            {
                Cursor = ChessCursor.WhiteCursor;
            }
            else
            {
                Cursor = ChessCursor.WhiteCursor;
            }
        }
    }
}