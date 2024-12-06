using Avalonia;
using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace EcosystemSimulatorProject.ViewModels;

public partial class Herbivores : GameObject
{
    // Randomize the Herbivores.Velocity value after each herbivores.Tick()

    Random random = new Random();
    double XSpeed;
    double YSpeed;

    private const double MaxSpeed = 5.0;
    private const double MinSpeed = -5.0;

    [ObservableProperty]
    private Point velocity;

    /*hitbox ?*/
    public double width => 50;
    public double height => 70;

    public Herbivores(Point location) : base(location)
    {
        XSpeed = random.NextDouble() * 2 - 1; // Value between -1 and 1
        YSpeed = random.NextDouble() * 2 - 1; // Value between -1 and 1
        velocity = new Point(XSpeed, YSpeed);
    }

    public Rect Bounds => new Rect(Location.X, Location.Y, width, height);

    public void Tick()
    {
        Location = Location + Velocity;
        RandomizeVelocity();
    }

    private void RandomizeVelocity()
    {
        double bonusX = random.NextDouble() - 0.5; // Value between -0.5 and 0.5
        double bonusY = random.NextDouble() - 0.5; // Value between -0.5 and 0.5
        double newX = Math.Clamp(Velocity.X + bonusX, MinSpeed, MaxSpeed);
        double newY = Math.Clamp(Velocity.Y + bonusY, MinSpeed, MaxSpeed);
        Velocity = new Point(newX, newY);
    }
}
