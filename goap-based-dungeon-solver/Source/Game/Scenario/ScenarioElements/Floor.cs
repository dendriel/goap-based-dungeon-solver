using Goap_Based_Dungeon_Solver.Properties;
using System.Drawing;

namespace Goap_Based_Dungeon_Solver.Source.Game
{
    class Floor : StaticElement
    {
        public Floor(Point position) : base(Resources.floor, position, MapElements.FLOOR, false)
        {}
    }
}
