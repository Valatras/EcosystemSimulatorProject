using Avalonia;
using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace EcosystemSimulatorProject.ViewModels;

public partial class Plants : Livings
{
    public double width;
    public double height;



    public Plants(Point location) : base(location)
    {
        energy = 100; // Initial energy value
        life = 100;   // Initial life value
    }

    
    

    public Rect Bounds;
}