using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace šachy_upg
{
    public static class Obrazky
    {
        private static readonly Dictionary<PieceType, ImageSource> whiteSources = new()
        {
            {PieceType.Pawn, LoadImage("figurky/PawnW.png")},
            {PieceType.Bishop, LoadImage("figurky/BishopW.png")}, //PieceType.Bishop
            {PieceType.Knight, LoadImage("figurky/KnightW.png")}, // PieceType.Knight
            {PieceType.Rook, LoadImage("figurky/RookW.png")},     //PieceType.Rook
            {PieceType.Queen, LoadImage("figurky/QueenW.png")},   //ieceType.Queen
            {PieceType.King, LoadImage("figurky/KingW.png")}     //PieceType.King
        };

        private static readonly Dictionary<PieceType, ImageSource> blackSources = new()
        {
            {PieceType.Pawn, LoadImage("figurky/PawnB.png")},
            {PieceType.Bishop, LoadImage("figurky/BishopB.png")},
            {PieceType.Knight, LoadImage("figurky/KnightB.png")},
            {PieceType.Rook, LoadImage("figurky/RookB.png")},
            {PieceType.Queen, LoadImage("figurky/QueenB.png")},
            {PieceType.King, LoadImage("figurky/KingB.png")}
        };

        private static ImageSource LoadImage(string filePath)
        {
            return new BitmapImage(new Uri(filePath, UriKind.Relative));
        }

        public static ImageSource GetImage(hrac color, PieceType type) 
        {
            return color switch
            {
                hrac.White => whiteSources[type],
                hrac.Black => blackSources[type],
                _ => null
            };
        }

        public static ImageSource GetImage(Piece piece)
        {
            if(piece == null)
            {
                return null;
            }

            return GetImage(piece.Color, piece.Type);
        }
    }
}
