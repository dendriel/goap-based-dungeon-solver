using CSGameUtils;
using Goap_Based_Dungeon_Solver.Source.Game.Actions;
using Goap_Based_Dungeon_Solver.Source.Game.Api;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Goap_Based_Dungeon_Solver.Source.Game
{
    class GameManager
    {
        public string CurrScenarioName {
            get { return (scenMan.LoadedScenario != null) ? scenMan.LoadedScenario.Name : ""; }
        }

        public string CurrScenarioInfo {
            get { return (scenMan.LoadedScenario != null) ? scenMan.LoadedScenario.Info : ""; }
        }

        public int ScenarioAmount { get { return scenMan.ScenarioAmount; } }

        private IUserInterface uiHandler;

        private ScenarioManager scenMan;

        private GameInteraction interaction;

        private ShortestPathFinder spFinder;

        public GameManager(IUserInterface uiHandler)
        {
            this.uiHandler = uiHandler;
            scenMan = new ScenarioManager(uiHandler);
            interaction = new GameInteraction();
            spFinder = new ShortestPathFinder(scenMan.LoadedScenario);
        }

        /// <summary>
        /// Load a map state.
        /// </summary>
        /// <param name="state">The map state to be loaded.</param>
        public void LoadMapState(MapElements[,] state)
        {
            scenMan.LoadedScenario.LoadElements(state);
        }
        
        public bool Interact()
        {
            bool actionRes = interaction.Interact();
            if (actionRes && IsVictorious())
            {
                HandleVictory();
            }

            return actionRes;
        }

        private bool IsVictorious()
        {
            return scenMan.LoadedScenario.HasClimbedUpLadder();
        }

        public bool MovePlayer(Directions dir)
        {
            return interaction.MovePlayer(dir);
        }

        private void HandleVictory()
        {
            MessageBox.Show("You finished this level!", "Level passed!");

            var nextScenarioIndex = scenMan.LoadedScenarioIndex + 1;
            if (nextScenarioIndex >= scenMan.ScenarioAmount)
            {
                nextScenarioIndex = 0;
            }

            LoadScenario(nextScenarioIndex);
        }

        /// <summary>
        /// Create a copy of the current map state.
        /// </summary>
        /// <returns>The current map state.</returns>
        public MapElements[,] CopyMap()
        {
            return scenMan.LoadedScenario.Map.Clone() as MapElements[,];
        }

        /// <summary>
        /// Load scenario by its index.
        /// </summary>
        /// <param name="id">Scenario index.</param>
        public void LoadScenario(int idx)
        {
            scenMan.LoadScenario(idx);
            interaction.SetCurrScenario(scenMan.LoadedScenario);
            spFinder.SetCurrScenario(scenMan.LoadedScenario);

            //var g = spFinder.BuildGraph();
            //var sp = spFinder.FindShortestPath(new Point(7, 7), new Point(7, 1));
            //DumpGraph(sp);
        }

        public List<GoapSpNode> FindShortestPath(Point from, Point to)
        {
            var spNodeGraph = spFinder.FindShortestPath(from, to);
            return SPNodeGrapToGoapGrap(spNodeGraph);
        }

        private List<GoapSpNode> SPNodeGrapToGoapGrap(SPNode[] spNodeGraph)
        {
            var goapGraph = new List<GoapSpNode>();
            spNodeGraph.ToList().ForEach(n => goapGraph.Add(n as GoapSpNode));
            return goapGraph;
        }

        private void DumpGraph(IEnumerable<SPNode> graph)
        {
            var graphText = string.Empty;
            foreach(var n in graph)
            {
                var neighText = "{ ";
                foreach(var neigh in n.Neighbors)
                {
                    neighText += neigh.ID + ", ";
                }
                neighText += "}";

                var type = (n as GoapSpNode).Type;
                graphText += n.ID + "(" + n.Weight + "|" + type + "): " + neighText + Environment.NewLine;
            }
            MessageBox.Show(graphText, "Graph");
        }

        /// <summary>
        /// Reload the current scenario.
        /// </summary>
        public void ReloadScenario()
        {
            scenMan.ReloadScenario();
        }

        public Point FindCharacterPos(MapElements character)
        {
            return BaseAction.FindCharacter(scenMan.LoadedScenario, character).Position;
        }

        public Point FindObjetPos(MapElements obj)
        {
            return BaseAction.FindObject(scenMan.LoadedScenario, obj).Position;
        }

        public bool PlayerHasObject(MapElements obj)
        {
            return scenMan.LoadedScenario.PlayerHasObject(obj);
        }
    }
}
