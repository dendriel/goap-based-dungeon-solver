using Goap_Based_Dungeon_Solver.Source.Game.Api;
using System.Drawing;
using System.Windows.Forms;

namespace Goap_Based_Dungeon_Solver.Source.Game
{
    /// <summary>
    /// Scenario element default properties.
    /// </summary>
    class ScenarioElement
    {
        /// <summary>
        /// Element position inside scenario.
        /// </summary>
        protected Point _position;
        public virtual Point Position {
            get { return _position; }
            set {
                _position = value;
                int xPos = (_position.X * ScenarioManager.SPRITE_WIDTH) + FormDefs.HOR_ALIGN_OFFSET;
                int yPos = (_position.Y * ScenarioManager.SPRITE_HEIGHT) + FormDefs.VER_ALIGN_OFFSET;
                sprite.Location = new Point(xPos, yPos);
                sprite.BringToFront();
            }
        }

        public MapElements Type { get; protected set; }

        /// <summary>
        /// Element may block the path?
        /// </summary>
        public bool Blockeable { get; protected set; }

        /// <summary>
        /// Element sprite in the scenario.
        /// </summary>
        protected PictureBox sprite { get; set; }

        /// <summary>
        /// Element image (set only).
        /// </summary>
        public virtual Bitmap Sprite { set { sprite.Image = value; } }

        public bool IsEnabled { get; private set; }

        public static IUserInterface uiHandler { get; set; }

        /// <summary>
        /// Create a new scenario element.
        /// </summary>
        /// <param name="image">Element appearance (image).</param>
        /// <param name="position">Element starting position.</param>
        public ScenarioElement(Bitmap image, Point position, MapElements type)
        {
            Type = type;

            sprite = new PictureBox() {
                Image = image,
                Width = ScenarioManager.SPRITE_WIDTH,
                Height = ScenarioManager.SPRITE_HEIGHT,
                BackColor = Color.Transparent
            };

            Position = position;

            IsEnabled = true;

            uiHandler.SetupPictureBox(sprite);
        }

        public void SetParentTile(ScenarioElement element)
        {
            sprite.Parent = element.sprite;
        }

        public void Disable()
        {
            IsEnabled = false;
            uiHandler.RemovePictureBox(sprite);
        }
    }
}
