using Avalonia;
using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace EcosystemSimulatorProject.ViewModels;

public partial class Plants : GameObject
{
    public double width;
    public double height;
    public Plants(Point location) : base(location)
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
}