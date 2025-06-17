using System.Windows;
using System;
using System.Windows.Controls;

namespace šachy_upg
{
    /// <summary>
    /// Interakční logika pro GameOverMenu.xaml
    /// </summary>
    public partial class GameOverMenu : UserControl
    {
        public event Action<Option> OptionSelected;
        public GameOverMenu(GameState gameState)
        {
            InitializeComponent();

            Result result = gameState.Result;
            WinnerText.Text = GetWinnerText(result.Winner);
            ReasonText.Text = GetReasonText(result.Reason, gameState.CurrentHrac);
        }

        private static string GetWinnerText(hrac winner)
        {
            return winner switch
            {
                hrac.White => "WHITE WINS",
                hrac.Black => "BLACK WINS",
                _ => "IT'S A DRAW"
            };
        }

        private static string PlayerString(hrac hrac)
        {
            return hrac switch
            {
                hrac.White => "WHITE",
                hrac.Black => "BLACK",
                _ => ""
            };
        }

        private static string GetReasonText(EndReason reason, hrac currentHrac)
        {
            return reason switch
            {
                EndReason.Stalemate => $"STALEMATE - {PlayerString(currentHrac)} CAN'T MOVE",
                EndReason.Checkmate => $"CHECKMATE - {PlayerString(currentHrac)} CAN'T MOVE",
                EndReason.FiftyMoveRule => "FIFTY-MOVE RULE",
                EndReason.InsufficientMaterial => "INSUFFICIENT MATERIAL",
                _ => ""
            };
        }

        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            OptionSelected?.Invoke(Option.Restart);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            OptionSelected?.Invoke(Option.Exit);
        }
    }
}
