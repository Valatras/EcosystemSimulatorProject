using System;
using Avalonia;

namespace EcosystemSimulatorProject.ViewModels
{
    public abstract class Animals(Point location) : Livings(location)
    {
        private static readonly Random random = new();
        protected string gender = random.Next(2) == 0 ? "Male" : "Female"; // chances : 50% male, 50% female

        public abstract Point Velocity { get; set; }

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
    }
    
}
