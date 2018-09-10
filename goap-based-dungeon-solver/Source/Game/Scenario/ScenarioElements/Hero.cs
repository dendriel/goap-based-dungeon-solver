using System.Drawing;
using Goap_Based_Dungeon_Solver.Properties;

namespace Goap_Based_Dungeon_Solver.Source.Game
{
    class Hero : DynamicElement
    {
        public Hero(Point startingPosition)
            : base(Resources.hero, startingPosition, MapElements.HERO)
        {}
    }
}
