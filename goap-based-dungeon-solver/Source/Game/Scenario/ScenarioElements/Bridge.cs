using Goap_Based_Dungeon_Solver.Properties;
using System.Drawing;

namespace Goap_Based_Dungeon_Solver.Source.Game
{
    class Bridge : DynamicElement
    {
        private readonly Bitmap brokenBridgeImage = Resources.broken_bridge;

        private readonly Bitmap fixedBridgeImage = Resources.fixed_bridge;
        
        bool _isFixed;
        public bool IsFixed {
            get { return _isFixed; }
            set {
                _isFixed = value;
                Sprite = value ? fixedBridgeImage : brokenBridgeImage;
                Type = value ? MapElements.FIXED_BRIDGE : MapElements.BROKEN_BRIDGE;
                Blockeable = !value;
            }
        }

        public Bridge(Point startingPosition, bool isFixed=false)
            : base(Resources.broken_bridge, startingPosition, MapElements.BROKEN_BRIDGE)
        {
            IsFixed = isFixed;
        }
    }
}
