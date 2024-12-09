using System;
using Avalonia;

namespace EcosystemSimulatorProject.ViewModels
{
    public abstract class Animals : GameObject // Changed from internal to public
    {
        public Animals(Point location) : base(location)
        {

        }

        private static readonly Random random = new Random();
        private const double MaxSpeed = 5.0;
        private const double MinSpeed = -5.0;

        private Point _velocity;
        public override Point Velocity
        {
            get => _velocity;
            set => SetProperty(ref _velocity, value);
        }

        public override void Tick()
        {
            base.Tick(); // Call the base Tick method to update life and energy
            Location = Location + Velocity;
            RandomizeVelocity();
        }

        private void RandomizeVelocity()
        {
            double bonusX = random.NextDouble() - 0.5; // Value between -0.5 and 0.5
            double bonusY = random.NextDouble() - 0.5; // Value between -0.5 and 0.5
            double newX = Math.Clamp(Velocity.X + bonusX, -5, 5);
            double newY = Math.Clamp(Velocity.Y + bonusY, -5, 5);
            if (this is Herbivores || this is Carnivores)
            {
                Velocity = new Point(newX, newY);
            }
        }
    }
    
}
