using System.Drawing;

namespace Survival
{
    public class CreatureAnimation
    {
        public ICell Creature;
        public CreatureCommand Command;
        public Point Location;
        public Point TargetLogicalLocation;
    }
}