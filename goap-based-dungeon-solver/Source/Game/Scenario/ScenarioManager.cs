using Goap_Based_Dungeon_Solver.Source.Game.Api;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Goap_Based_Dungeon_Solver.Source.Game
{
    /// <summary>
    /// Handles scenarios data.
    /// </summary>
    class ScenarioManager
    {
        /// <summary>
        /// Scenarios data directory name.
        /// </summary>
        public const string SCENARIOS_DIR = "scenarios";

        /// <summary>
        /// Sprites witdth.
        /// </summary>
        public const int SPRITE_WIDTH = 64;

        /// <summary>
        /// Sprites height.
        /// </summary>
        public const int SPRITE_HEIGHT = 64;
        
        /// <summary>
        /// Return the amount of available scenarios.
        /// </summary>
        public int ScenarioAmount { get { return scenariosData.Count; } }

        /// <summary>
        /// List of all loaded scenarios from scenarios directory.
        /// </summary>
        List<ScenarioData> scenariosData;

        /// <summary>
        /// Currently loaded scenario.
        /// </summary>
        public Scenario LoadedScenario { get; private set; }

        public int LoadedScenarioIndex { get; private set; }

        private IUserInterface uiHandler;

        /// <summary>
        /// Create a new ScenarioManager.
        /// </summary>
        public ScenarioManager(IUserInterface uiHandler)
        {
            this.uiHandler = uiHandler;

            scenariosData = new List<ScenarioData>();

            // Setting ui handler in the ScenarioElements prevents us from passing the
            // uiHandler for every scenario element.
            ScenarioElement.uiHandler = uiHandler;

            LoadScenarioData();
        }

        /// <summary>
        /// Load scenarios data from file.
        /// </summary>
        /// <param name="scenariosToLoad">List of scenarios filenames to be loaded.</param>
        /// <returns>The amount of succefully loaded scenarios.</returns>
        public int LoadScenarioData(string[] scenariosToLoad=null)
        {
            var scenFileArray = scenariosToLoad ?? GetScenariosFileNamesFromDefaultDir();

            var loaded = 0;
            foreach (var scenFile in scenFileArray)
            {
                try
                {
                    var scenarioJson = File.ReadAllText(@scenFile);
                    var newScenData = JsonConvert.DeserializeObject<ScenarioData>(scenarioJson);
                    scenariosData.Add(newScenData);
                    loaded++;
                }
                catch (Exception e)
                {
                    var msg = string.Format("Could not load scenario {0}{1}. Scenario not found or not properly formatted. e:{2}",
                        @scenFile, Environment.NewLine, e.ToString());
                    MessageBox.Show(msg);
                }
            }

            return loaded;
        }

        private string[] GetScenariosFileNamesFromDefaultDir()
        {
            var scenariosDir = ConfigurationManager.AppSettings["scenariosDirectory"];
            return Directory.GetFiles(@scenariosDir);
        }

        /// <summary>
        /// Load scenario by its index.
        /// </summary>
        /// <param name="id">Scenario index.</param>
        public void LoadScenario(int idx)
        {
            Debug.Assert(idx < scenariosData.Count, "Invalid scenario index: " + idx);

            uiHandler.ResetGraphics();

            LoadedScenario = new Scenario(scenariosData[idx]);
            LoadedScenarioIndex = idx;
        }

        public void ReloadScenario()
        {
            LoadedScenario.Reload();

            uiHandler.ResetGraphics();
        }
    }
}
