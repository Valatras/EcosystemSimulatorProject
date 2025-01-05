using Avalonia;

namespace EcosystemSimulatorProject.ViewModels
{
    public class OrganicWaste : GameObject
    {
     
        public OrganicWaste(Point location) : base(location)
        {
            Life = 100; // Set initial life
            Energy = 150; // Set initial energy
        }

        public override void Tick()
        {
            base.Tick(); // Call the base Tick method to update life and energy
            //can add more code functionality here

        }

    }
}
