using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace šachy_upg
{
    public static class ChessCursor
    {
        public static readonly Cursor WhiteCursor = LoadCursor("figurky/CursorW.cur");
        public static readonly Cursor BlackCursor = LoadCursor("figurky/CursorB.cur");

        private static Cursor LoadCursor(string filePath)
        {
            Stream stream = Application.GetResourceStream(new Uri(filePath, UriKind.Relative)).Stream;
            return new Cursor(stream, true);
        }
    }
}
