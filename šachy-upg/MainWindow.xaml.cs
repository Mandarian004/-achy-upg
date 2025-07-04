﻿using System;
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
            if(IsMenuOnScreen())
            {
                return;
            }

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
                if (move.Type == MoveType.PawnPromotion)
                {
                    HandlePromotion(move.FromPos, move.ToPos);
                }
                else
                { 
                    HandleMove(move);
                }
            }
        }

        private void HandlePromotion(Pozice from, Pozice to)
        {
            pieceImage[to.Row, to.Column].Source = Obrazky.GetImage(gameState.CurrentHrac, PieceType.Pawn);
            pieceImage[from.Row, from.Column].Source = null;
            PromotionMenu promMenu = new PromotionMenu(gameState.CurrentHrac);
            MenuContainer.Content = promMenu;

            promMenu.PieceSelected += type =>
            {
                MenuContainer.Content = null;
                Move promMove = new Pawn_Zmena(from, to, type);
                HandleMove(promMove);
            };
        }

        private void HandleMove(Move move)
        {
            gameState.MakeMove(move);
            DrawBoard(gameState.Deska);
            SetCursor(gameState.CurrentHrac);

            if (gameState.IsGameOver())
            {
                ShowGameOver();
            }

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
                Cursor = ChessCursor.BlackCursor;
            }
        }

        private bool IsMenuOnScreen()
        {
            return MenuContainer.Content != null;
        }

        private void ShowGameOver()
        {
            GameOverMenu gameOverMenu = new GameOverMenu(gameState);
            MenuContainer.Content = gameOverMenu;

            gameOverMenu.OptionSelected += option =>
            {
                if (option == Option.Restart)
                {
                    MenuContainer.Content = null;
                    RestartGame();
                }
                else
                {
                    Application.Current.Shutdown();
                }
            };
        }

        private void RestartGame()
        {
            HideHighLights();
            moveCache.Clear();
            gameState = new GameState(hrac.White, Deska.Initial());
            DrawBoard(gameState.Deska);
            SetCursor(gameState.CurrentHrac);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (!IsMenuOnScreen() && e.Key == Key.Escape)
            {
                ShowPauseMenu();
            }
        }

        private void ShowPauseMenu()
        {
            PauseMenu pauseMenu = new PauseMenu();
            MenuContainer.Content = pauseMenu;
            pauseMenu.OptionSelected += option =>
            {
                if (option == Option.Continue)
                {
                    MenuContainer.Content = null;
                }
                else if (option == Option.Restart)
                {
                    RestartGame();
                    MenuContainer.Content = null;
                }
            };
        }
    }
}