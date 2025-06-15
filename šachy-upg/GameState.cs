using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace šachy_upg
{
    public class GameState
    {
        public Deska Deska { get; }
        public hrac CurrentHrac { get; private set; }
        public GameState(hrac hrac, Deska deska)
        {
            CurrentHrac = hrac;
            Deska = deska;
        }
    }
}
