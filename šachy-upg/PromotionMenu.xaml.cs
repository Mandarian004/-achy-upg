using System.Windows.Controls;
using System.Windows.Input;
using System;

namespace šachy_upg
{
    /// <summary>
    /// Interakční logika pro PromotionMenu.xaml
    /// </summary>
    public partial class PromotionMenu : UserControl
    {
        public event Action<PieceType> PieceSelected;
        public PromotionMenu(hrac hrac)
        {
            InitializeComponent();

            QueenImg.Source = Obrazky.GetImage(hrac, PieceType.Queen);
            BishopImg.Source = Obrazky.GetImage(hrac, PieceType.Bishop);
            RookImg.Source = Obrazky.GetImage(hrac, PieceType.Rook);
            KnightImg.Source = Obrazky.GetImage(hrac, PieceType.Knight);
        }

        // Zde jsou chybějící metody, které musíte přidat zpět
        private void QueenImg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Zde bude logika pro výběr královny
            PieceSelected?.Invoke(PieceType.Queen);
        }

        private void BishopImg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Zde bude logika pro výběr střelce
            PieceSelected?.Invoke(PieceType.Bishop);
        }

        private void RookImg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Zde bude logika pro výběr věže
            PieceSelected?.Invoke(PieceType.Rook);
        }

        private void KnightImg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Zde bude logika pro výběr jezdce
            PieceSelected?.Invoke(PieceType.Knight);
        }
    }
}
