using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace šachy_upg
{
    public enum hrac
    {
        None,
        White,
        Black
    }
    public static class playerExtentions
    {
        public static hrac protihrac(this hrac hrac)
        {
            return hrac switch
            {
                hrac.White => hrac.Black,
                hrac.Black => hrac.White,
                _ => hrac.None,
            };
        }
    }
}
