using System;
using Avalonia;

namespace EcosystemSimulatorProject.ViewModels
{
    public abstract class Livings(Point location) : GameObject(location) // Changed from internal to public
    {
        private static readonly Random random = new();
        protected double detectionRange; // Define a detection range
        protected double contactRange;// Define a contact range

        public double DetectionRange => detectionRange; // Getter for detectionRange
        public double ContactRange => contactRange; // Getter for contactRange



        public override void Tick()
        {
            base.Tick(); // Call the base Tick method to update life and energy
        }

        public bool IsAtLocation(Point location)
        {
            return Math.Abs(Location.X - location.X) < 1 && Math.Abs(Location.Y - location.Y) < 1;
        }

        public double DistanceTo(Point target)
        {
            return Math.Sqrt(Math.Pow(Location.X - target.X, 2) + Math.Pow(Location.Y - target.Y, 2));
        }
    }
    
}
