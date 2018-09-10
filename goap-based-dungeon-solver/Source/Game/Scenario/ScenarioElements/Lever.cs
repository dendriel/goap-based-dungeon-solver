using Goap_Based_Dungeon_Solver.Properties;
using System.Drawing;

namespace Goap_Based_Dungeon_Solver.Source.Game
{
    class Lever : DynamicElement
    {
        private readonly Bitmap inactiveLeverImage = Resources.inactive_lever;

        private readonly Bitmap activeLeverImage = Resources.active_lever;
        
        bool _isActive;
        public bool IsActive {
            get { return _isActive; }
            set {
                _isActive = value;
                Sprite = (value) ? activeLeverImage : inactiveLeverImage;
                Type = value ? MapElements.ACTIVE_LEVER : MapElements.INACTIVE_LEVER;
            }
        }
        
        public Lever(Point startingPosition, bool isActive=false)
            : base(Resources.inactive_lever, startingPosition, MapElements.INACTIVE_LEVER)
        {
            IsActive = isActive;
        }
    }
}
