using Avalonia;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Linq;
using static EcosystemSimulatorProject.ViewModels.MainWindowViewModel;

namespace EcosystemSimulatorProject.ViewModels;

public partial class Herbivores : Animals
{
    public double width;
    public double height;
    private const double detectionRange = 400; // Define a detection range

    public double DetectionRange => detectionRange;

    public Herbivores(Point location) : base(location)
    {
        energy = 100; // Initial energy value for GameObject
        life = 100;   // Initial life value
    }

    private Point _velocity;
    public override Point Velocity
    {
        get => _velocity;
        set => SetProperty(ref _velocity, value);
    }

    public Rect Bounds;

    public override void Tick()
    {
        if (Energy>30 && Life<100)
        {
            Life += 0.2;
        }   
        base.Tick();
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

    public void Eat(Plants plant)
    {
        plant.Life -= 10; // Reduce plant's life
        if (Energy < 100) // Use the generated property instead of the field => Energy instead of energy
        {
            Energy += 20; // Restore herbivore's energy
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
