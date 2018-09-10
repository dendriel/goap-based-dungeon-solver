
using CSGameUtils;
using Goap_Based_Dungeon_Solver.Source.Game.Actions;
using System.Collections.Generic;
using System.Drawing;

namespace Goap_Based_Dungeon_Solver.Source.Game
{
    class ShortestPathFinder // Create an interface.
    {
        private const int baseNodeWeight = 10;

        private Scenario currScenario;

        private int currScenarioWidth { get { return currScenario.Map.GetLength(0); } }

        private int currScenarioHeight { get { return currScenario.Map.GetLength(1); } }

        public ShortestPathFinder(Scenario scenario)
        {
            currScenario = scenario;
        }

        public void SetCurrScenario(Scenario scenario)
        {
            currScenario = scenario;
        }

        public SPNode[] FindShortestPath(Point from, Point to)
        {
            var graph = BuildGraph();

            return ShortestPath.FindShortestPath(PosToNodeId(from), PosToNodeId(to), graph);
        }

        public List<SPNode> BuildGraph()
        {
            var graph = new List<SPNode>();

            var map = currScenario.Map;
            var width = currScenarioWidth;
            var height = currScenarioHeight;

            for (var row = 0; row < height; row++)
            {
                for (var col = 0; col < width; col++)
                {
                    var nodeId = PosToNodeId(new Point(col, row));
                    var node = graph.Find(n => n.ID == nodeId) ?? CreateNode(nodeId, new Point(col, row));
                    if (node == null)
                    {
                        continue;
                    }

                    AddNeighbors(node, col, row);

                    graph.Add(node);
                }
            }

            return graph;
        }

        private int PosToNodeId(Point pos)
        {
            return (pos.Y * currScenarioWidth) + pos.X;
        }

        private SPNode CreateNode(int nodeId, Point pos)
        {
            if (nodeId < 0 || nodeId >= (currScenarioWidth*currScenarioHeight))
            {
                return null;
            }

            var nodeWeigth = baseNodeWeight;

            var obj = BaseAction.GetObjectAtPos(currScenario, pos);
            if (obj != null)
            {
                nodeWeigth = GetObjectWeight(obj);
                return new GoapSpNode(nodeId, nodeWeigth, obj.Type);
            }

            var character = BaseAction.GetCharacterAtPos(currScenario, pos);
            if (character != null)
            {
                nodeWeigth = GetCharacterWeight(character);
                return new GoapSpNode(nodeId, nodeWeigth, character.Type);
            }

            var mapElemId = currScenario.Map[pos.Y, pos.X];
            if (mapElemId != MapElements.FLOOR)
            {
                return null;
            }

            return new GoapSpNode(nodeId, nodeWeigth, mapElemId);
        }

        private int GetObjectWeight(DynamicElement obj)
        {
            if (obj is Bridge || obj is IronDoor || obj is BronzeDoor)
            {
                return currScenarioWidth * currScenarioHeight;
            }

            return baseNodeWeight;
        }

        private int GetCharacterWeight(DynamicElement character)
        {
            if (character is Hero)
            {
                return baseNodeWeight;
            }

            return currScenarioWidth * currScenarioHeight;
        }

        private void AddNeighbors(SPNode node, int posX, int posY)
        {
            var nodePos = new Point(posX, posY);

            // north
            var northNeighborPos = new Point(posX, posY -1);
            var northNeighbor = CreateNode(node.ID - currScenarioWidth, northNeighborPos);
            if (northNeighbor != null)
            {
                node.AddNeighbor(northNeighbor);
            }

            // east
            var eastNeighborPos = DirUtils.GetEastFromPos(nodePos);
            var eastNeighbor = CreateNode(node.ID + 1, eastNeighborPos);
            if (eastNeighbor != null)
            {
                node.AddNeighbor(eastNeighbor);
            }

            // south
            var southNeighborPos = new Point(posX, posY + 1);
            var southNeighbor = CreateNode(node.ID + currScenarioWidth, southNeighborPos);
            if (southNeighbor != null)
            {
                node.AddNeighbor(southNeighbor);
            }

            // west
            var westNeighborPos = DirUtils.GetWestFromPos(nodePos);
            var westNeighbor = CreateNode(node.ID - 1, westNeighborPos);
            if (westNeighbor != null)
            {
                node.AddNeighbor(westNeighbor);
            }
        }
    }
}
