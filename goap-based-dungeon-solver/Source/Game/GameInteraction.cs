using Goap_Based_Dungeon_Solver.Source.Game.Actions;
using Goap_Based_Dungeon_Solver.Source.Game.Actions.Args;
using System;
using System.Windows.Forms;

namespace Goap_Based_Dungeon_Solver.Source.Game
{
    class GameInteraction
    {
        private Scenario currScenario;

        private InteractionCreator interactionCreator;

        private InteractionFinder interactionFinder;

        public GameInteraction()
        {
            interactionCreator = new InteractionCreator(currScenario);
            interactionFinder = new InteractionFinder(currScenario);
        }

        public void SetCurrScenario(Scenario scenario)
        {
            currScenario = scenario;
            interactionCreator.SetCurrScenario(scenario);
            interactionFinder.SetCurrScenario(scenario);
        }

        /// <summary>
        /// Move the player towards the given direction.
        /// </summary>
        /// <param name="dir">Direction to move.</param>
        /// <returns>true if moved; false if the path is blocked.</returns>
        public bool MovePlayer(Directions dir)
        {
            var player = BaseAction.FindCharacter(currScenario, MapElements.HERO);

            var moveArg = new MoveArg(player, dir);
            var move = new MoveAction();

            return move.Execute(currScenario, moveArg);
        }

        public bool Interact()
        {
            var scenario = currScenario;
            var character = BaseAction.FindCharacter(scenario, MapElements.HERO);
            var type = interactionFinder.FindInteractionType(character.Position);

            ActionArg actionArg = null;
            BaseAction action = null;

            if (interactionCreator.CreateInteraction(type, character, out action, out actionArg) != true)
            {
                return false;
            }

            action.Execute(scenario, actionArg);

            return true;
        }

        /// <summary>
        /// Debug. Prints the current map state.
        /// </summary>
        /// <param name="state">The map state to be printed.</param>
        public static void PrintMapState(MapElements[,] state)
        {
            for (int row = 0; row < state.GetLength(1); row++) {
                for (int col = 0; col < state.GetLength(0); col++) {
                    Console.Write(((int)state[col, row] + ", "));
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Translate a key input into a game direction.
        /// </summary>
        /// <param name="key">The key to be translated.</param>
        /// <returns>A direction if the key is know; Direction.NONE if the key is not handled.</returns>
        public static Directions KeyToDirection(Keys key)
        {
            if (key == Keys.Up) {
                return Directions.UP;

            } else if (key == Keys.Right) {
                return Directions.RIGHT;

            } else if (key == Keys.Down) {
                return Directions.DOWN;

            } else if (key == Keys.Left) {
                return Directions.LEFT;

            } else {
                return Directions.NONE;
            }
        }
    }
}
