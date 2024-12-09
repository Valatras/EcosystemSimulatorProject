using Avalonia;
using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace EcosystemSimulatorProject.ViewModels;

public partial class Carnivores : Animals
{
    public double width;
    public double height;
    private const double DetectionRange = 50.0; // Define a detection range

    public Carnivores(Point location) : base(location)
    {
        energy = 100; // Initial energy value
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
        base.Tick();
        
    }

    
}
