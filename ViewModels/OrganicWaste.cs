using Avalonia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcosystemSimulatorProject.ViewModels
{
    public class OrganicWaste : GameObject
    {
        public double width;
        public double height;
        public OrganicWaste(Point location) : base(location)
        {
            Life = 100; // Set initial life
            Energy = 150; // Set initial energy
        }

        public override Point Velocity { get; set; } = new Point(0, 0);

        public Rect Bounds;
    }
}
