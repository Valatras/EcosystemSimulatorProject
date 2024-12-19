using Avalonia;

namespace EcosystemSimulatorProject.ViewModels
{
    public class Meat : GameObject
    {
        public double width;
        public double height;

        public Meat(Point location) : base(location)
        {
            Life = 100; // Set initial life
            Energy = 150; // Set initial energy
        }

        public override void Tick()
        {
            base.Tick(); // Call the base Tick method to update life and energy
                         //can add more code functionality here
        }

        public Rect Bounds;
    }
}


//Ils doivent se transformer en organismes morts après un certain temps
//Ils doivent être consommés par les carnivores