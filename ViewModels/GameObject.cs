using Avalonia;
using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace EcosystemSimulatorProject.ViewModels;

// représente un objet affichable sur l'interface
public abstract partial class GameObject : ViewModelBase
{
    // Add life and energy properties. ObservableProperty is used to notify the UI when the value changes.
    [ObservableProperty]
    protected double life;

    [ObservableProperty]
    protected double energy;

    [ObservableProperty]
    private Point _location;

    public abstract Point Velocity { get; set; }

    protected GameObject(Point location)
    {
        Location = location;
    }

    public virtual void Tick()
    {
        Location = Location + Velocity;
        UpdateLifeAndEnergy();
    }

    // New method to update life and energy
    private void UpdateLifeAndEnergy()
    {
        if (Energy > 0)
        {
            Energy -= 0.5; // Decrease energy
        }
        else if (Life > 0)
        {
            Life -= 0.5; // Decrease life if energy is 0
        }
    }


}
