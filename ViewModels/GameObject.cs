using Avalonia;
using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace EcosystemSimulatorProject.ViewModels;

// représente un objet affichable sur l'interface
public abstract partial class GameObject : ViewModelBase
{
    [ObservableProperty]
    private Point _location;

    protected static readonly Random random = new Random(); // Changed to protected
    private const double MaxSpeed = 5.0;
    private const double MinSpeed = -5.0;

    // Add life and energy properties
    [ObservableProperty]
    private double life = 100.0;

    [ObservableProperty]
    private double energy = 100.0;

    protected GameObject(Point location)
    {
        Location = location;
    }

    public virtual void Tick()
    {
        Location = Location + Velocity;
        RandomizeVelocity();
        UpdateLifeAndEnergy();
    }

    protected void RandomizeVelocity()
    {
        double bonusX = random.NextDouble() - 0.5; // Value between -0.5 and 0.5
        double bonusY = random.NextDouble() - 0.5; // Value between -0.5 and 0.5
        double newX = Math.Clamp(Velocity.X + bonusX, MinSpeed, MaxSpeed);
        double newY = Math.Clamp(Velocity.Y + bonusY, MinSpeed, MaxSpeed);
        if (this is Herbivores || this is Carnivores)
        {
            Velocity = new Point(newX, newY);
        }
    }

    // New method to update life and energy
    private void UpdateLifeAndEnergy()
    {
        if (Energy > 0)
        {
            Energy -= 1.0; // Decrease energy
        }
        else if (Life > 0)
        {
            Life -= 1.0; // Decrease life if energy is 0
        }
    }

    public abstract Point Velocity { get; set; }
}
