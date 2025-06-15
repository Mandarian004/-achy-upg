using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace šachy_upg
{
    public abstract class Move
    {
        public abstract MoveType Type { get; }
        public abstract Pozice FromPos { get; }
        public abstract Pozice ToPos { get; }
        public abstract void Execute(Deska deska);
    }
}
