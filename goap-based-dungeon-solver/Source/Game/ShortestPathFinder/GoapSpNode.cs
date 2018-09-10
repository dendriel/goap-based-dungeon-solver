using CSGameUtils;
using System.Linq;

namespace Goap_Based_Dungeon_Solver.Source.Game
{
    class GoapSpNode : SPNode
    {
        public MapElements Type { get; private set; }

        public GoapSpNode(int id, int weight, MapElements type) : base(id, weight)
        {
            Type = type;
        }

        private GoapSpNode(SPNode baseNode)
            : base(baseNode)
        {
        }

        public override SPNode CopySelf()
        {
            var copy = new GoapSpNode(base.CopySelf());
            copy.Type = Type;
            return copy;
        }

        public override string ToString()
        {
            var neighborsIds = Neighbors.Select(n => n.ID);
            return string.Format("{0} - [{1}]", ID.ToString(), string.Join(", ", neighborsIds));
        }
    }
}
