using Goap_Based_Dungeon_Solver.Properties;
using System.Drawing;

namespace Goap_Based_Dungeon_Solver.Source.Game
{
    class Water : StaticElement
    {
        public Water(Point position) : base(Resources.water, position, MapElements.WATER, true)
        {}
    }
}
