using System;
using Avalonia;

namespace EcosystemSimulatorProject.ViewModels
{
    public abstract class Animals(Point location) : Livings(location) // Changed from internal to public
    {
        private static readonly Random random = new();

        public abstract Point Velocity { get; set; }


        public event Action<OrganicWaste>? OnPoop; // Event to notify when an animal poops

        public void OrganicWaste()
        {
            var organicWaste = new OrganicWaste(Location); // Create organicWaste at the animal's current location
            OnPoop?.Invoke(organicWaste);         // Notify subscribers (e.g., MainWindowViewModel)
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

            Velocity = new Point(newX, newY);
            
        }

        public void MoveTowards(GameObject target)
        {
            var direction = new Point(target.Location.X - Location.X, target.Location.Y - Location.Y);
            var length = Math.Sqrt(direction.X * direction.X + direction.Y * direction.Y);
            if (length > 0) // Prevent division by zero
            {
                Velocity = new Point(direction.X / length, direction.Y / length);
            }
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
