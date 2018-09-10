using CSGameUtils;
using Goap_Based_Dungeon_Solver.Source.Game;
using Goap_Based_Dungeon_Solver.Source.Game.Api;
using Source.Solver;
using Source.Solver.Node;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Windows.Forms;

namespace Goap_Based_Dungeon_Solver
{
    public partial class Form1 : Form, IUserInterface
    {
        private const int DEFAULT_SCENARIO = 0;

        private GameManager game;

        private Planner mazeSolver;

        private SolutionExecutor solutionExecutor;

        public Form1()
        {
            InitializeComponent();

            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;

            game = new GameManager(this);
            LoadFirstScenario();

            solutionExecutor = new SolutionExecutor(game);

            mazeSolver = new Planner(new ClimbLadder(), game);
            var solution = mazeSolver.FindSolution();

            solutionExecutor.ExecuteSolution(solution);
        }

        private void LoadFirstScenario()
        {
            if (game.ScenarioAmount <= 0)
            {
                MessageBox.Show("No scenarios could be loaded. Aborting the program...", "Warning");
                Application.Exit();
            }

            int startingScenario = DEFAULT_SCENARIO;
            int.TryParse(ConfigurationManager.AppSettings["startingScenario"], out startingScenario);

            LoadScenario(startingScenario);
        }

        /// <summary>
        /// Load a scenario.
        /// </summary>
        /// <param name="idx">The scenario index.</param>
        void LoadScenario(int idx)
        {
            game.LoadScenario(idx);

            //ClearInterfaceValues();
            //UpdateActionButtons(true);
            //PlaybackButton.Enabled = false;

            //UpdateFormTitle();
        }

        public void ResetGraphics()
        {
            for (var ix = Controls.Count - 1; ix >= 0; ix--)
            {
                if (Controls[ix] is PictureBox)
                {
                    Controls[ix].Dispose();
                }
            }
        }

        public void SetupPictureBox(PictureBox picBox)
        {
            Controls.Add(picBox);
        }

        public void RemovePictureBox(PictureBox picBox)
        {
            Controls.Remove(picBox);
            picBox.Dispose();
        }

        bool isRunningSolver;

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // Can't move while solver is running.
            if (isRunningSolver)
            {
                MessageBox.Show("Can't play while the solver is still running.");
                return;
                /* When the game is solved by the solver, we won't allow the player to move
                    * anymore.
                    */
            }

            var dir = GameInteraction.KeyToDirection(e.KeyCode);
            if (dir != Directions.NONE)
            {
                game.MovePlayer(dir);
            }

            if (IsInteractKeyCode(e.KeyCode))
            {
                game.Interact();
            }
        }

        private bool IsInteractKeyCode(Keys key)
        {
            return key == Keys.Space;
        }
    }
}
