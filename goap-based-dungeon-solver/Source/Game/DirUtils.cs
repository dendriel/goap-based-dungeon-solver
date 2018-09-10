using System.Drawing;

namespace Goap_Based_Dungeon_Solver.Source.Game
{
    static class DirUtils
    {
        public static Point GetNorthFromPos(Point pos)
        {
            return new Point(pos.X, pos.Y + 1);
        }

        public static Point GetEastFromPos(Point pos)
        {
            return new Point(pos.X + 1, pos.Y);
        }

        public static Point GetSouthFromPos(Point pos)
        {
            return new Point(pos.X, pos.Y - 1);
        }

        public static Point GetWestFromPos(Point pos)
        {
            return new Point(pos.X - 1, pos.Y);
        }
    }
}
