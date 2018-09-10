using Goap_Based_Dungeon_Solver.Properties;
using System.Drawing;

namespace Goap_Based_Dungeon_Solver.Source.Game
{
    class Wall : StaticElement
    {
        public Wall(Point position) : base(Resources.wall, position, MapElements.WALL, true)
        {}
    }
}
